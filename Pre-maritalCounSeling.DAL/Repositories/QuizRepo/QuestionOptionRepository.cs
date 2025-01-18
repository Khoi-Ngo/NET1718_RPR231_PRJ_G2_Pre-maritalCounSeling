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
    public class QuestionOptionRepository : GenericRepository<QuestionOption>
    {
        private ILogger logger;
        private IHttpContextAccessor httpContextAccessor;

        public QuestionOptionRepository(NET1718_RPR231_PRJ_G2_PremaritalCounselingContext context, ILogger logger, IHttpContextAccessor httpContextAccessor) : base(context)
        {
            this.logger = logger;
            this.httpContextAccessor = httpContextAccessor;
        }
    }
}
