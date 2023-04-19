using System;
using System.Collections.Generic;
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

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Window
    {
        public int ID;
        public int Doljnost;
        public string Familia;
        public string Imya;
        public string Otchestvo;
        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Authorization()
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);
        }

        private void EnterBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbLogin.Text != "" && PbPassword.Password != "")
            {
                try
                {
                    cmd.CommandText = "Select * from Sotrudniki "
                                    + "where Login = '" + TbLogin.Text + "' "
                                    + "and Password = '" + PbPassword.Password + "'";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                        ID = Convert.ToInt32(reader["ID_Sotr"]);
                        Doljnost = Convert.ToInt32(reader["Role_ID"]);
                        Familia = reader["Fam_Sotr"].ToString();
                        Imya = reader["Name_Sotr"].ToString();
                        Otchestvo = reader["Otch_Sotr"].ToString();
                    }
                    if (i == 1)
                    {
                        connection.Close();
                        Menu menu = new Menu(ID, Familia, Imya, Otchestvo, Doljnost);
                        menu.Show();
                        this.Close();
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Неверный логин или пароль!", "Ошибка авторизации");
                    }
                }
                catch
                {
                    connection.Close();
                    MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            HOST HOST = new HOST();
            HOST.Show();
            this.Close();
        }
        private void ExitBTN_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }
    }
}
