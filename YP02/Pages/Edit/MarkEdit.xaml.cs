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
    /// Логика взаимодействия для MarkEdit.xaml
    /// </summary>
    public partial class MarkEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Mark MainMark;
        public Models.Marks marks;

        Context.StudentsContext studentsContext = new Context.StudentsContext();
        Context.DisciplineProgramsContext disciplineProgramsContext = new Context.DisciplineProgramsContext();

        public MarkEdit(Pages.listPages.Mark MainMark, Models.Marks marks = null)
        {
            InitializeComponent();
            this.MainMark = MainMark;
            this.marks = marks;

            foreach (Models.Students students in studentsContext.Students)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = students.surname + " " + students.name + " " + students.lastname;
                item.Tag = students.id;
                if (students.id == marks.studentId)
                {
                    item.IsSelected = true;
                }
                cb_studentId.Items.Add(item);
            }
            foreach (DisciplinePrograms disciplinePrograms in disciplineProgramsContext.DisciplinePrograms)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = disciplinePrograms.theme;
                item.Tag = disciplinePrograms.id;
                if (disciplinePrograms.id == marks.disciplineProgramId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineProgramId.Items.Add(item);
            }
            tb_mark.Text = marks.mark;
            tb_discription.Text = marks.description;
        }

        private void Edit_Marks(object sender, RoutedEventArgs e)
        {
            try
            {
                Marks editMarks = MainMark._marksContext.Marks.FirstOrDefault(x => x.id == marks.id);
                if (editMarks != null)
                {
                    editMarks.studentId = (int)(cb_studentId.SelectedItem as ComboBoxItem).Tag;
                    editMarks.disciplineProgramId = (int)(cb_disciplineProgramId.SelectedItem as ComboBoxItem).Tag;
                    editMarks.mark = tb_mark.Text;
                    editMarks.description = tb_discription.Text;

                    MainMark._marksContext.SaveChanges();
                    MainWindow.init.OpenPages(MainWindow.pages.marks);
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
                ErrorLogger.LogError("Error updating Marks", ex.Message, "Failed to save Marks.");

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
