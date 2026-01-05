using Microsoft.AspNetCore.Mvc;

namespace WMTDotNetCoreTraining.RestApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BlogsController : Controller
{
    // GET
    [HttpGet]
    public IActionResult GetBlogs()
    {
        return Ok();
    }
    
    [HttpPost]
    public IActionResult CreateBlog()
    {
        return Ok();
    }
    
    [HttpPut]
    public IActionResult UpdateBlog()
    {
        return Ok();
    }
    
    [HttpDelete]
    public IActionResult DeleteBlog()
    {
        return Ok();
    }
}