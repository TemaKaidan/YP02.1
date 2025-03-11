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
        Pages.listPages.DisciplineProgram MainDisciplineProgram;
        Models.DisciplinePrograms disciplineProgram;

        public DisciplineProgramItem(DisciplinePrograms disciplinePrograms, DisciplineProgram MainDisciplineProgram)
        {
            InitializeComponent();
            this.disciplineProgram = disciplinePrograms;
            this.MainDisciplineProgram = MainDisciplineProgram;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            DisciplinesContext _disciplinesContext = new DisciplinesContext();
            var disciplinesContext = _disciplinesContext.Disciplines.FirstOrDefault(g => g.id == disciplinePrograms.disciplineId);
            lb_disciplineId.Content = "Дисциплина: " + (disciplinesContext != null ? disciplinesContext.name : "Неизвестно");

            lb_theme.Content = "Тема: " + disciplinePrograms.theme;

            LessonTypesContext _lessonTypesContext = new LessonTypesContext();
            var lessonTypesContext = _lessonTypesContext.LessonTypes.FirstOrDefault(g => g.id == disciplinePrograms.lessonTypeId);
            lb_lessonTypeId.Content = "Тип: " + (lessonTypesContext != null ? lessonTypesContext.typeName : "Неизвестно");

            lb_hoursCount.Content = "Количество часов: " + disciplinePrograms.hoursCount;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgramEdit, null, null, disciplineProgram);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.Remove(disciplineProgram);
                    MainDisciplineProgram._disciplinePrograms.SaveChanges();
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
