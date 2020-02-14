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
    /// GymWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class GymWindow : Window
    {

        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Gym gym;
        ObservableCollection<Gym> gyms;

        private Window1 window1;


        public GymWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        public GymWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allGyms()
        {
            gyms = new ObservableCollection<Gym>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Gym";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                gym = new Gym();
                gym.GymID = (int)reader["gymID"];
                gym.GymName = (string)reader["gymName"];
                gym.CityID = (int)reader["cityID"];
                gym.GymStreet= (string)reader["gymStreet"];
                gym.GymDistrict= (string)reader["gymDistrict"];
                gym.Capacity= (int)reader["capacity"];
                gym.ConstructionDate = (string)reader["constructionDate"];
                gyms.Add(gym);
            }
            connection.Close();
            listGyms.ItemsSource = gyms;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allGyms();
        }
        private void listGyms_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Gym secilen = new Gym();
            secilen = (Gym)listGyms.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Gym> filtre = gyms.Where(x => x.GymName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listGyms.ItemsSource = filtre;
            }
            catch
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Gym (gymName, cityID, gymDistrict, gymStreet, capacity, constructionDate) " +
                "VALUES (@gymName, @gymID, @gymDistrict,  @gymStreet, @capacity, @constructionDate)";
            command.Parameters.AddWithValue("@gymName", txtGymName.Text);
            command.Parameters.AddWithValue("@gymID", txtGymID.Text);
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            command.Parameters.AddWithValue("@gymDistrict", txtGymDistrict.Text);
            command.Parameters.AddWithValue("@gymStreet", txtGymStreet.Text);
            command.Parameters.AddWithValue("@capacity", txtCapacity.Text);
            command.Parameters.AddWithValue("@constructionDate", dp1.SelectedDate.Value);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allGyms();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Gym WHERE GymID=@gymID";
            command.Parameters.AddWithValue("@gymID", txtGymID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allGyms();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Gym SET GymName=@gymName,CityID=@cityID,GymStreet=@gymStreet," +
                "ConstructionDate=@constructionDate,Capacity=@capacity,GymDistrict=@gymDistrict WHERE GymID=@gymID";
            command.Parameters.AddWithValue("@gymID", txtGymID.Text);
            command.Parameters.AddWithValue("@gymName", txtGymName.Text);
            command.Parameters.AddWithValue("@gymStreet", txtGymStreet.Text);
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            command.Parameters.AddWithValue("@gymDistrict", txtGymDistrict.Text);
            command.Parameters.AddWithValue("@capacity", txtCapacity.Text);
            command.Parameters.AddWithValue("@constructionDate", dp1.SelectedDate.Value);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allGyms();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allGyms();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
