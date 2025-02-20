using Microsoft.AspNetCore.Http;
using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.BAL.ServiceQuiz
{
    public interface IQuizService
    {
        Task<List<Quiz>> GetQuizzesAsync();
    }
    public class QuizService : IQuizService
    {
        private readonly UnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public QuizService(UnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _unitOfWork = unitOfWork;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Quiz>> GetQuizzesAsync()
        => await _unitOfWork.QuizRepository.GetAllAsync();
    }
}
