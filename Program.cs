// See https://aka.ms/new-console-template for more information

using System.Data;
using System.Data.SqlClient;

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

string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123;";
Console.WriteLine("Connection string : " + connectionString);
SqlConnection connection = new SqlConnection(connectionString);

Console.WriteLine("Connection opening...");
connection.Open();
Console.WriteLine("Connection opened.");

string query = @"SELECT [BlogId],
                 [BlogTitle],
                 [BlogContent],
                 [DeleteFlag]
           FROM [dbo].[Tbl_Blogs] where DeleteFlag = 0";

SqlCommand cmd = new SqlCommand(query, connection);

// SqlDataAdapter adapter = new SqlDataAdapter(cmd);
// DataTable dt = new DataTable();
// adapter.Fill(dt);

SqlDataReader reader = cmd.ExecuteReader();
while (reader.Read())
{
    Console.WriteLine(reader["BlogId"] + " - " + reader["BlogTitle"] + " - " + reader["BlogContent"]);
}



// foreach (DataRow dr in dt.Rows)
// {
//     Console.WriteLine(dr["BlogId"]);
//     Console.WriteLine(dr["BlogTitle"]);
//     Console.WriteLine(dr["BlogContent"]);
//     // Console.WriteLine(dr["DeleteFlag"]);
// }

Console.WriteLine("Connection closing...");
connection.Close();
Console.WriteLine("Connection closed.");

// DataSet
// DataTable
// DataRow
// DataRow
// DataColumn

// foreach (DataRow dr in dt.Rows)
// {
//     Console.WriteLine(dr["BlogId"]);
//     Console.WriteLine(dr["BlogTitle"]);
//     Console.WriteLine(dr["BlogContent"]);
//    // Console.WriteLine(dr["DeleteFlag"]);
// }

Console.ReadKey();