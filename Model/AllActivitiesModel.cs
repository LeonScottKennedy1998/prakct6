using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calendar.Model
{
    public class AllActivitiesModel
    {
        public Dictionary<string, DailyActivityModel> AllActivities { get; set; } = new Dictionary<string, DailyActivityModel>();
    }
}
