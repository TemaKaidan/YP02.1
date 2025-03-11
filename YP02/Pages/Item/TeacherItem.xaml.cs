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
        // Основной контроллер для работы с преподавателем и модель преподавателя
        Pages.listPages.Teacher MainTeacher;
        Models.Teachers teachers;

        // Конструктор для инициализации элемента TeacherItem с данными
        public TeacherItem(Teachers teachers, Teacher MainTeacher)
        {
            InitializeComponent(); // Инициализация компонентов UserControl
            this.teachers = teachers; // Присваиваем объект преподавателя
            this.MainTeacher = MainTeacher; // Присваиваем основной контроллер для преподавателя

            // Показываем кнопки редактирования и удаления в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображаем данные преподавателя в метках
            lb_LastName.Content = "Фамилия: " + teachers.surname;
            lb_FirstName.Content = "Имя: " + teachers.name;
            lb_MiddleName.Content = "Отчество: " + teachers.lastname;
            lb_Login.Content = "Логин: " + teachers.login;
            lb_Password.Content = "Пароль: " + teachers.password;
        }

        // Обработчик события для редактирования данных преподавателя
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            // Открытие страницы редактирования преподавателя
            MainWindow.init.OpenPages(MainWindow.pages.teacherEdit, null, null, null, null, null, null, null, null, null, null, teachers);
        }

        // Обработчик события для удаления преподавателя
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Показываем сообщение с подтверждением удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление преподавателя из базы данных
                    MainTeacher._teachersContext.Teachers.Remove(teachers);
                    MainTeacher._teachersContext.SaveChanges();
                    // Удаление элемента из панели пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибок при удалении
                ErrorLogger.LogError("Error deleting Teachers", ex.Message, "Failed to save Teachers.");

                // Отображение сообщения об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
