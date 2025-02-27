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

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для GroupeEdit.xaml
    /// </summary>
    public partial class GroupeEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Group MainGrope;
        public Models.StudGroups studGroups;

        public GroupeEdit(Pages.listPages.Group MainGrope, Models.StudGroups studGroups = null)
        {
            InitializeComponent();
            this.MainGrope = MainGrope;
            this.studGroups = studGroups;

            tb_name.Text = studGroups.name;
        }

        private void Edit_Groupe(object sender, RoutedEventArgs e)
        {
            Models.StudGroups sgc = MainGrope._studgroupsContext.StudGroups.FirstOrDefault(x => x.id == studGroups.id);
            sgc.name = tb_name.Text;

            MainGrope._studgroupsContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.group);
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