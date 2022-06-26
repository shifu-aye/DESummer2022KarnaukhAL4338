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
using System.IO;
using ZXing;
using ZXing.Common;
using System.Drawing;
using System.Drawing.Imaging;

namespace DESummer2022KarnaukhAL4338.Windows
{
    /// <summary>
    /// Логика взаимодействия для QrCode.xaml
    /// </summary>
    public partial class QrCode : Window
    {
        Window _window;
        public QrCode(Orders order, Window window)
        {
            InitializeComponent();
            imageBar.Source = null;
            _window = window;
            OrderCode.Text = order.Order_code;
            GeneratorBar(order.Order_code);
        }
        private System.Drawing.Image GeneratorBar(string msg)
        {
            MultiFormatWriter mutiWriter = new MultiFormatWriter();
            BitMatrix bm = mutiWriter.encode(msg, BarcodeFormat.CODE_39, 350, 256);
            Bitmap img = new BarcodeWriter().Write(bm);
            imageBar.Source = BitmapToBitmapImage(img);
            return img;
        }
        public static BitmapImage BitmapToBitmapImage(Bitmap bitmap)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                bitmap.Save(stream, ImageFormat.Png);
                stream.Position = 0;
                BitmapImage result = new BitmapImage();
                result.BeginInit();
                result.CacheOption = BitmapCacheOption.OnLoad;
                result.StreamSource = stream;
                result.EndInit();
                result.Freeze();
                return result;
            }
        }
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Seller seller = new Seller(_window);
            seller.Show();
            this.Close();
        }
    }
}
