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
using YP02.Log;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для AbsenceItem.xaml
    /// </summary>
    public partial class AbsenceItem : UserControl
    {
        // Основные объекты для работы с отсутствиями
        Pages.listPages.Absence MainAbsence;
        Models.Absences absences;

        // Контексты для работы с данными о дисциплинах, студентах, консультациях и учебных группах
        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();
        private readonly StudentsContext _studentsContext = new StudentsContext();
        private readonly ConsultationsContext _consultationsContext = new ConsultationsContext();
        private readonly StudGroupsContext _studGroupsContext = new StudGroupsContext();

        /// <summary>
        /// Конструктор для инициализации компонента AbsenceItem с заданными данными
        /// </summary>
        public AbsenceItem(Absences absences, Absence MainAbsence)
        {
            InitializeComponent();
            this.absences = absences;
            this.MainAbsence = MainAbsence;

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            filePDF.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Получение данных студента по его ID
            Students students = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);
            lb_Student.Content = $"Студент: {students.surname} {students.name} {students.lastname}";

            // Получение названия дисциплины по ID
            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == absences.disciplineId).name;

            // Отображение информации об опоздании и объяснительной
            lb_Minutes.Content = "Кол-во минут: " + absences.delayMinutes;
            lb_Explanation.Content = "Объяснитальная: " + absences.explanatoryNote;
        }

        /// <summary>
        /// Генерация PDF документа с объяснительной
        /// </summary>
        public void GeneratePdf(Consultations consultation, Students student)
        {
            // Проверка наличия объяснительной
            if (string.IsNullOrEmpty(absences.explanatoryNote) || absences.explanatoryNote.Trim().ToLower() == "нет")
            {
                MessageBox.Show("Невозможно создать файл, так как объяснительная отсутствует.");
                return;
            }

            // Определение пути для сохранения файла PDF
            string filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"Объяснительная_{student.surname}_{student.name}.pdf");

            // Создание и запись в файл PDF
            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                PdfWriter writer = new PdfWriter(fs);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);

                // Установка шрифта для документа
                PdfFont font = PdfFontFactory.CreateFont("c:/windows/fonts/arial.ttf", PdfEncodings.IDENTITY_H);

                // Добавление текста в PDF
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

                // Заголовок "Объяснительная"
                var boldText = new Text("Объяснительная");
                document.Add(new Paragraph(boldText)
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                    .SetFont(font)
                    .SetFontSize(16));

                // Основной текст объяснительной
                document.Add(new Paragraph($"Я отсутствовал(а) на занятиях ____________ в течение ____________ пар, в связи с ____________. Обязуюсь восстановить конспект и сдать задолженности по пропущенным темам.")
                    .SetTextAlignment(iText.Layout.Properties.TextAlignment.JUSTIFIED)
                    .SetFont(font)
                    .SetFontSize(14));

                // Дата, подпись, классный руководитель и зав. отделением
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

            // Сообщение о успешном создании PDF
            MessageBox.Show($"PDF файл был успешно создан: {filePath}");
        }

        /// <summary>
        /// Обработчик для кнопки редактирования
        /// </summary>
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.absenceEdit, null, null, null, null, null, null, null, null, null,absences);
        }

        /// <summary>
        /// Обработчик для кнопки удаления
        /// </summary>
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление отсутствия из базы
                    MainAbsence._absencesContext.Absences.Remove(absences);
                    MainAbsence._absencesContext.SaveChanges();

                    // Удаление элемента из UI
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Absence", ex.Message, "Failed to save Absence.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик для кнопки создания PDF
        /// </summary>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Students student = _studentsContext.Students.FirstOrDefault(x => x.id == absences.studentId);
            Consultations consultation = _consultationsContext.Consultations.FirstOrDefault(x => x.id == absences.disciplineId);
            GeneratePdf(consultation, student);
        }
    }
}