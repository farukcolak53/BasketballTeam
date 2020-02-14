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
    /// BasketballMatchWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class BasketballMatchWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        BasketballMatch basketballMatch;
        ObservableCollection<BasketballMatch> basketballMatches;

        private Window1 window1;

        public BasketballMatchWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public BasketballMatchWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allMatches()
        {
            basketballMatches = new ObservableCollection<BasketballMatch>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT BasketballMatch.matchID, matchDate, matchTime, hTeamName, aTeamName, matchScore FROM  AwayTeamInfo INNER JOIN " +
                  "MatchStatistics ON dbo.AwayTeamInfo.aTeamID = MatchStatistics.aTeamID INNER JOIN " +
                  "BasketballMatch ON dbo.MatchStatistics.matchID = BasketballMatch.matchID INNER JOIN " +
                  "HomeTeamInfo ON dbo.MatchStatistics.hTeamID = HomeTeamInfo.hTeamID";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                basketballMatch = new BasketballMatch();
                basketballMatch.MatchID = (int)reader["matchID"];
                DateTime date = (DateTime)reader["matchDate"];
                basketballMatch.MatchDate = date.ToShortDateString();
                basketballMatch.MatchTime = (TimeSpan)reader["matchTime"];
                basketballMatch.AwayTeamName = (string)reader["aTeamName"];
                basketballMatch.HomeTeamName = (string)reader["hTeamName"];
                basketballMatch.MatchScore = (string)reader["matchScore"];
                basketballMatches.Add(basketballMatch);
            }
            connection.Close();
            listMatches.ItemsSource = basketballMatches;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allMatches();
        }
        private void listMatches_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BasketballMatch secilen = new BasketballMatch();
            secilen = (BasketballMatch)listMatches.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<BasketballMatch> filtre = basketballMatches.Where(x => x.AwayTeamName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listMatches.ItemsSource = filtre;
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
            allMatches();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
