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

namespace YP02.Pages
{
    /// <summary>
    /// Логика взаимодействия для Authorization.xaml
    /// </summary>
    public partial class Authorization : Page
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void Click_Authorization(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text;
            string password = PasswordBox.Password;

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль.");
                return;
            }

            using (var context = new UsersContext())
            {
                var user = context.Users.FirstOrDefault(u => u.login == username);

                if (user != null && user.password == password)
                {
                    string role = GetRole(user.role);  // Получаем роль пользователя
                    MainWindow.UserRole = role;        // Сохраняем роль в MainWindow
                    NavigateToPageBasedOnRole(role);  // Переходим на нужную страницу, передавая роль
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль.");
                }
            }
        }

        private string GetRole(int roleId)
        {
            // Находим роль пользователя по ID
            using (var context = new RolesContext())
            {
                var role = context.Roles.FirstOrDefault(r => r.id == roleId);
                return role?.roleName;
            }
        }

        private void NavigateToPageBasedOnRole(string role)
        {
            // Переход на страницу Main с передачей роли
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}