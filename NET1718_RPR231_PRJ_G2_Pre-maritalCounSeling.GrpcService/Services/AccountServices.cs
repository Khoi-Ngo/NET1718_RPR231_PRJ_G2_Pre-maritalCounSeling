using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NET1718_PRN231_PRJ_G2_PremaritalCounseling.GrpcService.Protos;
using Pre_maritalCounSeling.BAL.ServiceUser;

namespace NET1718_RPR231_PRJ_G2_Pre_maritalCounSeling.GrpcService.Services
{
    public class AccountServices : AccountUserGRPC.AccountUserGRPCBase
    {
        private readonly ILogger<AccountServices> _logger;
        private readonly IUserService _userService;

        public AccountServices(ILogger<AccountServices> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

    }
}
