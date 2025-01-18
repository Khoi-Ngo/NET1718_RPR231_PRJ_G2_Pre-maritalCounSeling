using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Pre_maritalCounSeling.DAL.Base;
using Pre_maritalCounSeling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.DAL.Repositories.UserRepo
{
    public class UserRepository : GenericRepository<User>
    {
        private ILogger logger;
        private IHttpContextAccessor httpContextAccessor;

        public UserRepository(NET1718_RPR231_PRJ_G2_PremaritalCounselingContext context, ILogger logger, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<User> GetUser(string userName, string password)
        {
            //some time does not need to check active
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);

        }

        public async Task<List<User>> Search(string item1, string item2)
        {
            throw new NotImplementedException();
        }
    }
}