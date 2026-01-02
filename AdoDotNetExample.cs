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
    
    
    public void Create()
    {
        
        Console.WriteLine("Blog Title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Blog Author: ");
        string author = Console.ReadLine();

        Console.WriteLine("Blog Content: ");
        string content = Console.ReadLine();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        // string queryInsert = $@"INSERT INTO [dbo].[Tbl_Blogs]
        //                         (
        //                          [BlogTitle],
        //                          [BlogAuthor],
        //                          [BlogContent],
        //                          [DeleteFlag])
        //                      VALUES 
        //                           ('{title}',
        //                          '{author}',
        //                          '{content}',
        //                          0)";

        string query = $@"INSERT INTO [dbo].[Tbl_Blogs]
                        (
                         [BlogTitle],
                         [BlogAuthor],
                         [BlogContent],
                         [DeleteFlag])
                     VALUES 
                          (@BlogTitle, @BlogAuthor, @BlogContent,0)";

        SqlCommand cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);

        // SqlDataAdapter adapter = new SqlDataAdapter(cmd2);
        // DataTable dt = new DataTable();
        // adapter.Fill(dt);

        int result = cmd.ExecuteNonQuery();

        connection.Close();

        // if (result == 1)
        // {
        //     Console.WriteLine("Saving Successful.");
        // }
        // else
        // {
        //     Console.WriteLine("Saving Failed.");
        // }

        Console.WriteLine(result == 1 ? "Saving Successful."  : "Saving Failed.");

    }
    
    public void Edit()
    {
        
        SqlConnection connection = new SqlConnection(_connectionString);

        connection.Open();

        Console.Write("Enter Blog id to edit : ");
        string id = Console.ReadLine();

        string query = $@"SELECT
                 [BlogId],
                 [BlogTitle],
                 [BlogContent],
                 [DeleteFlag]
           FROM [dbo].[Tbl_Blogs] where BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        
        SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
        DataTable  dataTable = new DataTable();
        dataAdapter.Fill(dataTable);

        if (dataTable.Rows.Count == 0)
        {
            Console.WriteLine("Blog not found.");
            return;
        }

        foreach (DataRow dr in dataTable.Rows)
        {
            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogContent"]);
            // Console.WriteLine(dr["DeleteFlag"]);
        }

        connection.Close();
        
    }
    
    public void Update()
    {
        Console.Write("Enter Blog id to update : ");
        string id = Console.ReadLine();
        
        Console.WriteLine("Blog Title: ");
        string title = Console.ReadLine();

        Console.WriteLine("Blog Author: ");
        string author = Console.ReadLine();

        Console.WriteLine("Blog Content: ");
        string content = Console.ReadLine();

        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();
        

        string query = $@"UPDATE [dbo].[Tbl_Blogs]
                     SET [BlogTitle] = @BlogTitle,
                         [BlogAuthor] =  @BlogAuthor,
                         [BlogContent] =  @BlogContent,
                         [DeleteFlag] = 0
                     WHERE 
                          BlogId = @BlogId";

        SqlCommand cmd = new SqlCommand(query, connection);

        cmd.Parameters.AddWithValue("@BlogTitle", title);
        cmd.Parameters.AddWithValue("@BlogAuthor", author);
        cmd.Parameters.AddWithValue("@BlogContent", content);

        int result = cmd.ExecuteNonQuery();

        connection.Close();

        Console.WriteLine(result == 1 ? "Updating Successful."  : "Updating Failed.");

    }

    public void Delete()
    {
        Console.Write("Enter Blog id to delete : ");
        string id = Console.ReadLine();
        
        SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open(); 
        string query = $@"DELETE FROM [dbo].[Tbl_Blogs] WHERE [BlogId] = @BlogId";
        SqlCommand cmd = new SqlCommand(query, connection);
        cmd.Parameters.AddWithValue("@BlogId", id);
        int result = cmd.ExecuteNonQuery();

        connection.Close();
        Console.WriteLine(result == 1 ? "Deleted Successful."  : "Deleted Failed.");
        
    }

}