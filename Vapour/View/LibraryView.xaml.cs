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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Vapour.View
{
    public partial class LibraryView : UserControl
    {
        public LibraryView()
        {
            InitializeComponent();
        }


        public static DependencyProperty CommentButtonContentProperty =
            DependencyProperty.Register(
                nameof(CommentButtonContent),
                typeof(string),
                typeof(LibraryView));
        public string CommentButtonContent
        {
            get { return (string)GetValue(CommentButtonContentProperty); }
            set { SetValue(CommentButtonContentProperty, value); }
        }


        public static DependencyProperty RateButtonContentProperty =
            DependencyProperty.Register(
                nameof(RateButtonContent),
                typeof(string),
                typeof(LibraryView));
        public string RateButtonContent
        {
            get { return (string)GetValue(RateButtonContentProperty); }
            set { SetValue(RateButtonContentProperty, value); }
        }



        public static DependencyProperty CommentTextProperty =
            DependencyProperty.Register(
                nameof(CommentText),
                typeof(string),
                typeof(LibraryView));
        public string CommentText
        {
            get { return (string)GetValue(CommentTextProperty); }
            set { SetValue(CommentTextProperty, value); }
        }






        public static DependencyProperty SliderValueProperty =
            DependencyProperty.Register(
                nameof(SliderValue),
                typeof(int),
                typeof(LibraryView));
        public int SliderValue
        {
            get { return (int)GetValue(SliderValueProperty); }
            set { SetValue(SliderValueProperty, value); }
        }



        public static DependencyProperty SliderTextProperty =
            DependencyProperty.Register(
                nameof(SliderText),
                typeof(string),
                typeof(LibraryView));
        public string SliderText
        {
            get { return (string)GetValue(SliderTextProperty); }
            set { SetValue(SliderTextProperty, value); }
        }

        private void SliderValueChanged_(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            SliderValue = (int)slider.Value;
        }
    }
}
