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

using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Kernel.Font;
using iText.Kernel.Colors;
using System.IO;
using iText.IO.Font;

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
            filePGF.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == consultationResults.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");
            
            lb_presence.Content="Присутсвтие (Да/Нет): " + consultationResults.explanatoryNote;
            lb_submittedPractice.Content= "Сданные ПР: " + consultationResults.submittedPractice;

            lb_Date.Content = "Дата: " + consultationResults.date;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ConsultationResults> consultationResults;
            using (var context = new ConsultationResultsContext())
            {
                consultationResults = context.ConsultationResults.ToList(); 
            }
            GeneratePdfWithTable(consultationResults);
        }

        public void GeneratePdfWithTable(List<ConsultationResults> consultationResults)
        {
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Результаты консультаций.pdf");

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);

                document.Add(new iText.Layout.Element.Paragraph("Результаты консультаций")
                    .SetFont(font)
                    .SetFontSize(18)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                iText.Layout.Element.Table table = new iText.Layout.Element.Table(4);

                table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("№").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("ФИО студента").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Сданные работы").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Дата").SetFont(font)));

                int rowNumber = 1;
                foreach (var result in consultationResults)
                {
                    Students student = new StudentsContext().Students.FirstOrDefault(x => x.id == result.studentId);

                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(rowNumber.ToString()).SetFont(font))); // Порядковый номер
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph($"{student?.surname} {student?.name} {student?.lastname}").SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(result.submittedPractice).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(result.date.ToString("dd.MM.yyyy")).SetFont(font)));

                    rowNumber++;
                }
                document.Add(table);
                document.Close();
            }
            MessageBox.Show($"PDF файл был успешно создан: {filePath}");
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