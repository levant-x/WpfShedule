using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchedule.ModuleSchedule
{
    public class DayView
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public List<LessonView> Lessons { get; set; }
    }
}
