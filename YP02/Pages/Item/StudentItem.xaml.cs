using System;
using System.Collections.Generic;
using System.Data;
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

        Pages.listPages.Student MainStudent;
        Models.Students students;

        public StudentItem(Students students, Student MainStudent)
        {
            InitializeComponent();
            this.students = students;
            this.MainStudent = MainStudent;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

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
            MainWindow.init.OpenPages(MainWindow.pages.studentEdit, null, students);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainStudent._studentsContext.Students.Remove(students);
                MainStudent._studentsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}