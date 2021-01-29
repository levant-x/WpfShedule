using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//---------------------------
using System.IO;
using System.Data;
using Excel;
using Newtonsoft.Json;

namespace WpfSchedule
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

    public class Schedule
    {
        private DataSet dset;

        public List<string> Groups { get; set; }
        public List<string> Teachers { get; set; }
        public List<string> Disciplines { get; set; }
        public List<string> Cabinets { get; set; }
        public List<string> Bells { get; set; }
        public List<Lesson> Lessons{ get; set; }

        public void SaveJson()
        {
            string result =
                JsonConvert.SerializeObject(this);
            using (var fs = new FileStream("schedule.json",
                                    FileMode.Create))
            {
                using (var sw = new StreamWriter(fs))
                {
                    sw.WriteLine(result);
                    sw.Flush();
                }
            }
        }

        static public Schedule LoadJson()
        {
            Schedule schedule = null;
            using (var fs = new FileStream("schedule.json",
                                    FileMode.Open))
            {
                using (var sr = new StreamReader(fs))
                {
                    string result = sr.ReadLine();
                    schedule =
                        JsonConvert.DeserializeObject<Schedule>(result);
                }
            }
            return schedule;
        }


        public List<LessonView> TestGroup(string group, 
                                      int day)
        {
            int id = Groups.IndexOf(group);
            if (id < 0) return null;
            var list = Lessons
                        .Where(l => l.idGroup == id)
                        .Where(l => l.DayOfWeek == day)
                        .OrderBy(l => l.N)
                        .Select(l => new LessonView(l, this))
                        .ToList();
            return list;
        }

        public Schedule()
        {
            Groups = new List<string>();
            Teachers = new List<string>();
            Disciplines = new List<string>();
            Cabinets = new List<string>();
            Bells = new List<string>();
            Lessons = new List<Lesson>();
        }

        public DataTable GetPage(int number)
        {
            return dset.Tables[number];
        }

        public bool LoadXlsFile()
        {
            // выберим из папки XlsFiles
            // самый свежий файл с раписанием
            var di = new DirectoryInfo("XlsFiles");
            if (!di.Exists) return false;
            // берём массив xls файлов
            // *.xls (2003 офис) Бинарные   
            // *.xlsx (2007) Открытый Xml
            var files = di.GetFiles("*.xls*");
            var fileXls = files
                    .OrderBy(f => f.CreationTime)
                    .Last();
            //--------------
            // расширение файла
            string ext = fileXls.Extension;
            // поток для файла
            FileStream fs = fileXls.OpenRead();
            // паттерн Фабрика
            IExcelDataReader xlsReader = null;
            //----------------------
            if (ext == ".xls")
                xlsReader = ExcelReaderFactory
                    .CreateBinaryReader(fs);
            else if (ext == ".xlsx")
                xlsReader = ExcelReaderFactory
                    .CreateOpenXmlReader(fs);
            //-------------------------------
            // xls файл превращается
            // в набор таблиц! DataTable
            dset = xlsReader.AsDataSet();
            fs.Close();
            return true;
        }

        public void Process()
        {
            for (int i = 0; i < dset.Tables.Count; i++)
            {
                ProcessTable(i);
            }
        }

        private void ProcessTable(int numTable)
        {
            DataTable tab = dset.Tables[numTable];
            // номер строки с группой
            int nRowGroups = -1;
            #region 1) нужно в таблице найти номер строки в которой перчислены группы
            for (int i = 0; i < tab.Rows.Count; i++)
            {
                // из ячеек берём текст
                // i-я строка, нулевой столбец
                string txt = tab.Rows[i][0].ToString();
                if (txt.Trim().ToLower().StartsWith("групп"))
                {
                    nRowGroups = i;
                }
            }
            #endregion
            // словарь -> (названиеГруппы, номер столбца)
            var dictGroup = new Dictionary<string, int>();
            #region 2) Просматриваем строку по столбцам ищем группы
            for (int j = 0; j < tab.Columns.Count; j++)
            {
                // берём текст из всех ячеек в строке nRowGroups
                string txt = tab.Rows[nRowGroups][j].ToString();
                if (txt.Length > 1 &&
                    !txt.Trim().ToLower().StartsWith("групп"))
                {
                    dictGroup.Add(txt, j);
                }
            }
            #endregion
            int[] daysOfWeek = new int[7];
            #region 3) собираем номера строк для дней недели
            int d = 0;
            for (int i = nRowGroups + 1; i < tab.Rows.Count; i++)
            {
                string txt = tab.Rows[i][0].ToString();
                if (txt.Length > 1)
                {
                    daysOfWeek[d] = i;
                    d++;
                }
            }
            // строка с кот нач воскресенье
            daysOfWeek[6] = tab.Rows.Count - 1;
            #endregion

            // пробегаем по всем группам и 
            // для каждой группы - по всем дням недели
            // смотрим ПАРЫ
            foreach (var gr in dictGroup)
            {
                // добавляем группу в список
                int IdGroup = GetId4List(Groups, gr.Key);
                int nColGroup = gr.Value;
                for (int i = 0; i < daysOfWeek.Length - 1; i++)
                {
                    int nRowDay = daysOfWeek[i];
                    string txt =
                        tab.Rows[nRowDay][0].ToString();
                    // просматриваем этот день недели
                    for (int j = nRowDay; j < daysOfWeek[i + 1]; j+=2)
                    {
                        string DisR = tab.Rows[j][nColGroup].ToString();
                        string TecR = tab.Rows[j+1][nColGroup].ToString();
                        string CabR = tab.Rows[j][nColGroup+3].ToString();
                        //--------------------------
                        string CabF = tab.Rows[j][nColGroup + 1].ToString();
                        //--------------------------
                        string DisS = tab.Rows[j][nColGroup+2].ToString();
                        string TecS = tab.Rows[j + 1][nColGroup+2].ToString();
                        //====================================================
                        bool IsSecond = false;
                        bool IsFirst = false;
                        //-------------------
                        if (CabF.Length>1)
                        { // мигалка - первая неделя
                            IsFirst = true;
                            var l = new Lesson();
                            l.WeekFlash = WeekFlash.First;
                            l.DayOfWeek = i + 1;
                            l.N = (j - nRowDay) / 2 + 1;
                            l.idCabinet = GetId4List(Cabinets, CabF);
                            l.idTeacher = GetId4List(Teachers, TecR);
                            l.idDiscipline = GetId4List(Disciplines, DisR);
                            l.idGroup = IdGroup;
                            Lessons.Add(l);
                            
                        }
                        if (DisS.Length > 1)
                        { // мигалка - вторая неделя
                            IsSecond = true;
                            var l = new Lesson();
                            l.WeekFlash = WeekFlash.Second;
                            l.DayOfWeek = i + 1;
                            l.N = (j - nRowDay) / 2 + 1;
                            l.idCabinet = GetId4List(Cabinets, CabR);
                            l.idTeacher = GetId4List(Teachers, TecS);
                            l.idDiscipline = GetId4List(Disciplines, DisS);
                            l.idGroup = IdGroup;
                            Lessons.Add(l);
                        }
                        if (!IsFirst && !IsSecond &&
                            DisR.Length>1)
                        { // регулярная пара
                            var l = new Lesson();
                            l.WeekFlash = WeekFlash.Regular;
                            l.DayOfWeek = i + 1;
                            l.N = (j - nRowDay) / 2 + 1;
                            l.idCabinet = GetId4List(Cabinets, CabR);
                            l.idTeacher = GetId4List(Teachers, TecR);
                            l.idDiscipline = GetId4List(Disciplines, DisR);
                            l.idGroup = IdGroup;
                            Lessons.Add(l);
                        }
                        
                    }
                }
            }
        }

        private int GetId4List(List<string> list, string txt)
        {
            int id = list.IndexOf(txt);
            if (id < 0)
            {
                list.Add(txt);
                id = list.Count - 1;
            }
            return id;
        }

    }

    public enum WeekFlash
    {
        Regular = 0,
        First = 1, 
        Second = 2,
    }

    public class Lesson
    {
        public  WeekFlash WeekFlash { get; set; }
        public int DayOfWeek { get; set; }
        public int N { get; set; }
        public int idTeacher { get; set; }
        public int idDiscipline { get; set; }
        public int idCabinet { get; set; }
        public int idGroup { get; set; }
    }

    public class DayView
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public List<LessonView> Lessons { get; set; }
    }

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
                                 Schedule sch)
        {
            DateTime today = DateTime.Today;
            int nowDayWeek = (int) today.DayOfWeek-1;
            // определяем дату на Понедельник
            DateTime monday = today.AddDays(-nowDayWeek);
            //------------------------
            Days = new List<DayView>();
            for (int i = 0; i < 7; i++)
            {
                var lessonsDay = 
                list.Where(l => l.DayOfWeek-1 == i)
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
