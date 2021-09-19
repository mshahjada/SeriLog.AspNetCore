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
            new() { Id = 3, Name = "Jimi", ContactNo = "876513" }
        };

        ILogger<Student> _logger;

        public StudentController(ILogger<Student> logger)
        {
            _logger = logger;
        }

        //[HttpGet]
        //public IActionResult Gets()
        //{
        //    return Ok(_students);
        //}


        [HttpGet]
        public IActionResult Get(int id)
        {
            var student = _students.FirstOrDefault(x => x.Id == id);


            LoggingFeature.loggingLevel.MinimumLevel = LogEventLevel.Information;
            Log.Information("Student Deleted: {@Student}", student);

            LoggingFeature.loggingLevel.MinimumLevel = LogEventLevel.Debug;
            Log.Debug("Student Deleted: {@Student} {ThreadId}", student);
            
            Log.Error("Student Deleted: {@Student}", student);
            //Log.Information("Student Deleted: {@Student}", student);
            //Log.Information("Student Deleted: {@Student}", student);

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
            try
            {
                var student = _students.FirstOrDefault(x => x.Id == id);
                if (student is null)
                    throw new ObjectNotFoundException<Student>();

                _students.Remove(student);

                Log.Error("Student Deleted: {@Student}", student);

                Log.Information("Student json: {@List}", new { Students = _students });

                //var scLog = Log.ForContext<Student>();

                //dynamic obj = new { List = _students };

                //scLog.Information("Source Contx { @Students }", obj);

                var fruit = new[] { "Apple", "Pear", "Orange" };
                Log.Information("In my bowl I have {Ruit}", fruit);


                var sensorInput = new { Latitude = 25, Longitude = 134 };
                Log.Information("Processing {@SensorInput}", sensorInput);
            }
            catch (Exception ex)
            {

                throw;
            }
           

            return Ok();
        }

    }
}
