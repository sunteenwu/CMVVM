using MySql.Data.MySqlClient;
using MySQLBlog.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySQLBlog.ViewModel
{
    public class TodoViewModel
    {
        private static TodoViewModel _todoViewModel = new TodoViewModel();
        private ObservableCollection<Todo> _allToDos = new ObservableCollection<Todo>();
        string connectionString;

        public ObservableCollection<Todo> AllTodos
        {
            get
            {
                return _todoViewModel._allToDos;
            }
        }

        public IEnumerable<Todo> GetTodos()
        {
            try
            {
                string server = "127.0.0.1";
                string database = "sakila";
                string user = "root";
                string pswd = "!QAZ2wsx";
                connectionString = "Server = " + server + ";database = " + database + ";uid = " + user + ";password = " + pswd + ";SslMode=None;";
                using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand getCommand = connection.CreateCommand();
                    getCommand.CommandText = "SELECT whatToDO FROM todo";
                    using (MySqlDataReader reader = getCommand.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            _todoViewModel._allToDos.Add(new Todo(reader.GetString("whatToDO")));
                        }
                    }
                }
            }
            catch (MySqlException sqlex)
            {
                // Handle it :)
            }
            return _todoViewModel.AllTodos;
        }

        public bool InsertNewTodo(string what)
         {
            Todo newTodo = new Todo(what);
            // Insert to the collection and update DB
            try
            {
               using (MySqlConnection connection = new MySqlConnection(connectionString))
                {
                    connection.Open();
                    MySqlCommand insertCommand = connection.CreateCommand();
                    insertCommand.CommandText = "INSERT INTO todo(whatToDO)VALUES(@whatToDO)";
                    insertCommand.Parameters.AddWithValue("@whatToDO", newTodo.whatToDO);
                    insertCommand.ExecuteNonQuery();
                    _todoViewModel._allToDos.Add(newTodo);
                    return true;

                }
            }
            catch (MySqlException sqlex)
            {
                // Don't forget to handle it
                return false;
            }

        }


        public TodoViewModel()
        {
            // Establish the connection

        }
    }
}
