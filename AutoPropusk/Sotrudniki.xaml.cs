using System;
using System.Collections.Generic;
using AutoPropusk.DannieTableAdapters;
using System.Data;
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
using Microsoft.Win32;
using System.IO;

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для Sotrudniki.xaml
    /// </summary>
    public partial class Sotrudniki : Window
    {
        public string Familia;
        public string Imya;
        public string Otchestvo;

        Dannie dataSet;
        SotrudnikiTableAdapter sotrudnikiTableAdapter;
        View_SotrudnikTableAdapter view_SotrudnikTableAdapter;
        View_RolsTableAdapter view_RolsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Sotrudniki()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            sotrudnikiTableAdapter = new SotrudnikiTableAdapter();
            view_SotrudnikTableAdapter = new View_SotrudnikTableAdapter();
            view_RolsTableAdapter = new View_RolsTableAdapter();

            sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
            view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
            view_RolsTableAdapter.Fill(dataSet.View_Rols);

            CbDolj.ItemsSource = dataSet.View_Rols.DefaultView;
            CbDolj.DisplayMemberPath = "Должность";
            CbDolj.SelectedValuePath = "ID_Role";
            CbDolj.SelectedIndex = 0;

            GridSotr.ItemsSource = dataSet.View_Sotrudnik.DefaultView;
            GridSotr.SelectionMode = DataGridSelectionMode.Single;
            GridSotr.SelectedValuePath = "ID_Sotr";
            GridSotr.CanUserAddRows = false;
            GridSotr.CanUserDeleteRows = false;
            GridSotr.IsReadOnly = true;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CSVBTN_Click(object sender, RoutedEventArgs e)
        {
            GridSotr.SelectionMode = DataGridSelectionMode.Extended;
            GridSotr.SelectAllCells();

            GridSotr.ClipboardCopyMode = DataGridClipboardCopyMode.IncludeHeader;
            ApplicationCommands.Copy.Execute(null, GridSotr);

            string result = (string)System.Windows.Clipboard.GetData(System.Windows.DataFormats.CommaSeparatedValue);

            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";
            if (saveFileDialog.ShowDialog() == true)
            {
                File.AppendAllText(saveFileDialog.FileName, result, UnicodeEncoding.UTF8);
            }

            GridSotr.UnselectAllCells();
            GridSotr.SelectionMode = DataGridSelectionMode.Single;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridSotr.Columns[0].Visibility = Visibility.Hidden;
            GridSotr.Columns[1].Visibility = Visibility.Hidden;
        }

        private void GridSotr_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridSotr.SelectedItem;
            if (GridSotr.SelectedValue != null)
            {
                if ((int)GridSotr.SelectedValue == 1)
                {
                    CbDolj.IsEnabled = false;
                    DeleteBTN.IsEnabled = false;
                }
                else
                {
                    CbDolj.IsEnabled = true;
                    DeleteBTN.IsEnabled = true;
                }
            }
            if (dataRowView != null)
            {
                TbLogin.Text = dataRowView.Row.Field<String>("Логин");
                PbPassword.Password = dataRowView.Row.Field<String>("Пароль");
                TbFamilia.Text = dataRowView.Row.Field<String>("Фамилия");
                TbImya.Text = dataRowView.Row.Field<String>("Имя");
                TbOtchestvo.Text = dataRowView.Row.Field<String>("Отчество");
                TbNomerTelephona.Text = dataRowView.Row.Field<String>("Телефон");
                DpBithday.Text = dataRowView.Row.Field<String>("Дата рождения");
                TbPochta.Text = dataRowView.Row.Field<String>("Почта");
                TbNomerPassporta.Text = dataRowView.Row.Field<String>("Номер");
                TbSeriaPassporta.Text = dataRowView.Row.Field<String>("Серия");
                CbDolj.SelectedValue = dataRowView.Row.Field<int>("Role_ID");
            }
        }

        private void InsertBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbFamilia.Text != "" && TbImya.Text != "" && TbNomerTelephona.Text != "" && TbSeriaPassporta.Text != "" && TbNomerPassporta.Text != "" &&
                DpBithday.Text != "" && TbLogin.Text != "" && PbPassword.Password != "" && CbDolj.SelectedIndex != -1)
            {
                if (TbLogin.Text.Length >= 6 && PbPassword.Password.Length >= 6)
                {
                    int upperCount = PbPassword.Password.Count(Char.IsUpper);
                    if ((PbPassword.Password.Contains('1') || PbPassword.Password.Contains('2') || PbPassword.Password.Contains('3') ||
                        PbPassword.Password.Contains('4') || PbPassword.Password.Contains('5') || PbPassword.Password.Contains('6') ||
                        PbPassword.Password.Contains('7') || PbPassword.Password.Contains('8') || PbPassword.Password.Contains('9') ||
                        PbPassword.Password.Contains('0')) && upperCount > 0)
                    {
                        if (TbPochta.Text.Contains("@yandex.ru") || TbPochta.Text.Contains("@mail.ru") || TbPochta.Text.Contains("@gmail.ru") || TbPochta.Text.Contains("@inbox.ru")
                                                || TbPochta.Text.Contains("@ok.ru") || TbPochta.Text.Contains("@rambler.ru") || TbPochta.Text.Contains("@yahoo.ru") || TbPochta.Text.Contains("@mpt.ru")
                                                || TbPochta.Text.Contains("@yandex.com") || TbPochta.Text.Contains("@mail.com") || TbPochta.Text.Contains("@gmail.com") || TbPochta.Text.Contains("@inbox.com")
                                                || TbPochta.Text.Contains("@ok.com") || TbPochta.Text.Contains("@rambler.com") || TbPochta.Text.Contains("@yahoo.com") || TbPochta.Text.Contains("@mpt.com"))
                        {
                            try
                            {
                                cmd.CommandText = "Select * from Sotrudniki "
                                                   + "where Login = '" + TbLogin.Text + "' ";
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
                                    int age = DateTime.Today.Year - DpBithday.SelectedDate.Value.Year;
                                    int month = DateTime.Today.Month - DpBithday.SelectedDate.Value.Month;
                                    int day = DateTime.Today.Day - DpBithday.SelectedDate.Value.Day;
                                    if (age < 18)
                                    {
                                        MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                    }
                                    else if (age > 18)
                                    {
                                        sotrudnikiTableAdapter.Insert(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                        TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                        DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue);
                                        sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                        view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                        MessageBox.Show("Сотрудник добавлен!", "Успешное добавление");
                                    }
                                    else if (age == 18)
                                    {
                                        if (month < 0)
                                        {
                                            MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                        }
                                        else if (month > 0)
                                        {
                                            sotrudnikiTableAdapter.Insert(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                            TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                            DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue);
                                            sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                            view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                            MessageBox.Show("Сотрудник добавлен!", "Успешное добавление");
                                        }
                                        else if (month == 0)
                                        {
                                            if (day < 0)
                                            {
                                                MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                            }
                                            else
                                            {
                                                sotrudnikiTableAdapter.Insert(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue);
                                                sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                MessageBox.Show("Сотрудник добавлен!", "Успешное добавление");
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    connection.Close();
                                    MessageBox.Show("Сотрудник с таким логином уже есть!", "Ошибка добавления");
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
                            MessageBox.Show("Введите корректную электронную почту!", "Ошибка почты");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен содержать одну цифру и один символ верхнего регистра!", "Ошибка пароля");
                    }
                }
                else
                {
                    MessageBox.Show("Логин и пароль должны содержать больше 6 символов!", "Ошибка логина и пароля");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
            }
        }

        private void DeleteBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridSotr.SelectedItem != null)
            {
                if ((int)GridSotr.SelectedValue != 1)
                {
                    try
                    {
                        sotrudnikiTableAdapter.DeleteQuery((int)GridSotr.SelectedValue);
                        sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                        view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                        MessageBox.Show("Сотрудник удалён!", "Успешное удаление");
                    }
                    catch
                    {
                        MessageBox.Show("Данный сотрудник используется для других таблиц!", "Ошибка удаления");
                    }
                }
                else
                {
                    MessageBox.Show("Нельзя удалить главного администратора!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для удаления!", "Ошибка удаления");
            }
        }

        private void EditBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridSotr.SelectedItem != null)
            {
                if (GridSotr.SelectedItem != null)
                {
                    try
                    {
                        cmd.CommandText = "Select * from Sotrudniki "
                                           + "where ID_Sotr = '" + (int)GridSotr.SelectedValue + "' ";
                        cmd.Connection = connection;
                        connection.Open();
                        reader = cmd.ExecuteReader();
                        int i = 0;
                        while (reader.Read())
                        {
                            i++;
                            Familia = reader["Fam_Sotr"].ToString();
                            Imya = reader["Name_Sotr"].ToString();
                            Otchestvo = reader["Otch_Sotr"].ToString();
                        }
                        if (i == 1)
                        {
                            connection.Close();
                            MessageBoxResult result = MessageBox.Show("Вы хотите изменить: " + Familia + " " + Imya + " " + Otchestvo + "?", "Изменение", MessageBoxButton.YesNo);
                            if (result == MessageBoxResult.Yes)
                            {
                                if (TbFamilia.Text != "" && TbImya.Text != "" && TbNomerTelephona.Text != "" && TbSeriaPassporta.Text != "" && TbNomerPassporta.Text != "" &&
                                    DpBithday.Text != "" && TbLogin.Text != "" && PbPassword.Password != "" && CbDolj.SelectedIndex != -1)
                                {
                                    if (TbLogin.Text.Length >= 6 && PbPassword.Password.Length >= 6)
                                    {
                                        int upperCount = PbPassword.Password.Count(Char.IsUpper);
                                        if ((PbPassword.Password.Contains('1') || PbPassword.Password.Contains('2') || PbPassword.Password.Contains('3') ||
                                            PbPassword.Password.Contains('4') || PbPassword.Password.Contains('5') || PbPassword.Password.Contains('6') ||
                                            PbPassword.Password.Contains('7') || PbPassword.Password.Contains('8') || PbPassword.Password.Contains('9') ||
                                            PbPassword.Password.Contains('0')) && upperCount > 0)
                                        {
                                            if (TbPochta.Text.Contains("@yandex.ru") || TbPochta.Text.Contains("@mail.ru") || TbPochta.Text.Contains("@gmail.ru") || TbPochta.Text.Contains("@inbox.ru")
                                                                    || TbPochta.Text.Contains("@ok.ru") || TbPochta.Text.Contains("@rambler.ru") || TbPochta.Text.Contains("@yahoo.ru") || TbPochta.Text.Contains("@mpt.ru")
                                                                    || TbPochta.Text.Contains("@yandex.com") || TbPochta.Text.Contains("@mail.com") || TbPochta.Text.Contains("@gmail.com") || TbPochta.Text.Contains("@inbox.com")
                                                                    || TbPochta.Text.Contains("@ok.com") || TbPochta.Text.Contains("@rambler.com") || TbPochta.Text.Contains("@yahoo.com") || TbPochta.Text.Contains("@mpt.com"))
                                            {
                                                try
                                                {
                                                    cmd.CommandText = "Select * from Sotrudniki "
                                                                       + "where Login = '" + TbLogin.Text + "' ";
                                                    cmd.Connection = connection;
                                                    connection.Open();
                                                    reader = cmd.ExecuteReader();
                                                    int j = 0;
                                                    while (reader.Read())
                                                    {
                                                        j++;
                                                    }
                                                    if (j == 0)
                                                    {
                                                        connection.Close();
                                                        int age = DateTime.Today.Year - DpBithday.SelectedDate.Value.Year;
                                                        int month = DateTime.Today.Month - DpBithday.SelectedDate.Value.Month;
                                                        int day = DateTime.Today.Day - DpBithday.SelectedDate.Value.Day;
                                                        if (age < 18)
                                                        {
                                                            MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                        }
                                                        else if (age > 18)
                                                        {
                                                            sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                            TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                            DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                            sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                            view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                            MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                        }
                                                        else if (age == 18)
                                                        {
                                                            if (month < 0)
                                                            {
                                                                MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                            }
                                                            else if (month > 0)
                                                            {
                                                                sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                                TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                                DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                                sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                                view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                                MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                            }
                                                            else if (month == 0)
                                                            {
                                                                if (day < 0)
                                                                {
                                                                    MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                                }
                                                                else
                                                                {
                                                                    sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                                    TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                                    DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                                    sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                                    view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                                    MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        connection.Close();
                                                        cmd.CommandText = "Select * from Sotrudniki "
                                                                       + "where Login = '" + TbLogin.Text + "' "
                                                                       + "and ID_Sotr = '" + (int)GridSotr.SelectedValue + "'";
                                                        cmd.Connection = connection;
                                                        connection.Open();
                                                        reader = cmd.ExecuteReader();
                                                        int k = 0;
                                                        while (reader.Read())
                                                        {
                                                            k++;
                                                        }
                                                        if (k == 1)
                                                        {
                                                            connection.Close();
                                                            int age = DateTime.Today.Year - DpBithday.SelectedDate.Value.Year;
                                                            int month = DateTime.Today.Month - DpBithday.SelectedDate.Value.Month;
                                                            int day = DateTime.Today.Day - DpBithday.SelectedDate.Value.Day;
                                                            if (age < 18)
                                                            {
                                                                MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                            }
                                                            else if (age > 18)
                                                            {
                                                                sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                                TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                                DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                                sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                                view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                                MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                            }
                                                            else if (age == 18)
                                                            {
                                                                if (month < 0)
                                                                {
                                                                    MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                                }
                                                                else if (month > 0)
                                                                {
                                                                    sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                                    TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                                    DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                                    sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                                    view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik); ;
                                                                    MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                                }
                                                                else if (month == 0)
                                                                {
                                                                    if (day < 0)
                                                                    {
                                                                        MessageBox.Show("Человеку нет 18 лет!", "Ошибка возраста");
                                                                    }
                                                                    else
                                                                    {
                                                                        sotrudnikiTableAdapter.UpdateQuery(TbFamilia.Text, TbImya.Text, TbOtchestvo.Text,
                                                                        TbNomerTelephona.Text, TbPochta.Text, TbSeriaPassporta.Text, TbNomerPassporta.Text,
                                                                        DpBithday.Text, TbLogin.Text, PbPassword.Password, (int)CbDolj.SelectedValue, (int)GridSotr.SelectedValue);
                                                                        sotrudnikiTableAdapter.Fill(dataSet.Sotrudniki);
                                                                        view_SotrudnikTableAdapter.Fill(dataSet.View_Sotrudnik);
                                                                        MessageBox.Show("Сотрудник изменён!", "Успешное изменение");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            connection.Close();
                                                            MessageBox.Show("Сотрудник с таким логином уже есть!", "Ошибка изменения");
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
                                                MessageBox.Show("Введите корректную электронную почту!", "Ошибка почты");
                                            }
                                        }
                                        else
                                        {
                                            MessageBox.Show("Пароль должен содержать одну цифру и один символ верхнего регистра!", "Ошибка пароля");
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Логин и пароль должны содержать больше 6 символов!", "Ошибка логина и пароля");
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("Заполните все поля!", "Ошибка заполнения");
                                }
                            }
                            else if (result == MessageBoxResult.No)
                            {
                            }
                        }
                        else
                        {
                            connection.Close();
                        }
                    }
                    catch
                    {
                        connection.Close();
                        MessageBox.Show("Ошибка чтения данных!", "Ошибка БД");
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите сотрудника для изменения!", "Ошибка изменения");
            }
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
        private void LogPasPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = " абвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ".IndexOf(e.Text) > 0;
        }
    }
}
