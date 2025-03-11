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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для AbsenceAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления нового отсутствия студента.
    /// </summary>
    public partial class AbsenceAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с отсутствиями.
        /// </summary>
        public Pages.listPages.Absence MainAbsence;

        /// <summary>
        /// Модель отсутствия, передаваемая для редактирования или добавления нового.
        /// </summary>
        public Models.Absences absences;

        /// <summary>
        /// Контекст базы данных для студентов.
        /// </summary>
        Context.StudentsContext studentsContext = new StudentsContext();

        /// <summary>
        /// Контекст базы данных для дисциплин.
        /// </summary>
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования отсутствия.
        /// </summary>
        /// <param name="MainAbsence">Главная страница для работы с отсутствиями.</param>
        /// <param name="absences">Модель отсутствия (если редактируется существующий, иначе null).</param>
        public AbsenceAdd(Pages.listPages.Absence MainAbsence, Models.Absences absences = null)
        {
            InitializeComponent();
            this.MainAbsence = MainAbsence;
            this.absences = absences;

            // Заполнение списка студентов для выбора
            cb_studentId.Items.Clear();
            cb_studentId.ItemsSource = studentsContext.Students.ToList();
            cb_studentId.DisplayMemberPath = "surname"; // Отображение фамилии студента
            cb_studentId.SelectedValuePath = "id"; // Привязка к ID студента

            // Заполнение списка дисциплин для выбора
            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name"; // Отображение названия дисциплины
            cb_disciplineId.SelectedValuePath = "id"; // Привязка к ID дисциплины
        }

        /// <summary>
        /// Метод для добавления нового отсутствия или редактирования существующего.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Absence(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если отсутствие не передано (при добавлении нового)
                if (absences == null)
                {
                    absences = new Models.Absences
                    {
                        studentId = (cb_studentId.SelectedItem as Models.Students).id, // ID выбранного студента
                        disciplineId = (cb_disciplineId.SelectedItem as Models.Disciplines).id, // ID выбранной дисциплины
                        delayMinutes = Convert.ToInt32(tb_delayMinutes.Text), // Задержка в минутах
                        explanatoryNote = cb_explanatoryNote.Text // Объяснительная записка
                    };

                    // Добавление нового отсутствия в контекст базы данных
                    MainAbsence._absencesContext.Absences.Add(absences);
                }

                // Сохранение изменений в базе данных
                MainAbsence._absencesContext.SaveChanges();

                // Переход на страницу с отсутствиями
                MainWindow.init.OpenPages(MainWindow.pages.absence);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding Absence", ex.Message, "Failed to save Absence.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
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
