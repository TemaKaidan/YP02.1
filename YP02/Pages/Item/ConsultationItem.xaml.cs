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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для ConsultationItem.xaml
    /// </summary>
    public partial class ConsultationItem : UserControl
    {
        Pages.listPages.Consultation MainConsultation;
        Models.Consultations consultations;

        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();

        public ConsultationItem(Consultations consultations, Consultation MainConsultation)
        {
            InitializeComponent();
            this.consultations = consultations;
            this.MainConsultation = MainConsultation;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == consultations.disciplineId).name;
            lb_Date.Content = "Дата: " + consultations.date;
            lb_submittedWorks.Content = "Сданные работы: " + consultations.submittedWorks;

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationEdit, null, null, null, null, null, consultations);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainConsultation._consultationsContext.Consultations.Remove(consultations);
                MainConsultation._consultationsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}
