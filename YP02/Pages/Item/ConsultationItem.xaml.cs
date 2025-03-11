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
    /// Логика взаимодействия для ConsultationItem.xaml
    /// </summary>
    public partial class ConsultationItem : UserControl
    {
        // Основные объекты для работы с консультациями
        Pages.listPages.Consultation MainConsultation;
        Models.Consultations consultations;

        // Контекст для работы с данными о дисциплинах
        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Конструктор для инициализации компонента ConsultationItem с заданными данными
        /// </summary>
        public ConsultationItem(Consultations consultations, Consultation MainConsultation)
        {
            InitializeComponent();
            this.consultations = consultations;
            this.MainConsultation = MainConsultation;

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображение информации о дисциплине и консультации
            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == consultations.disciplineId)?.name;
            lb_Date.Content = "Дата: " + consultations.date.ToString("dd.MM.yyyy");
            lb_submittedWorks.Content = "Сданные работы: " + consultations.submittedWorks;
        }

        /// <summary>
        /// Обработчик для кнопки редактирования
        /// </summary>
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            // Открытие страницы редактирования консультации
            MainWindow.init.OpenPages(MainWindow.pages.consultationEdit, null, null, null, null, null, consultations);
        }

        /// <summary>
        /// Обработчик для кнопки удаления
        /// </summary>
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление консультации из базы данных
                    MainConsultation._consultationsContext.Consultations.Remove(consultations);
                    MainConsultation._consultationsContext.SaveChanges();

                    // Удаление элемента из UI
                    (this.Parent as Panel)?.Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Consultations", ex.Message, "Failed to delete Consultation.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
