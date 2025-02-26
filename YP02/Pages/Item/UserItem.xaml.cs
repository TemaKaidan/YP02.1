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
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для UserItem.xaml
    /// </summary>
    public partial class UserItem : UserControl
    {
        private Users _users;
        private readonly UsersContext _usersContext;
        private readonly RolesContext _rolesContext = new RolesContext();

        public UserItem(Users users)
        {
            InitializeComponent();
            _users = users;
            lb_Login.Content = "Логин: " + users.login;
            lb_Password.Content = "Пароль: " + users.password;
            lb_Role.Content = "Роль: " + _rolesContext.Roles.FirstOrDefault(x => x.id == _users.role).roleName;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
