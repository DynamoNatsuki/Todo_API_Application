using System.Data.SqlClient;
using Todo_API_application.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Todo_API_application
{
    public class DBConnection
    {
        private SqlCommand GetSqlCommand()
        {
            string connectionString = "Data Source=localhost;Initial Catalog=Todo;Integrated Security=True;Encrypt=False";
            SqlConnection conn = new SqlConnection(connectionString);
            conn.Open();

            SqlCommand cmd = conn.CreateCommand();

            cmd.CommandType = System.Data.CommandType.Text;

            return cmd;
        }

        public List<Todo> GetAllTodos()
        {
            List<Todo> todos = new List<Todo>();

            var cmd = GetSqlCommand();

            cmd.CommandText = "SELECT * FROM Todo";

            var reader = cmd .ExecuteReader();

            while (reader.Read())
            {
                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    // Convert SQL date to C# DateOnly
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                };
                todos.Add(todo);
            }
            return todos;
        }

        public Todo GetTodoById(int id)
        {
            var cmd = GetSqlCommand ();
            cmd.CommandText = "SELECT * FROM Todo WHERE Id = @id";
            cmd.Parameters.AddWithValue("id", id);

            var reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var todo = new Todo()
                {
                    Id = int.Parse(reader["Id"].ToString()),
                    Title = reader["Title"].ToString(),
                    // Convert SQL date to C# DateOnly
                    Date = reader.GetDateTime(reader.GetOrdinal("Date")),
                };

                return todo;
            }
            return null;
        }

        public void SaveTodo(Todo todo)
        {
            var cmd = GetSqlCommand ();
            cmd.CommandText = "INSERT INTO Todo (Title, Date) VALUES (@title, @date)";

            cmd.Parameters.AddWithValue ("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);

            cmd.ExecuteNonQuery();
        }

        public void UpdateTodoById(int id, Todo todo)
        {
            var cmd = GetSqlCommand();
            cmd.CommandText = "UPDATE Todo SET Title = @title, Date = @date WHERE Id = @id";

            cmd.Parameters.AddWithValue("title", todo.Title);
            cmd.Parameters.AddWithValue("date", todo.Date);
            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }

        public void DeleteTodoById(int id)
        {
            var cmd = GetSqlCommand ();
            cmd.CommandText = "DELETE FROM Todo WHERE ID = @id";

            cmd.Parameters.AddWithValue("id", id);

            cmd.ExecuteNonQuery();
        }
    }
}
