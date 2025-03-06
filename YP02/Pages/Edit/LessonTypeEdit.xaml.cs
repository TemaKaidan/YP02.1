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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для LessonTypeEdit.xaml
    /// </summary>
    public partial class LessonTypeEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.LessonType MainLessonType;
        public Models.LessonTypes lessonTypes;

        public LessonTypeEdit(Pages.listPages.LessonType MainLessonType, Models.LessonTypes lessonTypes = null)
        {
            InitializeComponent();
            this.MainLessonType = MainLessonType;
            this.lessonTypes = lessonTypes;

            tb_typeName.Text = lessonTypes.typeName;
        }

        private void Edit_LessonType(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_typeName.Text))
            {
                MessageBox.Show("Введите наименование тип занятия");
                return;
            }

            Models.LessonTypes mlt = MainLessonType._lessonTypesContext.LessonTypes.FirstOrDefault(x => x.id == lessonTypes.id);
            mlt.typeName = tb_typeName.Text;

            MainLessonType._lessonTypesContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.lessonType);
        }

        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}