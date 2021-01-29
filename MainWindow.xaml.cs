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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//--------------------------
using WpfSchedule.ModuleSchedule;

namespace WpfSchedule
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private Schedule schedule;
        private int ShiftWeek;
        private ModelViewSchedule viewSchedule;
        private string currentGroup;

        public MainWindow()
        {
            InitializeComponent();
            ShiftWeek = 0;
            currentGroup = string.Empty;
            this.Loaded += MainWindow_Loaded;            
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            schedule = Schedule.LoadJson();
            labelWeekState.Content =
                schedule.GetCurrentWeekName(ShiftWeek);
            r1.IsChecked = true;
        }

        private void ActivateCourse(string v)
        {
            var list = schedule
                .Groups
                .Where(s => s.Contains("-"+v))
                .Select(s => s);
            panelGroups.Children.Clear();
            foreach (var g in list)
            {
                var b = new Button();
                b.Content = g;
                b.Padding = new Thickness(0);
                b.Margin = new Thickness(1);
                b.FontSize = 12;
                b.Click += BGroup_Click;
                panelGroups.Children.Add(b);
            }
        }

        private void BGroup_Click(object sender, RoutedEventArgs e)
        {
            var b = (Button) sender;
            currentGroup = b.Content.ToString();
            labelGroup.Content = currentGroup;
            ShowCurrentSchedule();
        }

        private void ShowCurrentSchedule()
        {
            labelWeekState.Content =
                schedule.GetCurrentWeekName(ShiftWeek);
            if (string.IsNullOrEmpty(currentGroup))
                return;
            viewSchedule = schedule
                .GetGroupSchedule(currentGroup);
            // показываем расписание в рабочей области
            panelSchedule.Children.Clear();
            foreach (var d in viewSchedule.Days)
            {
                var dayCtrl = new DayDescriptionControl(d);
                dayCtrl.Width = 320;
                dayCtrl.Margin = new Thickness(4);
                panelSchedule.Children.Add(dayCtrl);
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            var rb = (Fluent.RadioButton)sender;
            if (rb.IsChecked==true)
            {
                string st = rb
                    .Name
                    .TrimStart('r');
                ActivateCourse(st);
            }
        }

        private void ribbonWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            myScrollViewer.Height =
                this.ActualHeight 
                - ribbon.ActualHeight;
        }

        private void btnWeekLeft_Click(object sender, RoutedEventArgs e)
        {
            ShiftWeek -= 1;
            ShowCurrentSchedule();
        }

        private void btnWeekRight_Click(object sender, RoutedEventArgs e)
        {
            ShiftWeek += 1;
            ShowCurrentSchedule();
        }
    }
}
