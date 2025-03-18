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

namespace Pre_maritalCounSeling.DAL.Repositories.QuizRepo
{
    public class QuizResultRepository : GenericRepository<QuizResult>
    {
        private ILogger logger;
        private IHttpContextAccessor httpContextAccessor;

        public QuizResultRepository(NET1718_PRN231_PRJ_G2_PremaritalCounselingContext context) : base(context)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<QuizResult>> GetAllWithQuizAndUserAsync()
        {
            return await _context.QuizResults
                .Include(qr => qr.User).Include(qr => qr.Quiz).ToListAsync();
        }
        public async Task<List<QuizResult>> GetAllWithQuizAndUserAsync(long customerId)
        {
            return await _context.QuizResults
                .Include(qr => qr.User).Include(qr => qr.Quiz).Where(qr => qr.UserId == customerId).ToListAsync();
        }

        public async Task<QuizResult> GetWithQuizAndUserAsync(long id)
        {
            return await _context.QuizResults
                .Include(qr => qr.User).Include(qr => qr.Quiz).FirstOrDefaultAsync(qr => qr.Id == id);
        }
    }
}
