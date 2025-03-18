using Microsoft.AspNetCore.Http;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Infrastructure;

namespace Pre_maritalCounSeling.BAL.ServiceQuiz
{
    public interface IQuizResultService
    {
        Task DeleteQuizResultAsync(long id);
        Task<List<QuizResult>> GetQuizResultsAsync();
        Task<QuizResult> GetQuizResultAsync(long id);
        Task<List<QuizResult>> GetQuizResultsSimplyAsync();
    }
    public class QuizResultService : IQuizResultService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public QuizResultService(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task DeleteQuizResultAsync(long id)
        {
            var userRole = AppUtil.GetUserRoleFromUserContext(_httpContextAccessor);
            var quizResult = (await _unitOfWork.QuizResultRepository.GetByIdAsync(id));
            if (userRole.Equals("Admin") || quizResult.UserId == AppUtil.GetUserIdFromUserContext(_httpContextAccessor))
            {
                await _unitOfWork.QuizResultRepository.RemoveAsync(quizResult);
                return;
            }
            throw new Exception("Unauthorized access");
        }

        public async Task<List<QuizResult>> GetQuizResultsAsync()
        {
            var userRole = AppUtil.GetUserRoleFromUserContext(_httpContextAccessor);
            if (userRole.Equals("Admin")) return await _unitOfWork.QuizResultRepository.GetAllWithQuizAndUserAsync();
            if (userRole.Equals("Customer")) return await _unitOfWork.QuizResultRepository.GetAllWithQuizAndUserAsync(AppUtil.GetUserIdFromUserContext(_httpContextAccessor));
            throw new Exception("Unauthorized access / Cannot find the quiz results");
        }

        public async Task<List<QuizResult>> GetQuizResultsSimplyAsync()
        {

            var check = await _unitOfWork.QuizResultRepository.GetAllAsync();
            return check;

        }


        public async Task<QuizResult> GetQuizResultAsync(long id)
        {
            var userRole = AppUtil.GetUserRoleFromUserContext(_httpContextAccessor);
            var quizResult = await _unitOfWork.QuizResultRepository.GetWithQuizAndUserAsync(id);
            if (quizResult == null) throw new Exception("Quiz result not found");
            if (userRole.Equals("Admin")) return quizResult;
            if (userRole.Equals("Customer")) return quizResult.UserId == AppUtil.GetUserIdFromUserContext(_httpContextAccessor) ? quizResult : throw new Exception("Unauthorized access / Cannot find the quiz result");
            throw new Exception("Unauthorized access / Cannot find the quiz result");
        }

        //TODO: EXPERT can access the customer quiz results via new api endpoint/ method

    }
}
