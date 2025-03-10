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
    /// Логика взаимодействия для TeacherLoadAdd.xaml
    /// </summary>
    /// <summary>
    /// Страница для добавления или редактирования нагрузки преподавателя.
    /// </summary>
    public partial class TeacherLoadAdd : Page
    {
        /// <summary>
        /// Флаг, указывающий, свернуто ли меню.
        /// </summary>
        private bool isMenuCollapsed = false;

        /// <summary>
        /// Ссылка на главную страницу с нагрузками преподавателей.
        /// </summary>
        public Pages.listPages.TeachersLoad MainTeachersLoad;

        /// <summary>
        /// Модель нагрузки преподавателя, которая может быть передана для редактирования.
        /// </summary>
        public Models.TeachersLoad teachersLoad;

        /// <summary>
        /// Контекст базы данных для преподавателей.
        /// </summary>
        Context.TeachersContext teachersContext = new TeachersContext();

        /// <summary>
        /// Контекст базы данных для дисциплин.
        /// </summary>
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        /// <summary>
        /// Контекст базы данных для групп студентов.
        /// </summary>
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        /// <summary>
        /// Конструктор, инициализирует страницу для добавления или редактирования нагрузки преподавателя.
        /// </summary>
        /// <param name="MainTeachersLoad">Главная страница нагрузки преподавателей.</param>
        /// <param name="teachersLoad">Модель нагрузки преподавателя (может быть null для добавления новой нагрузки).</param>
        public TeacherLoadAdd(Pages.listPages.TeachersLoad MainTeachersLoad, Models.TeachersLoad teachersLoad = null)
        {
            InitializeComponent();
            this.MainTeachersLoad = MainTeachersLoad;
            this.teachersLoad = teachersLoad;

            // Заполнение комбинированного списка преподавателей
            cb_teacherId.Items.Clear();
            cb_teacherId.ItemsSource = teachersContext.Teachers.ToList();
            cb_teacherId.DisplayMemberPath = "surname"; // Отображать фамилию преподавателя
            cb_teacherId.SelectedValuePath = "id"; // Значение будет ID преподавателя

            // Заполнение комбинированного списка дисциплин
            cb_disciplineId.Items.Clear();
            cb_disciplineId.ItemsSource = disciplinesContext.Disciplines.ToList();
            cb_disciplineId.DisplayMemberPath = "name"; // Отображать название дисциплины
            cb_disciplineId.SelectedValuePath = "id"; // Значение будет ID дисциплины

            // Заполнение комбинированного списка групп студентов
            cb_groupe.Items.Clear();
            cb_groupe.ItemsSource = studGroupsContext.StudGroups.ToList();
            cb_groupe.DisplayMemberPath = "name"; // Отображать название группы
            cb_groupe.SelectedValuePath = "id"; // Значение будет ID группы
        }

        /// <summary>
        /// Метод для добавления или редактирования нагрузки преподавателя.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void Add_TeacherLoad(object sender, RoutedEventArgs e)
        {
            // Если нагрузка преподавателя не передана (добавляем новую запись)
            if (teachersLoad == null)
            {
                // Создаем новый объект нагрузки преподавателя
                teachersLoad = new Models.TeachersLoad
                {
                    teacherId = (cb_teacherId.SelectedItem as Models.Teachers).id, // Получаем ID преподавателя
                    disciplineId = (cb_disciplineId.SelectedItem as Disciplines).id, // Получаем ID дисциплины
                    studGroupId = (cb_groupe.SelectedItem as StudGroups).id, // Получаем ID группы студентов
                    lectureHours = Convert.ToInt32(tb_lectureHours.Text), // Часы лекций
                    practiceHours = Convert.ToInt32(tb_practiceHours.Text), // Часы практики
                    сonsultationHours = Convert.ToInt32(tb_сonsultationHours.Text), // Часы консультаций
                    courseprojectHours = Convert.ToInt32(tb_courseprojectHours.Text), // Часы курсовой работы
                    examHours = Convert.ToInt32(tb_examHours.Text) // Часы экзамена
                };

                // Добавляем новую нагрузку в базу данных
                MainTeachersLoad._teachersLoadContext.TeachersLoad.Add(teachersLoad);
            }

            // Сохраняем изменения в базе данных
            MainTeachersLoad._teachersLoadContext.SaveChanges();

            // Переходим на страницу нагрузки преподавателей
            MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
        }

        /// <summary>
        /// Метод для переключения состояния меню (свернуто/развернуто).
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
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
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Visible; // Отображение кнопок
                    }
                }
            }
            else
            {
                widthAnimation.From = 200;
                widthAnimation.To = 50;
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed; // Скрытие кнопок
                    }
                }
            }

            // Устанавливаем продолжительность анимации
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));

            // Запуск анимации
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);

            // Переключение флага состояния меню
            isMenuCollapsed = !isMenuCollapsed;
        }

        /// <summary>
        /// Обработчик для кнопки "Отменить", возвращает назад на предыдущую страницу.
        /// </summary>
        /// <param name="sender">Объект, вызвавший событие</param>
        /// <param name="e">Аргументы события</param>
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}