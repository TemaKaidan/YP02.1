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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using YP02.Context;
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для MarkItem.xaml
    /// </summary>
    public partial class MarkItem : UserControl
    {
        // Основные объекты для работы с оценками
        Pages.listPages.Mark MainMark;
        Models.Marks marks;

        /// <summary>
        /// Конструктор для инициализации компонента MarkItem с заданными данными
        /// </summary>
        public MarkItem(Models.Marks marks, Mark MainMark)
        {
            InitializeComponent(); // Инициализация компонентов интерфейса
            this.marks = marks; // Присваиваем данные оценки
            this.MainMark = MainMark; // Присваиваем основной объект для работы с оценками

            // Настройка видимости кнопок в зависимости от роли пользователя
            EditButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;
            DeleteButton.Visibility = (MainWindow.UserRole == "Администратор" || MainWindow.UserRole == "Преподаватель") ? Visibility.Visible : Visibility.Collapsed;

            // Устанавливаем цвет оценки в зависимости от ее значения
            int markValue;
            if (int.TryParse(marks.mark, out markValue))
            {
                SetMarkColor(markValue);
            }
            else
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Black); // Если не число, ставим черный цвет
            }

            // Отображаем оценку
            lb_mark.Content = "Оценка: " + marks.mark;

            // Получаем информацию о занятии, к которому относится оценка
            DisciplineProgramsContext _disciplinesContext = new DisciplineProgramsContext();
            var disciplines = _disciplinesContext.DisciplinePrograms.FirstOrDefault(g => g.id == marks.disciplineProgramId);
            lb_disciplineProgramId.Content = "Занятие: " + (disciplines != null ? disciplines.theme : "Неизвестно");

            // Получаем информацию о студенте, которому поставлена оценка
            StudentsContext _studentsContext = new StudentsContext();
            var studentsContext = _studentsContext.Students.FirstOrDefault(g => g.id == marks.studentId);
            lb_studentId.Content = "Студент: " + (studentsContext != null ? studentsContext.surname : "Неизвестно") + " " + (studentsContext != null ? studentsContext.name : "Неизвестно") + " " + (studentsContext != null ? studentsContext.lastname : "Неизвестно");

            // Отображаем описание оценки
            lb_description.Content = "Описание: " + marks.description;
        }

        /// <summary>
        /// Устанавливает цвет для оценки в зависимости от ее значения
        /// </summary>
        private void SetMarkColor(int mark)
        {
            if (mark == 5)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Green); // Зеленый для пятерки
            }
            else if (mark == 4)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.DarkGoldenrod); // Темно-золотой для четверки
            }
            else if (mark == 3)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Orange); // Оранжевый для тройки
            }
            else if (mark == 2)
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Red); // Красный для двойки
            }
            else
            {
                lb_mark.Foreground = new SolidColorBrush(Colors.Black); // Черный цвет для других значений
            }
        }

        // Обработчик для кнопки редактирования оценки
        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.markEdit, null, null, null, null, null, null, null, null, null, null, null,marks); // Открытие страницы редактирования оценки
        }

        // Обработчик для кнопки удаления оценки
        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            try
            {
                // Подтверждение удаления оценки
                MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Удаление оценки из базы данных
                    MainMark._marksContext.Marks.Remove(marks);
                    MainMark._marksContext.SaveChanges();
                    // Удаление элемента из пользовательского интерфейса
                    (this.Parent as Panel).Children.Remove(this);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error deleting Marks", ex.Message, "Failed to save Marks.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
