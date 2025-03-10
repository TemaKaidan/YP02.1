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

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для RoleAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления новой роли.
    /// </summary>
    public partial class RoleAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница ролей.
        /// </summary>
        public Pages.listPages.Role MainRole;

        /// <summary>
        /// Модель роли, которая может быть передана для редактирования.
        /// </summary>
        public Models.Roles roles;

        /// <summary>
        /// Конструктор для инициализации страницы добавления роли.
        /// </summary>
        /// <param name="MainRole">Главная страница ролей.</param>
        /// <param name="roles">Модель роли (если редактируется существующая роль, иначе null).</param>
        public RoleAdd(Pages.listPages.Role MainRole, Models.Roles roles = null)
        {
            InitializeComponent();
            this.MainRole = MainRole;
            this.roles = roles;
        }

        /// <summary>
        /// Метод для добавления новой роли.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_Groupe(object sender, RoutedEventArgs e)
        {
            // Если роль не передана (т.е. добавляем новую роль)
            if (roles == null)
            {
                // Создаем новую роль
                roles = new Models.Roles
                {
                    roleName = tb_roleName.Text // Название роли
                };

                // Добавляем новую роль в базу данных
                MainRole._rolesContext.Roles.Add(roles);
            }

            // Сохраняем изменения в базе данных
            MainRole._rolesContext.SaveChanges();

            // Переходим на страницу с ролями
            MainWindow.init.OpenPages(MainWindow.pages.role);
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