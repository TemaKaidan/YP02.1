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
    /// Логика взаимодействия для TeacherItem.xaml
    /// </summary>
    public partial class TeacherItem : UserControl
    {
        private Teachers _teachers;
        private readonly TeachersContext _teachersContext;

        public TeacherItem(Teachers teachers)
        {
            InitializeComponent();
            _teachers = teachers;

            lb_LastName.Content = "Фамилия: " + teachers.surname;
            lb_FirstName.Content = "Имя: " + teachers.name;
            lb_MiddleName.Content = "Отчество: " + teachers.lastname;

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
