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
using YP02.Models;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для RoleItem.xaml
    /// </summary>
    public partial class RoleItem : UserControl
    {
        private Roles _roles;
        private readonly RolesContext _rolesContext;

        public RoleItem(Roles roles)
        {
            InitializeComponent();
            _roles = roles;
            lb_RoleName.Content = "Роль: " + roles.roleName;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
