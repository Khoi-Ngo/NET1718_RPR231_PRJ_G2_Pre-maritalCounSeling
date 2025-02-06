using Microsoft.AspNetCore.Http;
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
    public class QuizSuggestionRepository : GenericRepository<QuizSuggestion>
    {
        private ILogger logger;
        private IHttpContextAccessor httpContextAccessor;

        public QuizSuggestionRepository(NET1718_RPR231_PRJ_G2_PremaritalCounselingContext context ) : base(context)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
