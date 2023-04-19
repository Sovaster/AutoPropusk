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
    /// Логика взаимодействия для PropuskDostup.xaml
    /// </summary>
    /// 
    public partial class PropuskDostup : Window
    {
        public int ID;
        Dannie dataSet;
        Pass_DostupTableAdapter pass_DostupTableAdapter;
        View_Pass_DostupTableAdapter view_Pass_DostupTableAdapter;
        View_DostupsTableAdapter view_DostupsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;

        public PropuskDostup(int id)
        {
            InitializeComponent();

            ID = id;

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            pass_DostupTableAdapter = new Pass_DostupTableAdapter();
            view_Pass_DostupTableAdapter = new View_Pass_DostupTableAdapter();
            view_DostupsTableAdapter = new View_DostupsTableAdapter();

            pass_DostupTableAdapter.Fill(dataSet.Pass_Dostup);
            view_Pass_DostupTableAdapter.Fill(dataSet.View_Pass_Dostup);
            view_DostupsTableAdapter.Fill(dataSet.View_Dostups);

            CbClient.ItemsSource = dataSet.View_Dostups.DefaultView;
            CbClient.DisplayMemberPath = "Название доступа";
            CbClient.SelectedValuePath = "ID_Dostup";
            CbClient.SelectedIndex = 0;

            GridPropusk.ItemsSource = dataSet.View_Pass_Dostup.DefaultView;
            GridPropusk.SelectionMode = DataGridSelectionMode.Single;
            GridPropusk.SelectedValuePath = "ID_Pass_Dostup";
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
                CbClient.SelectedValue = dataRowView.Row.Field<int>("Dostup_ID");
            }
        }

        private void DeleteMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridPropusk.SelectedItem != null)
            {
                try
                {
                    pass_DostupTableAdapter.DeleteQuery((int)GridPropusk.SelectedValue);
                    pass_DostupTableAdapter.Fill(dataSet.Pass_Dostup);
                    view_Pass_DostupTableAdapter.Fill(dataSet.View_Pass_Dostup);
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

        private void InsertMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (CbClient.SelectedIndex != -1)
            {
                try
                {
                    cmd.CommandText = "Select * from Pass_Dostup "
                                       + "where Dostup_ID = '" + (int)CbClient.SelectedValue + "' "
                                       + "and Pass_ID = '" + ID + "'";
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

                        pass_DostupTableAdapter.Insert(ID, (int)CbClient.SelectedValue);
                        pass_DostupTableAdapter.Fill(dataSet.Pass_Dostup);
                        view_Pass_DostupTableAdapter.Fill(dataSet.View_Pass_Dostup);
                        MessageBox.Show("Доступ добавлен!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Доступ уже открыт!", "Ошибка добавления");
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

    }
}
