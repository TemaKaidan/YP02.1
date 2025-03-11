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
using YP02.Pages.Item;

namespace YP02.Pages.listPages
{
    /// <summary>
    /// Логика взаимодействия для Absence.xaml
    /// </summary>
    public partial class Absence : Page
    {
        // Переменные для роли пользователя и состояния меню
        private string userRole;
        private bool isMenuCollapsed = false;

        // Контексты для работы с отсутствиями и студентами
        public AbsencesContext _absencesContext = new AbsencesContext();
        public StudentsContext _studentsContext = new StudentsContext();

        // Конструктор для инициализации страницы Absence с ролью пользователя
        public Absence(string role)
        {
            InitializeComponent(); // Инициализация компонентов страницы
            CreateUI(); // Создание пользовательского интерфейса

            userRole = role; // Присваиваем роль пользователя
            ConfigureMenuBasedOnRole(); // Настроить меню в зависимости от роли
        }

        // Настройка видимости меню в зависимости от роли пользователя
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

                // Кнопка добавления отсутствий скрыта
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

        // Метод для создания пользовательского интерфейса и добавления элементов
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очищаем существующие элементы на странице
            foreach (var x in _absencesContext.Absences.ToList()) // Добавляем все отсутствия
            {
                parrent.Children.Add(new AbsenceItem(x, this)); // Добавляем элемент AbsenceItem
            }
        }

        // Асинхронный метод для поиска студентов по ФИО
        private async void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower(); // Преобразуем текст поиска в нижний регистр

            // Поиск студентов по фамилии, имени и отчеству
            var foundStudents = await _studentsContext.Students
                .Where(t => t.surname.ToLower().Contains(searchText) ||
                             t.name.ToLower().Contains(searchText) ||
                             t.lastname.ToLower().Contains(searchText))
                .ToListAsync();

            parrent.Children.Clear(); // Очищаем текущие элементы на странице

            foreach (var student in foundStudents) // Для каждого найденного студента
            {
                var studentAbsences = _absencesContext.Absences
                    .Where(a => a.studentId == student.id) // Получаем отсутствия для студента
                    .ToList();

                foreach (var absence in studentAbsences) // Для каждого отсутствия
                {
                    parrent.Children.Add(new Pages.Item.AbsenceItem(absence, this)); // Добавляем элемент отсутствия на страницу
                }
            }
        }

        // Метод для переключения состояния меню (сворачивание/разворачивание)
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
        private void Click_Teachers(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.teacher); }
        private void Click_Marks(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.marks); }
        private void Click_ConsultationResults(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.consultationResult); }
        private void Click_LessonTypes(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.lessonType); }
        private void Click_Roles(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.role); }
        private void Click_Users(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.user); }
        private void Click_Add(object sender, RoutedEventArgs e) { MainWindow.init.OpenPages(MainWindow.pages.absenceAdd); }
    }
}
