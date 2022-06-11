using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public EmployeeController (IConfiguration configuration , IWebHostEnvironment env)
        {
            _configuration = configuration; 
            _env = env;
        }

        [HttpGet]

        public JsonResult Get()
        {
            MongoClient dbclient = new MongoClient(_configuration.GetConnectionString("EmployeeAppcon"));

            var employee = dbclient.GetDatabase("testdb").GetCollection<Employee>("Employee").AsQueryable();

            return new JsonResult(employee);

        }

        [HttpPost]

        public JsonResult Post(Employee dept)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            int LastEmployeeId = dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").AsQueryable().Count();

            dept.EmployeeId = LastEmployeeId + 1;

            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").InsertOne(dept);


            return new JsonResult("Added Successfuly");
        }

        [HttpPut]

        public JsonResult Put(Employee dept)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Employee>.Filter.Eq("EmployeeId", dept.EmployeeId);

            var update = Builders<Employee>.Update.Set("Department", dept.Department)
                                                   .Set("EmployeeName", dept.EmployeeName)
                                                   .Set("DateOfJoining", dept.DateOfJoining)
                                                   .Set("PhotoFileName", dept.PhotoFileName);
             

            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").UpdateOne(filter, update);


            return new JsonResult("Updated Successfuly");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Employee>.Filter.Eq("EmployeeId", id);


            dbClient.GetDatabase("testdb").GetCollection<Employee>("Employee").DeleteOne(filter);


            return new JsonResult("Deleted Successfuly");
        }

        [Route("SaveFile")]
        [HttpPost]

        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var PostedFile = httpRequest.Files[0];
                string FileName = PostedFile.FileName;
                var PhysicalPath = _env.ContentRootPath + "/Photos/" + FileName;

                using(var stream =  new FileStream(PhysicalPath,FileMode.Create))
                {
                    PostedFile.CopyTo(stream);
                }
                return new JsonResult(FileName);
            }
            catch (Exception)
            {

                return new JsonResult("user.png");
            }
        }

    }
}
