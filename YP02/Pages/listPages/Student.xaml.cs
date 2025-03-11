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
        private bool isMenuCollapsed = false; // Флаг, который указывает, свернуто ли меню

        private StudGroupsContext _studGroupsContext = new StudGroupsContext(); // Контекст для работы с группами студентов
        public StudentsContext _studentsContext = new StudentsContext(); // Контекст для работы с данными студентов

        private string userRole; // Роль пользователя

        // Конструктор, инициализирует страницу с ролью пользователя и настраивает меню
        public Student(string role)
        {
            InitializeComponent(); // Инициализация компонентов
            CreateUI(); // Создание UI элементов
            LoadGroups(); // Загрузка групп студентов

            userRole = role; // Устанавливаем роль пользователя
            ConfigureMenuBasedOnRole(); // Настроить меню в зависимости от роли пользователя
        }

        // Метод для настройки видимости элементов меню в зависимости от роли пользователя
        private void ConfigureMenuBasedOnRole()
        {
            if (userRole == "Студент")
            {
                // Кнопки, которые видит студент
                StudentsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Кнопки, которые не видит студент
                GroupsButton.Visibility = Visibility.Collapsed;
                ProgramsButton.Visibility = Visibility.Collapsed;
                TeacherWorkloadButton.Visibility = Visibility.Collapsed;
                TeachersButton.Visibility = Visibility.Collapsed;
                LessonTypesButton.Visibility = Visibility.Collapsed;
                RolesButton.Visibility = Visibility.Collapsed;
                UsersButton.Visibility = Visibility.Collapsed;

                AddButton.Visibility = Visibility.Collapsed; // Кнопка "Добавить" скрыта
            }
            else if (userRole == "Преподаватель")
            {
                // Кнопки, которые видит преподаватель
                StudentsButton.Visibility = Visibility.Visible;
                GroupsButton.Visibility = Visibility.Visible;
                DisciplinesButton.Visibility = Visibility.Visible;
                ProgramsButton.Visibility = Visibility.Visible;
                TeacherWorkloadButton.Visibility = Visibility.Visible;
                AbsencesButton.Visibility = Visibility.Visible;
                TeachersButton.Visibility = Visibility.Visible;
                MarksButton.Visibility = Visibility.Visible;
                ConsultationResultsButton.Visibility = Visibility.Visible;

                // Кнопки, которые не видит преподаватель
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

        // Метод для создания UI (элементов интерфейса)
        private void CreateUI()
        {
            parrent.Children.Clear(); // Очистка текущих элементов

            // Загружаем студентов и добавляем их в UI
            var groups = _studGroupsContext.StudGroups.ToList();
            foreach (var student in _studentsContext.Students.ToList())
            {
                parrent.Children.Add(new Pages.Item.StudentItem(student, this)); // Добавление каждого студента
            }
        }

        // Метод для загрузки групп студентов в комбобокс
        private void LoadGroups()
        {
            var groups = _studGroupsContext.StudGroups.ToList();

            // Добавляем элемент "Все группы" в начало списка
            var allGroupsItem = new StudGroups { id = -1, name = "Все группы" };
            groups.Insert(0, allGroupsItem);

            // Устанавливаем источник данных для комбобокса
            GroupComboBox.ItemsSource = groups;
            GroupComboBox.SelectedValuePath = "id";
        }

        // Обработчик изменения выбранной группы
        private void GroupComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GroupComboBox.SelectedValue != null)
            {
                int selectedGroupId = (int)GroupComboBox.SelectedValue;

                // Если выбрана группа "Все группы", показываем всех студентов
                if (selectedGroupId == -1)
                {
                    var allStudents = _studentsContext.Students.ToList();
                    DisplayStudents(allStudents);
                }
                else
                {
                    // Иначе показываем студентов, относящихся к выбранной группе
                    var studentsInGroup = _studentsContext.Students
                        .Where(x => x.studGroupId == selectedGroupId)
                        .ToList();
                    DisplayStudents(studentsInGroup);
                }
            }
        }

        // Метод для отображения студентов
        private void DisplayStudents(List<Students> students)
        {
            parrent.Children.Clear(); // Очистка текущих элементов
            foreach (var student in students)
            {
                parrent.Children.Add(new Pages.Item.StudentItem(student, this)); // Добавление студентов
            }
        }

        // Метод для поиска студентов по фамилии, имени или отчеству
        private void KeyDown_Search(object sender, KeyEventArgs e)
        {
            string searchText = search.Text.ToLower();

            // Поиск студентов по фамилии, имени и отчеству
            var resultsurname = _studentsContext.Students
                .Where(x => x.surname.ToLower().Contains(searchText));

            var resultname = _studentsContext.Students
                .Where(x => x.name.ToLower().Contains(searchText));

            var resultlastname = _studentsContext.Students
                .Where(x => x.lastname.ToLower().Contains(searchText));

            var combinedResults = resultsurname
                .Union(resultname)
                .Union(resultlastname);

            // Отображаем результаты поиска
            parrent.Children.Clear();
            foreach (var item in combinedResults)
            {
                parrent.Children.Add(new Pages.Item.StudentItem(item, this));
            }
        }

        // Метод для сортировки студентов по группам (по возрастанию)
        private async void SortUp(object sender, RoutedEventArgs e)
        {
            var sortedGroups = await _studGroupsContext.StudGroups
                .OrderBy(x => x.name)
                .ToListAsync();

            parrent.Children.Clear(); // Очистка элементов

            // Сортировка студентов по фамилии в каждой группе
            foreach (var group in sortedGroups)
            {
                var studentsInGroup = await _studentsContext.Students
                    .Where(x => x.studGroupId == group.id)
                    .OrderBy(x => x.surname)
                    .ToListAsync();

                foreach (var student in studentsInGroup)
                {
                    parrent.Children.Add(new Pages.Item.StudentItem(student, this)); // Добавление студентов
                }
            }
        }

        // Метод для сортировки студентов по группам (по убыванию)
        private async void SortDown(object sender, RoutedEventArgs e)
        {
            var sortedGroups = await _studGroupsContext.StudGroups
                .OrderByDescending(x => x.name)
                .ToListAsync();

            parrent.Children.Clear(); // Очистка элементов

            // Сортировка студентов по фамилии в каждой группе
            foreach (var group in sortedGroups)
            {
                var studentsInGroup = await _studentsContext.Students
                    .Where(x => x.studGroupId == group.id)
                    .OrderBy(x => x.surname)
                    .ToListAsync();

                foreach (var student in studentsInGroup)
                {
                    parrent.Children.Add(new Pages.Item.StudentItem(student, this)); // Добавление студентов
                }
            }
        }

        // Метод для сворачивания/разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Если меню свернуто, развернуть его
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200; // Устанавливаем ширину меню
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
                MenuPanel.Width = 50; // Сужаем меню
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

        private void Click_Marks(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.marks); // Открытие страницы с оценками
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
            MainWindow.init.OpenPages(MainWindow.pages.studentAdd); // Открытие страницы добавления студента
        }
    }
}