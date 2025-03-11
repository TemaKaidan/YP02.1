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
using YP02.Pages.Item;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для User.xaml
    /// </summary>
    public partial class User : Page
    {
        private bool isMenuCollapsed = false; // Переменная для отслеживания состояния меню (свернуто/развернуто)
        public UsersContext _usersContext = new UsersContext(); // Контекст данных пользователей

        private string userRole; // Переменная для хранения роли пользователя

        public User(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            CreateUI(); // Создание UI элементов

            userRole = role; // Присваиваем роль пользователя
            ConfigureMenuBasedOnRole(); // Настройка видимости элементов меню в зависимости от роли
        }

        // Метод для создания UI с элементами пользователей
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очищаем текущие элементы UI

            // Добавляем элементы для каждого пользователя из контекста
            foreach (var x in _usersContext.Users.ToList())
            {
                parrent.Children.Add(new UserItem(x, this)); // Добавляем элемент в UI
            }
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

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.userAdd); // Открытие страницы добавления пользователей
        }
    }
}