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
using YP02.Context;
using YP02.Log;
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для TeacherLoadEdit.xaml
    /// </summary>
    public partial class TeacherLoadEdit : Page
    {
        // Переменная для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты для работы с нагрузкой преподавателей
        public Pages.listPages.TeachersLoad MainTeachersLoad;
        public Models.TeachersLoad teachersLoad;

        // Контексты для работы с преподавателями, дисциплинами и группами студентов
        Context.TeachersContext teachersContext = new TeachersContext();
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        /// <summary>
        /// Конструктор, инициализирующий компоненты и заполняющий поля формы для редактирования нагрузки преподавателя
        /// </summary>
        public TeacherLoadEdit(Pages.listPages.TeachersLoad MainTeachersLoad, Models.TeachersLoad teachersLoad = null)
        {
            InitializeComponent();
            this.MainTeachersLoad = MainTeachersLoad;
            this.teachersLoad = teachersLoad;

            // Заполнение ComboBox с преподавателями
            foreach (Teachers teachers in teachersContext.Teachers)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = teachers.surname;
                item.Tag = teachers.id;
                if (teachers.id == teachersLoad.teacherId)
                {
                    item.IsSelected = true;
                }
                cb_teacherId.Items.Add(item);
            }

            // Заполнение ComboBox с дисциплинами
            foreach (Disciplines discipline in disciplinesContext.Disciplines)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = discipline.name;
                item.Tag = discipline.id;
                if (discipline.id == teachersLoad.disciplineId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineId.Items.Add(item);
            }

            // Заполнение ComboBox с группами студентов
            foreach (Models.StudGroups studGroups in studGroupsContext.StudGroups)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = studGroups.name;
                item.Tag = studGroups.id;
                if (studGroups.id == teachersLoad.studGroupId)
                {
                    item.IsSelected = true;
                }
                cb_groupe.Items.Add(item);
            }

            // Заполнение полей формы с данными о часах
            tb_lectureHours.Text = teachersLoad.lectureHours.ToString();
            tb_practiceHours.Text = teachersLoad.practiceHours.ToString();
            tb_сonsultationHours.Text = teachersLoad.сonsultationHours.ToString();
            tb_courseprojectHours.Text = teachersLoad.courseprojectHours.ToString();
            tb_examHours.Text = teachersLoad.examHours.ToString();
        }

        /// <summary>
        /// Обработчик для редактирования нагрузки преподавателя
        /// </summary>
        private void Edit_TeacherLoad(object sender, RoutedEventArgs e)
        {
            try
            {
                // Поиск нагрузки преподавателя по id
                Models.TeachersLoad editTeachersLoad = MainTeachersLoad._teachersLoadContext.TeachersLoad.FirstOrDefault(x => x.id == teachersLoad.id);
                if (editTeachersLoad != null)
                {
                    // Обновление данных нагрузки преподавателя
                    editTeachersLoad.teacherId = (int)(cb_teacherId.SelectedItem as ComboBoxItem).Tag;
                    editTeachersLoad.disciplineId = (int)(cb_disciplineId.SelectedItem as ComboBoxItem).Tag;
                    editTeachersLoad.studGroupId = (int)(cb_groupe.SelectedItem as ComboBoxItem).Tag;
                    editTeachersLoad.lectureHours = Convert.ToInt32(tb_lectureHours.Text);
                    editTeachersLoad.practiceHours = Convert.ToInt32(tb_practiceHours.Text);
                    editTeachersLoad.сonsultationHours = Convert.ToInt32(tb_сonsultationHours.Text);
                    editTeachersLoad.courseprojectHours = Convert.ToInt32(tb_courseprojectHours.Text);
                    editTeachersLoad.examHours = Convert.ToInt32(tb_examHours.Text);

                    // Сохранение изменений в базе данных
                    MainTeachersLoad._teachersLoadContext.SaveChanges();

                    // Переход на страницу списка нагрузки преподавателей
                    MainWindow.init.OpenPages(MainWindow.pages.teachersLoad);
                }
                else
                {
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating TeacherLoad", ex.Message, "Failed to save TeacherLoad.");

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