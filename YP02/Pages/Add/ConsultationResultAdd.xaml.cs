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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для ConsultationResultAdd.xaml
    /// </summary>
    public partial class ConsultationResultAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.ConsultationResult MainConsultationResult;
        public Models.ConsultationResults consultationResults;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.StudentsContext studentsContext  = new StudentsContext();

        public ConsultationResultAdd(Pages.listPages.ConsultationResult MainConsultationResult, Models.ConsultationResults consultationResults = null)
        {
            InitializeComponent();
            this.MainConsultationResult = MainConsultationResult;
            this.consultationResults = consultationResults;

            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname";
            cb_studentId.SelectedValuePath = "id";
        }

        private void Add_ConsultationResult(object sender, RoutedEventArgs e)
        {
            if (consultationResults == null)
            {
                consultationResults = new Models.ConsultationResults
                {
                    consultationId = 1,
                    studentId = (cb_studentId.SelectedItem as Models.Students).id,
                    presence = cb_presence.Text,
                    submittedPractice = tb_submittedPractice.Text,
                    date = db_date.SelectedDate ?? DateTime.MinValue,
                    explanatoryNote = cb_presence.Text
                };
                MainConsultationResult._consultationResultsContext.ConsultationResults.Add(consultationResults);
            }
            MainConsultationResult._consultationResultsContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
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
