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
            //string login = UserNameTextBox.Text;
            //string password = PasswordBox.Password;

            //if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            //{
            //    MessageBox.Show("Пожалуйста, заполните все поля.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}

            //Users user = Database.AuthenticateUser(login, password);

            //if (user != null)
            //{
            //    MessageBox.Show($"Добро пожаловать, {user.login}!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
            MainWindow.init.OpenPages(MainWindow.pages.main);
            //}
            //else
            //{
            //    MessageBox.Show("Неверный логин или пароль.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            //}
        }
    }
}
