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
    /// Логика взаимодействия для GroupeItem.xaml
    /// </summary>
    public partial class GroupeItem : UserControl
    {
        // Основные объекты для работы с группой студентов
        Pages.listPages.Group MainGrope;
        Models.StudGroups studGroups;

        /// <summary>
        /// Конструктор для инициализации компонента GroupeItem с заданными данными
        /// </summary>
        public GroupeItem(StudGroups studGroups, Group MainGrope)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.studGroups = studGroups; // Присваиваем данные группы студентов
            this.MainGrope = MainGrope; // Присваиваем основной объект для группы студентов

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображение информации о наименовании группы
            lb_Name.Content = "Наименование группы: " + studGroups.name;
        }

        // Обработчик для кнопки редактирования группы студентов
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.groupeEdit, studGroups); // Открытие страницы редактирования группы студентов
        }

        // Обработчик для кнопки удаления группы студентов
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления группы студентов
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление группы студентов из базы данных
                    MainGrope._studgroupsContext.StudGroups.Remove(studGroups);
                    MainGrope._studgroupsContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting StudGroups", ex.Message, "Failed to save StudGroups.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}