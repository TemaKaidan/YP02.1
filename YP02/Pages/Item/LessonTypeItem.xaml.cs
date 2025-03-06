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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для LessonTypeItem.xaml
    /// </summary>
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

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.lessonTypeEdit, null,null,null,null,null,null,null,null, lessonTypes);

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainLessonType._lessonTypesContext.LessonTypes.Remove(lessonTypes);
                MainLessonType._lessonTypesContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}