using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NET1718_PRN231_PRJ_G2_PremaritalCounseling.GrpcService.Protos;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using System.Text.Json.Serialization;
using System.Text.Json;
using Pre_maritalCounSeling.DAL.Entities;
using System.Web.Http;
using System.Net.Http;

namespace Pre_maritalCounSeling.GrpcService.Services
{
    public class QuizResultServices : QuizResultGRPC.QuizResultGRPCBase
    {
        private readonly ILogger<QuizResultServices> _logger;
        private readonly IQuizResultService _quizResultService;
        private readonly HttpClient _httpClient; // Add HttpClient for external API calls

        public QuizResultServices(ILogger<QuizResultServices> logger, IQuizResultService quizResultService, HttpClient httpClient)
        {
            _logger = logger;
            _quizResultService = quizResultService;
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:8080/"); // Set base URL for ChatBot API
        }

        //DELETE
        public override async Task<ActionResult> DeleteById(IdRequest request, ServerCallContext context)
        {
            try
            {
                if (request.Id == 0)
                {
                    return new ActionResult
                    {
                        Status = 400,
                        Message = "The ID should be provided"
                    };
                }


                await _quizResultService.DeleteQuizResultSimplyAsync(request.Id);

                return new ActionResult
                {
                    Status = 200,
                    Message = "Quiz result deleted successfully"
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting quiz result");
                return new ActionResult
                {
                    Status = 500,
                    Message = $"Error deleting quiz result: {ex.Message}"
                };
            }
        }

        //GET ALL
        public override async Task<QuizResultList> GetAll(Empty request, ServerCallContext context)
        {

            var result = new QuizResultList();
            var queryDal = await _quizResultService.GetQuizResultsSimplyAsync();

            //transfer to rpc datatype
            var opt = new JsonSerializerOptions()
            {
                ReferenceHandler = ReferenceHandler.IgnoreCycles,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
            };
            var quizResultsString = JsonSerializer.Serialize(queryDal, opt);
            var items = JsonSerializer.Deserialize<List<QuizResultModel>>(quizResultsString, opt);
            result.Data.AddRange(items);
            return await Task.FromResult(result);
        }

        //GET DETAIL
        public override async Task<QuizResultModel> GetById(IdRequest request, ServerCallContext context)
        {
            try
            {
                var quizResultDetail = await _quizResultService.GetQuizResultSimplyAsync(request.Id);

                if (quizResultDetail == null)
                {
                    return new QuizResultModel
                    {
                        // Return empty model with default values if not found
                    };
                }

                // Map entity to gRPC model
                var quizResultModel = new QuizResultModel
                {
                    Id = (int)quizResultDetail.Id,
                    Score = (int)quizResultDetail.Score,
                    UserId = (int)quizResultDetail.UserId,
                    QuizId = (int)quizResultDetail.QuizId,
                    QuizResultCode = quizResultDetail.QuizResultCode ?? "",
                    TimeCompleted = quizResultDetail.TimeCompleted,
                    AttemptTime = (int)quizResultDetail.AttemptTime,
                    SuggestionContent = quizResultDetail.SuggestionContent ?? "",
                    Feedback = quizResultDetail.FeedBack ?? "",
                    DoHaveFeedback = quizResultDetail.DoHaveFeedback,
                    UserAnswerData = quizResultDetail.UserAnswerData ?? "",
                    RecommendedAnswerData = quizResultDetail.RecommendedAnswerData ?? ""
                };

                return quizResultModel;
            }
            catch (Exception e)
            {
                // Log the exception (you might want to add proper logging)
                throw new RpcException(new Status(StatusCode.Internal, $"Error retrieving quiz result: {e.Message}"));
            }
        }

        // CREATE
        [HttpPost]
        public override async Task<ActionResult> Create(QuizResultModel request, ServerCallContext context)
        {
            try
            {
                var quizResult = new QuizResult
                {
                    Score = request.Score,
                    UserId = request.UserId,
                    QuizId = request.QuizId,
                    QuizResultCode = "RESULT-" + Guid.NewGuid().ToString(),
                    TimeCompleted = request.TimeCompleted,
                    AttemptTime = request.AttemptTime,
                    SuggestionContent = request.SuggestionContent ?? "Unavailable yet",
                    FeedBack = request.Feedback,
                    DoHaveFeedback = request.DoHaveFeedback,
                    UserAnswerData = request.UserAnswerData,
                    RecommendedAnswerData = request.RecommendedAnswerData
                };

                await _quizResultService.CreateQuizResultSimplyAsync(quizResult);

                return new ActionResult
                {
                    Status = 201,
                    Message = "Quiz result created successfully",
                    Result = request
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating quiz result");
                return new ActionResult
                {
                    Status = 500,
                    Message = $"Error creating quiz result: {ex.Message}"
                };
            }
        }

        // UPDATE
        public override async Task<ActionResult> Update(QuizResultModel request, ServerCallContext context)
        {
            try
            {
                if (request.Id == 0)
                    return new ActionResult
                    {
                        Status = 400,
                        Message = "The ID should be provided"
                    };

                var quizResult = new QuizResult
                {
                    Id = request.Id,
                    Score = request.Score,
                    UserId = request.UserId,
                    QuizId = request.QuizId,
                    QuizResultCode = request.QuizResultCode,
                    TimeCompleted = request.TimeCompleted,
                    AttemptTime = request.AttemptTime,
                    SuggestionContent = request.SuggestionContent,
                    FeedBack = request.Feedback,
                    DoHaveFeedback = request.DoHaveFeedback,
                    UserAnswerData = request.UserAnswerData,
                    RecommendedAnswerData = request.RecommendedAnswerData
                };

                await _quizResultService.UpdateQuizResultAsync(quizResult);

                return new ActionResult
                {
                    Status = 200,
                    Message = "Quiz result updated successfully",
                    Result = request
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating quiz result");
                return new ActionResult
                {
                    Status = 500,
                    Message = $"Error updating quiz result: {ex.Message}"
                };
            }
        }

        public override async Task<ChatResponse> SendChatMessage(ChatRequest request, ServerCallContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(request.Message))
                {
                    throw new RpcException(new Status(StatusCode.InvalidArgument, "Message cannot be empty"));
                }

                // Prepare the request payload for the ChatBot API
                var payload = new { message = request.Message };
                var jsonPayload = JsonSerializer.Serialize(payload);
                var content = new StringContent(jsonPayload, System.Text.Encoding.UTF8, "application/json");

                // Call the external ChatBot API
                var response = await _httpClient.PostAsync("api/ChatBot", content);
                response.EnsureSuccessStatusCode();

                // Parse the response
                var responseString = await response.Content.ReadAsStringAsync();
                var chatBotResponse = JsonSerializer.Deserialize<ChatBotApiResponse>(responseString);

                // Return the gRPC response
                return new ChatResponse
                {
                    Response = chatBotResponse?.response ?? "No response from ChatBot"
                };
            }
            catch (HttpRequestException ex)
            {
                _logger.LogError(ex, "Error calling ChatBot API");
                throw new RpcException(new Status(StatusCode.Unavailable, $"ChatBot API unavailable: {ex.Message}"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing chat message");
                throw new RpcException(new Status(StatusCode.Internal, $"Error processing chat message: {ex.Message}"));
            }
        }

    }
    public class ChatBotApiResponse
    {
        public string response { get; set; }
    }
}
