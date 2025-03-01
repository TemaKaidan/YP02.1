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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для LessonTypeAdd.xaml
    /// </summary>
    public partial class LessonTypeAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.LessonType MainLessonType;
        public Models.LessonTypes lessonTypes;

        public LessonTypeAdd(Pages.listPages.LessonType MainLessonType, Models.LessonTypes lessonTypes = null)
        {
            InitializeComponent();
            this.MainLessonType = MainLessonType;
            this.lessonTypes = lessonTypes;
        }

        private void Add_LessonType(object sender, RoutedEventArgs e)
        {
            if (lessonTypes == null)
            {
                lessonTypes = new Models.LessonTypes
                {
                    typeName = tb_typeName.Text
                };
                MainLessonType._lessonTypesContext.LessonTypes.Add(lessonTypes);
            }
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
