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
    /// CompetitionWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class CompetitionWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Competition competition;
        ObservableCollection<Competition> competitions;

        private Window1 window1;

        public CompetitionWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public CompetitionWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allCompetitions()
        {
            competitions = new ObservableCollection<Competition>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Competition";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                competition = new Competition();
                competition.CompetitionID = (int)reader["competitionID"];
                competition.CompetitionName = (string)reader["competitionName"];
                competition.Prize = (int)reader["prize"];
                competition.NumberOfParticipants = (int)reader["numberOfParticipants"];
                competition.LastChampion = (string)reader["lastChampion"];
                competitions.Add(competition);
            }
            connection.Close();
            listCompetitions.ItemsSource = competitions;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allCompetitions();
        }

        private void listCompetitions_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Competition secilen = new Competition();
            secilen = (Competition)listCompetitions.SelectedItem;
            grd1.DataContext = secilen;
        }
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Competition> filtre = competitions.Where(x => x.CompetitionName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listCompetitions.ItemsSource = filtre;
            }
            catch
            {

            }

        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Competition (competitionName, competitionID, prize, numberOfParticipants, lastChampion)" +
                "VALUES (@competitionName, @competitionID, @prize,  @numberOfParticipants, @lastChampion)";
            command.Parameters.AddWithValue("@competitionName", txtCompetitionName.Text);
            command.Parameters.AddWithValue("@competitionID", txtCompetitionID.Text);
            command.Parameters.AddWithValue("@prize", txtPrize.Text);
            command.Parameters.AddWithValue("@numberOfParticipants", txtNumOfParticipants.Text);
            command.Parameters.AddWithValue("@lastChampion", txtLastChampion.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCompetitions();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Competition WHERE CompetitionID=@competitionID";
            command.Parameters.AddWithValue("@competitionID", txtCompetitionID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCompetitions();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Competition SET CompetitionName=@competitionName,Prize=@prize,NumberOfParticipants=@numberOfParticipants, LastChampion=@lastChampion" +
                "WHERE CompetitionID=@competitionID";
            command.Parameters.AddWithValue("@competitionID", txtCompetitionID.Text);
            command.Parameters.AddWithValue("@competitionName", txtCompetitionName.Text);
            command.Parameters.AddWithValue("@numberOfParticipants", txtNumOfParticipants.Text);
            command.Parameters.AddWithValue("@prize", txtPrize.Text);
            command.Parameters.AddWithValue("@lastChampion", txtLastChampion.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allCompetitions();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allCompetitions();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
