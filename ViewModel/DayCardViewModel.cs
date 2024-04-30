using Calendar.Model;
using Calendar.View;
using Calendar.ViewModel.Helpers;
using System;
using System.IO;
using System.Windows;


namespace Calendar.ViewModel
{
    internal class DayCardViewModel: BindingHelper
    {
        public int Day { get; set; }
        public BindableCommand MenuItemCommand { get; }
        public BindableCommand OpenDayCommand { get;  }
        public BindableCommand ClearDayCommand { get;}

        DailyActivityModel dailyActivities = new DailyActivityModel();
        public DayCardViewModel(int day)
        {
            Day = day;
            OpenDayCommand = new BindableCommand(_ => OpenDay());
            ClearDayCommand = new BindableCommand(_ => ClearDay());  

        }

        private void OpenDay()
        {
            var mainWindow = (MainWindow)Application.Current.MainWindow;
            var chooseDayView = new ChooseDayView(Day);
            mainWindow.MainContentFrame.Navigate(chooseDayView);

        }
        private void LoadData()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "DailyActivities.json");
            if (File.Exists(filePath))
            {
                var allActivities = JsonHelper.Deserialize<AllActivitiesModel>(filePath);
                string dateKey = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day).ToString("yyyy-MM-dd");
                if (allActivities.AllActivities.TryGetValue(dateKey, out var activities))
                {
                    dailyActivities = activities;
                }
            }
        }
        private void ClearDay()
        {
            LoadData(); // Загружаем данные, если они еще не загружены

            foreach (var activity in dailyActivities.SelectedActivities)
            {
                activity.IsSelected = false;
            }

            SaveChanges();
        }

        private void SaveChanges()
        {
            string filePath = Path.Combine(Environment.CurrentDirectory, "DailyActivities.json");
            AllActivitiesModel allActivities;

            if (File.Exists(filePath))
            {
                allActivities = JsonHelper.Deserialize<AllActivitiesModel>(filePath);
            }
            else
            {
                allActivities = new AllActivitiesModel();
            }

            string dateKey = new DateTime(DateTime.Now.Year, DateTime.Now.Month, Day).ToString("yyyy-MM-dd");
            allActivities.AllActivities[dateKey] = dailyActivities; // Обновляем данные для дня

            JsonHelper.Serialize(allActivities, filePath); // Сериализуем обновленные данные в файл
        }
    }
}
