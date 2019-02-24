using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace MImageView
{
    [TemplatePart(Name = "BaseArea", Type = typeof(ScrollViewer))]
    [TemplatePart(Name = "DispalyArea", Type = typeof(ScrollViewer))]
    [TemplatePart(Name = "ImageList", Type = typeof(ScrollViewer))]
    [TemplatePart(Name = "img1", Type = typeof(Image))]
    [TemplatePart(Name = "img2", Type = typeof(Image))]
    [TemplatePart(Name = "ImgSwitch1", Type = typeof(Storyboard))]
    [TemplatePart(Name = "ImgSwitch2", Type = typeof(Storyboard))]
    [TemplatePart(Name = "scalelatetrans1", Type = typeof(ScaleTransform))]
    [TemplatePart(Name = "scalelatetrans2", Type = typeof(ScaleTransform))]
    public class MImageView : ItemsControl
    {
        public static readonly DependencyProperty SelectedIndexProperty =
    DependencyProperty.Register(
    "SelectedIndex",
    typeof(int),
    typeof(MImageView),
    new PropertyMetadata(0));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        public static readonly DependencyProperty AutoMaticProperty =
    DependencyProperty.Register(
    "AutoMaticProperty",
    typeof(bool),
    typeof(MImageView),
    new PropertyMetadata(false));
        public bool AutoMatic
        {
            get { return (bool)GetValue(AutoMaticProperty); }
            set
            {
                this.OnRenderSizeChanged(new SizeChangedInfo(this,this.RenderSize,true,true));
                if (value == true)
                {
                    if (ImgSwitch1 != null || ImgSwitch2 != null)
                    {
                        ImgSwitch1.Children.Add(Switch1Item3);
                        ImgSwitch2.Children.Add(Switch2Item3);
                        ImgSwitch1.Completed += ImgSwitch_Completed;
                        ImgSwitch2.Completed += ImgSwitch_Completed;
                        img1.Visibility = Visibility.Visible;
                        img2.Visibility = Visibility.Visible;
                    }
                }
                else
                {
                    if (ImgSwitch1 != null || ImgSwitch2 != null)
                    {
                        ImgSwitch1.Children.Remove(Switch1Item3);
                        ImgSwitch2.Children.Remove(Switch2Item3);
                        ImgSwitch1.Completed -= ImgSwitch_Completed;
                        ImgSwitch2.Completed -= ImgSwitch_Completed;
                        if (IsImg1 == true)
                        {
                            img1.Visibility = Visibility.Visible;
                            img2.Visibility = Visibility.Collapsed;
                        }
                        else
                        {
                            img2.Visibility = Visibility.Visible;
                            img1.Visibility = Visibility.Collapsed;
                        }
                    }                  
                }
                SetValue(AutoMaticProperty, value);
            }
        }



        public static readonly DependencyProperty AnimationEnableProperty =
            DependencyProperty.Register(
            "AnimationEnable",
            typeof(bool),
            typeof(MImageView),
            new PropertyMetadata(false));
        public bool AnimationEnable
        {
            get { return (bool)GetValue(AnimationEnableProperty); }
            set{ SetValue(AnimationEnableProperty, value); }
        }


        static MImageView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(MImageView), new FrameworkPropertyMetadata(typeof(MImageView)));
        }

        ScrollViewer ImageListViewer;
        ScrollViewer DispalyArea;
        Image img1;
        Image img2;
        ScrollViewer BaseArea;
        Storyboard ImgSwitch1;
        Storyboard ImgSwitch2;
        ScaleTransform scalelatetrans1;
        ScaleTransform scalelatetrans2;
        DoubleAnimationUsingKeyFrames Switch1Item3;
        DoubleAnimationUsingKeyFrames Switch2Item3;
        bool IsImg1 = true;
        int ImgCnt = 0;
        DispatcherTimer dTimer = new DispatcherTimer();
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            ImageListViewer = (ScrollViewer)this.Template.FindName("ImageList", this);
            DispalyArea = (ScrollViewer)this.Template.FindName("DispalyArea", this);
            BaseArea = (ScrollViewer)this.Template.FindName("BaseArea", this);
            img1 = (Image)this.Template.FindName("img1", this);
            img2 = (Image)this.Template.FindName("img2", this);
            scalelatetrans1 = (ScaleTransform)this.Template.FindName("scalelatetrans1", this);
            scalelatetrans2 = (ScaleTransform)this.Template.FindName("scalelatetrans2", this);
            img2.Visibility = Visibility.Collapsed;
            ImageListViewer.PreviewMouseWheel += (sender, e) =>
            {
                double num = Math.Abs((int)(e.Delta / 3));
                double offset = 0.0;
                if (e.Delta > 0)
                {
                    offset = Math.Max(0.0, (double)(ImageListViewer.HorizontalOffset - num));
                }
                else
                {
                    offset = Math.Min(ImageListViewer.ScrollableWidth, ImageListViewer.HorizontalOffset + num);
                }
                if (offset != ImageListViewer.HorizontalOffset)
                {
                    ImageListViewer.ScrollToHorizontalOffset(offset);
                }
            };
            MImageViewSeletorService.SeletorService.BitmapSeletorChanged = (btm) =>
            {
                ChangeImage(btm);
                if (AutoMatic == true)
                {
                    dTimer.Start();
                }
            };
            ImgSwitch1 = (Storyboard)DispalyArea.FindResource("ImgSwitch1");
            ImgSwitch2 = (Storyboard)DispalyArea.FindResource("ImgSwitch2");
            DoubleAnimationUsingKeyFrames Switch1Item1 = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames Switch1Item2 = new DoubleAnimationUsingKeyFrames();
            Switch1Item3 = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(Switch1Item1, "scalelatetrans1");
            Storyboard.SetTargetProperty(Switch1Item1, new PropertyPath("ScaleX"));
            Storyboard.SetTargetName(Switch1Item2, "scalelatetrans1");
            Storyboard.SetTargetProperty(Switch1Item2, new PropertyPath("ScaleY"));
            Storyboard.SetTargetName(Switch1Item3, "img1");
            Storyboard.SetTargetProperty(Switch1Item3, new PropertyPath("Opacity"));
            Switch1Item1.KeyFrames.Add(new SplineDoubleKeyFrame(1,KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch1Item1.KeyFrames.Add(new SplineDoubleKeyFrame(1.4, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            Switch1Item2.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch1Item2.KeyFrames.Add(new SplineDoubleKeyFrame(1.4, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            Switch1Item3.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch1Item3.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(26000))));
            Switch1Item3.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            ImgSwitch1.Children.Add(Switch1Item1);
            ImgSwitch1.Children.Add(Switch1Item2);

 

            DoubleAnimationUsingKeyFrames Switch2Item1 = new DoubleAnimationUsingKeyFrames();
            DoubleAnimationUsingKeyFrames Switch2Item2 = new DoubleAnimationUsingKeyFrames();
            Switch2Item3 = new DoubleAnimationUsingKeyFrames();
            Storyboard.SetTargetName(Switch2Item1, "scalelatetrans2");
            Storyboard.SetTargetProperty(Switch2Item1, new PropertyPath("ScaleX"));
            Storyboard.SetTargetName(Switch2Item2, "scalelatetrans2");
            Storyboard.SetTargetProperty(Switch2Item2, new PropertyPath("ScaleY"));
            Storyboard.SetTargetName(Switch2Item3, "img2");
            Storyboard.SetTargetProperty(Switch2Item3, new PropertyPath("Opacity"));
            Switch2Item1.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch2Item1.KeyFrames.Add(new SplineDoubleKeyFrame(1.4, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            Switch2Item2.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch2Item2.KeyFrames.Add(new SplineDoubleKeyFrame(1.4, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            Switch2Item3.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch2Item3.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(0))));
            Switch2Item3.KeyFrames.Add(new SplineDoubleKeyFrame(1, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(26000))));
            Switch2Item3.KeyFrames.Add(new SplineDoubleKeyFrame(0, KeyTime.FromTimeSpan(TimeSpan.FromMilliseconds(30000))));
            ImgSwitch2.Children.Add(Switch2Item1);
            ImgSwitch2.Children.Add(Switch2Item2);
            if (AutoMatic == true)
            {
                ImgSwitch1.Children.Add(Switch1Item3);
                ImgSwitch2.Children.Add(Switch2Item3);
                ImgSwitch1.Completed += ImgSwitch_Completed;
                ImgSwitch2.Completed += ImgSwitch_Completed;
                img2.Visibility = Visibility.Visible;
            }
            dTimer.Interval = TimeSpan.FromMilliseconds(26000);
            dTimer.Tick += DTimer_Tick;
            
        }



        protected override Size ArrangeOverride(Size finalSize)
        {
            BaseArea.Arrange(new Rect(0,0,finalSize.Width,finalSize.Height));
            if (AutoMatic == true)
            {
                DispalyArea.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height));
                ImageListViewer.Arrange(new Rect(0, 0, 0, 0));
            }
            else
            {
                DispalyArea.Arrange(new Rect(0, 0, finalSize.Width, finalSize.Height - 158));
                ImageListViewer.Arrange(new Rect(0, finalSize.Height - 158, finalSize.Width, 158));
            }


            img1.Height = DispalyArea.ActualHeight;
            img1.Width = DispalyArea.ActualWidth;
            img2.Height = DispalyArea.ActualHeight;
            img2.Width = DispalyArea.ActualWidth;
            scalelatetrans1.CenterX = DispalyArea.ActualWidth / 2;
            scalelatetrans1.CenterY = DispalyArea.ActualHeight / 2;
            scalelatetrans2.CenterX = DispalyArea.ActualWidth / 2;
            scalelatetrans2.CenterY = DispalyArea.ActualHeight / 2;
            return finalSize;
        }


        private void ChangeImage(BitmapImage btm)
        {
            if (IsImg1 == true)
            {
                img1.Source = btm;
                img1.Height = DispalyArea.ActualHeight;
                img1.Width = DispalyArea.ActualWidth;
                scalelatetrans1.CenterX = DispalyArea.ActualWidth / 2;
                scalelatetrans1.CenterY = DispalyArea.ActualHeight / 2;
                if (AnimationEnable == true)
                {
                    ImgSwitch1.Begin();
                }
            }
            else
            {
                img2.Source = btm;
                img2.Height = DispalyArea.ActualHeight;
                img2.Width = DispalyArea.ActualWidth;
                scalelatetrans2.CenterX = DispalyArea.ActualWidth / 2;
                scalelatetrans2.CenterY = DispalyArea.ActualHeight / 2;
                if (AnimationEnable == true)
                {
                    ImgSwitch2.Begin();
                }
            }

        }

        private void ImgSwitch_Completed(object sender, EventArgs e)
        {
            if (AutoMatic == true)
            {
                dTimer.Start();
            }
        }
        [DllImport("kernel32.dll", EntryPoint = "SetProcessWorkingSetSize")]
        private static extern int SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

        private void DTimer_Tick(object sender, EventArgs e)
        {
            SetProcessWorkingSetSize(System.Diagnostics.Process.GetCurrentProcess().Handle, -1, -1);
            IsImg1 = !IsImg1;
            if (AutoMatic == true)
            {
                ImgCnt++;
                if (IsImg1 == true)
                {
                    Panel.SetZIndex(img1, 1);
                    Panel.SetZIndex(img2, 2);
                    if (ImgCnt >= MImageViewSeletorService.SeletorService.BitmapBuf.Count)
                    {
                        ImgCnt = 0;
                    }
                    ChangeImage(MImageViewSeletorService.SeletorService.BitmapBuf[ImgCnt]);
                }
                else
                {
                    Panel.SetZIndex(img1, 2);
                    Panel.SetZIndex(img2, 1);
                    if (ImgCnt >= MImageViewSeletorService.SeletorService.BitmapBuf.Count)
                    {
                        ImgCnt = 0;
                    }
                    ChangeImage(MImageViewSeletorService.SeletorService.BitmapBuf[ImgCnt]);
                }
            }
        }

    }
}
