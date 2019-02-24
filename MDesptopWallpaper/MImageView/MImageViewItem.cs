using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace MImageView
{
    public class MImageViewItem: ItemsControl
    {
        public string SearchPath { get; set; }
        public string SearchDirectories { get; set; }
        public List<string> FileName { get; set; }
        internal List<BitmapImage> ImageList { get; set; }
        public MImageViewSearchMode SearchMode { get; set; }

        public Action LoadCompete;

        public int ImageCount = 0;

        static MImageViewItem()
        {
      
        }

        private void ReadImageTask(object sender)
        {
            if (SearchMode == MImageViewSearchMode.Single)
            {
                ReadImage(sender.ToString());
            }
            else if (SearchMode == MImageViewSearchMode.Mux)
            {
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.jpg"))
                {
                    ReadImage(filename);
                }
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.png"))
                {
                    ReadImage(filename);
                }
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.bmp"))
                {
                    ReadImage(filename);
                }
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.tiff"))
                {
                    ReadImage(filename);
                }
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.gif"))
                {
                    ReadImage(filename);
                }
                foreach (var filename in Directory.GetFiles(sender.ToString(), "*.exif"))
                {
                    ReadImage(filename);
                }
            }
            this.Dispatcher.BeginInvoke(new Action(() => {
                LoadCompete?.Invoke();
            }), new object[] { });

        }
        public void ReadImage(string finename)
        {
            using (BinaryReader loader = new BinaryReader(File.Open(finename, FileMode.Open, FileAccess.Read)))
            {
                FileInfo fd = new FileInfo(finename);
                int Length = (int)fd.Length;
                byte[] buf = new byte[Length];
                buf = loader.ReadBytes((int)fd.Length);

                loader.Close();
                BitmapImage btm = new BitmapImage();
                btm.BeginInit();
                btm.CacheOption = BitmapCacheOption.OnLoad;
                btm.StreamSource = new MemoryStream(buf);
                btm.EndInit();
                btm.Freeze();
                FileName.Add(fd.Name);
                ImageList.Add(btm);
                GC.Collect();
            }
        }

        public void LoadImage()
        {
            ImageCount = 0;
            if (ImageList == null)
            {
                ImageList = new List<BitmapImage>();
            }
            if (FileName == null)
            {
                FileName = new List<string>();
            }

            switch (SearchMode)
            {
                case MImageViewSearchMode.Mux:
                    try
                    {

                        if (Directory.Exists(SearchDirectories))
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ReadImageTask), SearchDirectories);
                            var files = Directory.GetFiles(SearchDirectories, "*.*");
                            foreach (var name in files)
                            {
                                string Extension = Path.GetExtension(name);
                                if (Extension == ".png"|| Extension==".jpg"|| Extension==".bmp"|| Extension == ".tiff" || Extension == ".gif"|| Extension == ".exif")
                                {
                                    ImageCount++;
                                }
                            }
                        }
                    }
                    catch
                    {

                    }
                    break;
                case MImageViewSearchMode.Single:
                    try
                    {
                        if (File.Exists(SearchPath))
                        {
                            ThreadPool.QueueUserWorkItem(new WaitCallback(ReadImageTask), SearchPath);
                            ImageCount++;
                        }
                    }
                    catch
                    {

                    }
                    break;
            }
        }

    }
}
