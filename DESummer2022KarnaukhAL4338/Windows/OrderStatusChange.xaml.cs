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
using DESummer2022KarnaukhAL4338.Models;
using DESummer2022KarnaukhAL4338.Windows;

namespace DESummer2022KarnaukhAL4338.Windows
{
    /// <summary>
    /// Логика взаимодействия для OrderStatusChange.xaml
    /// </summary>
    public partial class OrderStatusChange : Window
    {
        Orders _order;
        Window _window;
        public OrderStatusChange(Orders orders, Window window)
        {
            InitializeComponent();
            _order = orders;
            _window = window;
            OrderCode.Text += orders.Order_code;
            OrderStatus.Text += orders.Status;
            this.Closing += OrderStatusChange_Closing;
        }
        private void OrderStatusChange_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Seller seller = new Seller(_window);
            seller.Show();
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            using (SkiResortEntities db = new SkiResortEntities())
            {
                db.Orders.Attach(_order);
                _order.Status = StatusCBX.Text;
                _order.Close_date = DateTime.Now;
                db.SaveChanges();
            }
            this.Close();
        }
        private void StatusCBX_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_order.Status != "Закрыта")
                Apply.IsEnabled = true;
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
