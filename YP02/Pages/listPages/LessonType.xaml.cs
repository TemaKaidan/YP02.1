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
    /// Логика взаимодействия для LessonType.xaml
    /// </summary>
    public partial class LessonType : Page
    {
        private string userRole; // Роль пользователя, определяет доступные элементы интерфейса
        private bool isMenuCollapsed = false; // Флаг для отслеживания состояния меню (свернуто или развернуто)
        public LessonTypesContext _lessonTypesContext = new LessonTypesContext(); // Контекст базы данных для работы с типами занятий

        // Конструктор, принимающий роль пользователя и инициализирующий страницу
        public LessonType(string role)
        {
            InitializeComponent();
            CreateUI(); // Создает элементы интерфейса

            userRole = role; // Устанавливает роль пользователя
            ConfigureMenuBasedOnRole(); // Настроить меню в зависимости от роли пользователя
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

                // Скрыть другие элементы меню для студентов
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

                // Скрыть другие элементы меню для преподавателей
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Администратор") // Конфигурация для администратора
            {
                // Администратор видит все элементы меню
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
            parrent.Children.Clear(); // Очищаем текущие элементы
                                      // Для каждого типа занятия в базе данных создаем элемент интерфейса
            foreach (var x in _lessonTypesContext.LessonTypes.ToList())
            {
                parrent.Children.Add(new LessonTypeItem(x, this)); // Добавляем новый элемент в контейнер
            }
        }

        // Метод для сворачивания/разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Проверяем, свернуто ли меню
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200; // Разворачиваем меню
                                       // Проходим по всем элементам меню и отображаем их
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
                                      // Проходим по всем элементам меню и скрываем их
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3)); // Длительность анимации
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation); // Запускаем анимацию
            isMenuCollapsed = !isMenuCollapsed; // Меняем состояние меню
        }

        // Метод для открытия страницы со студентами
        private void Click_Student(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.student);
        }

        // Метод для открытия страницы с группами
        private void Click_Groups(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.group);
        }

        // Метод для открытия страницы с дисциплинами
        private void Click_Disciplines(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.discipline);
        }

        // Метод для открытия страницы с программами дисциплин
        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
        }

        // Метод для открытия страницы с нагрузкой преподавателей
        private void Click_TeachersLoad(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
        }

        // Метод для открытия страницы с консультациями
        private void Click_Consultations(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultation);
        }

        // Метод для открытия страницы с отсутствиями
        private void Click_Absences(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absence);
        }

        // Метод для открытия страницы с преподавателями
        private void Click_Teachers(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacher);
        }

        // Метод для открытия страницы с оценками
        private void Click_Marks(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.marks);
        }

        // Метод для открытия страницы с результатами консультаций
        private void Click_ConsultationResults(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
        }

        // Метод для открытия страницы с ролями
        private void Click_Roles(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.role);
        }

        // Метод для открытия страницы с пользователями
        private void Click_Users(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.user);
        }

        // Метод для открытия страницы с добавлением типа занятия
        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonTypeAdd);
        }
    }
}