using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ServerApp.Models
{
    public class User:IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime Created { get; set; }
        
        public string Phone { get; set; }
        public string ProfileImageUrl { get; set; }
        public string Adres { get; set; }
        public string Gender { get; set; }
        public  string Role { get; set; }
        public int ClassId { get; set; }
        public Classes Classes { get; set; }
        
    }
}