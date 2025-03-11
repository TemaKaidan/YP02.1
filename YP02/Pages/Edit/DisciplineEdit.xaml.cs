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
        private bool isMenuCollapsed = false;

        public Pages.listPages.Discipline MainDiscipline;
        public Models.Disciplines disciplines;

        public DisciplineEdit(Pages.listPages.Discipline MainDiscipline, Models.Disciplines disciplines = null)
        {
            InitializeComponent();
            this.MainDiscipline = MainDiscipline;
            this.disciplines = disciplines;

            tb_nameDiscipline.Text = disciplines.name;
        }

        private void Edit_Discipline(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(tb_nameDiscipline.Text))
                {
                    MessageBox.Show("Введите наименование дисциплины");
                    return;
                }

                Models.Disciplines md = MainDiscipline._disciplinesContext.Disciplines.FirstOrDefault(x => x.id == disciplines.id);
                md.name = tb_nameDiscipline.Text;

                MainDiscipline._disciplinesContext.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.discipline);
            }
            catch (Exception ex)
            {
                // Логирование ошибки
                ErrorLogger.LogError("Error updating Discipline", ex.Message, "Failed to save Discipline.");

                // Показываем сообщение об ошибке
                MessageBox.Show("Произошла ошибка.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

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

            widthAnimation.Duration = new Duration(TimeSpan.FromSeconds(0.3));
            MenuPanel.BeginAnimation(WidthProperty, widthAnimation);
            isMenuCollapsed = !isMenuCollapsed;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
