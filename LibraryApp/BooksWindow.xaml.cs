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
        public BooksWindow(SqlConnection connection)
        {
            InitializeComponent();
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
    }
}
