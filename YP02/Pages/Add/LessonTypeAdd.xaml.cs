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
    /// Логика взаимодействия для LessonTypeAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования типа занятия.
    /// </summary>
    public partial class LessonTypeAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с типами занятий.
        /// </summary>
        public Pages.listPages.LessonType MainLessonType;

        /// <summary>
        /// Модель типа занятия, которая может быть передана для редактирования.
        /// </summary>
        public Models.LessonTypes lessonTypes;

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования типа занятия.
        /// </summary>
        /// <param name="MainLessonType">Главная страница типов занятий.</param>
        /// <param name="lessonTypes">Модель типа занятия (если редактируется существующий тип, иначе null).</param>
        public LessonTypeAdd(Pages.listPages.LessonType MainLessonType, Models.LessonTypes lessonTypes = null)
        {
            InitializeComponent();
            this.MainLessonType = MainLessonType;
            this.lessonTypes = lessonTypes;
        }

        /// <summary>
        /// Метод для добавления нового типа занятия или редактирования существующего.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_LessonType(object sender, RoutedEventArgs e)
        {
            try
            {
                // Если тип занятия не передан (например, при добавлении нового), создаем новый объект типа занятия
                if (lessonTypes == null)
                {
                    lessonTypes = new Models.LessonTypes
                    {
                        typeName = tb_typeName.Text // Название типа занятия
                    };

                    // Добавляем новый тип занятия в контекст базы данных
                    MainLessonType._lessonTypesContext.LessonTypes.Add(lessonTypes);
                }

                // Сохраняем изменения в базе данных
                MainLessonType._lessonTypesContext.SaveChanges();

                // Переходим на страницу типов занятий
                MainWindow.init.OpenPages(MainWindow.pages.lessonType);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding LessonType", ex.Message, "Failed to save LessonType.");

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
