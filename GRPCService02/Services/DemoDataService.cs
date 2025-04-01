using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GRPCService02.Protos;
using GRPCService02.Models;
using Microsoft.EntityFrameworkCore;

namespace GRPCService02.Services
{
    public class DemoDataService : QuizResultGRPC.QuizResultGRPCBase
    {
        private readonly DEMO_PRN3_GRPCContext _context;

        public DemoDataService(DEMO_PRN3_GRPCContext context)
        {
            _context = context;
        }

        public override async Task<ActionResult> Create(Empty request, ServerCallContext context)
        {
            try
            {

                //randomly gen data ~ using default UserID = 7 | QuizID = 22 
                var quizResult = new DemoQrc
                {
                    QrcodeGen = "QrcodeGen" + new Guid().ToString(),
                    IsSynced = false,
                };

                _context.DemoQrcs.Add(quizResult);

                return new ActionResult
                {
                    Status = 200,
                    Message = "Created OK"
                };
            }
            catch (Exception ex)
            {
                return new ActionResult
                {
                    Status = 500,
                    Message = $"Error creating quiz result: {ex.Message}"
                };
            }
        }

        public override async Task<QuizResultModel> GetNotScanned(Empty request, ServerCallContext context)
        {
            try
            {
                var res = await _context.DemoQrcs.FirstOrDefaultAsync(x => x.IsSynced == false);
                res.IsSynced = true;
                _context.DemoQrcs.Update(res);
                return new QuizResultModel { 
                    Id = res.Id,
                    QuizResultCode = res.QrcodeGen
                };
            }
            catch (Exception ex)
            {
                return new QuizResultModel { };
            }
        }
    }
}
