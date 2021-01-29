using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfSchedule.ModuleSchedule
{
    public class LessonView
    {
        private static string[] bells =
        {
            "08:30\r\n10:05",
            "10:15\r\n11:50",
            "12:00\r\n13:35",
            "14:15\r\n15:50",
            "16:00\r\n17:35",
            "17:45\r\n19:20",
            "19:30\r\n21:05"
        };
        public string Time { get; set; }
        public string Teacher { get; set; }
        public string Discipline { get; set; }
        public string Cabinet { get; set; }
        public string Group { get; set; }
        public string N { get; set; }

        public LessonView(Lesson l, Schedule sch)
        {
            Teacher = sch.Teachers[l.idTeacher];
            Discipline = sch.Disciplines[l.idDiscipline];
            Cabinet = sch.Cabinets[l.idCabinet];
            Group = sch.Groups[l.idGroup];
            N = (l.N).ToString();
            Time = bells[l.N - 1];
        }
    }
}
