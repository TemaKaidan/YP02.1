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
        private bool isMenuCollapsed = false;

        public Pages.listPages.User MainUser;
        public Models.Users users;

        Context.RolesContext rolesContext = new Context.RolesContext();
        
        public UserEdit(Pages.listPages.User MainUser, Models.Users users = null)
        {
            InitializeComponent();
            this.MainUser = MainUser;
            this.users = users;

            tb_login.Text = users.login;
            tb_password.Text = users.password;

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

        private void Edit_User(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Users editUsers = MainUser._usersContext.Users.FirstOrDefault(x => x.id == users.id);
                if (editUsers != null)
                {
                    editUsers.login = tb_login.Text;
                    editUsers.password = tb_password.Text;
                    editUsers.role = (int)(cb_role.SelectedItem as ComboBoxItem).Tag;
                    MainUser._usersContext.SaveChanges();
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

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
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
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}