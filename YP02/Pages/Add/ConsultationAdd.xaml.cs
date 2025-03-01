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
    /// Логика взаимодействия для ConsultationAdd.xaml
    /// </summary>
    public partial class ConsultationAdd : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Consultation MainConsultation;
        public Models.Consultations consultations;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        public ConsultationAdd(Pages.listPages.Consultation MainConsultation, Models.Consultations consultations = null)
        {
            InitializeComponent();
            this.MainConsultation = MainConsultation;
            this.consultations = consultations;

            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name";
            cb_disciplineId.SelectedValuePath = "id";
        }

        private void Add_Consultation(object sender, RoutedEventArgs e)
        {
            if (consultations == null)
            {
                consultations = new Models.Consultations
                {
                    disciplineId = (cb_disciplineId.SelectedItem as Models.Disciplines).id,
                    date = db_date.SelectedDate ?? DateTime.MinValue
                };
                MainConsultation._consultationsContext.Consultations.Add(consultations);
            }
            MainConsultation._consultationsContext.SaveChanges();
            MainWindow.init.OpenPages(MainWindow.pages.consultation);
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
