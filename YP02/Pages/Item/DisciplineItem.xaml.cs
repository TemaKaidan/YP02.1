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
    /// Логика взаимодействия для DisciplineItem.xaml
    /// </summary>
    public partial class DisciplineItem : UserControl
    {
        // Основные объекты для работы с дисциплиной
        Pages.listPages.Discipline MainDiscipline;
        Models.Disciplines disciplines;

        /// <summary>
        /// Конструктор для инициализации компонента DisciplineItem с заданными данными
        /// </summary>
        public DisciplineItem(Disciplines disciplines, Discipline MainDiscipline)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.disciplines = disciplines; // Присваиваем данные дисциплины
            this.MainDiscipline = MainDiscipline; // Присваиваем основной объект для дисциплины

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображение информации о дисциплине
            lb_Name.Content = "Дисциплина: " + disciplines.name;
        }

        // Обработчик для кнопки редактирования дисциплины
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineEdit, null, null, null, disciplines); // Открытие страницы редактирования дисциплины
        }

        // Обработчик для кнопки удаления дисциплины
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления дисциплины
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление дисциплины из базы данных
                    MainDiscipline._disciplinesContext.Disciplines.Remove(disciplines);
                    MainDiscipline._disciplinesContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Disciplines", ex.Message, "Failed to save Disciplines.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
