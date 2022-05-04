using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class Classes
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
      
        public ICollection<User> Users { get; set; }
    }
}