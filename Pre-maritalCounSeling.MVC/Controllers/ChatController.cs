using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Pre_maritalCounSeling.Common.Util;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Pre_maritalCounSeling.MVC.Controllers
{
    public class ChatController : Controller
    {
        private static List<string> messages = new List<string>(); // Ensure this is never null

        public IActionResult ChatBox()
        {
            return PartialView("_ChatBox", messages);
        }

        [HttpPost]
        public async Task<IActionResult> SendMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
                return BadRequest("Message cannot be empty.");

            messages.Add("You: " + message); // Add user message

            var requestContent = new { message };
            var jsonContent = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");

            using (var httpClient = new HttpClient())
            {
                try
                {
                    using (var response = await httpClient.PostAsync("http://localhost:8080/api/ChatBot/", jsonContent))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            var responseString = await response.Content.ReadAsStringAsync();
                            var chatResponse = JsonConvert.DeserializeObject<ChatResponse>(responseString);

                            if (chatResponse != null && !string.IsNullOrEmpty(chatResponse.Response))
                            {
                                messages.Add("Bot: " + chatResponse.Response); // Add chatbot response
                            }
                            else
                            {
                                messages.Add("Bot: Received an empty response from the chatbot.");
                            }
                        }
                        else
                        {
                            messages.Add("Bot: Failed to send message.");
                        }
                    }
                }
                catch (HttpRequestException ex)
                {
                    messages.Add("Bot: Error communicating with chatbot. " + ex.Message);
                }
            }

            return PartialView("_ChatMessages", messages); // Always return a valid model
        }
    }

    public class ChatResponse
    {
        public string Response { get; set; }
    }
}
