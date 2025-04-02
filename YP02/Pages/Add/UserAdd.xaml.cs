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
    /// Логика взаимодействия для UserAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления нового пользователя.
    /// </summary>
    public partial class UserAdd : Page
    {
        private bool isMenuCollapsed = false;
        public Pages.listPages.User MainUser;
        public Models.Users users;
        Context.RolesContext rolesContext = new Context.RolesContext();
        public UserAdd(Pages.listPages.User MainUser, Models.Users users = null)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            this.users = users;

            cb_role.Items.Clear();
            cb_role.ItemsSource = rolesContext.Roles.ToList(); // Получаем все роли из базы данных
            cb_role.DisplayMemberPath = "roleName"; // Отображаем имя роли
            cb_role.SelectedValuePath = "id"; // Значение будет ID роли
        }

        private void Add_Groupe(object sender, RoutedEventArgs e)
        {
            try
            {
                if (users == null)
                {
                    users = new Models.Users
                    {
                        login = tb_login.Text, // Логин пользователя
                        password = tb_password.Text, // Пароль пользователя
                        role = (cb_role.SelectedItem as Models.Roles).id // ID роли пользователя
                    };

                    MainUser._usersContext.Users.Add(users);
                }

                MainUser._usersContext.SaveChanges();

                MainWindow.init.OpenPages(MainWindow.pages.user);
            }
            catch (Exception ex)
            {
                ErrorLogger.LogError("Error adding User", ex.Message, "Failed to save User.");

                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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