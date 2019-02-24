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

namespace MDesptopWallpaper
{
    /// <summary>
    /// DesptopWallpaper.xaml 的交互逻辑
    /// </summary>
    public partial class DesptopWallpaper : Window
    {
        public string ImagePath
        {
            get { return imageview.SearchDirectories; }
            set { imageview.SearchDirectories = value; }
        }

        public DesptopWallpaper()
        {
            InitializeComponent();
        }
    }
}
