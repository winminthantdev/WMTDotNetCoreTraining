using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WMTDotNetCoreTraining.Database.Models;

namespace WMTDotNetCoreTraining.RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsController : Controller
{
    private readonly AppDbContext _dbContext = new AppDbContext();
    
    // GET
    [HttpGet]
    public IActionResult GetBlogs()
    {
        var lst = _dbContext.TblBlogs
            .AsNoTracking()
            .Where( b  => b.DeleteFlag == false )
            .ToList();
        return Ok(lst);
    }

    [HttpGet("blogId")]
    public IActionResult GetBlog(int id)
    {
        var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (item is null)
        {
            return NotFound();
        }
        return Ok(item);
    }


    [HttpPost]
    public IActionResult CreateBlog(TblBlog blog)
    {
        _dbContext.TblBlogs.Add(blog);
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpPut("{id}")]
    public IActionResult UpdateBlog(int id, TblBlog blog)
    {
        var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (item is null)
        {
            return NotFound();
        }
        
        item.BlogTitle = blog.BlogTitle;
        item.BlogAuthor = blog.BlogAuthor;
        item.BlogContent = blog.BlogContent;
        
        _dbContext.Entry(item).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpPatch("{id}")]
    public IActionResult PatchBlog(int id, TblBlog blog)
    {
        var item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (item is null)
        {
            return NotFound();
        }

        if (!string.IsNullOrEmpty(blog.BlogTitle))
        {
            item.BlogTitle = blog.BlogTitle;
        }

        if (!string.IsNullOrEmpty(blog.BlogAuthor))
        {
            item.BlogAuthor = blog.BlogAuthor;  
        }

        if (!string.IsNullOrEmpty(blog.BlogContent))
        {
            item.BlogContent = blog.BlogContent;
        }
        
        _dbContext.Entry(item).State = EntityState.Modified;
        _dbContext.SaveChanges();
        return Ok();
    }
    
    [HttpDelete("{id}")]
    public IActionResult DeleteBlog(int id)
    {
        var  item = _dbContext.TblBlogs.AsNoTracking().FirstOrDefault(b => b.BlogId == id);
        if (item is null)
        {
            return NotFound();
        }

        item.DeleteFlag = true;
        _dbContext.Entry(item).State = EntityState.Modified;
        
        // _dbContext.Entry(item).State = EntityState.Deleted;
        _dbContext.TblBlogs.Remove(item);
        return Ok();
    }
}