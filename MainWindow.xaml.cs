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
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CSE3055.Windows;
using System.Configuration;
using System.Data;
using CSE3055.Models;

namespace CSE3055
{
    /// <summary>
    /// MainWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MainWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        User user;
        ObservableCollection<User> users;

        private Windows.Window1 window1;
        public MainWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        public MainWindow(Windows.Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        private void Submit_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);

            users = new ObservableCollection<User>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM USERS";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                user = new User();
                user.UserID = (string)reader["userid"];
                user.Password = (string)reader["password"];
                users.Add(user);
            }
            String username = textBoxUsername.Text;
            String pass = passwordBox.Password;
             
            foreach(var kullanici in users)
            {
                if(kullanici.UserID.Equals(username) && kullanici.Password.Equals(pass))
                {
                    this.Hide();
                    Windows.Window1 window1 = new Windows.Window1(this);
                    window1.Show();
                }
            }

        }

        private void Register_Click(object sender, RoutedEventArgs e)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            command = new SqlCommand();
            command.Connection = connection;

            command.CommandText = "INSERT INTO USERS(userid, password) VALUES (@userid, @pass)";
            command.Parameters.AddWithValue("@userid", textBoxUsername.Text);
            command.Parameters.AddWithValue("@pass", passwordBox.Password);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();

        }
    }
}
