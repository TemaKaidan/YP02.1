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
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Role MainRole;
        public Models.Roles roles;

        public RoleEdit(Pages.listPages.Role MainRole, Models.Roles roles = null)
        {
            InitializeComponent();
            this.MainRole = MainRole;
            this.roles = roles;

            tb_roleName.Text = roles.roleName;
        }

        private void Edit_Role(object sender, RoutedEventArgs e)
        {
            try
            {
                Models.Roles mr = MainRole._rolesContext.Roles.FirstOrDefault(x => x.id == roles.id);
                mr.roleName = tb_roleName.Text;

                MainRole._rolesContext.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.role);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating Role", ex.Message, "Failed to save Role.");

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