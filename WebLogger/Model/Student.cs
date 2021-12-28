using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebLogger.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ContactNo { get; set; }

        public List<StdCourse> Courses { get; set; }
    }


    public class StdCourse
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int CourseName { get; set; }
    }
}
