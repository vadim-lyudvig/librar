using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

namespace LibraryApp
{
    /// <summary>
    /// Логика взаимодействия для RegistrationsWindow.xaml
    /// </summary>
    public partial class RegistrationsWindow : Window
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlConnection connection;
        public RegistrationsWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            var sql = "SELECT Books.name as bookname, Readers.name as readername, [from], [to], COUNT(*) as [count] FROM Registrations\r\nLEFT JOIN Readers ON Readers.id = Registrations.readerId\r\nLEFT JOIN Books ON Books.id = Registrations.bookId\r\nGROUP BY Books.name, Readers.name, [from], [to], Registrations.readerId";
            SqlCommand command = new(sql, connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }

        private void BT_Search_Click(object sender, RoutedEventArgs e)
        {
            var sql = $"SELECT Books.name as bookname, Readers.name as readername, [from], [to], COUNT(*) as [count] FROM Registrations\r\nLEFT JOIN Readers ON Readers.id = Registrations.readerId\r\nLEFT JOIN Books ON Books.id = Registrations.bookId\r\nGROUP BY Books.name, Readers.name, [from], [to], Registrations.readerId \r\nHAVING Readers.name LIKE '%{TB_SearchByName.Text}%'";
            SqlCommand command = new(sql, connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }

        private void BT_GiveBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var sql1 = $"SELECT COUNT(*) FROM Registrations WHERE readerId = '{TB_GiveReaderID.Text}'";
                ;
                SqlCommand check = new(sql1, connection);
                // Выполнение запроса и получение результата в виде объекта SQLiteDataReader
                SqlDataReader reader = check.ExecuteReader();
                reader.Read();
                int count = reader.GetInt32(0);
                Trace.WriteLine($"Book Count: {count}");
                if (count >=5)
                {
                    MessageBox.Show("Книга не может быть выдана. Превышен лимит книг 5");
                    return;
                }
                reader.Close();

                Trace.WriteLine("BT_GiveBook_Click");
                var bookId = TB_GiveBookID.Text;
                var readerId = TB_GiveReaderID.Text;
                var from = TB_GiveDateFrom.Text;
                var to = TB_GiveDateTo.Text;
                var sql = $"INSERT INTO Registrations (bookId, readerId, [from], [to]) VALUES ('{bookId}', '{readerId}', '{from}', '{to}')";
                SqlCommand command = new(sql, connection);
                command.ExecuteNonQuery();
                readersGrid.UpdateLayout();
                MessageBox.Show("Книга выдана");
                Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show("Произошла ошибка при выдачи книги");
            }
        }

        private void BT_ReturnBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Trace.WriteLine("BT_ReturnBook_Click");
                var sql = $"DELETE FROM Registrations WHERE bookId = {TB_ReturnBook.Text}";
                SqlCommand command = new(sql, connection);
                command.ExecuteNonQuery();
                readersGrid.UpdateLayout();
                MessageBox.Show("Книга вернута");
                Close();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show("Произошла ошибка при возврате книги");
            }
        }
    }
}
