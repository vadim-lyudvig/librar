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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryApp
{
    /// <summary>
    /// Логика взаимодействия для GiveBookWindow.xaml
    /// </summary>
    public partial class ReadersWindow : Window
    {
        private SqlDataAdapter adapter;
        private DataTable table;
        private SqlConnection connection;
        public ReadersWindow(SqlConnection connection)
        {
            InitializeComponent();
            this.connection = connection;
            SqlCommand command = new ("SELECT * FROM Readers", connection);
            adapter = new (command);
            table = new ();
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

        private void BT_ReaderFindID_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new($"SELECT * FROM Readers WHERE passport LIKE '%{TB_ReaderFindID.Text}%'", connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }

        private void BT_ReaderFind_Click(object sender, RoutedEventArgs e)
        {
            SqlCommand command = new($"SELECT * FROM Readers WHERE name LIKE '%{TB_ReaderFind.Text}%'", connection);
            adapter = new(command);
            table = new();
            adapter.Fill(table);
            readersGrid.ItemsSource = table.DefaultView;
        }
    }
}
