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
    /// Логика взаимодействия для ConsultationResultItem.xaml
    /// </summary>
    public partial class ConsultationResultItem : UserControl
    {
        Pages.listPages.ConsultationResult MainConsultationResult;
        Models.ConsultationResults consultationResults;

        public ConsultationResultItem(ConsultationResults consultationResults, ConsultationResult MainConsultationResult)
        {
            InitializeComponent();
            this.consultationResults = consultationResults;
            this.MainConsultationResult = MainConsultationResult;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == consultationResults.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");
            
            lb_presence.Content="Присутсвтие (Да/Нет): " + consultationResults.explanatoryNote;
            lb_submittedPractice.Content= "Сданные ПР: " + consultationResults.submittedPractice;

            lb_Date.Content = "Дата: " + consultationResults.date;

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResultEdit, null,null,null,null,null,null,null,null,null,null,null,null,consultationResults);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainConsultationResult._consultationResultsContext.ConsultationResults.Remove(consultationResults);
                MainConsultationResult._consultationResultsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}
