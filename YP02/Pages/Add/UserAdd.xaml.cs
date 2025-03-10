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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления нового пользователя.
    /// </summary>
    public partial class UserAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница пользователей.
        /// </summary>
        public Pages.listPages.User MainUser;

        /// <summary>
        /// Модель пользователя, которая может быть передана для редактирования.
        /// </summary>
        public Models.Users users;

        /// <summary>
        /// Контекст базы данных для ролей пользователей.
        /// </summary>
        Context.RolesContext rolesContext = new Context.RolesContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления пользователя.
        /// </summary>
        /// <param name="MainUser">Главная страница пользователей.</param>
        /// <param name="users">Модель пользователя (если редактируется существующий пользователь, иначе null).</param>
        public UserAdd(Pages.listPages.User MainUser, Models.Users users = null)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            this.users = users;

            // Заполнение комбинированного списка ролей
            cb_role.Items.Clear();
            cb_role.ItemsSource = rolesContext.Roles.ToList(); // Получаем все роли из базы данных
            cb_role.DisplayMemberPath = "roleName"; // Отображаем имя роли
            cb_role.SelectedValuePath = "id"; // Значение будет ID роли
        }

        /// <summary>
        /// Метод для добавления нового пользователя.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Groupe(object sender, RoutedEventArgs e)
        {
            // Если пользователь не передан (т.е. добавляем нового пользователя)
            if (users == null)
            {
                // Создаем нового пользователя
                users = new Models.Users
                {
                    login = tb_login.Text, // Логин пользователя
                    password = tb_password.Text, // Пароль пользователя
                    role = (cb_role.SelectedItem as Models.Roles).id // ID роли пользователя
                };

                // Добавляем нового пользователя в базу данных
                MainUser._usersContext.Users.Add(users);
            }

            // Сохраняем изменения в базе данных
            MainUser._usersContext.SaveChanges();

            // Переходим на страницу с пользователями
            MainWindow.init.OpenPages(MainWindow.pages.user);
        }

        /// <summary>
        /// Метод для переключения состояния меню (свернуто/развернуто).
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
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
                    // Отображаем все кнопки, кроме кнопки с символом "☰"
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
                foreach (UIElement element in MenuPanel.Children)
                {
                    // Скрываем все кнопки, кроме кнопки с символом "☰"
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Устанавливаем продолжительность анимации
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            // Запуск анимации для изменения ширины панели меню
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключаем флаг состояния меню
            isMenuCollapsed = !isMenuCollapsed;
        }

        /// <summary>
        /// Обработчик для кнопки "Отменить", которая возвращает пользователя на предыдущую страницу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход назад на предыдущую страницу
            NavigationService.GoBack();
        }
    }
}