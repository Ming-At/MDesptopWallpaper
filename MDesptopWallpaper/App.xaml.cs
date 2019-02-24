using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace MDesptopWallpaper
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : System.Windows.Application
    {
    }

    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Process[] processes = Process.GetProcessesByName("MDesptopWallpaper");
            if (processes.Length > 1)
            {
                foreach (Process process in processes)
                {
                    if (process.Id != Process.GetCurrentProcess().Id)
                    {
                        process.Kill();
                    }
                }
            }
            if (args.Length > 0)
            {
                MDesptopWallpaper.App app = new MDesptopWallpaper.App();
                MainWindow.Init();
                int ScreenId = 0;
                if (args.Length > 0)
                {
                    ScreenId = int.Parse(args[0]);
                }

                if (ScreenId >= Screen.AllScreens.Length)
                {
                    ScreenId = 0;
                }
                Screen screen= Screen.AllScreens[ScreenId];

                DesptopWallpaper desptopWallpaper = new DesptopWallpaper();

                string path = @"C:\Windows\Web\Wallpaper\Theme1";
                if (args.Length > 1 && Directory.Exists(args[1]))
                {
                    path = args[1];                  
                }

                desptopWallpaper.ImagePath = path;
                desptopWallpaper.Show();
                MainWindow.SetParent(new WindowInteropHelper(desptopWallpaper).Handle, MainWindow.programIntPtr);
                desptopWallpaper.Left = screen.Bounds.Left;
                desptopWallpaper.Top = screen.Bounds.Top;
                desptopWallpaper.Width = screen.Bounds.Width;
                desptopWallpaper.Height = screen.Bounds.Height;

                app.Run();
            }
            else
            {
                MDesptopWallpaper.App app = new MDesptopWallpaper.App();
                app.InitializeComponent();
                app.Run();
            }
        }

    }

}
