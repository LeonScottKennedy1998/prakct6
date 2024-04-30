using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;


namespace Calendar.Model
{
    public class DailyActivityModel
    {
        public DateTime Date { get; set; }
        public ObservableCollection<ActivityItemModel> SelectedActivities { get; set; } = new ObservableCollection<ActivityItemModel>();


    }
}
