using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Diagnostics;

namespace LibraryApp
{
    /// <summary>
    /// Логика взаимодействия для ReturnBookWindow.xaml
    /// </summary>
    public partial class BooksWindow : Window
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlConnection connection;
        public BooksWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            SqlCommand command = new("SELECT * FROM Books", connection);
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

        private void BT_BookFind_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new($"SELECT * FROM Books WHERE name LIKE '%{TB_BookFind.Text}%'", connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }

        private void BT_BookFindID_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new($"SELECT * FROM Books WHERE id LIKE '%{TB_BookFindID.Text}%'", connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }
    }
}
