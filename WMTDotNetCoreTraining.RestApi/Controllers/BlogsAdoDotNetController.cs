using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.Data;
using WMTDotNetCoreTraining.Database.Models;
using WMTDotNetCoreTraining.RestApi.ViewModels;

namespace WMTDotNetCoreTraining.RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsAdoDotNetController : Controller
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123;TrustServerCertificate=True";

    [HttpGet]
    public IActionResult GetBlogs()
    {
        List<BlogViewModel> list = new List<BlogViewModel>();
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT * FROM [dbo].[Tbl_Blogs] WHERE DeleteFlag = 0";
        SqlCommand cmd = new SqlCommand(query, connection);
        SqlDataReader reader = cmd.ExecuteReader();

        while (reader.Read())
        {
            list.Add(new BlogViewModel
            {
                Id = Convert.ToInt32(reader["BlogId"]),
                Title = Convert.ToString(reader["BlogTitle"]),
                Author = Convert.ToString(reader["BlogAuthor"]),
                Content = Convert.ToString(reader["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
            });
        }
        return Ok(list);
    }

    [HttpGet("{id}")]
    public IActionResult GetBlog(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = "SELECT * FROM [dbo].[Tbl_Blogs] WHERE BlogId = @BlogId AND DeleteFlag = 0";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);

        SqlDataReader reader = cmd.ExecuteReader();
        if (reader.Read())
        {
            var item = new BlogViewModel
            {
                Id = Convert.ToInt32(reader["BlogId"]),
                Title = Convert.ToString(reader["BlogTitle"]),
                Author = Convert.ToString(reader["BlogAuthor"]),
                Content = Convert.ToString(reader["BlogContent"]),
                DeleteFlag = Convert.ToBoolean(reader["DeleteFlag"])
            };
            return Ok(item);
        }

        return NotFound("No blog found.");
    }

    [HttpPost]
    public IActionResult CreateBlog(TblBlog blog)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = @"INSERT INTO [dbo].[Tbl_Blogs] ([BlogTitle], [BlogAuthor], [BlogContent], [DeleteFlag]) 
                         VALUES (@Title, @Author, @Content, 0)";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Title", blog.BlogTitle);
        cmd.Parameters.AddWithValue("@Author", blog.BlogAuthor);
        cmd.Parameters.AddWithValue("@Content", blog.BlogContent);

        int result = cmd.ExecuteNonQuery();
        return result > 0 ? Ok("Blog Created.") : BadRequest("Creation Failed.");
    }

    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, TblBlog blog)
    {
        string conditioins = "";

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            conditioins += " [BlogTitle] = @BlogTitle, ";
        }

        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            conditioins += "[BlogAuthor] = @BlogAuthor, ";
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            conditioins += "[BlogContent] = @BlogContent, ";
        }

        if (conditioins.Length == 0)
        {
            return BadRequest("Invalid Parameters");
        }
        
        conditioins = conditioins.Substring(0, conditioins.Length - 2);
        
        
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        string query = @"UPDATE [dbo].[Tbl_Blogs] 
                         SET [BlogTitle] = @Title, [BlogAuthor] = @Author, [BlogContent] = @Content 
                         WHERE [BlogId] = @Id";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);
        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            cmd.Parameters.AddWithValue("@Title", blog.BlogTitle);    
        }
        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            cmd.Parameters.AddWithValue("@Author", blog.BlogAuthor);
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            cmd.Parameters.AddWithValue("@Content", blog.BlogContent);
        }

        int result = cmd.ExecuteNonQuery();
        return result > 0 ? Ok("Blog Updated.") : NotFound();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        // Soft delete implementation
        string query = "UPDATE [dbo].[Tbl_Blogs] SET DeleteFlag = 1 WHERE BlogId = @Id";
        
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@Id", id);

        int result = cmd.ExecuteNonQuery();
        return result > 0 ? Ok("Blog Deleted.") : NotFound();
    }
}