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
using YP02.Log;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для TeacherAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования информации о преподавателе.
    /// </summary>
    public partial class TeacherAdd : Page
    {
        /// <summary>
        /// Флаг, указывающий, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Ссылка на главную страницу преподавателей.
        /// </summary>
        public Pages.listPages.Teacher MainTeacher;

        /// <summary>
        /// Модель преподавателя, которая может быть передана для редактирования.
        /// </summary>
        public Models.Teachers teachers;

        /// <summary>
        /// Конструктор, инициализирует страницу для добавления или редактирования преподавателя.
        /// </summary>
        /// <param name="MainTeacher">Главная страница преподавателей</param>
        /// <param name="teachers">Модель преподавателя (может быть null для добавления нового преподавателя)</param>
        public TeacherAdd(Pages.listPages.Teacher MainTeacher, Models.Teachers teachers = null)
        {
            InitializeComponent();
            this.MainTeacher = MainTeacher;
            this.teachers = teachers;
        }

        /// <summary>
        /// Метод для добавления нового преподавателя в систему.
        /// Выполняет валидацию введенных данных и добавляет преподавателя в базу данных.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void Add_Teacher(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка фамилии на корректность
                if (string.IsNullOrEmpty(tb_surName.Text))
                {
                    MessageBox.Show("Введите фамилию");
                    return;
                }

                // Проверка имени на корректность
                if (string.IsNullOrEmpty(tb_name.Text))
                {
                    MessageBox.Show("Введите имя");
                    return;
                }

                // Проверка отчества на корректность
                if (string.IsNullOrEmpty(tb_lastName.Text))
                {
                    MessageBox.Show("Введите отчество");
                    return;
                }

                // Проверка логина на корректность
                if (string.IsNullOrEmpty(tb_login.Text))
                {
                    MessageBox.Show("Введите логин");
                    return;
                }

                // Проверка пароля на корректность
                if (string.IsNullOrEmpty(tb_password.Text))
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }

                // Если преподаватель не передан, создаем нового
                if (teachers == null)
                {
                    teachers = new Models.Teachers
                    {
                        surname = tb_surName.Text,
                        name = tb_name.Text,
                        lastname = tb_lastName.Text,
                        login = tb_login.Text,
                        password = tb_password.Text,
                        userId = 2  // Здесь предполагается, что для преподавателя userId = 2
                    };

                    // Добавление нового преподавателя в базу данных
                    MainTeacher._teachersContext.Teachers.Add(teachers);
                }

                // Сохранение изменений в базе данных
                MainTeacher._teachersContext.SaveChanges();

                // Переход на страницу преподавателей
                MainWindow.init.OpenPages(MainWindow.pages.teacher);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding Teacher", ex.Message, "Failed to save Teacher.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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

        /// <summary>
        /// Обработчик для кнопки "Отменить", возвращает назад на предыдущую страницу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
