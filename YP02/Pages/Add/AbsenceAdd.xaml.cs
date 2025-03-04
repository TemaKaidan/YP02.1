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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для AbsenceAdd.xaml
    /// </summary>
    public partial class AbsenceAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Absence MainAbsence;
        public Models.Absences absences;

        Context.StudentsContext studentsContext = new StudentsContext();
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        public AbsenceAdd(Pages.listPages.Absence MainAbsence, Models.Absences absences = null)
        {
            InitializeComponent();
            this.MainAbsence = MainAbsence;
            this.absences = absences;

            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname";
            cb_studentId.SelectedValuePath = "id";

            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name";
            cb_disciplineId.SelectedValuePath = "id";
        }

        private void Add_Absence(object sender, RoutedEventArgs e)
        {
            if (absences == null)
            {
                absences = new Models.Absences
                {
                    studentId = (cb_studentId.SelectedItem as Models.Students).id,
                    disciplineId = (cb_disciplineId.SelectedItem as Models.Disciplines).id,
                    date = db_date.SelectedDate ?? DateTime.MinValue,
                    delayMinutes = Convert.ToInt32(tb_delayMinutes.Text),
                    explanatoryNote = cb_explanatoryNote.Text
                };
                MainAbsence._absencesContext.Absences.Add(absences);
            }
            MainAbsence._absencesContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.absence);
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
