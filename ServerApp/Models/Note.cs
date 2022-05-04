using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class Note
    {
        public Guid Id { get; set; }
        public double Point { get; set; }
        public DateTime DateAdded { get; set; }
        public ICollection<Lesson> Lessons { get; set; }
    }
}