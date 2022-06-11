using MongoDB.Bson;

namespace WebApplication1.Models
{
    public class Department
    {
        public ObjectId Id { get; set; }

        public int DepartmentID { get; set;}


        public string DepartmentName { get; set;}   

        
    }
}
