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
        private string userRole; // Переменная для хранения роли пользователя
        private bool isMenuCollapsed = false; // Переменная для проверки состояния меню (свернуто/развернуто)
        public TeachersContext _teachersContext = new TeachersContext(); // Контекст данных для преподавателей

        public Teacher(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            CreateUI(); // Создание UI элементов

            userRole = role; // Присваиваем роль пользователя
            ConfigureMenuBasedOnRole(); // Настройка меню в зависимости от роли
        }

        // Метод для настройки видимости элементов меню в зависимости от роли
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

        // Метод для создания UI с преподавателями
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очищаем текущие элементы UI
            foreach (var x in _teachersContext.Teachers.ToList()) // Для каждого преподавателя из контекста
            {
                parrent.Children.Add(new TeacherItem(x, this)); // Добавляем элемент преподавателя в UI
            }
        }

        // Метод для поиска преподавателей по имени, фамилии и отчеству
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Получаем текст для поиска в нижнем регистре

            // Ищем преподавателей по фамилии, имени и отчеству
            var resultsurname = _teachersContext.Teachers
                .Where(x => x.surname.ToLower().Contains(searchText));

            var resultname = _teachersContext.Teachers
                .Where(x => x.name.ToLower().Contains(searchText));

            var resultlastname = _teachersContext.Teachers
                .Where(x => x.lastname.ToLower().Contains(searchText));

            // Объединяем результаты поиска
            var combinedResults = resultsurname
                .Union(resultname)
                .Union(resultlastname);

            parrent.Children.Clear(); // Очищаем текущие элементы UI
            foreach (var item in combinedResults) // Для каждого найденного преподавателя
            {
                parrent.Children.Add(new Pages.Item.TeacherItem(item, this)); // Добавляем его в UI
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

        private void Click_TeachersLoad(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad); // Открытие страницы нагрузки преподавателей
        }

        private void Click_Consultations(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultation); // Открытие страницы консультаций
        }

        private void Click_Absences(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absence); // Открытие страницы пропусков
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
            MainWindow.init.OpenPages(MainWindow.pages.teacherAdd); // Открытие страницы добавления преподавателя
        }
    }
}
