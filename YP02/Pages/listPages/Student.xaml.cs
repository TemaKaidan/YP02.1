using Microsoft.EntityFrameworkCore;
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
using System.Xml.Linq;
using YP02.Context;
using YP02.Models;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Student.xaml
    /// </summary>
    public partial class Student : Page
    {
        private bool isMenuCollapsed = false;

        private StudGroupsContext _studGroupsContext = new StudGroupsContext();
        public StudentsContext _studentsContext = new StudentsContext();

        private string userRole;
        public Student(string role)
        {
            InitializeComponent();
            CreateUI();
            LoadGroups();

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

            var groups = _studGroupsContext.StudGroups.ToList();
            foreach (var x in _studentsContext.Students.ToList())
            {
                parrent.Children.Add(new Pages.Item.StudentItem(x, this));
            }
        }

        private void LoadGroups()
        {
            var groups = _studGroupsContext.StudGroups.ToList();

            var allGroupsItem = new StudGroups { id = -1, name = "Все группы" };
            groups.Insert(0, allGroupsItem);

            GroupComboBox.ItemsSource = groups;
            GroupComboBox.SelectedValuePath = "id"; 
        }

        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupComboBox.SelectedValue != null)
            {
                int selectedGroupId = (int)GroupComboBox.SelectedValue;

                if (selectedGroupId == -1)
                {
                    var allStudents = _studentsContext.Students.ToList();
                    DisplayStudents(allStudents);
                }
                else
                {
                    var studentsInGroup = _studentsContext.Students
                        .Where(x => x.studGroupId == selectedGroupId)
                        .ToList();
                    DisplayStudents(studentsInGroup);
                }
            }
        }

        private void DisplayStudents(List<Students> students)
        {
            parrent.Children.Clear();
            foreach (var student in students)
            {
                parrent.Children.Add(new Pages.Item.StudentItem(student, this));
            }
        }

        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            var resultsurname = _studentsContext.Students
                .Where(x => x.surname.ToLower().Contains(searchText));

            var resultname = _studentsContext.Students
                .Where(x => x.name.ToLower().Contains(searchText));

            var resultlastname = _studentsContext.Students
                .Where(x => x.lastname.ToLower().Contains(searchText));

            var combinedResults = resultsurname
                .Union(resultname)
                .Union(resultlastname);

            parrent.Children.Clear();
            foreach (var item in combinedResults)
            {
                parrent.Children.Add(new Pages.Item.StudentItem(item, this));
            }
        }

        private async void SortUp(object sender, RoutedEventArgs e)
        {
            var sortedGroups = await _studGroupsContext.StudGroups
                .OrderBy(x => x.name)
                .ToListAsync();

            parrent.Children.Clear();

            foreach (var group in sortedGroups)
            {
                var studentsInGroup = await _studentsContext.Students
                    .Where(x => x.studGroupId == group.id)
                    .OrderBy(x => x.surname)
                    .ToListAsync();

                foreach (var student in studentsInGroup)
                {
                    parrent.Children.Add(new Pages.Item.StudentItem(student, this));
                }
            }
        }

        private async void SortDown(object sender, RoutedEventArgs e)
        {
            var sortedGroups = await _studGroupsContext.StudGroups
                .OrderByDescending(x => x.name)
                .ToListAsync();

            parrent.Children.Clear();

            foreach (var group in sortedGroups)
            {
                var studentsInGroup = await _studentsContext.Students
                    .Where(x => x.studGroupId == group.id)
                    .OrderBy(x => x.surname)
                    .ToListAsync();

                foreach (var student in studentsInGroup)
                {
                    parrent.Children.Add(new Pages.Item.StudentItem(student, this));
                }
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

        private void Click_Teachers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacher);
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
            MainWindow.init.OpenPages(MainWindow.pages.studentAdd);
        }
    }
}