using System.Data;
using System.Data.SqlClient;
using Dapper;
using WMTDotNetTraning.ConsoleApp.Models;

namespace WMTDotNetTraning.ConsoleApp;

public class DapperExample
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123;";

    
    public void Read()
    {
        // using (IDbConnection db = new SqlConnection(_connectionString))
        // {
        //     string query = "select * from tbl_blogs where DeleteFlag = 0;";
        //     var lst = db.Query(query).ToList();
        //     foreach (var item in lst)
        //     {
        //         Console.WriteLine(item.BlogId);
        //         Console.WriteLine(item.BlogTitle);
        //         Console.WriteLine(item.BlogAuthor);
        //         Console.WriteLine(item.BlogContent);
        //     }
        // }
        
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            string query = "select * from tbl_blogs where DeleteFlag = 0;";
            var lst = db.Query<BlogDataModel>(query).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
        
        // DTO = Data Transfer Object
    }
    
    public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blogs]
                            (
                             [BlogTitle],
                             [BlogAuthor],
                             [BlogContent],
                             [DeleteFlag])
                         VALUES 
                              (@BlogTitle, @BlogAuthor, @BlogContent,0)";
            
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query, new BlogDataModel {BlogTitle = title, BlogAuthor = author, BlogContent = content});
                Console.WriteLine(result == 1 ? "Saving Successful."  : "Saving Failed.");
                
            }
        }

    public void Edit(int blogId)
    {
        string query = $@"UPDATE [dbo].[Tbl_Blogs] where DeleteFlag = 0 and BlogId  = @BlogId;";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            var item = db.Query<BlogDataModel>(query, new BlogDataModel {BlogId = blogId}).FirstOrDefault();

            // if (item == null)
            if ( item is null )
            {
                Console.WriteLine("No Data Found.");
                return;
            }
            
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
    }
    
    public void Update(int blogId, string title, string author, string content)
    {
        string query = $@"UPDATE [dbo].[Tbl_Blogs]
                     SET [BlogTitle] = @BlogTitle,
                         [BlogAuthor] =  @BlogAuthor,
                         [BlogContent] =  @BlogContent,
                         [DeleteFlag] = 0
                     WHERE 
                          BlogId = @BlogId";
        
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            int result = db.Execute(query, new BlogDataModel {BlogId = blogId, BlogTitle = title, BlogAuthor = author, BlogContent = content});
            Console.WriteLine(result == 1 ? "Updating Successful."  : "Updating Failed.");
            
        }
    }

    public void Delete(int blogId)
    {
        string query = $@"DELETE FROM [dbo].[Tbl_Blogs] where BlogId = @BlogId";
        using (IDbConnection db = new SqlConnection(_connectionString))
        {
            int result = db.Execute(query, new BlogDataModel {BlogId = blogId});
            Console.WriteLine(result == 1 ? "Deleting Successful."  : "Deleting Failed.");
        }
    }
}