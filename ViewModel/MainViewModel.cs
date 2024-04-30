using Calendar.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.Globalization;


namespace Calendar.ViewModel
{
    internal class MainViewModel : BindingHelper
    {
        DateTime date = DateTime.Now;
        public ObservableCollection<DayCard> days { get; set; } = new ObservableCollection<DayCard>() ;
        public BindableCommand BackCommand1 { get; set; }
        public BindableCommand NextCommand2 { get; set; }

        public string tbx { get; set; }
        public MainViewModel()
        {
            Refresh();
            BackCommand1 = new BindableCommand(_ => BackCommand());
            NextCommand2 = new BindableCommand(_ => NextCommand());
        }
        private void Refresh()
        {
            days.Clear();
            tbx = date.ToString("MMMM, yyyy", CultureInfo.CurrentCulture);

            days.Clear();
            for (int i = 1; i <= DateTime.DaysInMonth(date.Year, date.Month); i++)
            {
                days.Add(new DayCard(i));
            }

            onPropertyChanged(nameof(days));
            onPropertyChanged(nameof(tbx));
        }
        public void BackCommand()
        {
            date = date.AddMonths(-1);
            Refresh();
        }
        
        public void NextCommand()
        {
            date = date.AddMonths(1);
            Refresh();
        }


    }
}
