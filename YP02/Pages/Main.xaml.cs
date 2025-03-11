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
using YP02.Pages.listPages;

namespace YP02.Pages
{
    /// <summary>
    /// Логика взаимодействия для Main.xaml
    /// </summary>
    public partial class Main : Page
    {
        private bool isMenuCollapsed = false; // Переменная для отслеживания состояния меню (свёрнуто/раскрыто)
        private string userRole; // Роль пользователя, переданная при создании страницы

        // Конструктор, принимающий роль пользователя и инициализирующий страницу
        public Main(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            userRole = role; // Сохранение роли пользователя
            ConfigureMenuBasedOnRole(); // Настройка меню в зависимости от роли пользователя
        }

        // Метод настройки видимости элементов меню в зависимости от роли пользователя
        private void ConfigureMenuBasedOnRole()
        {
            // Для Студента
            if (userRole == "Студент")
            {
                StudentsButton.Visibility = Visibility.Visible; // Студент видит кнопку студентов
                DisciplinesButton.Visibility = Visibility.Visible; // Студент видит дисциплины
                AbsencesButton.Visibility = Visibility.Visible; // Студент видит пропуски
                MarksButton.Visibility = Visibility.Visible; // Студент видит оценки
                ConsultationResultsButton.Visibility = Visibility.Visible; // Студент видит результаты консультаций

                // Студент не видит следующие элементы
                GroupsButton.Visibility = Visibility.Collapsed;
                ProgramsButton.Visibility = Visibility.Collapsed;
                TeacherWorkloadButton.Visibility = Visibility.Collapsed;
                TeachersButton.Visibility = Visibility.Collapsed;
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
            // Для Преподавателя
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

                // Преподаватель не видит следующие элементы
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;
            }
            // Для Администратора
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

        // Метод для переключения состояния меню (свёрнуто/раскрыто)
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                // Показываем все кнопки в меню, кроме кнопки меню
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
                // Скрываем все кнопки в меню, кроме кнопки меню
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
            isMenuCollapsed = !isMenuCollapsed; // Переключаем состояние меню
        }

        // Методы для перехода на различные страницы при нажатии на кнопки меню
        private void Click_Student(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.student); }
        private void Click_Groups(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.group); }
        private void Click_Disciplines(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.discipline); }
        private void Click_DisciplinePrograms(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram); }
        private void Click_TeachersLoad(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.teachersLoad); }
        private void Click_Consultations(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.consultation); }
        private void Click_Absences(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.absence); }
        private void Click_Teachers(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.teacher); }
        private void Click_Marks(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.marks); }
        private void Click_ConsultationResults(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.consultationResult); }
        private void Click_LessonTypes(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.lessonType); }
        private void Click_Roles(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.role); }
        private void Click_Users(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.user); }
    }
}