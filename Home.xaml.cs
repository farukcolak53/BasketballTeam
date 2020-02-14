using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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

namespace CSE3055.Windows
{
    /// <summary>
    /// Window1.xaml etkileşim mantığı
    /// </summary>
    public partial class Window1 : Window
    {
        private MainWindow mainWindow;
        private PlayerWindow playerWindow;
        private TeamWindow teamWindow;
        private CityWindow cityWindow;
        private StaffWindow staffWindow;
        private GymWindow gymWindow;
        private SponsorWindow sponsorWindow;
        private CompetitionWindow competitionWindow;
        private BasketballMatchWindow basketballMatchWindow;
        private MatchStatisticWindow matchStatisticWindow;

        public Window1(MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
        }

        public Window1(PlayerWindow playerWindow)
        {
            InitializeComponent();
            this.playerWindow = playerWindow;
        }

        public Window1(TeamWindow teamWindow)
        {
            InitializeComponent();
            this.teamWindow = teamWindow;
        }

        public Window1(CityWindow cityWindow)
        {
            InitializeComponent();
            this.cityWindow = cityWindow;
        }

        public Window1(StaffWindow staffWindow)
        {
            InitializeComponent();
            this.staffWindow = staffWindow;
        }

        public Window1(GymWindow gymWindow)
        {
            InitializeComponent();
            this.gymWindow = gymWindow;
        }

        public Window1(SponsorWindow sponsorWindow)
        {
            InitializeComponent();
            this.sponsorWindow = sponsorWindow;
        }

        public Window1(CompetitionWindow competitionWindow)
        {
            InitializeComponent();
            this.competitionWindow = competitionWindow;
        }

        public Window1(BasketballMatchWindow basketballMatchWindow)
        {
            InitializeComponent();
            this.basketballMatchWindow = basketballMatchWindow;
        }

        public Window1(MatchStatisticWindow matchStatisticWindow)
        {
            InitializeComponent();
            this.matchStatisticWindow = matchStatisticWindow;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MainWindow mainWindow = new MainWindow(this);
            mainWindow.Show();
        }

        private void Team_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            TeamWindow teamWindow= new TeamWindow(this);
            teamWindow.Show();
        }

        private void Player_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            PlayerWindow playerWindow = new PlayerWindow(this);
            playerWindow.Show();
        }

        private void City_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CityWindow cityWindow = new CityWindow(this);
            cityWindow.Show();
        }

        private void Staff_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            StaffWindow staffWindow = new StaffWindow(this);
            staffWindow.Show();
        }

        private void Gym_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            GymWindow gymWindow = new GymWindow(this);
            gymWindow.Show();
        }

        private void Sponsor_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            SponsorWindow sponsorWindow = new SponsorWindow(this);
            sponsorWindow.Show();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
            CompetitionWindow competitionWindow = new CompetitionWindow(this);
            competitionWindow.Show();
        }

        private void Match_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            BasketballMatchWindow basketballMatchWindow= new BasketballMatchWindow(this);
            basketballMatchWindow.Show();
        }

        private void Stats_Clicked(object sender, RoutedEventArgs e)
        {
            this.Hide();
            MatchStatisticWindow matchStatisticWindow= new MatchStatisticWindow(this);
            matchStatisticWindow.Show();
        }
    }
}
