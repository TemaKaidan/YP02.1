using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для StudentEdit.xaml
    /// </summary>
    public partial class StudentEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с данными студентов
        public Pages.listPages.Student MainStudent;
        public Models.Students students;

        // Контекст для работы с группами студентов
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        /// <summary>
        /// Конструктор, инициализирующий компоненты и заполняющий поля формы для редактирования студента
        /// </summary>
        /// <param name="MainStudent">Основная страница для работы с студентами</param>
        /// <param name="students">Объект студента для редактирования</param>
        public StudentEdit(Pages.listPages.Student MainStudent, Models.Students students = null)
        {
            InitializeComponent();
            this.MainStudent = MainStudent;
            this.students = students;

            // Заполнение полей с данными студента
            tb_surname.Text = students.surname;
            tb_name.Text = students.name;
            tb_lastname.Text = students.lastname;

            // Заполнение ComboBox с данными групп студентов
            foreach (Models.StudGroups studGroups in studGroupsContext.StudGroups)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = studGroups.name;
                item.Tag = studGroups.id;

                // Устанавливаем выбранную группу для студента
                if (studGroups.id == students.studGroupId)
                {
                    item.IsSelected = true;
                }
                cb_groupe.Items.Add(item);
            }

            // Заполнение поля с датой отчисления студента
            db_dateOfRemand.Text = students.dateOfRemand.ToString();
        }

        /// <summary>
        /// Обработчик для редактирования данных студента
        /// </summary>
        /// <param name="sender">Источник события</param>
        /// <param name="e">Аргументы события</param>
        private void Edit_Student(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка ввода фамилии студента
                if (string.IsNullOrEmpty(tb_surname.Text) || !Regex.IsMatch(tb_surname.Text, "[а-яА-Я]"))
                {
                    MessageBox.Show("Введите фамилию студента\n(буквенные значения русского алфавита)");
                    return;
                }

                // Проверка ввода имени студента
                if (string.IsNullOrEmpty(tb_name.Text) || !Regex.IsMatch(tb_name.Text, "[а-яА-Я]"))
                {
                    MessageBox.Show("Введите имя студента\n(буквенные значения русского алфавита)");
                    return;
                }

                // Проверка ввода отчества студента
                if (string.IsNullOrEmpty(tb_lastname.Text) || !Regex.IsMatch(tb_lastname.Text, "[а-яА-Я]"))
                {
                    MessageBox.Show("Введите отчество студента\n(буквенные значения русского алфавита)");
                    return;
                }

                // Проверка выбора группы студента
                if (string.IsNullOrEmpty(cb_groupe.Text))
                {
                    MessageBox.Show("Введите группу студента");
                    return;
                }

                // Проверка ввода даты отчисления
                if (string.IsNullOrEmpty(db_dateOfRemand.Text))
                {
                    MessageBox.Show("Введите дату отчисления\n(dd.mm.yyyy)");
                    return;
                }

                // Получаем студента из контекста и обновляем его данные
                Models.Students editstudents = MainStudent._studentsContext.Students.FirstOrDefault(x => x.id == students.id);
                if (editstudents != null)
                {
                    // Обновляем данные студента
                    editstudents.surname = tb_surname.Text;
                    editstudents.name = tb_name.Text;
                    editstudents.lastname = tb_lastname.Text;
                    editstudents.studGroupId = (int)(cb_groupe.SelectedItem as ComboBoxItem).Tag;
                    editstudents.dateOfRemand = DateTime.Parse(db_dateOfRemand.Text);

                    // Сохраняем изменения в базе данных
                    MainStudent._studentsContext.SaveChanges();

                    // Переходим на страницу списка студентов
                    MainWindow.init.OpenPages(MainWindow.pages.student);
                }
                else
                {
                    // Если не удалось найти студента для редактирования
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.student);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при обновлении данных студента
                ErrorLogger.LogError("Error updating Student", ex.Message, "Failed to save Student.");

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
