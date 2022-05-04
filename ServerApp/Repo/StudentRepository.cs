using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServerApp.Data;
using ServerApp.Models;

namespace ServerApp.Repo
{
    public class StudentRepository : IStudentRepository
    {
        private readonly KinderGartenContext _context;
        public StudentRepository(KinderGartenContext context)
        {
            _context=context;
        }
        public async Task<User> AddUser(User user)
        {
            var student =await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return student.Entity;
        }

        public async Task<User> DeleteUser(int userId)
        {
            var existstudent = await GetUserAsync(userId);
            if(existstudent!=null){
                _context.Users.Remove(existstudent);
                await _context.SaveChangesAsync();
                return existstudent;
            }
            return null;
        }
      
        public async Task<bool> ExistUser(int userId)
        {
          return await _context.Users.AnyAsync(x=>x.Id==userId);
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            return await _context.Users.Include(nameof(Classes)).ToListAsync();
        }

        public async Task<User> GetUserAsync(int userId)
        {
           return await _context.Users.Include(nameof(Classes)).Include(nameof(Lesson)).FirstOrDefaultAsync(x=>x.Id==userId);
        }

        public async Task<User> UpdateUser(int userId, User user)
        {
              var existstudent = await GetUserAsync(userId);
              if(existstudent!=null){
                  existstudent.FirstName=user.FirstName;
                  existstudent.LastName=user.LastName;
                  existstudent.DateOfBirth=user.DateOfBirth;
                  existstudent.Created=DateTime.Now;
                  existstudent.Phone=user.Phone;
                  existstudent.Adres=user.Adres;
                  existstudent.Classes.Name=user.Classes.Name;
                  
                   await _context.SaveChangesAsync();
                 return existstudent;


              }
              return null;
        }
    }
}