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
    /// TeamWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class TeamWindow : Window
    {

        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Team team;
        ObservableCollection<Team> teams;

        private Window1 window1;
        private TeamSponsorshipWindow teamSponsorshipWindow;

        public TeamWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        public TeamWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        public TeamWindow(TeamSponsorshipWindow teamSponsorshipWindow)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.teamSponsorshipWindow = teamSponsorshipWindow;
        }

        public void allTeams()
        {
            teams = new ObservableCollection<Team>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Team";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                team = new Team();
                team.TeamID = (int)reader["teamID"];
                team.TeamName = (string)reader["teamName"];
                team.TeamMascot = (string)reader["teamMascot"];
                team.FirstColor = (string)reader["firstColor"];
                team.SecondColor = (string)reader["secondColor"];
                team.CityID = (int)reader["cityID"];
                teams.Add(team);
            }
            connection.Close();
            listTeams.ItemsSource = teams;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allTeams();
        }
        private void listTeams_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Team secilen = new Team();
            secilen = (Team)listTeams.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Team> filtre = teams.Where(x => x.TeamName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listTeams.ItemsSource = filtre;
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
            command.CommandText = "INSERT INTO Team (teamName, teamMascot, firstColor, secondColor, cityID)" +
                "VALUES (@teamName, @teamMascot, @firstColor,  @secondColor, @cityID)";
            command.Parameters.AddWithValue("@teamName", txtTeamName.Text);
            command.Parameters.AddWithValue("@teamMascot", txtTeamMascot.Text);
            command.Parameters.AddWithValue("@firstColor", txtFirstColor.Text);
            command.Parameters.AddWithValue("@secondColor", txtSecondColor.Text);
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeams();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Team WHERE TeamID=@teamID";
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeams();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Team SET TeamName=@teamName,TeamMascot=@teamMascot," +
                "FirstColor=@firstColor,SecondColor=@secondColor,CityID=@cityID WHERE TeamID=@teamID";
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@teamName", txtTeamName.Text);
            command.Parameters.AddWithValue("@teamMascot", txtTeamMascot.Text);
            command.Parameters.AddWithValue("@firstColor", txtFirstColor.Text);
            command.Parameters.AddWithValue("@secondColor", txtSecondColor.Text);
            command.Parameters.AddWithValue("@cityID", txtCityID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allTeams();
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allTeams();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TeamSponsorshipWindow teamSponsorshipWindow = new TeamSponsorshipWindow(this);
            teamSponsorshipWindow.Show();
        }
    }
}

        
