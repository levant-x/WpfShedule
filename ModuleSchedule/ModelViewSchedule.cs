using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchedule.ModuleSchedule
{
    public class ModelViewSchedule
    {
        public string Title { get; set; }
        public List<DayView> Days { get; set; }

        private static string[] week =
        {
            "понедельник",
            "вторник",
            "среда",
            "четверг",
            "пятница",
            "суббота",
            "воскресенье"
        };

        public ModelViewSchedule(List<Lesson> list,
                                 Schedule sch, int shift)
        {
            DateTime today = DateTime.Today;
            today = today.AddDays(shift*7);
            int nowDayWeek = (int)today.DayOfWeek - 1;
            // определяем дату на Понедельник
            DateTime monday = today.AddDays(-nowDayWeek);
            //------------------------
            Days = new List<DayView>();
            for (int i = 0; i < 7; i++)
            {
                var lessonsDay =
                list.Where(l => l.DayOfWeek - 1 == i)
                    .Select(l => new LessonView(l, sch))
                    .ToList();
                if (lessonsDay.Count() > 0)
                {
                    var day = new DayView()
                    {
                        Lessons = lessonsDay,
                        Name = week[i],
                        Date = monday.AddDays(i)
                            .ToShortDateString()
                    };
                    Days.Add(day);
                }
            }
        }
    }
}
