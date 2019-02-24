using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace MImageView
{
    internal class MImageViewItemPresenter : ItemsPresenter
    {

        public static readonly DependencyProperty SelectedIndexProperty =
        DependencyProperty.Register(
        "SelectedIndex",
        typeof(int),
        typeof(MImageViewItemPresenter),
        new PropertyMetadata(0));


        public int SelectedIndex
        {
            get { return (int)GetValue(SelectedIndexProperty); }
            set { SetValue(SelectedIndexProperty, value); }
        }

        protected override Size MeasureOverride(Size constraint)
        {
            return base.MeasureOverride(constraint);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            var items = this.TemplatedParent as ItemsControl;
            if (SelectedIndex < items.Items.Count)
            {
               // MTransitioningSelectorService.TransitioningSelectorItemSelected(items.Items[SelectedIndex] as MTransitioningSelectorItem);
            }
            return base.ArrangeOverride(arrangeSize);
        }
    }
}
