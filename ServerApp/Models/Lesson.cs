using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ServerApp.Models
{
    public class Lesson
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Note> Notes { get; set; }
    }
}