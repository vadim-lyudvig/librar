using System;
using System.Collections.Generic;
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
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace LibraryApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SqlConnection connection = new("Data Source=.\\SQLEXPRESS;Initial Catalog=libraryDB;Integrated Security=True");
        public MainWindow()
        {
            connection.Open();
            InitializeComponent();
        }

        private void BT_Books_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("BT_GiveBook_Click");
            new BooksWindow(connection).Show();
        }

        private void BT_Readers_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("BT_Readers_Click");
            new ReadersWindow(connection).Show();
        }

        private void BT_Registrations_Click(object sender, RoutedEventArgs e)
        {
            Trace.WriteLine("BT_Registrations_Click");
            new RegistrationsWindow(connection).Show();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
    }
}
