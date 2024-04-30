using Calendar.ViewModel;
using System.Windows;
using System;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;


namespace Calendar
{
    public partial class DayCard : UserControl
    {
        public string Day { get; set; }

        public DayCard(int day)
        {
            InitializeComponent();
            DataContext = new DayCardViewModel(day);
        }
        private void DayCard_Loaded(object sender, RoutedEventArgs e)
        {

            DoubleAnimation fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = TimeSpan.FromSeconds(0.5)
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(fadeInAnimation);

            Storyboard.SetTarget(fadeInAnimation, this); 
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath("Opacity"));

            storyboard.Begin();
        }

        
    }
}
