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

namespace CSE3055
{
    /// <summary>
    /// SponsorWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class SponsorWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Sponsor sponsor;
        ObservableCollection<Sponsor> sponsors;

        private Window1 window1;

        public SponsorWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public SponsorWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        public void allSponsors()
        {
            sponsors = new ObservableCollection<Sponsor>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Sponsor";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                sponsor = new Sponsor();
                sponsor.SponsorID = (int)reader["sponsorID"];
                sponsor.SponsorName = (string)reader["sponsorName"];
                sponsor.CeoName = (string)reader["ceoName"];
                sponsor.Headquarter= (string)reader["headQuarter"];
                sponsors.Add(sponsor);
            }
            connection.Close();
            listSponsors.ItemsSource = sponsors;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allSponsors();
        }

        private void listSponsors_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Sponsor secilen = new Sponsor();
            secilen = (Sponsor)listSponsors.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Sponsor> filtre = sponsors.Where(x => x.SponsorName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listSponsors.ItemsSource = filtre;
            }
            catch
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Sponsor (sponsorName, sponsorID, ceoName, headQuarter)" +
                "VALUES (@sponsorName, @sponsorID, @ceoName,  @headQuarter)";
            command.Parameters.AddWithValue("@sponsorName", txtSponsorName.Text);
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@ceoName", txtCeoName.Text);
            command.Parameters.AddWithValue("@headQuater", txtHeadquarter.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allSponsors();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Sponsor WHERE SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allSponsors();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Sponsor SET SponsorName=@sponsorName,CeoName=@ceoName,Headquarter=@headQuarter" +
                "WHERE SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@sponsorName", txtSponsorName.Text);
            command.Parameters.AddWithValue("@ceoName", txtCeoName.Text);
            command.Parameters.AddWithValue("@headQuarter", txtHeadquarter.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allSponsors();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allSponsors();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
