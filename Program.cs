// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.SqlClient;
using WMTDotNetTraning.ConsoleApp;

Console.WriteLine("Hello, World!");
// Console.ReadLine();
// Console.ReadKey();

// md => markdown

// C# <=> Database

// ADO.NET
// Dapper(ORM)
// EFCore / Entity Frame (ORM)

// C# => sql query =>

// nuget

// Ctrl + .

// max connection = 100
// 100 = 99

// 101

AdoDotNetExample AdoDotNetExample = new AdoDotNetExample();

// AdoDotNetExample.read();
// AdoDotNetExample.Create();
// AdoDotNetExample.Edit();
// AdoDotNetExample.Delete();


DapperExample dapperExample = new DapperExample();
// dapperExample.Read();
// dapperExample.Create("Blog Title Two", "Win Min Thant","This is blog content for blog title two.");
dapperExample.Edit(2003);
// dapperExample.Update(2003,"Blog Title Two", "Min Thant","This is blog content for blog title two.");
// dapperExample.Delete(2003);

Console.ReadKey();