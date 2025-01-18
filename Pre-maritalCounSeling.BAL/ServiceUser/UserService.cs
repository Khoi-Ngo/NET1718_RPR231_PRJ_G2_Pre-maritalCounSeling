using Pre_maritalCounSeling.DAL.Entities;
using Pre_maritalCounSeling.DAL.Repositories.UserRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pre_maritalCounSeling.BAL.ServiceUser
{
    public interface IUserService
    {
        Task<List<User>> GetAll();
        Task<User> GetById(int id);
        Task<int> Create(User User);
        Task<int> Update(User User);
        Task<bool> Delete(User User);
        Task<List<User>> Search(string item1, string item2);
        Task<User> LoginAsync(string userName, string password);
    }
    public class UserService : IUserService
    {
        private UserRepository _userRepository;
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<int> Create(User User)
        {
            return await _userRepository.CreateAsync(User);
        }

        public async Task<bool> Delete(User User)
        {
            return await _userRepository.RemoveAsync(User);
        }

        public async Task<List<User>> GetAll()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public Task<User> LoginAsync(string userName, string password)
        {
            return _userRepository.GetUser(userName, password);
        }

        public async Task<List<User>> Search(string item1, string item2)
        {
            return await _userRepository.Search(item1, item2);
        }

        public Task<int> Update(User User)
        {
            //check active
            return _userRepository.UpdateAsync(User);
        }
    }
}
