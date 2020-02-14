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
    /// TeamSponsorshipWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class TeamSponsorshipWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        TeamSponsorship teamSponsorship;
        ObservableCollection<TeamSponsorship> teamSponsorships;

        private Window1 window1;
        private TeamWindow teamWindow;

        public TeamSponsorshipWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public TeamSponsorshipWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        public TeamSponsorshipWindow(TeamWindow teamWindow)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.teamWindow = teamWindow;
        }

        public void allTeamSponsorships()
        {
            teamSponsorships = new ObservableCollection<TeamSponsorship>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT Sponsor.sponsorName, Sponsor.sponsorID, TeamSponsorship.sponsorshipContractPeriod, TeamSponsorship.sponsorshipPayment, Team.teamName, Team.teamID " +
                                "FROM TeamSponsorship INNER JOIN  Sponsor ON TeamSponsorship.sponsorID = Sponsor.sponsorID " +
                                "INNER JOIN  Team ON TeamSponsorship.teamID = Team.teamID ORDER BY Sponsor.sponsorID ASC";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                teamSponsorship = new TeamSponsorship();
                teamSponsorship.TeamID = (int)reader["teamID"];
                teamSponsorship.TeamName = (string)reader["teamName"];
                teamSponsorship.SponsorID = (int)reader["sponsorID"];
                teamSponsorship.SponsorName = (string)reader["sponsorName"];
                teamSponsorship.SponsorshipContractPeriod = (int)reader["sponsorshipContractPeriod"];
                Console.WriteLine(teamSponsorship.SponsorshipContractPeriod);
                teamSponsorship.SponsorshipPayment = (int)reader["sponsorshipPayment"];
                teamSponsorships.Add(teamSponsorship);
            }
            connection.Close();
            listTeamSponsorships.ItemsSource = teamSponsorships;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allTeamSponsorships();
        }
        private void listTeamSponsorships_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TeamSponsorship secilen = new TeamSponsorship();
            secilen = (TeamSponsorship)listTeamSponsorships.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<TeamSponsorship> filtre = teamSponsorships.Where(x => x.TeamName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listTeamSponsorships.ItemsSource = filtre;
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
            command.CommandText = "INSERT INTO TeamSponsorship (sponsorID, teamID, sponsorshipContractPeriod, sponsorshipPayment)" +
                "VALUES (@sponsorID, @teamID, @sponsorshipContractPeriod,  @sponsorshipPayment)";
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@playerID", txtTeamID.Text);
            command.Parameters.AddWithValue("@sponsorshipContractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@sponsorshipPayment", txtPayment.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeamSponsorships();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM TeamSponsorship WHERE TeamID=@teamID AND SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeamSponsorships();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE TeamSponsorship SET TeamID=@teamID, SponsorID=@sponsorID," +
                "SponsorshipPayment=@sponsorshipPayment,SponsorshipContractPeriod=@sponsorshipContractPeriod WHERE TeamID=@teamID AND SponsorID=@sponsorID";
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@sponsorID", txtSponsorID.Text);
            command.Parameters.AddWithValue("@sponsorshipContractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@sponsorshipPayment", txtPayment.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeamSponsorships();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allTeamSponsorships();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TeamWindow teamWindow = new TeamWindow(this);
            teamWindow.Show();
        }
    }
}
