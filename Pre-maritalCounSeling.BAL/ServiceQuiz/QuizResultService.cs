using Microsoft.AspNetCore.Http;
using Pre_maritalCounSeling.Common.Util;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Infrastructure;

namespace Pre_maritalCounSeling.BAL.ServiceQuiz
{
    public interface IQuizResultService
    {
        Task DeleteQuizResultAsync(long id);
        Task DeleteQuizResultSimplyAsync(long id);
        Task<List<QuizResult>> GetQuizResultsAsync();
        Task<QuizResult> GetQuizResultAsync(long id);
        Task<List<QuizResult>> GetQuizResultsSimplyAsync();
        Task UpdateQuizResultAsync(QuizResult request);
        Task CreateQuizResultSimplyAsync(QuizResult request);
        Task<List<UserDetail>> GetUserDetailsAsync();
        Task<UserDetail> GetUserDetailAsync(long id);
        Task<List<UserDetailCategory>> GetUserDetailCateSimplyAsync();
        Task CreateUserDetailAsync(UserDetail request);
        Task<QuizResult> GetQuizResultSimplyAsync(long id);
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
        public async Task<QuizResult> GetQuizResultSimplyAsync(long id)
        {
            return await _unitOfWork.QuizResultRepository.GetByIdAsync(id);
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

        public async Task UpdateQuizResultAsync(QuizResult request)
        {
            await _unitOfWork.QuizResultRepository.UpdateAsync(request);
        }
        public async Task CreateQuizResultSimplyAsync(QuizResult request)
        {
            await _unitOfWork.QuizResultRepository.CreateAsync(request);
        }

        public async Task<List<UserDetail>> GetUserDetailsAsync()
        {
            return await _unitOfWork.UserDetailRepository.GetAll2Async();
        }

        public async Task<UserDetail> GetUserDetailAsync(long id)
        {
            return await _unitOfWork.UserDetailRepository.GetById2Async(id);
        }

        public async Task<List<UserDetailCategory>> GetUserDetailCateSimplyAsync()
        {
            return await _unitOfWork.UserDetailCateRepository.GetAllAsync();
        }

        public async Task CreateUserDetailAsync(UserDetail request)
        {
            await _unitOfWork.UserDetailRepository.CreateAsync(request);

        }

        public async Task DeleteQuizResultSimplyAsync(long id)
        {
            var deleteObj = await _unitOfWork.QuizResultRepository.GetByIdAsync(id);
            await _unitOfWork.QuizResultRepository.RemoveAsync(deleteObj);
        }
    }
}
