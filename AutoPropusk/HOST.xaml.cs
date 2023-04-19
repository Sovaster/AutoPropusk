using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class HOST : Window
    {
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public HOST()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            servTB.Text = stringBuilder.DataSource;
            dbTB.Text = stringBuilder.InitialCatalog;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            string path = @"C:\PropDbBackUpFiles";
            DirectoryInfo dirInfo = new DirectoryInfo(path);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }
        }

        private void ConnectBTN_Click(object sender, RoutedEventArgs e)
        {
            if (servTB.Text != "" && dbTB.Text != "")
            {
                stringBuilder.DataSource = servTB.Text;
                stringBuilder.InitialCatalog = dbTB.Text;
                stringBuilder.IntegratedSecurity = true;
                var config = ConfigurationManager.OpenExeConfiguration(System.Configuration.ConfigurationUserLevel.None);
                var conStrSect = (ConnectionStringsSection)config.GetSection("connectionStrings");
                conStrSect.ConnectionStrings["AutoPropusk.Properties.Settings.PropuskConnectionString"].ConnectionString = stringBuilder.ConnectionString;
                config.Save();
                ConfigurationManager.RefreshSection("connectionStrings");
                Application.Current.Shutdown();
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void ContinueBTN_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            CreateBackup(ConnectionString, "Propusk", "C:\\PropDbBackUpFiles\\PropuskBackUp_" + DateTime.Now.ToString("dd.MM.yyyy") + ".bak");
        }

        private void GoBTN_Click(object sender, RoutedEventArgs e)
        {
            UseBackup(ConnectionString, "Propusk", "C:\\PropDbBackUpFiles\\PropuskBackUp_" + DateTime.Now.ToString("dd.MM.yyyy") + ".bak");
        }

        private static void CreateBackup(string connectionString, string databaseName, string backupFilePath)
        {
            var backupCommand = "BACKUP DATABASE @databaseName TO DISK = @backupFilePath";
            using (var conn = new SqlConnection(connectionString))
            using (var cmd = new SqlCommand(backupCommand, conn))
            {
                conn.Open();
                cmd.Parameters.AddWithValue("@databaseName", databaseName);
                cmd.Parameters.AddWithValue("@backupFilePath", backupFilePath);
                cmd.ExecuteNonQuery();
            }
        }

        private static void UseBackup(string connectionString, string databaseName, string backupFilePath)
        {
            string PuthBDback = "";
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "BackUp file (*.bak)|*.bak";
            openFileDialog.InitialDirectory = @"C:\PropDbBackUpFiles";
            if (openFileDialog.ShowDialog() == true)
            {
                PuthBDback = openFileDialog.FileName;
                Console.WriteLine(PuthBDback);
            }
            if (PuthBDback != "")
            {
                var backupCommand = @"USE [Master]; 
                                                ALTER DATABASE Propusk SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                                RESTORE DATABASE Propusk FROM DISK = '" + PuthBDback + @"' WITH REPLACE;
                                                ALTER DATABASE Propusk SET MULTI_USER; ";
                using (var conn = new SqlConnection(connectionString))
                using (var cmd = new SqlCommand(backupCommand, conn))
                {
                    conn.Open();
                    cmd.Parameters.AddWithValue("@databaseName", databaseName);
                    cmd.Parameters.AddWithValue("@backupFilePath", backupFilePath);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
