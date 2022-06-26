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
using DESummer2022KarnaukhAL4338.Windows;
using DESummer2022KarnaukhAL4338.Models;

namespace DESummer2022KarnaukhAL4338.Windows
{
    /// <summary>
    /// Логика взаимодействия для MainClient.xaml
    /// </summary>
    public partial class MainClient : Window
    {
        Clients _client;
        public MainClient(Clients clients)
        {
            InitializeComponent();
            _client = clients;
            ListServices();
        }
        private void ListServices()
        {
            using (SkiResortEntities skiResortEntities = new SkiResortEntities())
            {
               Goods.ItemsSource = skiResortEntities.Services.ToList();
            }
        }

        private void ReserveBTN_Click(object sender, RoutedEventArgs e)
        {
            if (ReservedServices.Count > 0)
            {
                ClientOrder CO = new ClientOrder(ReservedServices, _client);
                CO.Show();
                this.Close();
            }
            else
                MessageBox.Show("Выберите услугу");
        }
        List<Services> ReservedServices = new List<Services>();
        private void ReserveCX_Checked(object sender, RoutedEventArgs e)
        {
            Services service = (sender as CheckBox).DataContext as Services;
            using (SkiResortEntities skiResortEntities = new SkiResortEntities())
            {
                ReservedServices.Add(service);
            }
        }

        private void ReserveCX_Unchecked(object sender, RoutedEventArgs e)
        {
            Services service = (sender as CheckBox).DataContext as Services;
            using (SkiResortEntities skiResortEntities = new SkiResortEntities())
            {
                ReservedServices.Remove(service);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow MW = new MainWindow();
            MW.Show();
            this.Close();
        }
    }
}
