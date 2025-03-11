using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using YP02.Log;
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для DisciplineEdit.xaml
    /// </summary>
    public partial class DisciplineEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с основной дисциплиной и выбранной дисциплиной
        public Pages.listPages.Discipline MainDiscipline;
        public Models.Disciplines disciplines;

        // Конструктор, инициализирующий компоненты и заполняющий поля формы
        public DisciplineEdit(Pages.listPages.Discipline MainDiscipline, Models.Disciplines disciplines = null)
        {
            InitializeComponent();
            this.MainDiscipline = MainDiscipline;
            this.disciplines = disciplines;

            // Устанавливаем значение наименования дисциплины в соответствующее текстовое поле
            tb_nameDiscipline.Text = disciplines.name;
        }

        // Обработчик для редактирования дисциплины
        private void Edit_Discipline(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка, что введено название дисциплины
                if (string.IsNullOrEmpty(tb_nameDiscipline.Text))
                {
                    MessageBox.Show("Введите наименование дисциплины");
                    return;
                }

                // Получаем объект дисциплины из базы данных и обновляем его
                Models.Disciplines md = MainDiscipline._disciplinesContext.Disciplines.FirstOrDefault(x => x.id == disciplines.id);
                md.name = tb_nameDiscipline.Text;

                // Сохраняем изменения в базе данных
                MainDiscipline._disciplinesContext.SaveChanges();

                // Открываем страницу с перечнем дисциплин
                MainWindow.init.OpenPages(MainWindow.pages.discipline);
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении дисциплины
                ErrorLogger.LogError("Error updating Discipline", ex.Message, "Failed to save Discipline.");

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
