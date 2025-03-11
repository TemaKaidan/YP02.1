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
using YP02.Context;
using YP02.Models;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для TeachersLoad.xaml
    /// </summary>
    public partial class TeachersLoad : Page
    {
        private string userRole; // Переменная для хранения роли пользователя
        private bool isMenuCollapsed = false; // Переменная для отслеживания состояния меню (свернуто/развернуто)
        public TeachersLoadContext _teachersLoadContext = new TeachersLoadContext(); // Контекст данных для нагрузки преподавателей

        private TeachersContext _teachersContext = new TeachersContext(); // Контекст данных для преподавателей
        private DisciplinesContext _disciplinesContext = new DisciplinesContext(); // Контекст данных для дисциплин
        private StudGroupsContext _studGroupsContext = new StudGroupsContext(); // Контекст данных для учебных групп

        public TeachersLoad(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            CreateUI(); // Создание UI элементов

            userRole = role; // Присваиваем роль пользователя
            ConfigureMenuBasedOnRole(); // Настройка видимости элементов меню в зависимости от роли
        }

        // Метод для настройки меню в зависимости от роли пользователя
        private void ConfigureMenuBasedOnRole()
        {
            if (userRole == "Студент")
            {
                // Видимые элементы для роли "Студент"
                StudentsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Скрытые элементы для роли "Студент"
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
                // Видимые элементы для роли "Преподаватель"
                StudentsButton.Visibility = Visibility.Visible;
                GroupsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                ProgramsButton.Visibility = Visibility.Visible;
                TeacherWorkloadButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                TeachersButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Скрытые элементы для роли "Преподаватель"
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

        // Метод для создания UI с нагрузкой преподавателей
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очищаем текущие элементы UI

            var teachers = _teachersContext.Teachers.ToList(); // Получаем список преподавателей
            var disciplines = _disciplinesContext.Disciplines.ToList(); // Получаем список дисциплин
            var studGroup = _studGroupsContext.StudGroups.ToList(); // Получаем список учебных групп

            // Для каждого элемента из нагрузки преподавателей
            foreach (var x in _teachersLoadContext.TeachersLoad.ToList())
            {
                parrent.Children.Add(new Pages.Item.TeacherLoadItem(x, this)); // Добавляем элемент в UI
            }
        }

        // Метод для поиска преподавателей по ФИО
        private async void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получаем текст для поиска в нижнем регистре

            // Ищем преподавателей по фамилии, имени и отчеству
            var foundTeachers = await _teachersContext.Teachers
                .Where(t => t.surname.ToLower().Contains(searchText) ||
                             t.name.ToLower().Contains(searchText) ||
                             t.lastname.ToLower().Contains(searchText))
                .ToListAsync();

            parrent.Children.Clear(); // Очищаем текущие элементы UI

            // Для каждого найденного преподавателя
            foreach (var teacher in foundTeachers)
            {
                // Получаем нагрузку для каждого преподавателя
                var teacherLoads = await _teachersLoadContext.TeachersLoad
                    .Include(t => t.Teacher)
                    .Where(t => t.teacherId == teacher.id)
                    .ToListAsync();

                // Добавляем нагрузку преподавателя в UI
                foreach (var teacherLoad in teacherLoads)
                {
                    parrent.Children.Add(new Pages.Item.TeacherLoadItem(teacherLoad, this)); // Добавляем элемент в UI
                }
            }
        }

        // Метод для анимации и переключения состояния меню (свернуто/развернуто)
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation(); // Создаем анимацию для изменения ширины меню

            if (isMenuCollapsed) // Если меню свернуто
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200; // Устанавливаем ширину меню
                foreach (UIElement element in MenuPanel.Children) // Для каждого элемента в меню
                {
                    if (element is Button btn && btn.Content.ToString() != "☰") // Если это кнопка, но не кнопка меню
                    {
                        btn.Visibility = Visibility.Visible; // Делаем кнопку видимой
                    }
                }
            }
            else // Если меню развернуто
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children) // Для каждого элемента в меню
                {
                    if (element is Button btn && btn.Content.ToString() != "☰") // Если это кнопка, но не кнопка меню
                    {
                        btn.Visibility = Visibility.Collapsed; // Прячем кнопку
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3)); // Устанавливаем продолжительность анимации
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation); // Запускаем анимацию
            isMenuCollapsed = !isMenuCollapsed; // Переключаем состояние меню
        }

        // Методы для перехода на другие страницы (клик по кнопкам)
        private void Click_Student(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.student); // Открытие страницы студентов
        }

        private void Click_Groups(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.group); // Открытие страницы групп
        }

        private void Click_Disciplines(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.discipline); // Открытие страницы дисциплин
        }

        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram); // Открытие страницы программ дисциплин
        }

        private void Click_Consultations(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultation); // Открытие страницы консультаций
        }

        private void Click_Absences(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absence); // Открытие страницы пропусков
        }

        private void Click_Teachers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacher); // Открытие страницы преподавателей
        }

        private void Click_Marks(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.marks); // Открытие страницы оценок
        }

        private void Click_ConsultationResults(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult); // Открытие страницы результатов консультаций
        }

        private void Click_LessonTypes(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonType); // Открытие страницы типов занятий
        }

        private void Click_Roles(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.role); // Открытие страницы ролей
        }

        private void Click_Users(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.user); // Открытие страницы пользователей
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoadAdd); // Открытие страницы добавления нагрузки преподавателей
        }
    }
}