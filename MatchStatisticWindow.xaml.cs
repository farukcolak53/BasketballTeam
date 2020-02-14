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
    /// MatchStatisticWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class MatchStatisticWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        MatchStatistics matchStatistics;
        ObservableCollection<MatchStatistics> matchStatisticsList;

        private Window1 window1;

        public MatchStatisticWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public MatchStatisticWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allMatchStatistics()
        {
            matchStatisticsList = new ObservableCollection<MatchStatistics>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT  MatchStatistics.matchID,MatchStatistics.matchStatisticID,hTeamName, hPoints, hAsists,hRebounds,hTurnovers,h2P,h3P,hFP,hFG,aTeamName, aPoints, aAsists,aRebounds,aTurnovers,a2P,a3P,aFP,aFG " +
                "FROM MatchStatistics " +
                "INNER JOIN HomeTeamMatchStatistic ON MatchStatistics.matchStatisticID = HomeTeamMatchStatistic.matchStatisticID " + 
                "INNER JOIN HomeTeamInfo ON HomeTeamMatchStatistic.hTeamID = HomeTeamInfo.hTeamID " + 
                "INNER JOIN AwayTeamMatchStatistic ON MatchStatistics.matchStatisticID = AwayTeamMatchStatistic.matchStatisticID " +
                "INNER JOIN AwayTeamInfo ON AwayTeamMatchStatistic.aTeamID = AwayTeamInfo.aTeamID";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                matchStatistics = new MatchStatistics();
                matchStatistics.MatchID = (int)reader["matchID"];
                matchStatistics.MatchStatisticID= (int)reader["matchStatisticID"];
                matchStatistics.HomeTeamName = (string)reader["hTeamName"];
                matchStatistics.HomeTeamPoints = (int)reader["hPoints"];
                matchStatistics.HomeTeamRebounds = (int)reader["hRebounds"];
                matchStatistics.HomeTeamAssists = (int)reader["hAsists"];
                matchStatistics.HomeTeamTurnovers = (int)reader["hTurnovers"];
                matchStatistics.HomeTeamFG = (string)reader["hFG"];
                matchStatistics.HomeTeam3P = (string)reader["h3P"];
                matchStatistics.HomeTeam2P = (string)reader["h2P"];
                matchStatistics.HomeTeamFP = (string)reader["hFP"];

                matchStatistics.AwayTeamName = (string)reader["aTeamName"];
                matchStatistics.AwayTeamPoints = (int)reader["aPoints"];
                matchStatistics.AwayTeamRebounds = (int)reader["aRebounds"];
                matchStatistics.AwayTeamAssists = (int)reader["aAsists"];
                matchStatistics.AwayTeamTurnovers = (int)reader["aTurnovers"];
                matchStatistics.AwayTeamFG = (string)reader["aFG"];
                matchStatistics.AwayTeam3P = (string)reader["a3P"];
                matchStatistics.AwayTeam2P = (string)reader["a2P"];
                matchStatistics.AwayTeamFP = (string)reader["aFP"];

                matchStatisticsList.Add(matchStatistics);
            }
            connection.Close();
            listMatchStats.ItemsSource = matchStatisticsList;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allMatchStatistics();
        }
        private void listMatchStats_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            MatchStatistics secilen = new MatchStatistics();
            secilen = (MatchStatistics)listMatchStats.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<MatchStatistics> filtre = matchStatisticsList.Where(x => x.AwayTeamName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listMatchStats.ItemsSource = filtre;
            }
            catch
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allMatchStatistics();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
