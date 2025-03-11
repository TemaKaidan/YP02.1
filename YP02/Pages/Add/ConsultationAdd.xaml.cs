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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для ConsultationAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления новой консультации.
    /// </summary>
    public partial class ConsultationAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с консультациями.
        /// </summary>
        public Pages.listPages.Consultation MainConsultation;

        /// <summary>
        /// Модель консультации, передаваемая для редактирования или добавления нового.
        /// </summary>
        public Models.Consultations consultations;

        /// <summary>
        /// Контекст базы данных для дисциплин.
        /// </summary>
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования консультации.
        /// </summary>
        /// <param name="MainConsultation">Главная страница для работы с консультациями.</param>
        /// <param name="consultations">Модель консультации (если редактируется существующий, иначе null).</param>
        public ConsultationAdd(Pages.listPages.Consultation MainConsultation, Models.Consultations consultations = null)
        {
            InitializeComponent();
            this.MainConsultation = MainConsultation;
            this.consultations = consultations;

            // Заполнение списка дисциплин для выбора
            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name"; // Отображение названия дисциплины
            cb_disciplineId.SelectedValuePath = "id"; // Привязка к ID дисциплины
        }

        /// <summary>
        /// Метод для добавления новой консультации или редактирования существующей.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Consultation(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если консультация не передана (при добавлении новой)
                if (consultations == null)
                {
                    consultations = new Models.Consultations
                    {
                        disciplineId = (cb_disciplineId.SelectedItem as Models.Disciplines).id, // ID выбранной дисциплины
                        date = db_date.SelectedDate ?? DateTime.MinValue, // Дата консультации (если дата не выбрана, ставится минимальная дата)
                        submittedWorks = tb_submittedWorks.Text // Сданные работы
                    };

                    // Добавление новой консультации в контекст базы данных
                    MainConsultation._consultationsContext.Consultations.Add(consultations);
                }

                // Сохранение изменений в базе данных
                MainConsultation._consultationsContext.SaveChanges();

                // Переход на страницу с консультациями
                MainWindow.init.OpenPages(MainWindow.pages.consultation);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding Consultation", ex.Message, "Failed to save Consultation.");

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
