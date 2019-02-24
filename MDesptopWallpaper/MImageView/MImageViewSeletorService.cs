using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace MImageView
{
    internal class MImageViewSeletorService
    {
        private static MImageViewSeletorService __SeletorService;
        public Action<BitmapImage> BitmapSeletorChanged;
        public List<BitmapImage> BitmapBuf = new List<BitmapImage>();
        public static MImageViewSeletorService SeletorService
        {
            get
            {
                if (__SeletorService == null)
                {
                    __SeletorService = new MImageViewSeletorService();
                }
                return __SeletorService;
            }
        }
        private MImageViewSeletorService()
        {

        }

        public void SetDisPlayImage(BitmapImage btm)
        {
            BitmapSeletorChanged?.Invoke(btm);
        }

    }
}
