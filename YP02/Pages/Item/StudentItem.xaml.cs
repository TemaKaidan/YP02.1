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
    /// Логика взаимодействия для StudentItem.xaml
    /// </summary>
    public partial class StudentItem : UserControl
    {
        private Students _students;
        private readonly StudentsContext _studentsContext;
        public StudentItem(Students students)
        {
            InitializeComponent();
            _students = students;

            lb_surname.Content = "Фамилия: " + students.surname;
            lb_name.Content = "Имя: " + students.name;
            lb_lastname.Content = "Отчество: " + students.lastname;

            StudGroupsContext _groupe = new StudGroupsContext();
            var group = _groupe.StudGroups.FirstOrDefault(g => g.id == students.studGroupId);
            lb_studGroupId.Content = "Группа: " + (group != null ? group.name : "Неизвестно");

            lb_dateOfRemand.Content = "Дата отчисления: " + students.dateOfRemand;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}