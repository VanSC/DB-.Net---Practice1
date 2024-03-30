using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Lab3vane
{
    /// <summary>
    /// Lógica de interacción para Conecction.xaml
    /// </summary>
    public partial class Conecction : Window
    {
        string connectionString = "Data Source=LAB1504-18\\SQLEXPRESS;Initial Catalog=dbvane;User Id=vanne;Password=123456";


        public Conecction()
        {
            InitializeComponent();
        }

        private void DataReader_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Student> students = new List<Student>();
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32("StudentId");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");

                    students.Add(new Student { StudentId = studentId, FirstName = firstName, LastName = lastName });

                }
                connection.Close();

                studentData.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }

        private void DataTable_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                //Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                //Intermediario
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();

                adapter.Fill(dataTable);

                connection.Close();

                //DESCONECTADA A LA BASE DE DATOS
                //Mostrar la información
                studentData.ItemsSource = dataTable.DefaultView;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                List<Student> students = new List<Student>();

                string txtFirstName = searchTextBox.Text;

                //Comandos de TRANSACT SQL
                // Consulta SQL con parámetro
                SqlCommand command = new SqlCommand($"SELECT * FROM Students WHERE FirstName LIKE '{txtFirstName}'", connection);


                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32("StudentId");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");

                    students.Add(new Student { StudentId = studentId, FirstName = firstName, LastName = lastName });

                }
                connection.Close();

                studentData.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }
    }
}
