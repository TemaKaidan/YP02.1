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
    /// Логика взаимодействия для TeacherLoadItem.xaml
    /// </summary>
    public partial class TeacherLoadItem : UserControl
    {
        // Объявление переменных для работы с данными преподавателя и нагрузки
        Pages.listPages.TeachersLoad MainTeachersLoad;
        Models.TeachersLoad teachersLoad;

        // Объявление переменных для работы с данными группы студентов
        Pages.listPages.Group MainGrope;
        Models.StudGroups studGroups;

        // Конструктор, инициализирует компоненты и настраивает отображение данных
        public TeacherLoadItem(Models.TeachersLoad teachersLoad, Pages.listPages.TeachersLoad MainTeachersLoad)
        {
            InitializeComponent();
            this.teachersLoad = teachersLoad;
            this.MainTeachersLoad = MainTeachersLoad;

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Получение информации о преподавателе из базы данных
            TeachersContext _teachersContext = new TeachersContext();
            var teacher = _teachersContext.Teachers.FirstOrDefault(g => g.id == teachersLoad.teacherId);
            lb_teacherId.Content = "Преподователь: " + (teacher != null ? teacher.surname : "Неизвестно ") + " " + (teacher != null ? teacher.name : "Неизвестно ") + " " + (teacher != null ? teacher.lastname : "Неизвестно ");

            // Получение информации о дисциплине из базы данных
            DisciplinesContext _disciplinesContext = new DisciplinesContext();
            var disciplinesContext = _disciplinesContext.Disciplines.FirstOrDefault(g => g.id == teachersLoad.disciplineId);
            lb_disciplineId.Content = "Дисциплина: " + (disciplinesContext != null ? disciplinesContext.name : "Неизвестно");

            // Получение информации о группе студентов из базы данных
            StudGroupsContext _groupe = new StudGroupsContext();
            var group = _groupe.StudGroups.FirstOrDefault(g => g.id == teachersLoad.studGroupId);
            lb_studGroupId.Content = "Группа: " + (group != null ? group.name : "Неизвестно");

            // Отображение количества часов по типам занятий
            lb_lectureHours.Content = "Часы лекций: " + teachersLoad.lectureHours;
            lb_practiceHours.Content = "Часы практик: " + teachersLoad.practiceHours;
            lb_сonsultationHours.Content = "Часы консультаций: " + teachersLoad.сonsultationHours;
            lb_courseprojectHours.Content = "Часы курсового проекта: " + teachersLoad.courseprojectHours;
            lb_examHours.Content = "Часы экзамена: " + teachersLoad.examHours;

            // Вычисление общего количества часов и отображение
            int totalHours = CalculateTotalHours(teachersLoad);
            lb_totalHours.Content = "Общее количество часов на дисциплину: " + totalHours;
        }

        // Метод для вычисления общего количества часов
        private int CalculateTotalHours(Models.TeachersLoad teachersLoad)
        {
            return teachersLoad.lectureHours +
                   teachersLoad.practiceHours +
                   teachersLoad.сonsultationHours +
                   teachersLoad.courseprojectHours +
                   teachersLoad.examHours;
        }

        // Обработчик события клика на кнопку редактирования
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoadEdit, null, null, null, null, teachersLoad);
        }

        // Обработчик события клика на кнопку удаления
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Запрос подтверждения удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление записи из базы данных и сохранение изменений
                    MainTeachersLoad._teachersLoadContext.TeachersLoad.Remove(teachersLoad);
                    MainTeachersLoad._teachersLoadContext.SaveChanges();

                    // Удаление элемента из визуального списка
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при удалении
                ErrorLogger.LogError("Error deleting TeachersLoad", ex.Message, "Failed to save TeachersLoad.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}