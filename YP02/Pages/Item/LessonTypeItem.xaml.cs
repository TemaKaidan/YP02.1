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
        private LessonTypes _lessontype;
        private readonly LessonTypesContext _lessonTypesContext;

        public LessonTypeItem(LessonTypes lessontypes)
        {
            InitializeComponent();
            _lessontype = lessontypes;
            lb_LessonType.Content = "Тип занятия: " + lessontypes.typeName;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
