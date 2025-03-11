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
    /// Логика взаимодействия для RoleItem.xaml
    /// </summary>
    public partial class RoleItem : UserControl
    {
        // Основные объекты для работы с ролями
        Pages.listPages.Role MainRole;
        Models.Roles roles;

        /// <summary>
        /// Конструктор для инициализации компонента RoleItem с заданными данными
        /// </summary>
        public RoleItem(Roles roles, Role MainRole)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.roles = roles; // Присваиваем данные роли
            this.MainRole = MainRole; // Присваиваем основной объект для работы с ролями

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображаем название роли
            lb_RoleName.Content = "Роль: " + roles.roleName;
        }

        // Обработчик для кнопки редактирования роли
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.roleEdit, null, null, null, null, null, null, null, roles); // Открытие страницы редактирования роли
        }

        // Обработчик для кнопки удаления роли
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления роли
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление роли из базы данных
                    MainRole._rolesContext.Roles.Remove(roles);
                    MainRole._rolesContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Roles", ex.Message, "Failed to save Roles.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
