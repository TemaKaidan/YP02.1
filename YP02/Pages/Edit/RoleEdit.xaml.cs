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
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для RoleEdit.xaml
    /// </summary>
    public partial class RoleEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с ролями
        public Pages.listPages.Role MainRole;
        public Models.Roles roles;

        /// <summary>
        /// Конструктор, инициализирующий компоненты и заполняющий поля формы
        /// </summary>
        /// <param name="MainRole">Основная страница для работы с ролями</param>
        /// <param name="roles">Объект роли для редактирования</param>
        public RoleEdit(Pages.listPages.Role MainRole, Models.Roles roles = null)
        {
            InitializeComponent();
            this.MainRole = MainRole;
            this.roles = roles;

            // Заполнение поля с названием роли
            tb_roleName.Text = roles.roleName;
        }

        /// <summary>
        /// Обработчик для редактирования роли
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Edit_Role(object sender, RoutedEventArgs e)
        {
            try
            {
                // Получаем роль из контекста и обновляем её
                Models.Roles mr = MainRole._rolesContext.Roles.FirstOrDefault(x => x.id == roles.id);
                mr.roleName = tb_roleName.Text;

                // Сохраняем изменения в базе данных
                MainRole._rolesContext.SaveChanges();

                // Переходим на страницу списка ролей
                MainWindow.init.OpenPages(MainWindow.pages.role);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating Role", ex.Message, "Failed to save Role.");

                // Показываем сообщение об ошибке пользователю
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик для сворачивания и разворачивания меню
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
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

        /// <summary>
        /// Обработчик кнопки "Отмена", возвращает пользователя на предыдущую страницу
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}