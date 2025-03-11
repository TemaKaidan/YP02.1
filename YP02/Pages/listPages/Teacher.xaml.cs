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
using YP02.Context;
using YP02.Models;
using YP02.Pages.Item;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Teacher.xaml
    /// </summary>
    public partial class Teacher : Page
    {
        private string userRole;
        private bool isMenuCollapsed = false;
        public TeachersContext _teachersContext = new TeachersContext();

        public Teacher(string role)
        {
            InitializeComponent();
            CreateUI();

            userRole = role;
            ConfigureMenuBasedOnRole();
        }

        private void ConfigureMenuBasedOnRole()
        {
            if (userRole == "Студент")
            {
                // Видит
                StudentsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Не видит
                GroupsButton.Visibility = Visibility.Collapsed;
                ProgramsButton.Visibility = Visibility.Collapsed;
                TeacherWorkloadButton.Visibility = Visibility.Collapsed;
                TeachersButton.Visibility = Visibility.Collapsed;
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;

                AddButton.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Преподаватель")
            {
                StudentsButton.Visibility = Visibility.Visible;
                GroupsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                ProgramsButton.Visibility = Visibility.Visible;
                TeacherWorkloadButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                TeachersButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Администратор")
            {
                // Администратор видит все кнопки
                StudentsButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                GroupsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                ProgramsButton.Visibility = Visibility.Visible;
                TeacherWorkloadButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                TeachersButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;
                LessonTypesButton.Visibility = Visibility.Visible;
                RolesButton.Visibility = Visibility.Visible;
                UsersButton.Visibility = Visibility.Visible;
            }
        }

        private void CreateUI()
        {
            parrent.Children.Clear();
            foreach (var x in _teachersContext.Teachers.ToList())
            {
                parrent.Children.Add(new TeacherItem(x, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            var resultsurname = _teachersContext.Teachers
                .Where(x => x.surname.ToLower().Contains(searchText));

            var resultname = _teachersContext.Teachers
                .Where(x => x.name.ToLower().Contains(searchText));

            var resultlastname = _teachersContext.Teachers
                .Where(x => x.lastname.ToLower().Contains(searchText));

            var combinedResults = resultsurname
                .Union(resultname)
                .Union(resultlastname);

            parrent.Children.Clear();
            foreach (var item in combinedResults)
            {
                parrent.Children.Add(new Pages.Item.TeacherItem(item, this));
            }
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

        private void Click_Student(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.student);
        }

        private void Click_Groups(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.group);
        }

        private void Click_Disciplines(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.discipline);
        }

        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
        }

        private void Click_TeachersLoad(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
        }

        private void Click_Consultations(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultation);
        }

        private void Click_Absences(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absence);
        }

        private void Click_Marks(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.marks);
        }

        private void Click_ConsultationResults(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
        }

        private void Click_LessonTypes(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonType);
        }

        private void Click_Roles(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.role);
        }

        private void Click_Users(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.user);
        }
        
        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacherAdd);
        }
    }
}
