using Grpc.Core;
using NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService;

namespace NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService.Services
{
    public class GreeterService : Greeter.GreeterBase
    {
        private readonly ILogger<GreeterService> _logger;
        public GreeterService(ILogger<GreeterService> logger)
        {
            _logger = logger;
        }

        public override Task<HelloReply> SayHello(HelloRequest request, ServerCallContext context)
        {
            return Task.FromResult(new HelloReply
            {
                Message = "Hello " + request.Name
            });
        }
    }
}
