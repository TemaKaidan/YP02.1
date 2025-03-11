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
    /// Логика взаимодействия для GroupeAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования группы студентов.
    /// </summary>
    public partial class GroupeAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с группами студентов.
        /// </summary>
        public Pages.listPages.Group MainGrope;

        /// <summary>
        /// Модель группы студентов, которая может быть передана для редактирования.
        /// </summary>
        public Models.StudGroups studGroups;

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования группы студентов.
        /// </summary>
        /// <param name="MainGrope">Главная страница для работы с группами студентов.</param>
        /// <param name="studGroups">Модель группы студентов (если редактируется существующая группа, иначе null).</param>
        public GroupeAdd(Pages.listPages.Group MainGrope, Models.StudGroups studGroups = null)
        {
            InitializeComponent();
            this.MainGrope = MainGrope;
            this.studGroups = studGroups;
        }

        /// <summary>
        /// Метод для добавления новой группы студентов или редактирования существующей.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Groupe(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на наличие имени группы
                if (string.IsNullOrEmpty(tb_name.Text))
                {
                    MessageBox.Show("Введите наименование группы");
                    return; // Если наименование не введено, останавливаем выполнение
                }

                // Если группа не передана (например, при добавлении новой группы), создаем новый объект группы
                if (studGroups == null)
                {
                    studGroups = new Models.StudGroups
                    {
                        name = tb_name.Text // Название группы
                    };

                    // Добавляем новую группу в контекст базы данных
                    MainGrope._studgroupsContext.StudGroups.Add(studGroups);
                }

                // Сохраняем изменения в базе данных
                MainGrope._studgroupsContext.SaveChanges();

                // Переходим на страницу с группами
                MainWindow.init.OpenPages(MainWindow.pages.group);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error adding Groupe", ex.Message, "Failed to save Groupe.");

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