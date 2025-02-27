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
    /// Логика взаимодействия для DisciplineProgramAdd.xaml
    /// </summary>
    public partial class DisciplineProgramAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.DisciplineProgram MainDisciplineProgram;
        public Models.DisciplinePrograms programs;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.LessonTypesContext lessonTypesContext = new LessonTypesContext();

        public DisciplineProgramAdd(Pages.listPages.DisciplineProgram MainDisciplineProgram, Models.DisciplinePrograms programs = null)
        {
            InitializeComponent();
            this.MainDisciplineProgram = MainDisciplineProgram;
            this.programs = programs;

            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name";
            cb_disciplineId.SelectedValuePath = "id";

            cb_lessonTypeId.Items.Clear();
            cb_lessonTypeId.ItemsSource = lessonTypesContext.LessonTypes.ToList();
            cb_lessonTypeId.DisplayMemberPath = "typeName";
            cb_lessonTypeId.SelectedValuePath = "id";
        }

        private void Add_DisciplineProgram(object sender, RoutedEventArgs e)
        {
            if (programs == null)
            {
                programs = new Models.DisciplinePrograms 
                {
                    disciplineId = (cb_disciplineId.SelectedItem as Disciplines).id,
                    theme = tb_theme.Text,
                    lessonTypeId = (cb_lessonTypeId.SelectedItem as LessonTypes).id,
                    hoursCount = Convert.ToInt32(tb_hoursCount.Text)
                };
                MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.Add(programs);
            }
            MainDisciplineProgram._disciplinePrograms.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
    }
}
