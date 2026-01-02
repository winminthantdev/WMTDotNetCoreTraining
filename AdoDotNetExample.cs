using System.Data;
using System.Data.SqlClient;

namespace WMTDotNetTraning.ConsoleApp;

public class AdoDotNetExample
{
    private readonly string _connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=Temporary123;";
        
    public void Read()
    {
        
        Console.WriteLine("Connection string : " + _connectionString);
        SqlConnection connection = new SqlConnection(_connectionString);

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


    }
    
}