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
            InitializeComponent(); // Инициализация компонентов страницы
        }

        // Метод для обработки клика по кнопке авторизации
        private void Click_Authorization(object sender, RoutedEventArgs e)
        {
            string username = UserNameTextBox.Text; // Получаем введенный логин
            string password = PasswordBox.Password; // Получаем введенный пароль

            // Проверяем, что логин и пароль не пустые
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Пожалуйста, введите логин и пароль."); // Показываем сообщение об ошибке
                return;
            }

            // Используем контекст для поиска пользователя в базе данных
            using (var context = new UsersContext())
            {
                var user = context.Users.FirstOrDefault(u => u.login == username); // Находим пользователя по логину

                // Если пользователь найден и пароль совпадает
                if (user != null && user.password == password)
                {
                    string role = GetRole(user.role);  // Получаем роль пользователя по его ID
                    MainWindow.UserRole = role;        // Сохраняем роль пользователя в MainWindow
                    NavigateToPageBasedOnRole(role);  // Переходим на страницу в зависимости от роли
                }
                else
                {
                    MessageBox.Show("Неверный логин или пароль."); // Если логин или пароль неверные
                }
            }
        }

        // Метод для получения роли пользователя по его ID
        private string GetRole(int roleId)
        {
            using (var context = new RolesContext())
            {
                var role = context.Roles.FirstOrDefault(r => r.id == roleId); // Находим роль по ID
                return role?.roleName; // Возвращаем название роли (если найдено)
            }
        }

        // Метод для перехода на нужную страницу в зависимости от роли пользователя
        private void NavigateToPageBasedOnRole(string role)
        {
            // Переход на главную страницу приложения с передачей роли
            MainWindow.init.OpenPages(MainWindow.pages.main);
        }
    }
}