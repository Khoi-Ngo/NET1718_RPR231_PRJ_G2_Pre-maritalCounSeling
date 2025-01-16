using Microsoft.EntityFrameworkCore;
using Pre_maritalCounSeling.DAL.Base;
using Pre_maritalCounSeling.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.DAL.Repositories
{
    public class UserRepository : GenericRepository<User>
    {
        //public async Task<User> GetUser(string userName, string password)
        //{
        //    return await _context.Users
        //        .FirstOrDefaultAsync(u => u.UserName == userName && u.Password == password && u.IsActive == true);

        //}
    }
}
