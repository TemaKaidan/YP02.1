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
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для TeacherEdit.xaml
    /// </summary>
    public partial class TeacherEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Teacher MainTeacher;
        public Models.Teachers teachers;

        public TeacherEdit(Pages.listPages.Teacher MainTeacher, Models.Teachers teachers = null)
        {
            InitializeComponent();
            this.MainTeacher = MainTeacher;
            this.teachers = teachers;

            tb_surName.Text = teachers.surname;
            tb_name.Text = teachers.name;
            tb_lastName.Text = teachers.lastname;
            tb_login.Text = teachers.login;
            tb_password.Text = teachers.password;
        }

        private void Edit_Teacher(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_surName.Text))
            {
                MessageBox.Show("Введите фамилию");
                return;
            }
            if (string.IsNullOrEmpty(tb_name.Text))
            {
                MessageBox.Show("Введите имя");
                return;
            }
            if (string.IsNullOrEmpty(tb_lastName.Text))
            {
                MessageBox.Show("Введите отчество");
                return;
            }
            if (string.IsNullOrEmpty(tb_login.Text))
            {
                MessageBox.Show("Введите логин");
                return;
            }
            if (string.IsNullOrEmpty(tb_password.Text))
            {
                MessageBox.Show("Введите пароль");
                return;
            }

            Models.Teachers mt = MainTeacher._teachersContext.Teachers.FirstOrDefault(x=>x.id == teachers.id);
            mt.surname = tb_surName.Text;
            mt.name = tb_name.Text;
            mt.lastname = tb_lastName.Text;
            mt.login = tb_login.Text;
            mt.password = tb_password.Text;

            MainTeacher._teachersContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.teacher);
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

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
