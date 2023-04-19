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
using AutoSalon;

namespace AutoPropusk
{
    /// <summary>
    /// Логика взаимодействия для Propusk.xaml
    /// </summary>
    public partial class Propusk : Window
    {
        public int id;
        public int ID_PR;
        Dannie dataSet;
        PassTableAdapter passTableAdapter;
        View_PassTableAdapter view_PassTableAdapter;
        View_Cars_ComboBoxTableAdapter view_Cars_ComboBoxTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Propusk(int ID)
        {
            InitializeComponent();

            id = ID;

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            passTableAdapter = new PassTableAdapter();
            view_PassTableAdapter = new View_PassTableAdapter();
            view_Cars_ComboBoxTableAdapter = new View_Cars_ComboBoxTableAdapter();

            passTableAdapter.Fill(dataSet.Pass);
            view_PassTableAdapter.Fill(dataSet.View_Pass);
            view_Cars_ComboBoxTableAdapter.Fill(dataSet.View_Cars_ComboBox);

            CbClient.ItemsSource = dataSet.View_Cars_ComboBox.DefaultView;
            CbClient.DisplayMemberPath = "Машина";
            CbClient.SelectedValuePath = "ID_Car";
            CbClient.SelectedIndex = 0;

            GridPropusk.ItemsSource = dataSet.View_Pass.DefaultView;
            GridPropusk.SelectionMode = DataGridSelectionMode.Single;
            GridPropusk.SelectedValuePath = "ID_Pass";
            GridPropusk.CanUserAddRows = false;
            GridPropusk.CanUserDeleteRows = false;
            GridPropusk.IsReadOnly = true;

        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridPropusk.Columns[0].Visibility = Visibility.Hidden;
            GridPropusk.Columns[1].Visibility = Visibility.Hidden;
            GridPropusk.Columns[2].Visibility = Visibility.Hidden;
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void GridPropusk_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridPropusk.SelectedItem;
            if (dataRowView != null)
            {
                CbClient.SelectedValue = dataRowView.Row.Field<int>("Car_ID");
                ID_PR = dataRowView.Row.Field<int>("ID_Pass");
            }
        }
        private void WordBTN_Click(object sender, RoutedEventArgs e)
        {
            var items = new Dictionary<string, string> { };
            DataRowView dataRowView = (DataRowView)GridPropusk.SelectedItem;

            if (dataRowView != null)
            {
                var insert = new WorldInsert($@"C:\Program Files (x86)\Sovaster Company\PropuskFinal\Shablon.docx", dataRowView.Row.Field<String>("ФИО владельца"));
                items = new Dictionary<string, string>
                {
                    {"<Klient>",dataRowView.Row.Field<String>("ФИО владельца")},
                    {"<Car>",dataRowView.Row.Field<String>("Машина")},
                    {"<Year>",dataRowView.Row.Field<String>("Год выпуска")},
                    {"<Nomer>",dataRowView.Row.Field<String>("Номер машины")},
                    {"<Date>",DateTime.Now.ToString("dd.MM.yyyy")},
                    {"<Sotrudnik>",dataRowView.Row.Field<String>("ФИО сотрудника")},
                };
                insert.Process(items);

                MessageBox.Show("Успешно");
            }
            else
            {
                MessageBox.Show("Выберите пропуск для экспорта в шаблон!", "Ошибка пропуска");
            }
        }
        private void DeleteMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridPropusk.SelectedItem != null)
            {
                try
                {
                    passTableAdapter.DeleteQuery((int)GridPropusk.SelectedValue);
                    passTableAdapter.Fill(dataSet.Pass);
                    view_PassTableAdapter.Fill(dataSet.View_Pass);
                    MessageBox.Show("Пропуск удалён!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данный пропуск используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите пропуск для удаления!", "Ошибка удаления");
            }
        }

        private void InsertMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CbClient.SelectedIndex != -1)
            {
                try
                {
                    cmd.CommandText = "Select * from Pass "
                                        + "where Car_ID = " + (int)CbClient.SelectedValue;
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

                        passTableAdapter.Insert(id, (int)CbClient.SelectedValue);
                        passTableAdapter.Fill(dataSet.Pass);
                        view_PassTableAdapter.Fill(dataSet.View_Pass);
                        MessageBox.Show("Пропуск добавлен!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Пропуск с такими данными уже есть!", "Ошибка добавления");
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

        private void GridPropusk_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            new PropuskDostup(ID_PR).ShowDialog();
        }
    }
}
