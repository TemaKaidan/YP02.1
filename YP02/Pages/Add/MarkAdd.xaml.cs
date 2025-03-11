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
using YP02.Log;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для MarkAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования оценки студента.
    /// </summary>
    public partial class MarkAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с оценками.
        /// </summary>
        public Pages.listPages.Mark MainMark;

        /// <summary>
        /// Модель оценки, которая может быть передана для редактирования.
        /// </summary>
        public Models.Marks marks;

        /// <summary>
        /// Контекст для работы с данными студентов.
        /// </summary>
        Context.StudentsContext studentsContext = new Context.StudentsContext();

        /// <summary>
        /// Контекст для работы с программами дисциплин.
        /// </summary>
        Context.DisciplineProgramsContext disciplinesContext = new Context.DisciplineProgramsContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования оценки.
        /// </summary>
        /// <param name="MainMark">Главная страница оценок.</param>
        /// <param name="marks">Модель оценки (если редактируется существующая оценка, иначе null).</param>
        public MarkAdd(Pages.listPages.Mark MainMark, Models.Marks marks = null)
        {
            InitializeComponent();
            this.MainMark = MainMark;
            this.marks = marks;

            // Заполнение комбинированного списка дисциплин, доступных для выбора
            cb_disciplineProgramId.Items.Clear();
            cb_disciplineProgramId.ItemsSource = disciplinesContext.DisciplinePrograms.ToList();
            cb_disciplineProgramId.DisplayMemberPath = "theme"; // Отображаем название темы дисциплины
            cb_disciplineProgramId.SelectedValuePath = "id"; // Идентификатор дисциплины

            // Заполнение комбинированного списка студентов, доступных для выбора
            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname"; // Отображаем фамилию студента
            cb_studentId.SelectedValuePath = "id"; // Идентификатор студента
        }

        /// <summary>
        /// Метод для добавления или редактирования оценки студента.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Marks(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если оценка не передана, то добавляем новую
                if (marks == null)
                {
                    // Создаем новый объект оценки
                    marks = new Models.Marks
                    {
                        mark = tb_mark.Text, // Оценка студента
                        disciplineProgramId = (cb_disciplineProgramId.SelectedItem as Models.DisciplinePrograms).id, // Выбранная дисциплина
                        studentId = (cb_studentId.SelectedItem as Models.Students).id, // Выбранный студент
                        description = tb_discription.Text // Описание оценки
                    };

                    // Добавляем новую оценку в контекст базы данных
                    MainMark._marksContext.Marks.Add(marks);
                }

                // Сохраняем изменения в базе данных
                MainMark._marksContext.SaveChanges();

                // Переходим на страницу с оценками
                MainWindow.init.OpenPages(MainWindow.pages.marks);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding Marks", ex.Message, "Failed to save Marks.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Метод для переключения состояния меню (свернуто/развернуто).
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            // Создание анимации для изменения ширины меню
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Логика изменения ширины и видимости элементов в меню
            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
                foreach (UIElement element in MenuPanel.Children)
                {
                    // Отображаем все кнопки, кроме кнопки с символом "☰"
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
                    // Скрываем все кнопки, кроме кнопки с символом "☰"
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Устанавливаем продолжительность анимации
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            // Запуск анимации для изменения ширины панели меню
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключаем флаг состояния меню
            isMenuCollapsed = !isMenuCollapsed;
        }

        /// <summary>
        /// Обработчик для кнопки "Отменить", которая возвращает пользователя на предыдущую страницу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            // Переход назад на предыдущую страницу
            NavigationService.GoBack();
        }
    }
}

