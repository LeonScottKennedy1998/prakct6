using Calendar.View;
using Calendar.ViewModel;
using System.Windows;

namespace Calendar
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainContentFrame.Navigate(new CalendarPageView());

        }

    }
}
