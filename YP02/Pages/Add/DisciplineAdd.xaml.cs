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
using YP02.Pages.listPages;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для DisciplineAdd.xaml
    /// </summary>
    public partial class DisciplineAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Discipline MainDiscipline;
        public Models.Disciplines disciplines;

        public DisciplineAdd(Pages.listPages.Discipline MainDiscipline, Models.Disciplines disciplines = null)
        {
            InitializeComponent();
            this.MainDiscipline = MainDiscipline;
            this.disciplines = disciplines;
        }

        private void Add_Discipline(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_nameDiscipline.Text))
            {
                MessageBox.Show("Введите наименование дисциплины");
                return;
            }

            if (disciplines == null)
            {
                disciplines = new Models.Disciplines
                {
                    name = tb_nameDiscipline.Text,
                    teacherId = 1
                };
                MainDiscipline._disciplinesContext.Disciplines.Add(disciplines);
            }
            MainDiscipline._disciplinesContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.discipline);
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
