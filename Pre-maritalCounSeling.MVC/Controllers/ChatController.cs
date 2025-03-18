using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class ChatController : Controller
    {
        private const string SessionKey = "ChatMessages";

        // Retrieve chat messages from session
        private List<string> GetChatMessages()
        {
            var sessionData = HttpContext.Session.GetString(SessionKey);
            return string.IsNullOrEmpty(sessionData)
                ? new List<string>()
                : JsonConvert.DeserializeObject<List<string>>(sessionData);
        }

        // Save chat messages to session
        private void SaveChatMessages(List<string> messages)
        {
            HttpContext.Session.SetString(SessionKey, JsonConvert.SerializeObject(messages));
        }

        public IActionResult ChatBox()
        {
            var messages = GetChatMessages();
            return PartialView("_ChatMessages", messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatMessage request)
        {
            if (string.IsNullOrEmpty(request?.Message))
                return BadRequest(new { success = false, error = "Message cannot be empty." });

            var messages = GetChatMessages();
            messages.Add("You: " + request.Message);

            var requestContent = new { message = request.Message };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    var response = await httpClient.PostAsync("http://localhost:8080/api/ChatBot/", jsonContent);
                    if (response.IsSuccessStatusCode)
                    {
                        var responseString = await response.Content.ReadAsStringAsync();
                        var chatResponse = JsonConvert.DeserializeObject<ChatResponse>(responseString);

                        if (!string.IsNullOrEmpty(chatResponse?.Response))
                        {
                            messages.Add("Bot: " + chatResponse.Response);
                        }
                        else
                        {
                            messages.Add("Bot: No response from chatbot.");
                        }
                    }
                    else
                    {
                        messages.Add("Bot: Failed to send message.");
                    }
                }
                catch (HttpRequestException ex)
                {
                    messages.Add("Bot: Error communicating with chatbot. " + ex.Message);
                }
            }

            SaveChatMessages(messages);

            return Ok(new { success = true, messages });
        }
    }

    public class ChatMessage
    {
        public string Message { get; set; }
    }
    public class ChatResponse
    {
        public string Response { get; set; }
    }

}
