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
    /// PlayerWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class PlayerWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Player player;
        ObservableCollection<Player> players;

        private Window1 window1;
        private PlayerSponsorshipWindow playerSponsorshipWindow;

        public PlayerWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        public PlayerWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }

        public PlayerWindow(PlayerSponsorshipWindow playerSponsorshipWindow)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.playerSponsorshipWindow = playerSponsorshipWindow;
        }

        public void allPlayers()
        {
            players = new ObservableCollection<Player>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Player";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                player = new Player();
                player.PlayerID = (int)reader["playerID"];
                player.PlayerName = (string)reader["playerName"];
                player.TeamID = (int)reader["teamID"];
                player.Position = (string)reader["position"];
                player.Minutes = (int)reader["minutes"];
                player.Points = (int)reader["points"];
                player.Rebounds = (int)reader["rebounds"];
                player.Assists = (int)reader["asists"];
                player.Salary = (int)reader["salary"];
                DateTime date = (DateTime)reader["playerBirthDate"];
                player.PlayerBirthDate = date.Year.ToString();
                player.ContractPeriod = (int)reader["contractPeriod"];
                player.Height = (int)reader["height"];
                players.Add(player);
            }
            connection.Close();
            listPlayers.ItemsSource = players;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allPlayers();
        }
        private void listPlayers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Player secilen = new Player();
            secilen = (Player)listPlayers.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Player> filtre = players.Where(x => x.PlayerName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listPlayers.ItemsSource = filtre;
            }
            catch
            {

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Player (playerName, teamID, position, minutes, points, rebounds, asists, contractPeriod, salary, height, playerBirthDate) " +
                "VALUES (@playerName, @teamID, @position,  @minutes, @points, @rebounds, @asists, @contractPeriod, @salary, @height, @playerBirthDate)";
            command.Parameters.AddWithValue("@playerName", txtName.Text);
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            command.Parameters.AddWithValue("@position", txtPosition.Text);
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@minutes", txtMinutes.Text);
            command.Parameters.AddWithValue("@points", txtPoints.Text);
            command.Parameters.AddWithValue("@rebounds", txtRebounds.Text);
            command.Parameters.AddWithValue("@asists", txtAssists.Text);
            command.Parameters.AddWithValue("@contractPeriod", txtContract.Text);
            command.Parameters.AddWithValue("@salary", txtSalary.Text);
            command.Parameters.AddWithValue("@height", txtHeight.Text);
            command.Parameters.AddWithValue("@playerBirthDate", txtBirthDate.Text);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayers();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Player WHERE PlayerID=@playerID";
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayers();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Player SET PlayerName=@playerName,TeamID=@teamID,Position=@position," +
                "PlayerBirthDate=@playerBirthDate,Minutes=@minutes,Points=@points, Rebounds=@rebounds, Asists=@asists, ContractPeriod=@contractPeriod," +
                "Salary=@salary,Height=@height WHERE PlayerID=@playerID";
            command.Parameters.AddWithValue("@playerID", txtPlayerID.Text);
            command.Parameters.AddWithValue("@playerName", txtName.Text);
            command.Parameters.AddWithValue("@position", txtPosition.Text);
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@minutes", txtMinutes.Text);
            command.Parameters.AddWithValue("@points", txtPoints.Text);
            command.Parameters.AddWithValue("@rebounds", txtRebounds.Text);
            command.Parameters.AddWithValue("@asists", txtAssists.Text);
            command.Parameters.AddWithValue("@contractPeriod", txtContract.Text);
            command.Parameters.AddWithValue("@salary", txtSalary.Text);
            command.Parameters.AddWithValue("@height", txtHeight.Text);
            command.Parameters.AddWithValue("@playerBirthDate", txtBirthDate.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allPlayers();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allPlayers();
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
            PlayerSponsorshipWindow playerSponsorshipWindow = new PlayerSponsorshipWindow(this);
            playerSponsorshipWindow.Show();
        }
    }
}
