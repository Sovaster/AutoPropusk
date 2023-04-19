using System;
using System.Collections.Generic;
using AutoPropusk.DannieTableAdapters;
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
using System.Data.SqlClient;
using System.Data;

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для Klienti.xaml
    /// </summary>
    public partial class Klienti : Window
    {
        Dannie dataSet;
        ClientsTableAdapter clientsTableAdapter;
        View_ClientsTableAdapter view_ClientsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Klienti()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            clientsTableAdapter = new ClientsTableAdapter();
            view_ClientsTableAdapter = new View_ClientsTableAdapter();

            clientsTableAdapter.Fill(dataSet.Clients);
            view_ClientsTableAdapter.Fill(dataSet.View_Clients);

            GridKlient.ItemsSource = dataSet.View_Clients.DefaultView;
            GridKlient.SelectionMode = DataGridSelectionMode.Single;
            GridKlient.SelectedValuePath = "ID_Client";
            GridKlient.CanUserAddRows = false;
            GridKlient.CanUserDeleteRows = false;
            GridKlient.IsReadOnly = true;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void GridSotr_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridKlient.SelectedItem;
            if (dataRowView != null)
            {
                TbFamilia.Text = dataRowView.Row.Field<String>("Фамилия");
                TbImya.Text = dataRowView.Row.Field<String>("Имя");
                TbOtchestvo.Text = dataRowView.Row.Field<String>("Отчество");
                TbNomerTelephona.Text = dataRowView.Row.Field<String>("Телефон");
                TbPochta.Text = dataRowView.Row.Field<String>("Почта");
                TbSeriaPassporta.Text = dataRowView.Row.Field<String>("Серия");
                TbNomerPassporta.Text = dataRowView.Row.Field<String>("Номер");
                DpBithday.Text = dataRowView.Row.Field<String>("Дата рождения");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridKlient.Columns[0].Visibility = Visibility.Hidden;
        }

        private void InsertBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbFamilia.Text != "" && TbImya.Text != "" && TbNomerTelephona.Text != "" && TbSeriaPassporta.Text != "" && TbNomerPassporta.Text != "" && DpBithday.Text != "")
            {
                try
                {
                    connection.Close();
                    cmd.CommandText = "Select * from Clients "
                                   + "where Fam_Client = '" + TbFamilia.Text + "' "
                                   + "and Name_Client = '" + TbImya.Text + "'"
                                   + "and Otch_Client = '" + TbOtchestvo.Text + "'"
                                   + "and Date_Client = '" + DpBithday.Text + "'"
                                   + "and Pas_Number_Client = '" + TbNomerPassporta.Text + "'"
                                   + "and Pas_Seria_Client = '" + TbSeriaPassporta.Text + "'";
                    cmd.Connection = connection;
                    connection.Open();
                    reader = cmd.ExecuteReader();
                    int i = 0;
                    while (reader.Read())
                    {
                        i++;
                    }
                    if (i == 0)
                    {
                        connection.Close();
                        clientsTableAdapter.Insert(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text, TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text, DpBithday.Text);
                        clientsTableAdapter.Fill(dataSet.Clients);
                        view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                        MessageBox.Show("Клиент добавлен!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Клиент с такими данными уже есть!", "Ошибка добавления");
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

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridKlient.SelectedItem != null)
            {
                try
                {
                    clientsTableAdapter.DeleteQuery((int)GridKlient.SelectedValue);
                    clientsTableAdapter.Fill(dataSet.Clients);
                    view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                    MessageBox.Show("Клиент удалён!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данный клиент используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите клиента для удаления!", "Ошибка удаления");
            }
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridKlient.SelectedItem != null)
            {
                if (TbFamilia.Text != "" && TbImya.Text != "" && TbNomerTelephona.Text != "" && TbPochta.Text != "")
                {
                    try
                    {
                        connection.Close();
                        cmd.CommandText = "Select * from Clients "
                                       + "where Fam_Client = '" + TbFamilia.Text + "' "
                                       + "and Name_Client = '" + TbImya.Text + "'"
                                       + "and Otch_Client = '" + TbOtchestvo.Text + "'"
                                       + "and Date_Client = '" + DpBithday.Text + "'"
                                       + "and Pas_Number_Client = '" + TbNomerPassporta.Text + "'"
                                       + "and Pas_Seria_Client = '" + TbSeriaPassporta.Text + "'";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                        }
                        if (i == 0)
                        {
                            connection.Close();
                            clientsTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text, TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text, DpBithday.Text, (int)GridKlient.SelectedValue);
                            clientsTableAdapter.Fill(dataSet.Clients);
                            view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                            MessageBox.Show("Клиент изменён!", "Успешное изменение");
                        }
                        else
                        {
                            connection.Close();
                            try
                            {
                                cmd.CommandText = "Select * from Clients "
                                               + "where Fam_Client = '" + TbFamilia.Text + "' "
                                               + "and Name_Client = '" + TbImya.Text + "'"
                                               + "and Otch_Client = '" + TbOtchestvo.Text + "'"
                                               + "and Date_Client = '" + DpBithday.Text + "'"
                                               + "and ID_Client = '" + (int)GridKlient.SelectedValue + "'"
                                               + "and Pas_Number_Client = '" + TbNomerPassporta.Text + "'"
                                               + "and Pas_Seria_Client = '" + TbSeriaPassporta.Text + "'";
                                cmd.Connection = connection;
                                connection.Open();
                                reader = cmd.ExecuteReader();
                                int j = 0;
                                while (reader.Read())
                                {
                                    j++;
                                }
                                if (j == 1)
                                {
                                    connection.Close();
                                    clientsTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text, TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text, DpBithday.Text, (int)GridKlient.SelectedValue);
                                    clientsTableAdapter.Fill(dataSet.Clients);
                                    view_ClientsTableAdapter.Fill(dataSet.View_Clients);
                                    MessageBox.Show("Клиент изменён!", "Успешное изменение");
                                }
                                else
                                {
                                    connection.Close();
                                    MessageBox.Show("Клиент с такими данными уже есть!", "Ошибка регистрации");
                                }
                            }
                            catch
                            {
                                connection.Close();
                                MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
                            }
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
            else
            {
                MessageBox.Show("Выберите клиента для изменения!", "Ошибка изменения");
            }
        }

        private void DataPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890.".IndexOf(e.Text) < 0;
        }
        private void DataPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (((DatePicker)sender).Text.Length == 10)
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }
        private void PassportPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
        }
        private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }
        private void SpacePreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
        }
        private void FIOPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "-_АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя".IndexOf(e.Text) < 0;
        }
        private void PhonePreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "1234567890".IndexOf(e.Text) < 0;
            if (((TextBox)sender).Text.Length == 0)
            {
                ((TextBox)sender).Text += "8(";
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
            if (((TextBox)sender).Text.Length == 1)
            {
                ((TextBox)sender).Text += "(";
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
            if (((TextBox)sender).Text.Length == 5)
            {
                ((TextBox)sender).Text += ")-";
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
            if (((TextBox)sender).Text.Length == 6)
            {
                ((TextBox)sender).Text += "-";
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
            if (((TextBox)sender).Text.Length == 10)
            {
                ((TextBox)sender).Text += '-';
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
            if (((TextBox)sender).Text.Length == 13)
            {
                ((TextBox)sender).Text += '-';
                TbNomerTelephona.SelectionStart = TbNomerTelephona.MaxLength;
            }
        }
        private void PochtaPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = " абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) > 0;
        }
        private void PochtaPreviewKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space)
            {
                e.Handled = true;
            }
            if (((TextBox)sender).Text.Contains("@yandex.ru") || ((TextBox)sender).Text.Contains("@mail.ru") || ((TextBox)sender).Text.Contains("@gmail.ru") || ((TextBox)sender).Text.Contains("@inbox.ru")
                    || ((TextBox)sender).Text.Contains("@ok.ru") || ((TextBox)sender).Text.Contains("@rambler.ru") || ((TextBox)sender).Text.Contains("@yahoo.ru") || ((TextBox)sender).Text.Contains("@mpt.ru")
                    || ((TextBox)sender).Text.Contains("@yandex.com") || ((TextBox)sender).Text.Contains("@mail.com") || ((TextBox)sender).Text.Contains("@gmail.com") || ((TextBox)sender).Text.Contains("@inbox.com")
                    || ((TextBox)sender).Text.Contains("@ok.com") || ((TextBox)sender).Text.Contains("@rambler.com") || ((TextBox)sender).Text.Contains("@yahoo.com") || ((TextBox)sender).Text.Contains("@mpt.com"))
            {
                e.Handled = true;
            }
            if (e.Key == Key.Back)
            {
                e.Handled = false;
            }
        }
    }
}
