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
    /// Логика взаимодействия для MarkEdit.xaml
    /// </summary>
    public partial class MarkEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с отметками студентов
        public Pages.listPages.Mark MainMark;
        public Models.Marks marks;

        // Контексты для работы с данными студентов и дисциплин
        Context.StudentsContext studentsContext = new Context.StudentsContext();
        Context.DisciplineProgramsContext disciplineProgramsContext = new Context.DisciplineProgramsContext();

        // Конструктор, инициализирующий компоненты и заполняющий поля формы
        public MarkEdit(Pages.listPages.Mark MainMark, Models.Marks marks = null)
        {
            InitializeComponent();
            this.MainMark = MainMark;
            this.marks = marks;

            // Заполнение ComboBox с данными студентов
            foreach (Models.Students students in studentsContext.Students)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = students.surname + " " + students.name + " " + students.lastname;
                item.Tag = students.id;

                // Устанавливаем выбранного студента
                if (students.id == marks.studentId)
                {
                    item.IsSelected = true;
                }
                cb_studentId.Items.Add(item);
            }

            // Заполнение ComboBox с дисциплинами
            foreach (DisciplinePrograms disciplinePrograms in disciplineProgramsContext.DisciplinePrograms)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = disciplinePrograms.theme;
                item.Tag = disciplinePrograms.id;

                // Устанавливаем выбранную дисциплину
                if (disciplinePrograms.id == marks.disciplineProgramId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineProgramId.Items.Add(item);
            }

            // Заполнение полей формы данными отметки
            tb_mark.Text = marks.mark;
            tb_discription.Text = marks.description;
        }

        // Обработчик для редактирования отметки
        private void Edit_Marks(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем отметку из контекста и обновляем её
                Marks editMarks = MainMark._marksContext.Marks.FirstOrDefault(x => x.id == marks.id);
                if (editMarks != null)
                {
                    // Обновляем данные отметки
                    editMarks.studentId = (int)(cb_studentId.SelectedItem as ComboBoxItem).Tag;
                    editMarks.disciplineProgramId = (int)(cb_disciplineProgramId.SelectedItem as ComboBoxItem).Tag;
                    editMarks.mark = tb_mark.Text;
                    editMarks.description = tb_discription.Text;

                    // Сохраняем изменения в базе данных
                    MainMark._marksContext.SaveChanges();

                    // Переходим на страницу списка отметок
                    MainWindow.init.OpenPages(MainWindow.pages.marks);
                }
                else
                {
                    // Если не удалось найти отметку для редактирования
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении отметки
                ErrorLogger.LogError("Error updating Marks", ex.Message, "Failed to save Marks.");

                // Показываем сообщение об ошибке пользователю
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для сворачивания и разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Проверяем текущее состояние меню (свёрнуто или развернуто)
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;

                // Делаем видимыми все кнопки в меню, кроме кнопки "☰"
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

                // Прячем все кнопки в меню, кроме кнопки "☰"
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Настроим продолжительность анимации и применим её к ширине меню
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключаем состояние меню
            isMenuCollapsed = !isMenuCollapsed;
        }

        // Обработчик кнопки "Отмена", возвращает пользователя на предыдущую страницу
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
