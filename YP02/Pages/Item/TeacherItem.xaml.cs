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
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для TeacherItem.xaml
    /// </summary>
    public partial class TeacherItem : UserControl
    {
        Pages.listPages.Teacher MainTeacher;
        Models.Teachers teachers;

        public TeacherItem(Teachers teachers, Teacher MainTeacher)
        {
            InitializeComponent();
            this.teachers = teachers;
            this.MainTeacher = MainTeacher;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            lb_LastName.Content = "Фамилия: " + teachers.surname;
            lb_FirstName.Content = "Имя: " + teachers.name;
            lb_MiddleName.Content = "Отчество: " + teachers.lastname;
            lb_Login.Content = "Логин: " + teachers.login;
            lb_Password.Content = "Пароль: " + teachers.password;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teacherEdit, null,null,null,null,null,null,null,null,null,null, teachers);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainTeacher._teachersContext.Teachers.Remove(teachers);
                    MainTeacher._teachersContext.SaveChanges();
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Teachers", ex.Message, "Failed to save Teachers.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
