using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Repositories.QuizRepo;
using Pre_maritalCounSeling.DAL.Repositories.UserRepo;

namespace Pre_maritalCounSeling.DAL.Infrastructure
{
    public class UnitOfWork
    {
        private readonly ILogger _logger;
        private readonly NET1718_RPR231_PRJ_G2_PremaritalCounselingContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        #region entities repositories

        public UserRepository UserRepository { get; }
        public QuestionOptionRepository QuestionOptionRepository { get; }
        public QuestionRepository QuestionRepository { get; }
        public QuizQuestionRepository QuizQuestionRepository { get; }
        public QuizRepository QuizRepository { get; }
        public QuizResultDetailRepository QuizResultDetailRepository { get; }
        public QuizResultRepository QuizResultRepository { get; }
        public QuizSuggestionRepository QuizSuggestionRepository { get; }
        public RoleRepository RoleRepository { get; }

        public UnitOfWork(ILoggerFactory loggerFactory, NET1718_RPR231_PRJ_G2_PremaritalCounselingContext context, IHttpContextAccessor httpContextAccessor)
        {
            _logger = loggerFactory.CreateLogger("Logs");
            _context = context;
            _httpContextAccessor = httpContextAccessor;

            #region singleton repositories

            UserRepository ??= new UserRepository(_context);
            QuestionOptionRepository ??= new QuestionOptionRepository(_context);
            QuestionRepository ??= new QuestionRepository(_context);
            QuizQuestionRepository ??= new QuizQuestionRepository(_context);
            QuizRepository ??= new QuizRepository(_context);
            QuizResultDetailRepository ??= new QuizResultDetailRepository(_context);
            QuizResultRepository ??= new QuizResultRepository(_context);
            QuizSuggestionRepository ??= new QuizSuggestionRepository(_context);
            RoleRepository ??= new RoleRepository(_context);
            #endregion
        }
        public void Dispose()
        {
            _context?.Dispose();
        }

        #endregion


    }
}
