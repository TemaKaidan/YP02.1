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
using YP02.Log;
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для ConsultationEdit.xaml
    /// </summary>
    public partial class ConsultationEdit : Page
    {
        // Поле для отслеживания состояния меню (свёрнуто/развернуто)
        private bool isMenuCollapsed = false;

        // Объекты, представляющие основную консультацию и список всех консультаций
        public Pages.listPages.Consultation MainConsultation;
        public Models.Consultations consultations;

        // Контекст для работы с данными дисциплин
        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        // Конструктор, инициализирующий компоненты и заполняющий выпадающий список дисциплин
        public ConsultationEdit(Pages.listPages.Consultation MainConsultation, Models.Consultations consultations = null)
        {
            InitializeComponent();
            this.MainConsultation = MainConsultation;
            this.consultations = consultations;

            // Заполнение комбинированного списка дисциплин
            foreach (Models.Disciplines disciplines in disciplinesContext.Disciplines)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = disciplines.name;
                item.Tag = disciplines.id;
                if (disciplines.id == consultations.disciplineId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineId.Items.Add(item);
            }

            // Установка даты консультации в текстовое поле
            db_date.Text = consultations.date.ToString();
            // Установка информации о предоставленных работах в текстовое поле
            tb_submittedWorks.Text = consultations.submittedWorks;
        }

        // Обработчик редактирования консультации
        private void Edit_Consultation(object sender, RoutedEventArgs e)
        {
            try
            {
                // Поиск консультации по ID и её редактирование
                Models.Consultations editConsultations = MainConsultation._consultationsContext.Consultations.
                    FirstOrDefault(x => x.id == consultations.id);
                if (editConsultations != null)
                {
                    // Обновление информации о консультации
                    editConsultations.disciplineId = (int)(cb_disciplineId.SelectedItem as ComboBoxItem).Tag;
                    editConsultations.date = DateTime.Parse(db_date.Text);
                    editConsultations.submittedWorks = tb_submittedWorks.Text;

                    // Сохранение изменений в базе данных
                    MainConsultation._consultationsContext.SaveChanges();
                    MainWindow.init.OpenPages(MainWindow.pages.consultation);
                }
                else
                {
                    // Сообщение об ошибке, если консультация не найдена
                    MessageBox.Show("Произошла ошибка!");
                    MainWindow.init.OpenPages(MainWindow.pages.consultation);
                }
            }
            catch (Exception ex)
            {
                // Логирование ошибки при сохранении данных
                ErrorLogger.LogError("Error updating Consultation", ex.Message, "Failed to save Consultation.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // Обработчик для сворачивания и разворачивания меню
        private void ToggleMenu(object sender, RoutedEventArgs e)
        {
            DoubleAnimation widthAnimation = new DoubleAnimation();

            if (isMenuCollapsed)
            {
                widthAnimation.From = 50;
                widthAnimation.To = 200;
                MenuPanel.Width = 200;
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
                foreach (UIElement element in MenuPanel.Children)
                {
                    if (element is Button btn && btn.Content.ToString() != "☰")
                    {
                        btn.Visibility = Visibility.Collapsed;
                    }
                }
            }

            // Анимация ширины меню
            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        // Обработчик кнопки "Отмена", возвращающий на предыдущую страницу
        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}