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

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Mark.xaml
    /// </summary>
    public partial class Mark : Page
    {
        private string userRole; // Роль пользователя, для настройки доступности элементов интерфейса
        private bool isMenuCollapsed = false; // Флаг, указывающий на состояние меню (свернуто или развернуто)

        public MarksContext _marksContext = new MarksContext(); // Контекст для работы с оценками
        private DisciplineProgramsContext _disciplineProgramsContext = new DisciplineProgramsContext(); // Контекст для работы с программами дисциплин
        private StudentsContext _studentsContext = new StudentsContext(); // Контекст для работы с студентами

        // Конструктор, инициализирует страницу с ролью пользователя
        public Mark(string role)
        {
            InitializeComponent(); // Инициализация компонентов
            CreateUI(); // Создание UI элементов

            userRole = role; // Устанавливаем роль пользователя
            ConfigureMenuBasedOnRole(); // Настройка меню в зависимости от роли пользователя
        }

        // Метод для настройки видимости элементов меню в зависимости от роли пользователя
        private void ConfigureMenuBasedOnRole()
        {
            if (userRole == "Студент") // Конфигурация для студента
            {
                StudentsButton.Visibility = Visibility.Visible; // Кнопка "Студенты" видна
                DisciplinesButton.Visibility = Visibility.Visible; // Кнопка "Дисциплины" видна
                AbsencesButton.Visibility = Visibility.Visible; // Кнопка "Отсутствия" видна
                MarksButton.Visibility = Visibility.Visible; // Кнопка "Оценки" видна
                ConsultationResultsButton.Visibility = Visibility.Visible; // Кнопка "Результаты консультаций" видна

                // Скрытие других кнопок
                GroupsButton.Visibility = Visibility.Collapsed;
                ProgramsButton.Visibility = Visibility.Collapsed;
                TeacherWorkloadButton.Visibility = Visibility.Collapsed;
                TeachersButton.Visibility = Visibility.Collapsed;
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;

                AddButton.Visibility = Visibility.Collapsed; // Кнопка "Добавить" скрыта
            }
            else if (userRole == "Преподаватель") // Конфигурация для преподавателя
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

                // Скрытие некоторых кнопок для преподавателей
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Администратор") // Конфигурация для администратора
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

        // Метод для создания UI (элементов интерфейса)
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очистка текущих элементов
            var disciplinePrograms = _disciplineProgramsContext.DisciplinePrograms.ToList(); // Получаем все программы дисциплин
            var students = _studentsContext.Students.ToList(); // Получаем всех студентов

            // Добавляем элементы для каждого типа оценки
            foreach (var x in _marksContext.Marks.ToList())
            {
                parrent.Children.Add(new Pages.Item.MarkItem(x, this)); // Добавляем элемент MarkItem для каждой оценки
            }
        }

        // Метод для сворачивания/разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Проверяем, если меню свернуто, то разворачиваем его
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200; // Разворачиваем меню
                                       // Отображаем все кнопки (кроме кнопки "☰")
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
                MenuPanel.Width = 50; // Сворачиваем меню
                                      // Скрываем все кнопки (кроме кнопки "☰")
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3)); // Длительность анимации
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation); // Запускаем анимацию для изменения ширины меню
            isMenuCollapsed = !isMenuCollapsed; // Меняем состояние флага меню
        }

        // Обработчики кликов для открытия разных страниц

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
            MainWindow.init.OpenPages(MainWindow.pages.absence); // Открытие страницы с отсутствиями
        }

        private void Click_Teachers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacher); // Открытие страницы преподавателей
        }

        private void Click_ConsultationResults(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult); // Открытие страницы с результатами консультаций
        }

        private void Click_LessonTypes(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonType); // Открытие страницы с типами занятий
        }

        private void Click_Roles(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.role); // Открытие страницы с ролями
        }

        private void Click_Users(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.user); // Открытие страницы с пользователями
        }

        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.markAdd); // Открытие страницы для добавления оценки
        }
    }
}
