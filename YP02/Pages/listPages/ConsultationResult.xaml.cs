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
    /// Логика взаимодействия для ConsultationResult.xaml
    /// </summary>
    public partial class ConsultationResult : Page
    {
        // Переменные для роли пользователя, состояния меню и контекста данных
        private string userRole;
        private bool isMenuCollapsed = false;
        public ConsultationResultsContext _consultationResultsContext = new ConsultationResultsContext();
        private ConsultationsContext _consultationContext = new ConsultationsContext();
        private StudentsContext _studentsContext = new StudentsContext();

        // Конструктор страницы ConsultationResult с передачей роли пользователя
        public ConsultationResult(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            CreateUI(); // Создание пользовательского интерфейса
            userRole = role; // Присваиваем роль пользователя
            ConfigureMenuBasedOnRole(); // Настройка меню в зависимости от роли
        }

        // Настройка видимости кнопок меню в зависимости от роли пользователя
        private void ConfigureMenuBasedOnRole()
        {
            if (userRole == "Студент")
            {
                // Студент видит эти кнопки
                StudentsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Студент не видит эти кнопки
                GroupsButton.Visibility = Visibility.Collapsed;
                ProgramsButton.Visibility = Visibility.Collapsed;
                TeacherWorkloadButton.Visibility = Visibility.Collapsed;
                TeachersButton.Visibility = Visibility.Collapsed;
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;

                // Кнопка добавления консультаций скрыта
                AddButton.Visibility = Visibility.Collapsed;
            }
            else if (userRole == "Преподаватель")
            {
                // Преподаватель видит эти кнопки
                StudentsButton.Visibility = Visibility.Visible;
                GroupsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                ProgramsButton.Visibility = Visibility.Visible;
                TeacherWorkloadButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                TeachersButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Преподаватель не видит эти кнопки
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

        // Создание UI элементов для страницы ConsultationResult
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очищаем существующие элементы
            var _consultation = _consultationContext.Consultations.ToList();
            var students = _studentsContext.Students.ToList();
            foreach (var x in _consultationResultsContext.ConsultationResults.ToList())
            {
                parrent.Children.Add(new Pages.Item.ConsultationResultItem(x, this)); // Добавляем элементы для каждой консультации
            }
        }

        // Переключение состояния меню (сворачивание/разворачивание)
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Если меню свернуто, то разворачиваем его
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;

                // Делаем видимыми кнопки меню
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else // Если меню развернуто, то сворачиваем его
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;

                // Скрываем кнопки меню
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Анимация изменения ширины меню
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed; // Переключаем состояние меню
        }

        // Обработчики кликов для перехода на различные страницы
        private void Click_Student(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.student); }
        private void Click_Groups(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.group); }
        private void Click_Disciplines(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.discipline); }
        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram); }
        private void Click_TeachersLoad(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.teachersLoad); }
        private void Click_Consultations(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.consultation); }
        private void Click_Absences(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.absence); }
        private void Click_Teachers(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.teacher); }
        private void Click_Marks(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.marks); }
        private void Click_LessonTypes(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.lessonType); }
        private void Click_Roles(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.role); }
        private void Click_Users(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.user); }

        // Открытие страницы добавления результатов консультации
        private void Click_Add(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResultAdd);
        }
    }
}