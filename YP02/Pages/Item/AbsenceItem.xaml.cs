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

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для AbsenceItem.xaml
    /// </summary>
    public partial class AbsenceItem : UserControl
    {
        private Absences _absences;
        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();
        private readonly StudentsContext _studentsContext = new StudentsContext();
        public AbsenceItem(Absences absences)
        {
            InitializeComponent();
            _absences = absences;
            Students students = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);
            lb_Student.Content = $"Студент: {students.surname} {students.name} {students.lastname}";
            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == absences.disciplineId).name;
            lb_Date.Content = "Дата: " + absences.date;
            lb_Minutes.Content = "Кол-во минут: " + absences.delayMinutes;
            lb_Explanation.Content = "Объяснитальная: " + absences.explanatoryNote;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
