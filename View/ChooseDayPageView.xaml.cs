using Calendar.ViewModel;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace Calendar.View
{

    public partial class ChooseDayView : Page
    {
        public ChooseDayView(int day)
        {
            InitializeComponent();
            DataContext = new ChooseDayViewModel(day);
        }

        private void AnimateOpacity(UIElement element)
        {
            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                Duration = new Duration(TimeSpan.FromSeconds(1)),
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);
            Storyboard.SetTarget(fadeInAnimation, element);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));

            storyboard.Begin();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            AnimateOpacity(sender as UIElement);
        }
    }
}
