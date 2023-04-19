using System;
using System.Collections.Generic;
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
    /// Логика взаимодействия для Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public int ID;
        public int DOLJ;
        public Menu(int id, string fam, string im, string otch, int dolj)
        {
            InitializeComponent();
            LbName.Text = fam + " " + im + " " + otch;
            ID = id;
            DOLJ = dolj;

            SotrBTN.Visibility = Visibility.Collapsed;
            KlientiBTN.Visibility = Visibility.Collapsed;
            MMBTN.Visibility = Visibility.Collapsed;
            CarsBTN.Visibility = Visibility.Collapsed;
            DostupBTN.Visibility = Visibility.Collapsed;
            PropuskBTN.Visibility = Visibility.Collapsed;

            if (dolj == 1)
            {
                SotrBTN.Visibility = Visibility.Visible;
                KlientiBTN.Visibility = Visibility.Visible;
                MMBTN.Visibility = Visibility.Visible;
                CarsBTN.Visibility = Visibility.Visible;
                DostupBTN.Visibility = Visibility.Visible;
                PropuskBTN.Visibility = Visibility.Visible;
            }
            else if (dolj == 2)
            {
                KlientiBTN.Visibility = Visibility.Visible;
                MMBTN.Visibility = Visibility.Visible;
                CarsBTN.Visibility = Visibility.Visible;
                PropuskBTN.Visibility = Visibility.Visible;
            }
            else if (dolj == 3)
            {
                MessageBox.Show("Отказ в доступе!", "У вас нет доступа к данной системе. Обратитесь к администратору.");
            }
        }

        private void InfoBTN_Click(object sender, RoutedEventArgs e)
        {
            new Information(ID).ShowDialog();
        }

        private void BackBTN_Click(object sender, RoutedEventArgs e)
        {
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Close();
        }

        private void SotrBTN_Click(object sender, RoutedEventArgs e)
        {
            new Sotrudniki().ShowDialog();
        }

        private void KlientiBTN_Click(object sender, RoutedEventArgs e)
        {
            new Klienti().ShowDialog();
        }

        private void CarsBTN_Click(object sender, RoutedEventArgs e)
        {
            new Cars().ShowDialog();
        }

        private void MMBTN_Click(object sender, RoutedEventArgs e)
        {
            new MMark().ShowDialog();
        }

        private void DostupBTN_Click(object sender, RoutedEventArgs e)
        {
            new Dostup().ShowDialog();
        }

        private void PropuskBTN_Click(object sender, RoutedEventArgs e)
        {
            new Propusk(ID).ShowDialog();
        }
    }
}
