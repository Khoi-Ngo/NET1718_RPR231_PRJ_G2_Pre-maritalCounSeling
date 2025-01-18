using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Repositories.QuizRepo;
using Pre_maritalCounSeling.DAL.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.DAL.Infrastructure
{
    public class UnitOfWrork
    {
        private readonly ILogger _logger;
        private readonly NET1718_RPR231_PRJ_G2_PremaritalCounselingContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region entities repositories

        private readonly UserRepository _userRepository;
        private readonly QuestionOptionRepository _questionOptionRepository;
        private readonly QuestionRepository _questionRepository;
        private readonly QuizQuestionRepository _quizQuestionRepository;
        private readonly QuizRepository _quizRepository;
        private readonly QuizResultDetailRepository _quizResultDetailRepository;
        private readonly QuizResultRepository _quizResultRepository;
        private readonly QuizSuggestionRepository _quizSuggestionRepository;

        public UnitOfWrork(ILoggerFactory loggerFactory, NET1718_RPR231_PRJ_G2_PremaritalCounselingContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger("Logs");
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            #region singleton repositories

            _userRepository ??= new UserRepository(_context, _logger, _httpContextAccessor);
            _questionOptionRepository ??= new QuestionOptionRepository(_context, _logger, _httpContextAccessor);
            _questionRepository ??= new QuestionRepository(_context, _logger, _httpContextAccessor);
            _quizQuestionRepository ??= new QuizQuestionRepository(_context, _logger, _httpContextAccessor);
            _quizRepository ??= new QuizRepository(_context, _logger, _httpContextAccessor);
            _quizResultDetailRepository ??= new QuizResultDetailRepository(_context, _logger, _httpContextAccessor);
            _quizResultRepository ??= new QuizResultRepository(_context, _logger, _httpContextAccessor);
            _quizSuggestionRepository ??= new QuizSuggestionRepository(_context, _logger, _httpContextAccessor);

            #endregion
        }

        #endregion


    }
}
