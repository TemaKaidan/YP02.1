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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Add
{
    /// <summary>
    /// Логика взаимодействия для DisciplineProgramAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования программы дисциплины.
    /// </summary>
    public partial class DisciplineProgramAdd : Page
    {
        /// <summary>
        /// Флаг для определения, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Главная страница для работы с программами дисциплин.
        /// </summary>
        public Pages.listPages.DisciplineProgram MainDisciplineProgram;

        /// <summary>
        /// Модель программы дисциплины, передаваемая для редактирования или добавления новой.
        /// </summary>
        public Models.DisciplinePrograms programs;

        /// <summary>
        /// Контекст базы данных для дисциплин.
        /// </summary>
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Контекст базы данных для типов занятий.
        /// </summary>
        Context.LessonTypesContext lessonTypesContext = new LessonTypesContext();

        /// <summary>
        /// Конструктор для инициализации страницы добавления или редактирования программы дисциплины.
        /// </summary>
        /// <param name="MainDisciplineProgram">Главная страница для работы с программами дисциплин.</param>
        /// <param name="programs">Модель программы дисциплины (если редактируется существующая программа, иначе null).</param>
        public DisciplineProgramAdd(Pages.listPages.DisciplineProgram MainDisciplineProgram, Models.DisciplinePrograms programs = null)
        {
            InitializeComponent();
            this.MainDisciplineProgram = MainDisciplineProgram;
            this.programs = programs;

            // Инициализация списка дисциплин для комбобокса
            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name";
            cb_disciplineId.SelectedValuePath = "id";

            // Инициализация списка типов занятий для комбобокса
            cb_lessonTypeId.Items.Clear();
            cb_lessonTypeId.ItemsSource = lessonTypesContext.LessonTypes.ToList();
            cb_lessonTypeId.DisplayMemberPath = "typeName";
            cb_lessonTypeId.SelectedValuePath = "id";
        }

        /// <summary>
        /// Метод для добавления новой программы дисциплины или редактирования существующей.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие.</param>
        /// <param name="e">Аргументы события.</param>
        private void Add_DisciplineProgram(object sender, RoutedEventArgs e)
        {
            // Проверка на наличие дисциплины
            if (string.IsNullOrEmpty(cb_disciplineId.Text))
            {
                MessageBox.Show("Введите дисциплину");
                return;
            }

            // Проверка на наличие темы
            if (string.IsNullOrEmpty(tb_theme.Text))
            {
                MessageBox.Show("Введите тему");
                return;
            }

            // Проверка на наличие типа занятия
            if (string.IsNullOrEmpty(cb_lessonTypeId.Text))
            {
                MessageBox.Show("Введите тип");
                return;
            }

            // Проверка на наличие количества часов
            if (string.IsNullOrEmpty(tb_hoursCount.Text))
            {
                MessageBox.Show("Введите количество часов");
                return;
            }

            // Если программа дисциплины не передана (например, при добавлении новой), создаем новый объект программы
            if (programs == null)
            {
                programs = new Models.DisciplinePrograms
                {
                    disciplineId = (cb_disciplineId.SelectedItem as Disciplines).id, // ID дисциплины
                    theme = tb_theme.Text, // Тема
                    lessonTypeId = (cb_lessonTypeId.SelectedItem as LessonTypes).id, // Тип занятия
                    hoursCount = Convert.ToInt32(tb_hoursCount.Text) // Количество часов
                };

                // Добавляем новую программу в контекст базы данных
                MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.Add(programs);
            }

            // Сохраняем изменения в базе данных
            MainDisciplineProgram._disciplinePrograms.SaveChanges();

            // Переходим на страницу с программами дисциплин
            MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
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
