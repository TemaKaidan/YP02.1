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
    /// Логика взаимодействия для LessonTypeItem.xaml
    /// </summary>
    public partial class LessonTypeItem : UserControl
    {
        // Основные объекты для работы с типами занятий
        Pages.listPages.LessonType MainLessonType;
        Models.LessonTypes lessonTypes;

        /// <summary>
        /// Конструктор для инициализации компонента LessonTypeItem с заданными данными
        /// </summary>
        public LessonTypeItem(LessonTypes lessonTypes, LessonType MainLessonType)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.lessonTypes = lessonTypes; // Присваиваем данные типа занятия
            this.MainLessonType = MainLessonType; // Присваиваем основной объект для типа занятия

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Отображение информации о типе занятия
            lb_LessonType.Content = "Тип занятия: " + lessonTypes.typeName;
        }

        // Обработчик для кнопки редактирования типа занятия
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonTypeEdit, null, null, null, null, null, null, null, null, lessonTypes); // Открытие страницы редактирования типа занятия
        }

        // Обработчик для кнопки удаления типа занятия
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления типа занятия
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление типа занятия из базы данных
                    MainLessonType._lessonTypesContext.LessonTypes.Remove(lessonTypes);
                    MainLessonType._lessonTypesContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting LessonTypes", ex.Message, "Failed to save LessonTypes.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}