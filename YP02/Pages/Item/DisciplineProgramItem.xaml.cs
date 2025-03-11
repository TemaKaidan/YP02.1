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
    /// Логика взаимодействия для DisciplineProgramItem.xaml
    /// </summary>
    public partial class DisciplineProgramItem : UserControl
    {
        // Основные объекты для работы с программой дисциплины
        Pages.listPages.DisciplineProgram MainDisciplineProgram;
        Models.DisciplinePrograms disciplineProgram;

        /// <summary>
        /// Конструктор для инициализации компонента DisciplineProgramItem с заданными данными
        /// </summary>
        public DisciplineProgramItem(DisciplinePrograms disciplinePrograms, DisciplineProgram MainDisciplineProgram)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.disciplineProgram = disciplinePrograms; // Присваиваем данные программы дисциплины
            this.MainDisciplineProgram = MainDisciplineProgram; // Присваиваем основной объект для программы дисциплины

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображение информации о дисциплине
            DisciplinesContext _disciplinesContext = new DisciplinesContext();
            var disciplinesContext = _disciplinesContext.Disciplines.FirstOrDefault(g => g.id == disciplinePrograms.disciplineId);
            lb_disciplineId.Content = "Дисциплина: " + (disciplinesContext != null ? disciplinesContext.name : "Неизвестно");

            // Отображение информации о теме программы
            lb_theme.Content = "Тема: " + disciplinePrograms.theme;

            // Отображение информации о типе занятия
            LessonTypesContext _lessonTypesContext = new LessonTypesContext();
            var lessonTypesContext = _lessonTypesContext.LessonTypes.FirstOrDefault(g => g.id == disciplinePrograms.lessonTypeId);
            lb_lessonTypeId.Content = "Тип: " + (lessonTypesContext != null ? lessonTypesContext.typeName : "Неизвестно");

            // Отображение количества часов
            lb_hoursCount.Content = "Количество часов: " + disciplinePrograms.hoursCount;
        }

        // Обработчик для кнопки редактирования программы дисциплины
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgramEdit, null, null, disciplineProgram); // Открытие страницы редактирования программы дисциплины
        }

        // Обработчик для кнопки удаления программы дисциплины
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления программы дисциплины
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление программы дисциплины из базы данных
                    MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.Remove(disciplineProgram);
                    MainDisciplineProgram._disciplinePrograms.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting DisciplinePrograms", ex.Message, "Failed to save DisciplinePrograms.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
