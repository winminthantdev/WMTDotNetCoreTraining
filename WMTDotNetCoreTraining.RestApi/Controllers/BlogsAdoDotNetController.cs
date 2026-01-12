using Microsoft.AspNetCore.Mvc;

namespace WMTDotNetCoreTraining.RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]

public class BlogsAdoDotNetController : Controller
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123;";
    
    // GET
    [HttpGet]
    public IActionResult GetBlogs()
    {
        Console.WriteLine("Connection string : " + _connectionString);
        SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        string query = @"SELECT [BlogId],
                 [BlogTitle],
                 [BlogContent],
                 [DeleteFlag]
           FROM [dbo].[Tbl_Blogs] where DeleteFlag = 0";

        SqlCommand cmd = new SqlCommand(query, connection);


        SqlDataReader reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine(reader["BlogId"] + " - " + reader["BlogTitle"] + " - " + reader["BlogContent"]);
        }




        connection.Close();

     
    }

    [HttpGet("blogId")]
    public IActionResult GetBlog(int id)
    {
        
    }


    [HttpPost]
    public IActionResult CreateBlog(TblBlog blog)
    {
       
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, TblBlog blog)
    {
        
    }
    
    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, TblBlog blog)
    {
        
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        
    }
}