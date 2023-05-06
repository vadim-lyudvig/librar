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
        public RegistrationsWindow(SqlConnection connection)
        {
            InitializeComponent();
            var sql = "SELECT Books.name as bookname, Readers.name as readername, [from], [to], COUNT(*) as [count] FROM Registrations\r\nLEFT JOIN Readers ON Readers.id = Registrations.readerId\r\nLEFT JOIN Books ON Books.id = Registrations.bookId\r\nGROUP BY Books.name, Readers.name, [from], [to], Registrations.readerId";
            SqlCommand command = new(sql, connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }

        private void BT_Update_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                SqlCommandBuilder comandbuilder = new SqlCommandBuilder(adapter);
                adapter.Update(table);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.Message);
                MessageBox.Show("Произошла ошибка при обновлении даннных");
            }
        }
    }
}
