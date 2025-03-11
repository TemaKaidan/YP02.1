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
    /// Логика взаимодействия для LessonTypeEdit.xaml
    /// </summary>
    public partial class LessonTypeEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с типами занятий
        public Pages.listPages.LessonType MainLessonType;
        public Models.LessonTypes lessonTypes;

        // Конструктор, инициализирующий компоненты и заполняющий поля формы
        public LessonTypeEdit(Pages.listPages.LessonType MainLessonType, Models.LessonTypes lessonTypes = null)
        {
            InitializeComponent();
            this.MainLessonType = MainLessonType;
            this.lessonTypes = lessonTypes;

            // Заполнение поля с наименованием типа занятия
            tb_typeName.Text = lessonTypes.typeName;
        }

        // Обработчик для редактирования типа занятия
        private void Edit_LessonType(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка на пустое наименование типа занятия
                if (string.IsNullOrEmpty(tb_typeName.Text))
                {
                    MessageBox.Show("Введите наименование тип занятия");
                    return;
                }

                // Получаем тип занятия из контекста и обновляем его данные
                Models.LessonTypes mlt = MainLessonType._lessonTypesContext.LessonTypes.FirstOrDefault(x => x.id == lessonTypes.id);
                mlt.typeName = tb_typeName.Text;

                // Сохраняем изменения в базе данных
                MainLessonType._lessonTypesContext.SaveChanges();

                // Переходим на страницу списка типов занятий
                MainWindow.init.OpenPages(MainWindow.pages.lessonType);
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении типа занятия
                ErrorLogger.LogError("Error updating LessonType", ex.Message, "Failed to save LessonType.");

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