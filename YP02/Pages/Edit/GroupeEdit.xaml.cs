using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using YP02.Log;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для GroupeEdit.xaml
    /// </summary>
    public partial class GroupeEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с группой студентов
        public Pages.listPages.Group MainGrope;
        public Models.StudGroups studGroups;

        // Конструктор, инициализирующий компоненты и заполняющий поля формы
        public GroupeEdit(Pages.listPages.Group MainGrope, Models.StudGroups studGroups = null)
        {
            InitializeComponent();
            this.MainGrope = MainGrope;
            this.studGroups = studGroups;

            // Заполнение поля с названием группы
            tb_name.Text = studGroups.name;
        }

        // Обработчик для редактирования группы студентов
        private void Edit_Groupe(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустое название группы
                if (string.IsNullOrEmpty(tb_name.Text))
                {
                    MessageBox.Show("Введите наименование группы");
                    return;
                }

                // Получаем группу из контекста и обновляем её данные
                Models.StudGroups sgc = MainGrope._studgroupsContext.StudGroups.FirstOrDefault(x => x.id == studGroups.id);
                sgc.name = tb_name.Text;

                // Сохраняем изменения в базе данных
                MainGrope._studgroupsContext.SaveChanges();

                // Переходим на страницу списка групп
                MainWindow.init.OpenPages(MainWindow.pages.group);
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении группы
                ErrorLogger.LogError("Error updating Groupe", ex.Message, "Failed to save Groupe.");

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