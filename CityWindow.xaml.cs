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
using CSE3055.Models;
using CSE3055.Windows;
using System.Configuration;
using System.Data;
using System.Windows.Navigation;

namespace CSE3055
{
    /// <summary>
    /// CityWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class CityWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        City city;
        ObservableCollection<City> cities;

        private Window1 window1;

        public CityWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        public CityWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allCities()
        {
            cities = new ObservableCollection<City>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM City";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                city = new City();
                city.CityID = (int)reader["cityID"];
                city.CityName = (string)reader["cityName"];
                city.CityCode = (int)reader["cityCode"];
                city.Population= (int)reader["population"];
                cities.Add(city);
            }
            connection.Close();
            listCities.ItemsSource = cities;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allCities();
        }
        private void listCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            City secilen = new City();
            secilen = (City)listCities.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<City> filtre = cities.Where(x => x.CityName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listCities.ItemsSource = filtre;
            }
            catch
            {

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO City (cityName, cityCode, population" +
                "VALUES (@cityName, @cityCode, @population)";
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCode", txtCityCode.Text);
            command.Parameters.AddWithValue("@population", txtPopulation.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCities();
        }
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM City WHERE CityID=@cityID";
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCities();
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE City SET CityName=@cityName,CityCode=@cityCode,Population=@population WHERE CityID=@cityID";
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            command.Parameters.AddWithValue("@cityName", txtCityName.Text);
            command.Parameters.AddWithValue("@cityCode", txtCityCode.Text);
            command.Parameters.AddWithValue("@population", txtPopulation.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCities();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allCities();
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
