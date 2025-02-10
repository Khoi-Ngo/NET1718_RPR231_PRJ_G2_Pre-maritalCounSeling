using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Infrastructure;

namespace Pre_maritalCounSeling.BAL.ServiceQuiz
{
    public interface IQuizResultService
    {
        Task DeleteQuizResultAsync(long id);
        Task<List<QuizResult>> GetQuizResultsAsync();
        Task<QuizResult> GetQuizResultAsync(long id);
    }
    public class QuizResultService : IQuizResultService
    {
        private readonly UnitOfWork _unitOfWork;
        public QuizResultService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task DeleteQuizResultAsync(long id)
        {
            var quizResult = (await _unitOfWork.QuizResultRepository.GetByIdAsync(id));
            await _unitOfWork.QuizResultRepository.RemoveAsync(quizResult);
        }

        public async Task<List<QuizResult>> GetQuizResultsAsync()
            => await _unitOfWork.QuizResultRepository.GetAllAsync();

        public async Task<QuizResult> GetQuizResultAsync(long id)
            => await _unitOfWork.QuizResultRepository.GetByIdAsync(id);

    }
}
