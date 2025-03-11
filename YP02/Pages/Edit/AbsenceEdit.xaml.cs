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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для AbsenceEdit.xaml
    /// </summary>
    public partial class AbsenceEdit : Page
    {
        // Поле для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты, представляющие основное отсутствие и список всех отсутствий
        public Pages.listPages.Absence MainAbsence;
        public Models.Absences absences;

        // Контексты для работы с данными студентов и дисциплин
        Context.StudentsContext studentsContext = new StudentsContext();
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        // Конструктор, инициализирующий компоненты и заполняющий выпадающие списки данными
        public AbsenceEdit(Pages.listPages.Absence MainAbsence, Models.Absences absences = null)
        {
            InitializeComponent();
            this.MainAbsence = MainAbsence;
            this.absences = absences;

            // Заполнение комбинированного списка студентов
            foreach (Models.Students students in studentsContext.Students)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = students.surname + " " + students.name + " " + students.lastname;
                item.Tag = students.id;
                if (students.id == absences.studentId)
                {
                    item.IsSelected = true;
                }
                cb_studentId.Items.Add(item);
            }

            // Заполнение комбинированного списка дисциплин
            foreach (Disciplines discipline in disciplinesContext.Disciplines)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = discipline.name;
                item.Tag = discipline.id;
                if (discipline.id == absences.disciplineId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineId.Items.Add(item);
            }

            // Установка задержки в текстовое поле
            tb_delayMinutes.Text = absences.delayMinutes.ToString();

            // Установка значения для объяснительной записки
            if (absences.explanatoryNote == "Есть")
            {
                cb_explanatoryNote.SelectedIndex = 0;
            }
            else if (absences.explanatoryNote == "Нет")
            {
                cb_explanatoryNote.SelectedIndex = 1;
            }
        }

        // Обработчик изменения выбранного значения в комбинированном списке объяснительной записки
        private void ExplanatoryNote_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItem = (ComboBoxItem)cb_explanatoryNote.SelectedItem;
            if (selectedItem != null)
            {
                string selectedValue = selectedItem.Content.ToString();
                absences.explanatoryNote = selectedValue;
            }
        }

        // Обработчик редактирования отсутствия
        private void Edit_Absence(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на корректность ввода количества минут задержки
                if (int.TryParse(tb_delayMinutes.Text, out int delayMinutes))
                {
                    absences.delayMinutes = delayMinutes;
                }
                else
                {
                    MessageBox.Show("Пожалуйста, введите корректное количество минут.");
                }

                // Сохранение выбранной объяснительной записки
                var selectedItem = (ComboBoxItem)cb_explanatoryNote.SelectedItem;
                if (selectedItem != null)
                {
                    absences.explanatoryNote = selectedItem.Content.ToString();
                }

                // Сохранение выбранного студента
                var selectedStudentItem = (ComboBoxItem)cb_studentId.SelectedItem;
                if (selectedStudentItem != null)
                {
                    absences.studentId = (int)selectedStudentItem.Tag;
                }

                // Сохранение выбранной дисциплины
                var selectedDisciplineItem = (ComboBoxItem)cb_disciplineId.SelectedItem;
                if (selectedDisciplineItem != null)
                {
                    absences.disciplineId = (int)selectedDisciplineItem.Tag;
                }

                try
                {
                    // Работа с контекстом данных для обновления отсутствия в базе данных
                    Context.AbsencesContext absencesContext = new Context.AbsencesContext();

                    var existingAbsence = absencesContext.Absences.FirstOrDefault(a => a.id == absences.id);

                    if (existingAbsence != null)
                    {
                        // Обновление информации о пропуске
                        existingAbsence.studentId = absences.studentId;
                        existingAbsence.disciplineId = absences.disciplineId;
                        existingAbsence.delayMinutes = absences.delayMinutes;
                        existingAbsence.explanatoryNote = absences.explanatoryNote;

                        absencesContext.SaveChanges();
                        MainWindow.init.OpenPages(MainWindow.pages.absence);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}");
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при сохранении данных
                ErrorLogger.LogError("Error updating Absence", ex.Message, "Failed to save Absence.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для сворачивания и разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible;
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Анимация ширины меню
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        // Обработчик кнопки "Отмена", возвращающий на предыдущую страницу
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}