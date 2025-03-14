﻿using System;
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
    /// Логика взаимодействия для TeacherEdit.xaml
    /// </summary>
    public partial class TeacherEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с данными преподавателей
        public Pages.listPages.Teacher MainTeacher;
        public Models.Teachers teachers;

        /// <summary>
        /// Конструктор, инициализирующий компоненты и заполняющий поля формы для редактирования преподавателя
        /// </summary>
        public TeacherEdit(Pages.listPages.Teacher MainTeacher, Models.Teachers teachers = null)
        {
            InitializeComponent();
            this.MainTeacher = MainTeacher;
            this.teachers = teachers;

            // Заполнение полей с данными преподавателя
            tb_surName.Text = teachers.surname;
            tb_name.Text = teachers.name;
            tb_lastName.Text = teachers.lastname;
            tb_login.Text = teachers.login;
            tb_password.Text = teachers.password;
        }

        /// <summary>
        /// Обработчик для редактирования данных преподавателя
        /// </summary>
        private void Edit_Teacher(object sender, RoutedEventArgs e)
        {
            try
            {
                // Проверка наличия данных в полях
                if (string.IsNullOrEmpty(tb_surName.Text))
                {
                    MessageBox.Show("Введите фамилию");
                    return;
                }
                if (string.IsNullOrEmpty(tb_name.Text))
                {
                    MessageBox.Show("Введите имя");
                    return;
                }
                if (string.IsNullOrEmpty(tb_lastName.Text))
                {
                    MessageBox.Show("Введите отчество");
                    return;
                }
                if (string.IsNullOrEmpty(tb_login.Text))
                {
                    MessageBox.Show("Введите логин");
                    return;
                }
                if (string.IsNullOrEmpty(tb_password.Text))
                {
                    MessageBox.Show("Введите пароль");
                    return;
                }

                // Обновление данных преподавателя в контексте
                Models.Teachers mt = MainTeacher._teachersContext.Teachers.FirstOrDefault(x => x.id == teachers.id);
                mt.surname = tb_surName.Text;
                mt.name = tb_name.Text;
                mt.lastname = tb_lastName.Text;
                mt.login = tb_login.Text;
                mt.password = tb_password.Text;

                // Сохранение изменений в базе данных
                MainTeacher._teachersContext.SaveChanges();

                // Переход на страницу списка преподавателей
                MainWindow.init.OpenPages(MainWindow.pages.teacher);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating Teacher", ex.Message, "Failed to save Teacher.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Обработчик для сворачивания и разворачивания меню
        /// </summary>
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            // Проверка текущего состояния меню (свёрнуто или развернуто)
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
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
