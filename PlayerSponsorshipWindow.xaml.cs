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
using CSE3055.Windows;
using System.Configuration;
using System.Data;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using CSE3055.Models;

namespace CSE3055
{
    /// <summary>
    /// PlayerSponsorshipWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class PlayerSponsorshipWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        TeamSponsorship teamSponsorship;
        ObservableCollection<PlayerSponsorship> playerSponsorships;

        private Window1 window1;
        private PlayerWindow playerWindow;

        public PlayerSponsorshipWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public PlayerSponsorshipWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        public PlayerSponsorshipWindow(PlayerWindow playerWindow)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.playerWindow = playerWindow;
        }

        public void allPlayerSponsorships()
        {
            playerSponsorships = new ObservableCollection<PlayerSponsorship>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Sponsor.sponsorName, Sponsor.sponsorID, PlayerSponsorship.sponsorshipContractPeriod, PlayerSponsorship.sponsorshipPayment, Player.playerName, Player.playerID " +
                                "FROM PlayerSponsorship INNER JOIN  Sponsor ON PlayerSponsorship.sponsorID = Sponsor.sponsorID " +
                                "INNER JOIN  Player ON PlayerSponsorship.playerID = Player.playerID ORDER BY Sponsor.sponsorID ASC";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                PlayerSponsorship playerSponsorship= new PlayerSponsorship();
                playerSponsorship.PlayerID = (int)reader["playerID"];
                playerSponsorship.PlayerName = (string)reader["playerName"];
                playerSponsorship.SponsorID = (int)reader["sponsorID"];
                playerSponsorship.SponsorName = (string)reader["sponsorName"];
                playerSponsorship.SponsorshipContractPeriod = (int)reader["sponsorshipContractPeriod"];
                playerSponsorship.SponsorshipPayment = (int)reader["sponsorshipPayment"];
                playerSponsorships.Add(playerSponsorship);
            }
            connection.Close();
            listPlayerSponsorships.ItemsSource = playerSponsorships;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allPlayerSponsorships();
        }
        private void listPlayerSponsorships_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PlayerSponsorship secilen = new PlayerSponsorship();
            secilen = (PlayerSponsorship)listPlayerSponsorships.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<PlayerSponsorship> filtre = playerSponsorships.Where(x => x.PlayerName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listPlayerSponsorships.ItemsSource = filtre;
            }
            catch
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO PlayerSponsorship (sponsorID, playerID, sponsorshipContractPeriod, sponsorshipPayment)" +
                "VALUES (@sponsorID, @playerID, @sponsorshipContractPeriod,  @sponsorshipPayment)";
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            command.Parameters.AddWithValue("@sponsorshipContractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@sponsorshipPayment", txtPayment.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayerSponsorships();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM PlayerSponsorship WHERE PlayerID=@playerID AND SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayerSponsorships();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE PlayerSponsorship SET PlayerID=@playerID, SponsorID=@sponsorID," +
                "SponsorshipPayment=@sponsorshipPayment,SponsorshipContractPeriod=@sponsorshipContractPeriod WHERE PlayerID=@playerID AND SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@sponsorshipContractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@sponsorshipPayment", txtPayment.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayerSponsorships();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allPlayerSponsorships();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PlayerWindow playerWindow = new PlayerWindow(this);
            playerWindow.Show();
        }
    }
}
