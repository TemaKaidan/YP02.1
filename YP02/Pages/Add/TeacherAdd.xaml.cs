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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для TeacherAdd.xaml
    /// </summary>
    public partial class TeacherAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Teacher MainTeacher;
        public Models.Teachers teachers;

        public TeacherAdd(Pages.listPages.Teacher MainTeacher, Models.Teachers teachers = null)
        {
            InitializeComponent();
            this.MainTeacher = MainTeacher;
            this.teachers = teachers;
        }

        private void Add_Teacher(object sender, RoutedEventArgs e)
        {
            if (teachers == null)
            {
                teachers = new Models.Teachers
                {
                    surname = tb_surName.Text,
                    name = tb_name.Text,
                    lastname = tb_lastName.Text,
                    userId = 2
                };
                MainTeacher._teachersContext.Teachers.Add(teachers);
            }
            MainTeacher._teachersContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.teacher);
        }
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }
    }
}
