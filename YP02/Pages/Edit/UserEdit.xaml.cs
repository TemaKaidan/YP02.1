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
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для UserEdit.xaml
    /// </summary>
    public partial class UserEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с пользователями
        public Pages.listPages.User MainUser;
        public Models.Users users;

        // Контекст для работы с ролями
        Context.RolesContext rolesContext = new Context.RolesContext();

        /// <summary>
        /// Конструктор, инициализирующий компоненты и заполняющий поля формы для редактирования пользователя
        /// </summary>
        public UserEdit(Pages.listPages.User MainUser, Models.Users users = null)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            this.users = users;

            // Заполнение полей формы данными пользователя
            tb_login.Text = users.login;
            tb_password.Text = users.password;

            // Заполнение ComboBox с ролями
            foreach (Models.Roles roles in rolesContext.Roles)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = roles.roleName;
                item.Tag = roles.id;
                if (roles.id == users.role)
                {
                    item.IsSelected = true;
                }
                cb_role.Items.Add(item);
            }
        }

        /// <summary>
        /// Обработчик для редактирования пользователя
        /// </summary>
        private void Edit_User(object sender, RoutedEventArgs e)
        {
            try
            {
                // Поиск пользователя по id
                Models.Users editUsers = MainUser._usersContext.Users.FirstOrDefault(x => x.id == users.id);
                if (editUsers != null)
                {
                    // Обновление данных пользователя
                    editUsers.login = tb_login.Text;
                    editUsers.password = tb_password.Text;
                    editUsers.role = (int)(cb_role.SelectedItem as ComboBoxItem).Tag;

                    // Сохранение изменений в базе данных
                    MainUser._usersContext.SaveChanges();

                    // Переход на страницу списка пользователей
                    MainWindow.init.OpenPages(MainWindow.pages.user);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.user);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating User", ex.Message, "Failed to save User.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик для сворачивания и разворачивания меню
        /// </summary>
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Проверка текущего состояния меню (свёрнуто или развернуто)
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                // Делаем видимыми все кнопки в меню, кроме кнопки "☰"
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
                // Прячем все кнопки в меню, кроме кнопки "☰"
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Настроим продолжительность анимации и применим её к ширине меню
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключаем состояние меню
            isMenuCollapsed = !isMenuCollapsed;
        }

        /// <summary>
        /// Обработчик кнопки "Отмена", возвращает пользователя на предыдущую страницу
        /// </summary>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}