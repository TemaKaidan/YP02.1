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
    /// Логика взаимодействия для MarkAdd.xaml
    /// </summary>
    public partial class MarkAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Mark MainMark;
        public Models.Marks marks;

        Context.StudentsContext studentsContext = new Context.StudentsContext();
        Context.DisciplineProgramsContext disciplinesContext = new Context.DisciplineProgramsContext();

        public MarkAdd(Pages.listPages.Mark MainMark, Models.Marks marks = null)
        {
            InitializeComponent();
            this.MainMark = MainMark;
            this.marks = marks;

            cb_disciplineProgramId.Items.Clear();
            cb_disciplineProgramId.ItemsSource = disciplinesContext.DisciplinePrograms.ToList();
            cb_disciplineProgramId.DisplayMemberPath = "theme";
            cb_disciplineProgramId.SelectedValuePath = "id";

            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname";
            cb_studentId.SelectedValuePath = "id";
        }
        private void Add_Marks(object sender, RoutedEventArgs e)
        {
            if (marks == null)
            {
                marks = new Models.Marks
                {
                    mark = tb_mark.Text,
                    disciplineProgramId = (cb_disciplineProgramId.SelectedItem as Models.DisciplinePrograms).id,
                    studentId = (cb_studentId.SelectedItem as Models.Students).id,
                    description = tb_discription.Text
                };
                MainMark._marksContext.Marks.Add(marks);
            }
            MainMark._marksContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.marks);
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

