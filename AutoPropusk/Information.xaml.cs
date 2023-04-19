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
    /// Логика взаимодействия для Infromation.xaml
    /// </summary>
    public partial class Information : Window
    {
        public int Dolj;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Information(int id)
        {
            InitializeComponent();
            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            try
            {
                cmd.CommandText = "Select * from Sotrudniki "
                                + "where ID_Sotr = '" + id + "' ";
                cmd.Connection = connection;
                connection.Open();
                reader = cmd.ExecuteReader();
                int i = 0;
                while (reader.Read())
                {
                    i++;
                    Dolj = Convert.ToInt32(reader["Role_ID"]);
                    LbFamilia.Text = "Фамилия: " + reader["Fam_Sotr"].ToString();
                    LbImya.Text = "Имя: " + reader["Name_Sotr"].ToString();
                    LbOtchestvo.Text = "Отчество: " + reader["Otch_Sotr"].ToString();
                    LbDataRojdenia.Text = "Дата рождения: " + reader["Date_Sotr"].ToString();
                    LbSeria.Text = "Серия паспорта: " + reader["Pas_Seria_Sotr"].ToString();
                    LbNomer.Text = "Номер паспорта: " + reader["Pas_Number_Sotr"].ToString();
                    LbPochta.Text = "Почта: " + reader["Mail_Sotr"].ToString();
                    LbPhone.Text = "Телефон: " + reader["Number_Sotr"].ToString();
                    LbLogin.Text = "Логин: " + reader["Login"].ToString();
                    LbPassword.Text = "Пароль: " + reader["Password"].ToString();
                }
                if (i == 1)
                {
                    connection.Close();
                    cmd.CommandText = "Select * from Rols "
                                + "where ID_Role = '" + Dolj + "' ";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int j = 0;
                    while (reader.Read())
                    {
                        j++;
                        LbDoljnost.Text = "Должность: " + reader["Name_Role"].ToString();
                    }
                    if (j == 1)
                    {
                        connection.Close();
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
                    }
                }
                else
                {
                    connection.Close();
                    MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
                }
            }
            catch
            {
                connection.Close();
                MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
            }
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
