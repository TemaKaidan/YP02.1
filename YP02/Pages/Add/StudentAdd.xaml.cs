using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для StudentAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования студента.
    /// </summary>
    public partial class StudentAdd : Page
    {
        /// <summary>
        /// Флаг, указывающий, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Ссылка на главную страницу студентов.
        /// </summary>
        public Pages.listPages.Student MainStudent;

        /// <summary>
        /// Модель студента, которая может быть передана для редактирования.
        /// </summary>
        public Models.Students students;

        /// <summary>
        /// Контекст для работы с группами студентов.
        /// </summary>
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        /// <summary>
        /// Конструктор, инициализирует страницу для добавления или редактирования студента.
        /// </summary>
        /// <param name="MainStudent">Главная страница студентов</param>
        /// <param name="students">Модель студента (может быть null для добавления нового студента)</param>
        public StudentAdd(Pages.listPages.Student MainStudent, Models.Students students = null)
        {
            InitializeComponent();
            this.MainStudent = MainStudent;
            this.students = students;

            // Заполнение комбинированного списка групп студентов
            cb_groupe.Items.Clear();
            cb_groupe.ItemsSource = studGroupsContext.StudGroups.ToList();
            cb_groupe.DisplayMemberPath = "name"; // отображать имя группы
            cb_groupe.SelectedValuePath = "id";  // использовать id как значение
        }

        /// <summary>
        /// Метод для добавления нового студента в систему.
        /// Выполняет валидацию введенных данных и добавляет студента в базу данных.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void Add_Student(object sender, RoutedEventArgs e)
        {
            // Проверка фамилии на корректность
            if (string.IsNullOrEmpty(tb_surname.Text) || !Regex.IsMatch(tb_surname.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите фамилию студента\n(буквенные значения русского алфавита)");
                return;
            }

            // Проверка имени на корректность
            if (string.IsNullOrEmpty(tb_name.Text) || !Regex.IsMatch(tb_name.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите имя студента\n(буквенные значения русского алфавита)");
                return;
            }

            // Проверка отчества на корректность
            if (string.IsNullOrEmpty(tb_lastname.Text) || !Regex.IsMatch(tb_lastname.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите отчество студента\n(буквенные значения русского алфавита)");
                return;
            }

            // Проверка выбора группы
            if (string.IsNullOrEmpty(cb_groupe.Text))
            {
                MessageBox.Show("Введите группу студента");
                return;
            }

            // Проверка даты отчисления
            if (string.IsNullOrEmpty(db_dateOfRemand.Text))
            {
                MessageBox.Show("Введите дату отчисления\n(dd.mm.yyyy)");
                return;
            }

            // Если студент не передан, создаем нового
            if (students == null)
            {
                students = new Models.Students
                {
                    surname = tb_surname.Text,
                    name = tb_name.Text,
                    lastname = tb_lastname.Text,
                    studGroupId = (cb_groupe.SelectedItem as StudGroups).id,
                    dateOfRemand = db_dateOfRemand.SelectedDate ?? DateTime.MinValue,
                    userId = 1
                };

                // Добавление нового студента в базу данных
                MainStudent._studentsContext.Students.Add(students);
            }

            // Сохранение изменений в базе данных
            MainStudent._studentsContext.SaveChanges();

            // Переход на страницу студентов
            MainWindow.init.OpenPages(MainWindow.pages.student);
        }

        /// <summary>
        /// Обработчик для кнопки "Отменить", возвращает назад на предыдущую страницу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

        /// <summary>
        /// Метод для переключения состояния меню (свернуто/развернуто).
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            // Создание анимации для изменения ширины меню
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Логика изменения ширины и видимости элементов в меню
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible; // Отображение кнопок
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
                        btn.Visibility = Visibility.Collapsed; // Скрытие кнопок
                    }
                }
            }

            // Устанавливаем продолжительность анимации
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            // Запуск анимации
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключение флага состояния меню
            isMenuCollapsed = !isMenuCollapsed;
        }
    }
}