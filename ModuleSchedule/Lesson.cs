using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchedule.ModuleSchedule
{
    public class Lesson
    {
        public WeekFlash WeekFlash { get; set; }
        public int DayOfWeek { get; set; }
        public int N { get; set; }
        public int idTeacher { get; set; }
        public int idDiscipline { get; set; }
        public int idCabinet { get; set; }
        public int idGroup { get; set; }
    }
}
