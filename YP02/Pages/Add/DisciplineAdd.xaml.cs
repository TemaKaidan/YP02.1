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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для DisciplineAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления новой дисциплины.
    /// </summary>
    public partial class DisciplineAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с дисциплинами.
        /// </summary>
        public Pages.listPages.Discipline MainDiscipline;

        /// <summary>
        /// Модель дисциплины, передаваемая для редактирования или добавления новой.
        /// </summary>
        public Models.Disciplines disciplines;

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования дисциплины.
        /// </summary>
        /// <param name="MainDiscipline">Главная страница для работы с дисциплинами.</param>
        /// <param name="disciplines">Модель дисциплины (если редактируется существующая, иначе null).</param>
        public DisciplineAdd(Pages.listPages.Discipline MainDiscipline, Models.Disciplines disciplines = null)
        {
            InitializeComponent();
            this.MainDiscipline = MainDiscipline;
            this.disciplines = disciplines;
        }

        /// <summary>
        /// Метод для добавления новой дисциплины или редактирования существующей.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Discipline(object sender, RoutedEventArgs e)
        {
            // Проверка на наличие наименования дисциплины
            if (string.IsNullOrEmpty(tb_nameDiscipline.Text))
            {
                MessageBox.Show("Введите наименование дисциплины");
                return;
            }

            // Если дисциплина не передана (например, при добавлении новой), создаем новый объект дисциплины
            if (disciplines == null)
            {
                disciplines = new Models.Disciplines
                {
                    name = tb_nameDiscipline.Text, // Наименование дисциплины
                    teacherId = 1 // Установим временное значение для teacherId (например, по умолчанию 1)
                };

                // Добавляем новую дисциплину в контекст базы данных
                MainDiscipline._disciplinesContext.Disciplines.Add(disciplines);
            }

            // Сохраняем изменения в базе данных
            MainDiscipline._disciplinesContext.SaveChanges();

            // Переходим на страницу с дисциплинами
            MainWindow.init.OpenPages(MainWindow.pages.discipline);
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