using Microsoft.AspNetCore.Mvc;
using Grpc.Net.Client;
using System.Threading.Tasks;
using NET1718_PRN231_PRJ_G2_PremaritalCounseling.GrpcService.Protos;

namespace GRPC_UI.Controllers
{
    public class ChatController : Controller
    {
        private readonly QuizResultGRPC.QuizResultGRPCClient _client;

        public ChatController()
        {
            var channel = GrpcChannel.ForAddress("https://localhost:7172"); // Adjust the port
            _client = new QuizResultGRPC.QuizResultGRPCClient(channel);
        }

        // GET: Index - Display chat UI
        public IActionResult Index()
        {
            return View();
        }

        // POST: SendMessage - Send user message to the chatbot and get response
        [HttpPost]
        public async Task<IActionResult> SendMessage([FromBody] ChatRequestViewModel request)
        {
            try
            {
                var grpcRequest = new ChatRequest { Message = request.Message };
                var grpcResponse = await _client.SendChatMessageAsync(grpcRequest);

                return Json(new { response = grpcResponse.Response });
            }
            catch (Exception ex)
            {
                return Json(new { error = $"Error communicating with chatbot: {ex.Message}" });
            }
        }
    }

    public class ChatRequestViewModel
    {
        public string Message { get; set; }
    }
}
