using Microsoft.EntityFrameworkCore;
using WMTDotNetTraning.ConsoleApp.Models;

namespace WMTDotNetTraning.ConsoleApp;

public class EFCoreExample
{
    public void Read()
    {
        AppDbContext dbContext = new AppDbContext();
        var blogs = dbContext.Blogs.Where(x => x.DeleteFlag == false).ToList();
        foreach (var blog in blogs)
        {
            Console.WriteLine(blog.BlogId);
            Console.WriteLine(blog.BlogTitle);
            Console.WriteLine(blog.BlogAuthor);
            Console.WriteLine(blog.BlogContent);
        }
    }

    public void Create(string blogTitle, string blogAuthor, string blogContent)
    {
        BlogDataModel blog = new BlogDataModel
        {
            BlogTitle = blogTitle,
            BlogAuthor = blogAuthor,
            BlogContent = blogContent
        };
        
        AppDbContext dbContext = new AppDbContext();
        dbContext.Blogs.Add(blog);
        
        var result = dbContext.SaveChanges();
        
        Console.WriteLine(result == 1 ? "Saving Successful."  : "Saving Failed.");
        
    }

    public void Edit(int blogId)
    {
        AppDbContext dbContext = new AppDbContext();

        // dbContext.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
        
        var blog = dbContext.Blogs.FirstOrDefault(b => b.BlogId == blogId);

        if (blog is null)
        {
            Console.WriteLine("Blog not found.");
            return;
        }
        
        Console.WriteLine(blog.BlogId);
        Console.WriteLine(blog.BlogTitle);
        Console.WriteLine(blog.BlogAuthor);
        Console.WriteLine(blog.BlogContent);
    }
    
    public void Update(int blogId, string blogTitle, string blogAuthor, string blogContent)
    {
        AppDbContext dbContext = new AppDbContext();
        
        var blog = dbContext.Blogs.AsNoTracking().FirstOrDefault(b => b.BlogId == blogId);

        if (blog is null)
        {
            Console.WriteLine("Blog not found.");
            return;
        }

        if (!string.IsNullOrEmpty(blogTitle))
        {
            blog.BlogTitle = blogTitle;
        }

        if (!string.IsNullOrEmpty(blogAuthor))
        {
            blog.BlogAuthor = blogAuthor;
        }
        
        if (!string.IsNullOrEmpty(blogContent))
        {
            blog.BlogContent = blogContent;
        }
        
        dbContext.Entry(blog).State = EntityState.Modified;
        var result = dbContext.SaveChanges();
        
        Console.WriteLine(result == 1 ? "Updating Successful."  : "Updating Failed.");
    }

    public void Delete(int blogId)
    {
        AppDbContext dbContext = new AppDbContext();
        var blog = dbContext.Blogs.AsNoTracking().FirstOrDefault(b => b.BlogId == blogId);

        if (blog is null)
        {
            Console.WriteLine("Blog not found.");
            return;
        }
        
        dbContext.Entry(blog).State = EntityState.Deleted;
        var result = dbContext.SaveChanges();
        
        Console.WriteLine(result == 1 ? "Deleting Successful."  : "Deleting Failed.");
        

    }
    
}