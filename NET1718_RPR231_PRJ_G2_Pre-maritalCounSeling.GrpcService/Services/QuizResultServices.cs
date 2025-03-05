using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NET1718_PRN231_PRJ_G2_PremaritalCounseling.GrpcService.Protos;
using NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService;
using Pre_maritalCounSeling.BAL.ServiceQuiz;
using System.Text.Json.Serialization;
using System.Text.Json;
using Pre_maritalCounSeling.DAL.Entities;

namespace NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService.Services
{
    public class QuizResultServices : QuizResultGRPC.QuizResultGRPCBase
    {
        private readonly ILogger<QuizResultServices> _logger;
        private readonly IQuizResultService _quizResultService;
        public QuizResultServices(ILogger<QuizResultServices> logger, IQuizResultService quizResultService)
        {
            _logger = logger;
            _quizResultService = quizResultService;
        }

        public override Task<ActionResult> DeleteById(IdRequest request, ServerCallContext context)
        {
            return base.DeleteById(request, context);
        }

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

        public override Task<QuizResultModel> GetById(IdRequest request, ServerCallContext context)
        {
            return base.GetById(request, context);
        }
    }
}
