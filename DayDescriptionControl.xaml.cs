using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfSchedule.ModuleSchedule;

namespace WpfSchedule
{
    /// <summary>
    /// Логика взаимодействия для DayDescriptionControl.xaml
    /// </summary>
    public partial class DayDescriptionControl : UserControl
    {
        public DayDescriptionControl()
        {
            InitializeComponent();
        }

        public DayDescriptionControl(DayView day)
        {
            InitializeComponent();
            // выставляем день недели
            this.labelDayOfWeek.Content = day.Name;
            // выставляем дату
            this.labelDate.Content = day.Date;
            // выставляем занятия
            this.panelLessons.Children.Clear();
            foreach (var l in day.Lessons)
            {
                var lessonCtrl = new LessonDescriptionControl();
                lessonCtrl.labelCabinet.Content = l.Cabinet;
                lessonCtrl.labelDiscipline.Text = l.Discipline;
                lessonCtrl.labelTeacher.Content = l.Teacher;
                lessonCtrl.labelN.Content = l.N;
                lessonCtrl.textBlockTime.Text = l.Time;
                lessonCtrl.Margin = new Thickness(4);
                panelLessons.Children.Add(lessonCtrl);
            }
        }

        private void mainBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Style style = (Style) FindResource("dropShadow");
            mainBorder.Style = style;
        }

        private void mainBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBorder.Style = null;
        }
    }
}
