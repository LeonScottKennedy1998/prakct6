using Calendar.ViewModel;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calendar.View
{
    /// <summary>
    /// Логика взаимодействия для CalendarPageView.xaml
    /// </summary>
    public partial class CalendarPageView : Page
    {
        private MediaPlayer mediaPlayer = new MediaPlayer();
        public CalendarPageView()
        {
            InitializeComponent();
            DataContext = new MainViewModel();

            
            mediaPlayer.Open(new Uri("meow.mp3", UriKind.RelativeOrAbsolute));
            mediaPlayer.MediaOpened += (sender, args) => {
                mediaPlayer.Pause();  
            };
            mediaPlayer.Volume = 1;
        }

        private void OnThirdAnimationButtonClick(object sender, RoutedEventArgs e)
        {
            mediaPlayer.Open(new Uri("meow.mp3", UriKind.RelativeOrAbsolute));
            mediaPlayer.Play();

            CreateBlinkAnimation();  
            ScaryImage.Visibility = Visibility.Visible;
            
            var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromSeconds(3) };
            timer.Tick += (s, args) =>
            {
                ScaryImage.Opacity = 0; 
                mediaPlayer.Stop();  
                timer.Stop();
                ScaryImage.Visibility = Visibility.Hidden;
            };
            timer.Start();
        }
        private void CreateBlinkAnimation()
        {
            DoubleAnimation blinkAnimation = new DoubleAnimation
            {
                From = 0.0,
                To = 1.0,
                AutoReverse = true,
                Duration = new Duration(TimeSpan.FromSeconds(0.2)),
                RepeatBehavior = new RepeatBehavior(5)  
            };

            Storyboard storyboard = new Storyboard();
            storyboard.Children.Add(blinkAnimation);

            Storyboard.SetTarget(blinkAnimation, ScaryImage);
            Storyboard.SetTargetProperty(blinkAnimation, new PropertyPath("Opacity"));

            storyboard.Begin();
        }
    }
}
