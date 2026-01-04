// See https://aka.ms/new-console-template for more information

using WMTDotNetCoreTraining.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext dbContext = new AppDbContext();
var list = dbContext.TblBlogs.ToList();