using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DepartmentController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }

        [HttpGet]
        
        public JsonResult Get()
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var dbList = dbClient.GetDatabase("testdb").GetCollection<Department>("Department").AsQueryable();


            return new JsonResult(dbList);

        }

        [HttpPost]

        public JsonResult Post(Department dept)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            int LastDepartmentId = dbClient.GetDatabase("testdb").GetCollection<Department>("Department").AsQueryable().Count();

            dept.DepartmentID = LastDepartmentId + 1;

            dbClient.GetDatabase("testdb").GetCollection<Department>("Department").InsertOne(dept);


            return new JsonResult("Added Successfuly");
        }

        [HttpPut]

        public JsonResult Put(Department dept)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Department>.Filter.Eq("DepartmentID", dept.DepartmentID);

            var update = Builders<Department>.Update.Set("DepartmentName", dept.DepartmentName);
  

            dbClient.GetDatabase("testdb").GetCollection<Department>("Department").UpdateOne(filter , update);


            return new JsonResult("Updated Successfuly");
        }

        [HttpDelete("{id}")]

        public JsonResult Delete(int id)
        {
            MongoClient dbClient = new MongoClient(_configuration.GetConnectionString("EmployeeAppCon"));

            var filter = Builders<Department>.Filter.Eq("DepartmentID", id);


            dbClient.GetDatabase("testdb").GetCollection<Department>("Department").DeleteOne(filter);


            return new JsonResult("Deleted Successfuly");
        }

    }
}
