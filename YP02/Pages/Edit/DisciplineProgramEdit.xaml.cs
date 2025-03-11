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
using Microsoft.EntityFrameworkCore;
using YP02.Context;
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для DisciplineProgramEdit.xaml
    /// </summary>
    public partial class DisciplineProgramEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с основной программой дисциплины и выбранной программой
        public Pages.listPages.DisciplineProgram MainDisciplineProgram;
        public Models.DisciplinePrograms programs;

        // Контексты для работы с данными о дисциплинах и типах уроков
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.LessonTypesContext lessonTypesContext = new LessonTypesContext();

        // Конструктор, инициализирующий компоненты и заполняющий поля формы
        public DisciplineProgramEdit(Pages.listPages.DisciplineProgram MainDisciplineProgram, Models.DisciplinePrograms programs = null)
        {
            InitializeComponent();
            this.programs = programs;
            this.MainDisciplineProgram = MainDisciplineProgram;

            // Заполнение ComboBox дисциплинами из контекста
            foreach (Disciplines discipline in disciplinesContext.Disciplines)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = discipline.name;
                item.Tag = discipline.id;

                // Устанавливаем выбранную дисциплину, если она соответствует программе
                if (discipline.id == programs.disciplineId)
                {
                    item.IsSelected = true;
                }

                cb_disciplineId.Items.Add(item);
            }

            // Заполнение текстового поля темой дисциплины
            tb_theme.Text = programs.theme;

            // Заполнение ComboBox типами уроков
            foreach (LessonTypes lessonType in lessonTypesContext.LessonTypes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = lessonType.typeName;
                item.Tag = lessonType.id;

                // Устанавливаем выбранный тип урока, если он соответствует программе
                if (lessonType.id == programs.lessonTypeId)
                {
                    item.IsSelected = true;
                }

                cb_lessonTypeId.Items.Add(item);
            }

            // Заполнение текстового поля количеством часов
            tb_hoursCount.Text = programs.hoursCount.ToString();
        }

        // Обработчик для редактирования программы дисциплины
        private void Edit_DisciplineProgram(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка наличия введённых данных
                if (string.IsNullOrEmpty(cb_disciplineId.Text))
                {
                    MessageBox.Show("Введите дисциплину");
                    return;
                }
                if (string.IsNullOrEmpty(tb_theme.Text))
                {
                    MessageBox.Show("Введите тему");
                    return;
                }
                if (string.IsNullOrEmpty(cb_lessonTypeId.Text))
                {
                    MessageBox.Show("Введите тип");
                    return;
                }
                if (string.IsNullOrEmpty(tb_hoursCount.Text))
                {
                    MessageBox.Show("Введите количество часов");
                    return;
                }

                // Получаем объект программы дисциплины из базы данных и обновляем его
                DisciplinePrograms editPrograms = MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.FirstOrDefault(x => x.id == programs.id);

                if (editPrograms != null)
                {
                    // Обновляем свойства программы дисциплины
                    editPrograms.theme = tb_theme.Text;
                    editPrograms.hoursCount = Convert.ToInt32(tb_hoursCount.Text);
                    editPrograms.disciplineId = (int)(cb_disciplineId.SelectedItem as ComboBoxItem).Tag;
                    editPrograms.lessonTypeId = (int)(cb_lessonTypeId.SelectedItem as ComboBoxItem).Tag;

                    // Сохраняем изменения в базе данных
                    MainDisciplineProgram._disciplinePrograms.SaveChanges();

                    // Открываем страницу с перечнем программ дисциплин
                    MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении программы дисциплины
                ErrorLogger.LogError("Error updating DisciplineProgram", ex.Message, "Failed to save DisciplineProgram.");

                // Показываем сообщение об ошибке пользователю
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик кнопки "Отмена", возвращает пользователя на предыдущую страницу
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
    }
}
