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

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для ConsultationResultEdit.xaml
    /// </summary>
    public partial class ConsultationResultEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.ConsultationResult MainConsultationResult;
        public Models.ConsultationResults consultationResults;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.StudentsContext studentsContext = new StudentsContext();
        Context.ConsultationsContext consultationsContext = new ConsultationsContext();

        public ConsultationResultEdit(Pages.listPages.ConsultationResult MainConsultationResult, Models.ConsultationResults consultationResults = null)
        {
            InitializeComponent();
            this.MainConsultationResult = MainConsultationResult;
            this.consultationResults = consultationResults;

            foreach (Models.Students students in studentsContext.Students)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = students.surname + " " + students.name + " " + students.lastname;
                item.Tag = students.id;
                if (students.id == consultationResults.studentId)
                {
                    item.IsSelected = true;
                }
                cb_studentId.Items.Add(item);
            }

            if (consultationResults.explanatoryNote == "Да")
            {
                cb_explanatoryNote.SelectedItem = cb_explanatoryNote.Items.Cast<ComboBoxItem>()
                                          .FirstOrDefault(item => item.Content.ToString() == "Да");
            }
            else if (consultationResults.explanatoryNote == "Нет")
            {
                cb_explanatoryNote.SelectedItem = cb_explanatoryNote.Items.Cast<ComboBoxItem>()
                                          .FirstOrDefault(item => item.Content.ToString() == "Нет");
            }

            tb_submittedPractice.Text = consultationResults.submittedPractice;
            db_date.Text = consultationResults.date.ToString();
        }

        private void ExplanatoryNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)cb_explanatoryNote.SelectedItem;
            if (selectedItem != null)
            {
                consultationResults.explanatoryNote = selectedItem.Content.ToString();
            }
        }

        private void Edit_ConsultationResult(object sender, RoutedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)cb_explanatoryNote.SelectedItem;
            if (selectedItem != null)
            {
                consultationResults.explanatoryNote = selectedItem.Content.ToString();
            }
            Models.ConsultationResults editConsultationResult = MainConsultationResult._consultationResultsContext.ConsultationResults.FirstOrDefault(x => x.id == consultationResults.id);
            if (editConsultationResult != null)
            {
                editConsultationResult.studentId = (int)(cb_studentId.SelectedItem as ComboBoxItem).Tag;
                editConsultationResult.submittedPractice = tb_submittedPractice.Text;
                editConsultationResult.explanatoryNote = consultationResults.explanatoryNote;
                editConsultationResult.date = DateTime.Parse(db_date.Text);

                MainConsultationResult._consultationResultsContext.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
            }
            else
            {
                MessageBox.Show("Произошла ошибка!");
                MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
            }
        
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
