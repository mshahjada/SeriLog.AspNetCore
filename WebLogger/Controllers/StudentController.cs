using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebLogger.Exceptions;
using WebLogger.Handler;
using WebLogger.Model;

namespace WebLogger.Controllers
{
    [Route("api/student")]
    [GlobalExceptionHandler]
    public class StudentController : ControllerBase
    {
        protected List<Student> _students = new() {
            new() { Id = 1, Name = "Jamal", ContactNo = "0121683" },
            new() { Id = 2, Name = "Rik", ContactNo = "546512" },
            new() { Id = 3, Name = "Jimi", ContactNo = "876513" },
            new() { Id = 4, Name = "Rick", ContactNo = "876513" },
            new() { Id = 5, Name = "Name-5", ContactNo = "876513" },
            new() { Id = 6, Name = "Name-6", ContactNo = "876513" },
            new() { Id = 7, Name = "Name-7", ContactNo = "876513" },
            new() { Id = 8, Name = "Name-8", ContactNo = "876513" },
            new() { Id = 9, Name = "Name-9", ContactNo = "876513" },
            new() { Id = 10, Name = "Name-10", ContactNo = "876513" },
            new() { Id = 11, Name = "Name-11", ContactNo = "876513" },
            new() { Id = 12, Name = "Name-12", ContactNo = "876513" }
        };

        ILogger<Student> _logger;

        public StudentController(ILogger<Student> logger)
        {
            _logger = logger;
        }


        [HttpGet]
        public IActionResult Get(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);


            LoggingFeature.loggingLevel.MinimumLevel = LogEventLevel.Information;
            Log.Information("Students: {@Student}", student);

           
            return Ok(student);
        }


        [HttpPost]
        public IActionResult Add(Student student)
        {
            _students.Add(student);
            return Ok();
        }


        [HttpPut]
        public IActionResult Edit(int id, Student student)
        {
            if (!_students.Any(x => x.Id == id))
                throw new ArgumentNullException();

            _students.Where(x => x.Id == id).Select(x => x = student);

            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);
            if (student is null)
                throw new ObjectNotFoundException<Student>();

            _students.Remove(student);

            Log.Information("Student Deleted: {@Student}", student);

            return Ok();
        }

    }
}
