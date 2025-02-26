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

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для MarkItem.xaml
    /// </summary>
    public partial class MarkItem : UserControl
    {
        private Models.Marks _marks;
        private readonly DisciplineProgramsContext _disciplineProgramsContext;
        private readonly StudentsContext _studentsContext;

        public MarkItem(Models.Marks marks)
        {
            InitializeComponent();
            _marks = marks;

            lb_date.Content = "Дата: " + marks.date;
            lb_mark.Content = "Оценка: " + marks.mark;

            DisciplineProgramsContext _disciplinePrograms = new DisciplineProgramsContext();
            var disciplinePrograms = _disciplinePrograms.DisciplinePrograms.FirstOrDefault(g => g.id == marks.disciplineProgramId);
            lb_disciplineProgramId.Content = "Программа дисциплина: " + (disciplinePrograms != null ? disciplinePrograms.theme : "Неизвестно");

            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == marks.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");

            lb_description.Content = "Описание: " + marks.description;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
