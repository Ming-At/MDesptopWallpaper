using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace MImageView
{
    internal class MImageViewPanel : Panel
    {
        bool DisplayIsNull = true;
        protected override Size ArrangeOverride(Size finalSize)
        {
            var parent = VisualTreeHelper.GetParent(VisualTreeHelper.GetParent(this)) as StackPanel;
            parent.Orientation = Orientation.Horizontal;

            parent.Children.Clear();
            MImageViewSeletorService.SeletorService.BitmapBuf.Clear();
            foreach (MImageViewItem Item in Children)
            {
                Item.LoadImage();//后台加载
                Item.LoadCompete = () =>
                {
                    foreach (BitmapImage btm in Item.ImageList)
                    {
                        Image eleimage = new Image();
                            //eleimage.Source = btm;
                            eleimage.HorizontalAlignment = HorizontalAlignment.Left;
                        eleimage.Margin = new Thickness(5, 5, 5, 5);
                        Border bd = new Border();
                        bd.Child = eleimage;
                        bd.BorderBrush = Brushes.Lavender;
                        bd.BorderThickness = new Thickness(1, 1, 1, 1); ;
                        bd.MouseEnter += (sender, e) =>
                        {
                            bd.BorderBrush = Brushes.Silver;
                        };
                        bd.MouseLeave += (sender, e) =>
                        {
                            bd.BorderBrush = Brushes.Lavender;
                        };

                        parent.Children.Add(bd);
                        eleimage.Source = btm;
                        bd.PreviewMouseLeftButtonUp += (sender, e) =>
                        {
                            MImageViewSeletorService.SeletorService.SetDisPlayImage(btm);
                        };
                        MImageViewSeletorService.SeletorService.BitmapBuf.Add(btm);
                    }
                    if (MImageViewSeletorService.SeletorService.BitmapBuf.Count > 0&& DisplayIsNull==true)
                    {
                        DisplayIsNull = false;
                        MImageViewSeletorService.SeletorService.SetDisPlayImage(MImageViewSeletorService.SeletorService.BitmapBuf[0]);
                    }
                };//加载完成
            }

            return finalSize;
        }



    }
}
