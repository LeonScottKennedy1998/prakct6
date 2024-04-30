using Calendar.Model;
using Calendar.View;
using Calendar.ViewModel.Helpers;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Calendar.ViewModel
{
    class ChooseDayViewModel: BindingHelper
    {
        public DailyActivityModel DailyActivities { get; private set; }
        public BindableCommand SaveCommand { get; private set; }
        public BindableCommand CancelCommand { get; private set; }

        private int _selectedDay;

        public ChooseDayViewModel(int day)
        {
            _selectedDay = day;
            DateTime currentDate = DateTime.Now;
            DateTime selectedDate = new DateTime(currentDate.Year, currentDate.Month, day);
            DailyActivities = new DailyActivityModel
            {
            Date = selectedDate,
            SelectedActivities = new ObservableCollection<ActivityItemModel>()
            };

            FillActivityList();
            LoadSavedData();

            SaveCommand = new BindableCommand(SaveChanges);
            CancelCommand = new BindableCommand(CancelChanges);

            
        }
        private void FillActivityList()
        {
            DailyActivities.SelectedActivities.Add(new ActivityItemModel { Name = "Погладил собакена", IconPath = "pack://application:,,,/Images/dog.jpg", IsSelected = false });
            DailyActivities.SelectedActivities.Add(new ActivityItemModel { Name = "Погладил котейку", IconPath = "pack://application:,,,/Images/cat.jpg", IsSelected = false });
            DailyActivities.SelectedActivities.Add(new ActivityItemModel { Name = "Погладил птица", IconPath = "pack://application:,,,/Images/bird.jpg", IsSelected = false });
            DailyActivities.SelectedActivities.Add(new ActivityItemModel { Name = "Погладил БАТОН", IconPath = "pack://application:,,,/Images/baton.jpg", IsSelected = false });

        }

        private void SaveChanges(object parameter)
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "DailyActivities.json");
            AllActivitiesModel allActivities;

            var culture = new System.Globalization.CultureInfo("ru-RU");

            if (File.Exists(filePath))
            {
                allActivities = JsonHelper.Deserialize<AllActivitiesModel>(filePath);
            }
            else
            {
                allActivities = new AllActivitiesModel();
            }

            string dateKey = DailyActivities.Date.ToString("yyyy-MM-dd", culture);
            if (allActivities.AllActivities.ContainsKey(dateKey))
            {
                allActivities.AllActivities[dateKey] = DailyActivities;
            }
            else
            {
                allActivities.AllActivities.Add(dateKey, DailyActivities);
            }

            JsonHelper.Serialize(allActivities, filePath);
            GoBackToCalendar();

        }
        private void LoadSavedData()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "DailyActivities.json");
            if (File.Exists(filePath))
            {
                var allActivities = JsonHelper.Deserialize<AllActivitiesModel>(filePath);
                string dateKey = new DateTime(DateTime.Now.Year, DateTime.Now.Month, _selectedDay).ToString("yyyy-MM-dd");

                if (allActivities.AllActivities.TryGetValue(dateKey, out var dailyActivities))
                {
                    DailyActivities = dailyActivities;
                }
            }
        }

        private void GoBackToCalendar()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var Calendarage = new CalendarPageView();
            mainWindow.MainContentFrame.Navigate(Calendarage);
        }

        private void CancelChanges(object parameter)
        {
            GoBackToCalendar();
        }

    }
}
