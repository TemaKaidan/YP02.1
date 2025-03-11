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
using YP02.Log;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для ConsultationResultItem.xaml
    /// </summary>
    public partial class ConsultationResultItem : UserControl
    {
        // Основные объекты для работы с результатами консультаций
        Pages.listPages.ConsultationResult MainConsultationResult;
        Models.ConsultationResults consultationResults;

        /// <summary>
        /// Конструктор для инициализации компонента ConsultationResultItem с заданными данными
        /// </summary>
        public ConsultationResultItem(ConsultationResults consultationResults, ConsultationResult MainConsultationResult)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.consultationResults = consultationResults; // Присваиваем данные результата консультации
            this.MainConsultationResult = MainConsultationResult; // Присваиваем основной объект для результата консультации

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            filePGF.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Получение данных о студенте по его ID
            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == consultationResults.studentId);

            // Отображение информации о студенте
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " +
                                   (studentsContext != null ? studentsContext.name : "Неизвестно") + " " +
                                   (studentsContext != null ? studentsContext.lastname : "Неизвестно");

            // Отображение информации о присутствии и сданных работах
            lb_presence.Content = "Присутсвтие (Да/Нет): " + consultationResults.explanatoryNote;
            lb_submittedPractice.Content = "Сданные ПР: " + consultationResults.submittedPractice;

            // Отображение даты консультации
            lb_Date.Content = "Дата: " + consultationResults.date;
        }

        // Обработчик для кнопки, генерирующей PDF
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            List<ConsultationResults> consultationResults;
            using (var context = new ConsultationResultsContext())
            {
                consultationResults = context.ConsultationResults.ToList(); // Получение списка всех результатов консультаций
            }
            GeneratePdfWithTable(consultationResults); // Генерация PDF
        }

        /// <summary>
        /// Метод для генерации PDF с таблицей всех результатов консультаций
        /// </summary>
        public void GeneratePdfWithTable(List<ConsultationResults> consultationResults)
        {
            // Путь для сохранения PDF на рабочем столе
            string filePath = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Результаты консультаций.pdf");

            // Открытие потока для записи PDF
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);

                // Добавление заголовка в PDF
                document.Add(new iText.Layout.Element.Paragraph("Результаты консультаций")
                    .SetFont(font)
                    .SetFontSize(18)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER));

                // Создание таблицы для отображения данных
                iText.Layout.Element.Table table = new iText.Layout.Element.Table(4);
                table.SetHorizontalAlignment(iText.Layout.Properties.HorizontalAlignment.CENTER);

                // Заголовки колонок таблицы
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("№").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("ФИО студента").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Сданные работы").SetFont(font)));
                table.AddHeaderCell(new Cell().Add(new iText.Layout.Element.Paragraph("Дата").SetFont(font)));

                // Добавление данных о каждом результате консультации в таблицу
                int rowNumber = 1;
                foreach (var result in consultationResults)
                {
                    Students student = new StudentsContext().Students.FirstOrDefault(x => x.id == result.studentId);

                    // Добавление строк в таблицу
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(rowNumber.ToString()).SetFont(font))); // Порядковый номер
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph($"{student?.surname} {student?.name} {student?.lastname}").SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(result.submittedPractice).SetFont(font)));
                    table.AddCell(new Cell().Add(new iText.Layout.Element.Paragraph(result.date.ToString("dd.MM.yyyy")).SetFont(font)));

                    rowNumber++;
                }

                // Добавление таблицы в документ
                document.Add(table);
                document.Close(); // Закрытие документа
            }

            // Вывод сообщения об успешном создании PDF
            MessageBox.Show($"PDF файл был успешно создан: {filePath}");
        }

        // Обработчик для кнопки редактирования результата консультации
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.consultationResultEdit, null, null, null, null, null, null, null, null, null, null, null, null, consultationResults);
        }

        // Обработчик для кнопки удаления результата консультации
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление результата консультации из базы данных
                    MainConsultationResult._consultationResultsContext.ConsultationResults.Remove(consultationResults);
                    MainConsultationResult._consultationResultsContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting ConsultationResults", ex.Message, "Failed to save ConsultationResults.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}