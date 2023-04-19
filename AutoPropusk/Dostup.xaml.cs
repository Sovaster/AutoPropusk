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
    /// Логика взаимодействия для Dostup.xaml
    /// </summary>
    public partial class Dostup : Window
    {
        Dannie dataSet;
        DostupsTableAdapter dostupsTableAdapter;
        View_DostupsTableAdapter view_DostupsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public Dostup()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            dostupsTableAdapter = new DostupsTableAdapter();
            view_DostupsTableAdapter = new View_DostupsTableAdapter();

            dostupsTableAdapter.Fill(dataSet.Dostups);
            view_DostupsTableAdapter.Fill(dataSet.View_Dostups);

            GridDostup.ItemsSource = dataSet.View_Dostups.DefaultView;
            GridDostup.SelectionMode = DataGridSelectionMode.Single;
            GridDostup.SelectedValuePath = "ID_Dostup";
            GridDostup.CanUserAddRows = false;
            GridDostup.CanUserDeleteRows = false;
            GridDostup.IsReadOnly = true;
        }
        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void GridDostup_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridDostup.SelectedItem;
            if (dataRowView != null)
            {
                TbNazvanieDostup.Text = dataRowView.Row.Field<String>("Название доступа");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridDostup.Columns[0].Visibility = Visibility.Hidden;
        }

        private void HandleCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (e.Command == ApplicationCommands.Cut || e.Command == ApplicationCommands.Copy || e.Command == ApplicationCommands.Paste)
            {
                e.CanExecute = false;
                e.Handled = true;
            }
        }

        private void TbNazvanieDostup_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = " АБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890".IndexOf(e.Text) < 0;
        }

        private void InsertMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbNazvanieDostup.Text != "")
            {
                try
                {
                    cmd.CommandText = "Select * from Dostups "
                                        + "where Name_Dostup = '" + TbNazvanieDostup.Text + "' ";
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

                        dostupsTableAdapter.Insert(TbNazvanieDostup.Text);
                        dostupsTableAdapter.Fill(dataSet.Dostups);
                        view_DostupsTableAdapter.Fill(dataSet.View_Dostups);
                        MessageBox.Show("Доступ добавлен!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Доступ с таким названием уже есть!", "Ошибка добавления");
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

        private void DeleteMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridDostup.SelectedItem != null)
            {
                try
                {
                    dostupsTableAdapter.DeleteQuery((int)GridDostup.SelectedValue);
                    dostupsTableAdapter.Fill(dataSet.Dostups);
                    view_DostupsTableAdapter.Fill(dataSet.View_Dostups);
                    MessageBox.Show("Доступ удалён!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данный доступ используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите доступ для удаления!", "Ошибка удаления");
            }
        }
    }
}
