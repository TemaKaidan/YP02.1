using System.Windows;
using System.Windows.Controls;
using YP02.Context;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для AbsenceItem.xaml
    /// </summary>
    public partial class AbsenceItem : UserControl
    {
        Pages.listPages.Absence MainAbsence;
        Models.Absences absences;

        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();
        private readonly StudentsContext _studentsContext = new StudentsContext();
        private readonly ConsultationsContext _consultationsContext = new ConsultationsContext();
        public AbsenceItem(Absences absences, Absence MainAbsence)
        {
            InitializeComponent();
            this.absences = absences;
            this.MainAbsence = MainAbsence;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            Students students = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);
            lb_Student.Content = $"Студент: {students.surname} {students.name} {students.lastname}";
            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == absences.disciplineId).name;
            lb_Minutes.Content = "Кол-во минут: " + absences.delayMinutes;
            lb_Explanation.Content = "Объяснитальная: " + absences.explanatoryNote;
        }

        public void GeneratePdf(Consultations consultation, Students student)
        {

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absenceEdit,null,null,null,null,null,null,null,null,null, absences);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainAbsence._absencesContext.Absences.Remove(absences);
                MainAbsence._absencesContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Students student = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);

            // Получаем консультацию из правильного контекста
            Consultations consultation = _consultationsContext.Consultations.FirstOrDefault(x => x.id == absences.disciplineId);

            // Генерация PDF
            GeneratePdf(consultation, student);
        }
    }
}
