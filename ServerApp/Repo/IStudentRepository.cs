using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServerApp.Models;

namespace ServerApp.Repo
{
    public interface IStudentRepository
    {

        Task<List<User>> GetAllUserAsync();
        Task<User> GetUserAsync(int userId);
        Task<bool> ExistUser(int userId);
        Task<User> UpdateUser(int userId, User user);
        Task<User> DeleteUser(int userId);
        Task<User> AddUser(User user);



    }
}