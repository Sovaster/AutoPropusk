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

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : Window
    {
        Dannie dataSet;
        CarsTableAdapter carsTableAdapter;
        View_CarsTableAdapter view_CarsTableAdapter;
        View_Marks_ComboboxTableAdapter view_Marks_ComboboxTableAdapter;
        View_Clients_ComboBoxTableAdapter view_Clients_ComboBoxTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Cars()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            carsTableAdapter = new CarsTableAdapter();
            view_CarsTableAdapter = new View_CarsTableAdapter();
            view_Marks_ComboboxTableAdapter = new View_Marks_ComboboxTableAdapter();
            view_Clients_ComboBoxTableAdapter = new View_Clients_ComboBoxTableAdapter();

            carsTableAdapter.Fill(dataSet.Cars);
            view_CarsTableAdapter.Fill(dataSet.View_Cars);
            view_Marks_ComboboxTableAdapter.Fill(dataSet.View_Marks_Combobox);
            view_Clients_ComboBoxTableAdapter.Fill(dataSet.View_Clients_ComboBox);

            CbClient.ItemsSource = dataSet.View_Clients_ComboBox.DefaultView;
            CbClient.DisplayMemberPath = "Клиент";
            CbClient.SelectedValuePath = "ID_Client";
            CbClient.SelectedIndex = 0;

            CbMark.ItemsSource = dataSet.View_Marks_Combobox.DefaultView;
            CbMark.DisplayMemberPath = "Марка";
            CbMark.SelectedValuePath = "ID_Mark";
            CbMark.SelectedIndex = 0;

            GridCar.ItemsSource = dataSet.View_Cars.DefaultView;
            GridCar.SelectionMode = DataGridSelectionMode.Single;
            GridCar.SelectedValuePath = "ID_Car";
            GridCar.CanUserAddRows = false;
            GridCar.CanUserDeleteRows = false;
            GridCar.IsReadOnly = true;
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridCar.Columns[0].Visibility = Visibility.Hidden;
            GridCar.Columns[1].Visibility = Visibility.Hidden;
            GridCar.Columns[2].Visibility = Visibility.Hidden;
        }

        private void InsertMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbNomer.Text != "" && CbClient.SelectedIndex != -1 && TbGod.Text != "" && CbMark.SelectedIndex != -1)
            {
                try
                {
                    cmd.CommandText = "Select * from Cars "
                                        + "where Number_Car = '" + TbNomer.Text + "' "
                                        + "and Year_Car = '" + TbGod.Text + "'"
                                        + "and Mark_ID = '" + (int)CbMark.SelectedValue + "'"
                                        + "and Client_ID = '" + (int)CbClient.SelectedValue + "'";
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

                        carsTableAdapter.Insert(TbNomer.Text, TbGod.Text, (int)CbMark.SelectedValue, (int)CbClient.SelectedValue);
                        carsTableAdapter.Fill(dataSet.Cars);
                        view_CarsTableAdapter.Fill(dataSet.View_Cars);
                        MessageBox.Show("Машина добавлена!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Машина с такими данными уже есть!", "Ошибка добавления");
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

        private void EditMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridCar.SelectedItem != null)
            {
                if (TbNomer.Text != "" && CbClient.SelectedIndex != -1 && TbGod.Text != "" && CbMark.SelectedIndex != -1)
                {
                    try
                    {
                        cmd.CommandText = "Select * from Cars "
                                            + "where Number_Car = '" + TbNomer.Text + "' "
                                            + "and Year_Car = '" + TbGod.Text + "'"
                                            + "and Mark_ID = '" + (int)CbMark.SelectedValue + "'"
                                            + "and Client_ID = '" + (int)CbClient.SelectedValue + "'";
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

                            carsTableAdapter.UpdateQuery(TbNomer.Text, TbGod.Text, (int)CbMark.SelectedValue, (int)CbClient.SelectedValue, (int)GridCar.SelectedValue);
                            carsTableAdapter.Fill(dataSet.Cars);
                            view_CarsTableAdapter.Fill(dataSet.View_Cars);
                            MessageBox.Show("Машина изменена!", "Успешное изменение");
                        }
                        else
                        {
                            try
                            {
                                connection.Close();
                                cmd.CommandText = "Select * from Cars "
                                                    + "where Number_Car = '" + TbNomer.Text + "' "
                                                    + "and Year_Car = '" + TbGod.Text + "'"
                                                    + "and Mark_ID = '" + (int)CbMark.SelectedValue + "'"
                                                    + "and ID_Car = '" + (int)GridCar.SelectedValue + "'"
                                                    + "and Client_ID = '" + (int)CbClient.SelectedValue + "'";
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

                                    carsTableAdapter.UpdateQuery(TbNomer.Text, TbGod.Text, (int)CbMark.SelectedValue, (int)CbClient.SelectedValue, (int)GridCar.SelectedValue);
                                    carsTableAdapter.Fill(dataSet.Cars);
                                    view_CarsTableAdapter.Fill(dataSet.View_Cars);
                                    MessageBox.Show("Машина изменена!", "Успешное изменение");
                                }
                                else
                                {
                                    connection.Close();
                                    MessageBox.Show("Машина с такими данными уже есть!", "Ошибка изменения");
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
                MessageBox.Show("Выберите машину для изменения!", "Ошибка изменения");
            }
        }

        private void DeleteMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridCar.SelectedItem != null)
            {
                try
                {
                    carsTableAdapter.DeleteQuery((int)GridCar.SelectedValue);
                    carsTableAdapter.Fill(dataSet.Cars);
                    view_CarsTableAdapter.Fill(dataSet.View_Cars);
                    MessageBox.Show("Машина удалена!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данная машина используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите машину для удаления!", "Ошибка удаления");
            }
        }

        private void GridCar_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridCar.SelectedItem;
            if (dataRowView != null)
            {
                TbGod.Text = dataRowView.Row.Field<String>("Год выпуска");
                TbNomer.Text = dataRowView.Row.Field<String>("Номер");
                CbMark.SelectedValue = dataRowView.Row.Field<int>("Mark_ID");
                CbClient.SelectedValue = dataRowView.Row.Field<int>("Client_ID");
            }
        }

        private void TbGod_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = " 1234567890".IndexOf(e.Text) < 0;
        }
        private void TbNomer_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "  qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890".IndexOf(e.Text) < 0;
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
    }
}
