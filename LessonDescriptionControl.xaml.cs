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

namespace WpfSchedule
{
    /// <summary>
    /// Логика взаимодействия для LessonDescriptionControl.xaml
    /// </summary>
    public partial class LessonDescriptionControl : UserControl
    {
        public LessonDescriptionControl()
        {
            InitializeComponent();
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            mainBorder.Background = Brushes.Bisque;
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            mainBorder.Background = Brushes.Beige;
        }
    }
}
