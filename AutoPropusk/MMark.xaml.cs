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
    /// Логика взаимодействия для MMark.xaml
    /// </summary>
    public partial class MMark : Window
    {
        Dannie dataSet;
        MarksTableAdapter marksTableAdapter;
        ModelsTableAdapter modelsTableAdapter;
        View_MarksTableAdapter view_MarksTableAdapter;
        View_ModelsTableAdapter view_ModelsTableAdapter;

        private String ConnectionString;
        private SqlConnection connection;
        private SqlCommand cmd = new SqlCommand();
        private SqlDataReader reader;
        SqlConnectionStringBuilder stringBuilder;
        public MMark()
        {
            InitializeComponent();

            stringBuilder = new SqlConnectionStringBuilder();
            stringBuilder.ConnectionString = Properties.Settings.Default.PropuskConnectionString;
            ConnectionString = stringBuilder.ConnectionString;
            connection = new SqlConnection(ConnectionString);

            dataSet = new Dannie();
            marksTableAdapter = new MarksTableAdapter();
            modelsTableAdapter = new ModelsTableAdapter();
            view_MarksTableAdapter = new View_MarksTableAdapter();
            view_ModelsTableAdapter = new View_ModelsTableAdapter();

            marksTableAdapter.Fill(dataSet.Marks);
            modelsTableAdapter.Fill(dataSet.Models);
            view_MarksTableAdapter.Fill(dataSet.View_Marks);
            view_ModelsTableAdapter.Fill(dataSet.View_Models);

            CbModel.ItemsSource = dataSet.View_Models.DefaultView;
            CbModel.DisplayMemberPath = "Модели";
            CbModel.SelectedValuePath = "ID_Model";
            CbModel.SelectedIndex = 0;

            GridMark.ItemsSource = dataSet.View_Marks.DefaultView;
            GridMark.SelectionMode = DataGridSelectionMode.Single;
            GridMark.SelectedValuePath = "ID_Mark";
            GridMark.CanUserAddRows = false;
            GridMark.CanUserDeleteRows = false;
            GridMark.IsReadOnly = true;

            GridModel.ItemsSource = dataSet.View_Models.DefaultView;
            GridModel.SelectionMode = DataGridSelectionMode.Single;
            GridModel.SelectedValuePath = "ID_Model";
            GridModel.CanUserAddRows = false;
            GridModel.CanUserDeleteRows = false;
            GridModel.IsReadOnly = true;
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GridMark.Columns[0].Visibility = Visibility.Hidden;
            GridMark.Columns[1].Visibility = Visibility.Hidden;

            GridModel.Columns[0].Visibility = Visibility.Hidden;
        }

        private void InsertMarkBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbNazvanieMark.Text != ""  && CbModel.SelectedIndex != -1)
            {
                try
                {
                    cmd.CommandText = "Select * from Marks "
                                        + "where Name_Mark = '" + TbNazvanieMark.Text + "' "
                                        + "and Model_ID = '" + (int)CbModel.SelectedValue + "'";
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

                        marksTableAdapter.Insert(TbNazvanieMark.Text, (int)CbModel.SelectedValue);
                        marksTableAdapter.Fill(dataSet.Marks);
                        view_MarksTableAdapter.Fill(dataSet.View_Marks);
                        MessageBox.Show("Марка добавлена!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Марка с таким названием и моделью уже есть!", "Ошибка добавления");
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
            if (GridMark.SelectedItem != null)
            {
                try
                {
                    marksTableAdapter.DeleteQuery((int)GridMark.SelectedValue);
                    marksTableAdapter.Fill(dataSet.Marks);
                    view_MarksTableAdapter.Fill(dataSet.View_Marks);
                    MessageBox.Show("Марка удалена!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данная марка используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите марку для удаления!", "Ошибка удаления");
            }
        }

        private void InsertModelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (TbNazvanieModel.Text != "")
            {
                try
                {
                    cmd.CommandText = "Select * from Models "
                                        + "where Name_Model = '" + TbNazvanieModel.Text + "' ";
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

                        modelsTableAdapter.Insert(TbNazvanieModel.Text);
                        modelsTableAdapter.Fill(dataSet.Models);
                        view_ModelsTableAdapter.Fill(dataSet.View_Models);
                        MessageBox.Show("Модель добавлена!", "Успешное добавление");
                    }
                    else
                    {
                        connection.Close();
                        MessageBox.Show("Модель с таким названием уже есть!", "Ошибка добавления");
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

        private void DeleteModelBTN_Click(object sender, RoutedEventArgs e)
        {
            if (GridModel.SelectedItem != null)
            {
                try
                {
                    modelsTableAdapter.DeleteQuery((int)GridModel.SelectedValue);
                    modelsTableAdapter.Fill(dataSet.Models);
                    view_ModelsTableAdapter.Fill(dataSet.View_Models);
                    MessageBox.Show("Модель удалена!", "Успешное удаление");
                }
                catch
                {
                    MessageBox.Show("Данная модель используется для других таблиц!", "Ошибка удаления");
                }
            }
            else
            {
                MessageBox.Show("Выберите модель для удаления!", "Ошибка удаления");
            }
        }

        private void GridMark_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridMark.SelectedItem;
            if (dataRowView != null)
            {
                TbNazvanieMark.Text = dataRowView.Row.Field<String>("Марка");
                CbModel.SelectedValue = dataRowView.Row.Field<int>("Model_ID");
            }
        }

        private void GridModel_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)GridModel.SelectedItem;
            if (dataRowView != null)
            {
                TbNazvanieModel.Text = dataRowView.Row.Field<String>("Модели");
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
        private void NazvanieMarkPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "  qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя".IndexOf(e.Text) < 0;
        }
        private void NazvanieModelPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = "  qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNMАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯабвгдеёжзийклмнопрстуфхцчшщъыьэюя1234567890".IndexOf(e.Text) < 0;
        }
    }
}
