using System.Windows;
using System.Windows.Controls;
using YP02.Context;
using YP02.Models;
using YP02.Pages.listPages;

using System;
using System.IO;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font;
using iText.Layout.Properties;

using iText.Layout.Element;
using iText.Layout.Properties;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas;
using iText.IO.Font;
using iText.Layout;
using System.IO;
using iText.Kernel.Font;

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
        private readonly StudGroupsContext _studGroupsContext = new StudGroupsContext();

        public AbsenceItem(Absences absences, Absence MainAbsence)
        {
            InitializeComponent();
            this.absences = absences;
            this.MainAbsence = MainAbsence;

            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            filePDF.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            Students students = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);
            lb_Student.Content = $"Студент: {students.surname} {students.name} {students.lastname}";
            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == absences.disciplineId).name;
            lb_Minutes.Content = "Кол-во минут: " + absences.delayMinutes;
            lb_Explanation.Content = "Объяснитальная: " + absences.explanatoryNote;
        }

        public void GeneratePdf(Consultations consultation, Students student)
        {
            if (string.IsNullOrEmpty(absences.explanatoryNote) || absences.explanatoryNote.Trim().ToLower() == "нет")
            {
                MessageBox.Show("Невозможно создать файл, так как объяснительная отсутствует.");
                return;
            }

            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Объяснительная_{student.surname}_{student.name}.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);

                document.Add(new Paragraph("Зав. Отделением")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph("____________")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph("Студента")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFont(font)
                    .SetFontSize(14));

                var studGroup = _studGroupsContext.StudGroups.FirstOrDefault(x => x.id == student.studGroupId);
                document.Add(new Paragraph($"гр. {studGroup?.name}")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph($"{student.surname} {student.name} {student.lastname}")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.RIGHT)
                    .SetFont(font)
                    .SetFontSize(14));

                var boldText = new Text("Объяснительная");
                document.Add(new Paragraph(boldText)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFont(font)
                    .SetFontSize(16));

                document.Add(new Paragraph($"Я отсутствовал(а) на занятиях ____________ в течение ____________ пар, в связи с ____________. Обязуюсь восстановить конспект и сдать задолженности по пропущенным темам.")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph($"Дата: ____________")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph("Подпись: ____________")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph("Классный руководитель: ____________")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Add(new Paragraph("Зав. отделением: ____________")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.LEFT)
                    .SetFont(font)
                    .SetFontSize(14));

                document.Close();
            }
            MessageBox.Show($"PDF файл был успешно создан: {filePath}");
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
            Consultations consultation = _consultationsContext.Consultations.FirstOrDefault(x => x.id == absences.disciplineId);
            GeneratePdf(consultation, student);
        }
    }
}
