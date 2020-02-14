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
    /// StaffWindow.xaml etkileşim mantığı
    /// </summary>
    public partial class StaffWindow : Window
    {
        SqlConnection connection;
        SqlDataReader reader;
        SqlCommand command;
        Staff staff;
        ObservableCollection<Staff> staffs;

        private Window1 window1;

        public StaffWindow()
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }
        public StaffWindow(Window1 window1)
        {
            InitializeComponent();
            string ConnectionString = ConfigurationManager.ConnectionStrings["dataConnection"].ConnectionString;
            connection = new SqlConnection(ConnectionString);
            this.window1 = window1;
        }
        public void allStaffs()
        {
            staffs = new ObservableCollection<Staff>();
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "SELECT * FROM Staff";
            connection.Open();
            reader = command.ExecuteReader();
            while (reader.Read())
            {
                staff = new Staff();
                staff.StaffID = (int)reader["staffID"];
                staff.StaffName = (string)reader["staffName"];
                staff.TeamID = (int)reader["teamID"];
                staff.Salary = (int)reader["salary"];
                staff.StaffBirthDate = (int)reader["staffBirthDate"];
                staff.ContractPeriod = (int)reader["contractPeriod"];
                staffs.Add(staff);
            }
            connection.Close();
            listStaff.ItemsSource = staffs;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            allStaffs();
        }
        private void listStaff_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Staff secilen = new Staff();
            secilen = (Staff)listStaff.SelectedItem;
            grd1.DataContext = secilen;
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                List<Staff> filtre = staffs.Where(x => x.StaffName.ToLower().Contains(txtAra.Text.ToLower())).ToList();
                listStaff.ItemsSource = filtre;
            }
            catch
            {

            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "INSERT INTO Staff (staffName, teamID, contractPeriod, salary) " +
                "VALUES (@staffName, @teamID, @contractPeriod, @salary)";
            command.Parameters.AddWithValue("@staffName", txtStaffName.Text);
            command.Parameters.AddWithValue("@staffID", txtStaffID.Text);
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@contractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@salary", txtSalary.Text);
            command.Parameters.AddWithValue("@staffBirthDate", txtStaffBirthDate.Text);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allStaffs();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "DELETE FROM Staff WHERE StaffID=@staffID";
            command.Parameters.AddWithValue("@staffID", txtStaffID.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allStaffs();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            command = new SqlCommand();
            command.Connection = connection;
            command = new SqlCommand();
            command.Connection = connection;
            command.CommandText = "UPDATE Staff SET StaffName=@staffName,TeamID=@teamID" +
                ", ContractPeriod=@contractPeriod, Salary=@salary WHERE StaffID=@staffID";
            command.Parameters.AddWithValue("@staffID", txtStaffID.Text);
            command.Parameters.AddWithValue("@staffName", txtStaffName.Text);
            command.Parameters.AddWithValue("@teamID", txtTeamID.Text);
            command.Parameters.AddWithValue("@contractPeriod", txtContractPeriod.Text);
            command.Parameters.AddWithValue("@salary", txtSalary.Text);
            command.Parameters.AddWithValue("@staffBirthDate", txtStaffBirthDate.Text);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            allStaffs();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            allStaffs();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            this.Hide();
            Window1 window1 = new Window1(this);
            window1.Show();
        }
    }
}
