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
            cb_role.ItemsSource = rolesContext.Roles.ToList();
            cb_role.DisplayMemberPath = "roleName";
            cb_role.SelectedValuePath = "id";
        }

        private void Add_Groupe(object sender, RoutedEventArgs e)
        {
            if (users == null)
            {
                users = new Models.Users
                {
                    login = tb_login.Text,
                    password = tb_password.Text,
                    role = (cb_role.SelectedItem as Models.Roles).id
                };
                MainUser._usersContext.Users.Add(users);
            }
            MainUser._usersContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.user);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
    }
}