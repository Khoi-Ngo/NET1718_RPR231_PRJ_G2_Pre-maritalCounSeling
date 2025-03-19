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

namespace Pre_maritalCounSeling.DAL.Repositories
{
    public class UserDetailRepository : GenericRepository<UserDetail>
    {
        private ILogger logger;
        private IHttpContextAccessor httpContextAccessor;

        public UserDetailRepository(NET1718_PRN231_PRJ_G2_PremaritalCounselingContext context) : base(context)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<UserDetail>> GetAll2Async()
        {
            return await _context.UserDetails.Include(x => x.User).Include(x => x.Category).ToListAsync(); 
        }

        public async Task<UserDetail> GetById2Async(long id)
        {
            return await _context.UserDetails.Include(x => x.User).Include(x => x.Category).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
