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
using YP02.Context;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для TeacherLoadAdd.xaml
    /// </summary>
    public partial class TeacherLoadAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.TeachersLoad MainTeachersLoad;
        public Models.TeachersLoad teachersLoad;

        Context.TeachersContext teachersContext = new TeachersContext();
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        public TeacherLoadAdd(Pages.listPages.TeachersLoad MainTeachersLoad, Models.TeachersLoad teachersLoad = null)
        {
            InitializeComponent();
            this.MainTeachersLoad = MainTeachersLoad;
            this.teachersLoad = teachersLoad;

            cb_teacherId.Items.Clear();
            cb_teacherId.ItemsSource = teachersContext.Teachers.ToList();
            cb_teacherId.DisplayMemberPath = "surname";
            cb_teacherId.SelectedValuePath = "id";

            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name";
            cb_disciplineId.SelectedValuePath = "id";

            cb_groupe.Items.Clear();
            cb_groupe.ItemsSource = studGroupsContext.StudGroups.ToList();
            cb_groupe.DisplayMemberPath = "name";
            cb_groupe.SelectedValuePath = "id";
        }

        private void Add_TeacherLoad(object sender, RoutedEventArgs e)
        {
            if (teachersLoad == null)
            {
                teachersLoad = new Models.TeachersLoad
                {
                    teacherId = (cb_teacherId.SelectedItem as Models.Teachers).id,
                    disciplineId = (cb_disciplineId.SelectedItem as Disciplines).id,
                    studGroupId = (cb_groupe.SelectedItem as StudGroups).id,
                    lectureHours = Convert.ToInt32(tb_lectureHours.Text),
                    practiceHours = Convert.ToInt32(tb_practiceHours.Text),
                    сonsultationHours = Convert.ToInt32(tb_сonsultationHours.Text),
                    courseprojectHours = Convert.ToInt32(tb_courseprojectHours.Text),
                    examHours = Convert.ToInt32(tb_examHours.Text)
                }; 
                MainTeachersLoad._teachersLoadContext.TeachersLoad.Add(teachersLoad);
            }
            MainTeachersLoad._teachersLoadContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
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