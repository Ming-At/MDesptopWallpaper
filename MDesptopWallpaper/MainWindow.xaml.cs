using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Interop;
using System.Windows.Forms;
using System.Diagnostics;

namespace MDesptopWallpaper
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string winName);

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessageTimeout(IntPtr hwnd, uint msg, IntPtr wParam, IntPtr lParam, uint fuFlage, uint timeout, IntPtr result);

        [DllImport("user32.dll")]
        public static extern bool EnumWindows(EnumWindowsProc proc, IntPtr lParam);
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string className, string winName);

        [DllImport("user32.dll")]
        public static extern bool ShowWindow(IntPtr hwnd, int nCmdShow);

        [DllImport("user32.dll")]
        public static extern IntPtr SetParent(IntPtr hwnd, IntPtr parentHwnd);


        public MainWindow()
        {
            InitializeComponent();
        }

        public MainWindow(string[] args)
        {
            InitializeComponent();
            
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            displayselect.ItemsSource = Screen.AllScreens;
            if (Screen.AllScreens.Length > 0)
            {
                displayselect.SelectedIndex = 0;
            }

            pathselect.Text = @"C:\Windows\Web\Wallpaper\Theme1";
        }

        public static IntPtr programIntPtr = IntPtr.Zero;
        private DesptopWallpaper desptopWallpaper = null;
        public static void Init()
        {
            // 通过类名查找一个窗口，返回窗口句柄。
            programIntPtr = FindWindow("Progman", null);
            // 窗口句柄有效
            if (programIntPtr != IntPtr.Zero)
            {
                IntPtr result = IntPtr.Zero;
                // 向 Program Manager 窗口发送 0x52c 的一个消息，超时设置为0x3e8（1秒）。
                SendMessageTimeout(programIntPtr, 0x52c, IntPtr.Zero, IntPtr.Zero, 0, 0x3e8, result);

                // 遍历顶级窗口
                EnumWindows((hwnd, lParam) =>
                {
                    // 找到包含 SHELLDLL_DefView 这个窗口句柄的 WorkerW
                    if (FindWindowEx(hwnd, IntPtr.Zero, "SHELLDLL_DefView", null) != IntPtr.Zero)
                    {
                        // 找到当前 WorkerW 窗口的，后一个 WorkerW 窗口。 
                        IntPtr tempHwnd = FindWindowEx(IntPtr.Zero, hwnd, "WorkerW", null);

                        // 隐藏这个窗口
                        ShowWindow(tempHwnd, 0);
                    }
                    return true;
                }, IntPtr.Zero);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Screen screen = displayselect.SelectedItem as Screen;
            if (desptopWallpaper==null && screen!=null)
            {
                Init();
                desptopWallpaper = new DesptopWallpaper();
                desptopWallpaper.ImagePath= pathselect.Text;
                desptopWallpaper.Show();    
                SetParent(new WindowInteropHelper(desptopWallpaper).Handle, programIntPtr);
                desptopWallpaper.Left = screen.Bounds.Left;
                desptopWallpaper.Top = screen.Bounds.Top;
                desptopWallpaper.Width = screen.Bounds.Width;
                desptopWallpaper.Height = screen.Bounds.Height;
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (desptopWallpaper!=null)
            {
                desptopWallpaper.Close();
                desptopWallpaper = null;
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (desptopWallpaper != null)
            //{
            //    desptopWallpaper.Close();
            //    desptopWallpaper = null;
            //}
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog dialog = new System.Windows.Forms.FolderBrowserDialog();
            dialog.Description = "请选择壁纸所在文件夹";
            dialog.SelectedPath = pathselect.Text;
            if (dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (string.IsNullOrEmpty(dialog.SelectedPath))
                {
                    System.Windows.MessageBox.Show(this, "文件夹路径不能为空", "提示");
                    return;
                }
                pathselect.Text = dialog.SelectedPath;
            }
        }
    }
}
