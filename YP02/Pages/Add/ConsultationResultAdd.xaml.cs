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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для ConsultationResultAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления результата консультации.
    /// </summary>
    public partial class ConsultationResultAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с результатами консультации.
        /// </summary>
        public Pages.listPages.ConsultationResult MainConsultationResult;

        /// <summary>
        /// Модель результата консультации, передаваемая для редактирования или добавления нового.
        /// </summary>
        public Models.ConsultationResults consultationResults;

        /// <summary>
        /// Контекст базы данных для дисциплин.
        /// </summary>
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Контекст базы данных для студентов.
        /// </summary>
        Context.StudentsContext studentsContext = new StudentsContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования результата консультации.
        /// </summary>
        /// <param name="MainConsultationResult">Главная страница для работы с результатами консультации.</param>
        /// <param name="consultationResults">Модель результата консультации (если редактируется существующий, иначе null).</param>
        public ConsultationResultAdd(Pages.listPages.ConsultationResult MainConsultationResult, Models.ConsultationResults consultationResults = null)
        {
            InitializeComponent();
            this.MainConsultationResult = MainConsultationResult;
            this.consultationResults = consultationResults;

            // Заполнение списка студентов для выбора
            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname"; // Отображение фамилии студента
            cb_studentId.SelectedValuePath = "id"; // Привязка к ID студента
        }

        /// <summary>
        /// Метод для добавления нового результата консультации или редактирования существующего.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_ConsultationResult(object sender, RoutedEventArgs e)
        {
            // Если результат консультации не передан (например, при добавлении нового)
            if (consultationResults == null)
            {
                consultationResults = new Models.ConsultationResults
                {
                    consultationId = 1, // Пример ID консультации (следует заменить на реальный, если необходимо)
                    studentId = (cb_studentId.SelectedItem as Models.Students).id, // ID выбранного студента
                    presence = cb_presence.Text, // Присутствие
                    submittedPractice = tb_submittedPractice.Text, // Сданная практика
                    date = db_date.SelectedDate ?? DateTime.MinValue, // Дата консультации (если дата не выбрана, ставится минимальная дата)
                    explanatoryNote = cb_presence.Text // Примечание (пока устанавливаем по значению присутствия)
                };

                // Добавление нового результата в контекст базы данных
                MainConsultationResult._consultationResultsContext.ConsultationResults.Add(consultationResults);
            }

            // Сохранение изменений в базе данных
            MainConsultationResult._consultationResultsContext.SaveChanges();

            // Переход на страницу с результатами консультаций
            MainWindow.init.OpenPages(MainWindow.pages.consultationResult);
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
    }
}
