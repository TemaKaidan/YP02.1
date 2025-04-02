using System.Windows;
using System.Windows.Controls;
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    public partial class LessonTypeItem : UserControl
    {
        Pages.listPages.LessonType MainLessonType;
        Models.LessonTypes lessonTypes;
        public LessonTypeItem(LessonTypes lessonTypes, LessonType MainLessonType)
        {
            InitializeComponent();
            this.lessonTypes = lessonTypes; 
            this.MainLessonType = MainLessonType; 

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