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
    /// Логика взаимодействия для MarkItem.xaml
    /// </summary>
    public partial class MarkItem : UserControl
    {
        Pages.listPages.Mark MainMark;
        Models.Marks marks;

        public MarkItem(Models.Marks marks, Mark MainMark)
        {
            InitializeComponent();
            this.marks = marks;
            this.MainMark = MainMark;

            int markValue;
            if (int.TryParse(marks.mark, out markValue)) // Пробуем преобразовать строку в число
            {
                SetMarkColor(markValue); // Если преобразование успешно, передаем в SetMarkColor
            }
            else
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Black); // Если не удалось преобразовать, ставим чёрный цвет
            }

            lb_mark.Content = "Оценка: " + marks.mark;

            DisciplineProgramsContext _disciplinesContext = new DisciplineProgramsContext();
            var disciplines = _disciplinesContext.DisciplinePrograms.FirstOrDefault(g => g.id == marks.disciplineProgramId);
            lb_disciplineProgramId.Content = "Занятие: " + (disciplines != null ? disciplines.theme : "Неизвестно");

            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == marks.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");

            lb_description.Content = "Описание: " + marks.description;
        }

        private void SetMarkColor(int mark)
        {
            // Устанавливаем цвет в зависимости от оценки
            if (mark == 5)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Green); // Оценка 5 - зелёный
            }
            else if (mark == 4)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.DarkGoldenrod); // Оценка 4 - тёмно-жёлтый
            }
            else if (mark == 3)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Orange); // Оценка 3 - оранжевый
            }
            else if (mark == 2)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Red); // Оценка 2 - красный
            }
            else
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Black); // Для других значений
            }
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.markEdit, null,null,null,null,null,null,null,null,null,null,null,marks);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainMark._marksContext.Marks.Remove(marks);
                MainMark._marksContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}
