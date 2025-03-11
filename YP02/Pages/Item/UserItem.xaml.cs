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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для UserItem.xaml
    /// </summary>
    public partial class UserItem : UserControl
    {
        // Основной контроллер для работы с пользователем и модель пользователя
        Pages.listPages.User MainUser;
        Models.Users users;

        // Контексты для работы с пользователями и ролями
        private readonly UsersContext _usersContext;
        private readonly RolesContext _rolesContext = new RolesContext();

        // Конструктор для инициализации элемента UserItem с данными
        public UserItem(Users users, User MainUser)
        {
            InitializeComponent(); // Инициализация компонентов UserControl
            this.users = users; // Присваиваем объект пользователя
            this.MainUser = MainUser; // Присваиваем основной контроллер для пользователя

            // Показываем кнопки редактирования и удаления в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображаем данные пользователя в метках
            lb_Login.Content = "Логин: " + users.login;
            lb_Password.Content = "Пароль: " + users.password;
            lb_Role.Content = "Роль: " + _rolesContext.Roles.FirstOrDefault(x => x.id == users.role).roleName;
        }

        // Обработчик события для редактирования данных пользователя
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            // Открытие страницы редактирования пользователя
            MainWindow.init.OpenPages(MainWindow.pages.userEdit, null, null, null, null, null, null, users);
        }

        // Обработчик события для удаления пользователя
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Показываем сообщение с подтверждением удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление пользователя из базы данных
                    MainUser._usersContext.Users.Remove(users);
                    MainUser._usersContext.SaveChanges();
                    // Удаление элемента из панели пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибок при удалении
                ErrorLogger.LogError("Error deleting Users", ex.Message, "Failed to save Users.");

                // Отображение сообщения об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
