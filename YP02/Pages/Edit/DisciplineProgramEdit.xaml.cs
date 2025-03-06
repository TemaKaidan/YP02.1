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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для DisciplineProgramEdit.xaml
    /// </summary>
    public partial class DisciplineProgramEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.DisciplineProgram MainDisciplineProgram;
        public Models.DisciplinePrograms programs;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();
        Context.LessonTypesContext lessonTypesContext = new LessonTypesContext();
        public DisciplineProgramEdit(Pages.listPages.DisciplineProgram MainDisciplineProgram, Models.DisciplinePrograms programs = null)
        {
            InitializeComponent();
            this.programs = programs;
            this.MainDisciplineProgram = MainDisciplineProgram;

            foreach (Disciplines discipline in disciplinesContext.Disciplines)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = discipline.name;
                item.Tag = discipline.id;
                if (discipline.id == programs.disciplineId)
                {
                    item.IsSelected = true;
                }
                cb_disciplineId.Items.Add(item);
                
            }

            tb_theme.Text = programs.theme;

            foreach (LessonTypes lessonType in lessonTypesContext.LessonTypes)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = lessonType.typeName;
                item.Tag = lessonType.id;
                if (lessonType.id == programs.lessonTypeId)
                {
                    item.IsSelected = true;
                }
                cb_lessonTypeId.Items.Add(item);

            }

            tb_hoursCount.Text = programs.hoursCount.ToString();
        }

        private void Edit_DisciplineProgram(object sender, RoutedEventArgs e)
        {
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

            DisciplinePrograms editPrograms = MainDisciplineProgram._disciplinePrograms.DisciplinePrograms.FirstOrDefault(x => x.id == programs.id);

            if (editPrograms != null)
            {
                editPrograms.theme = tb_theme.Text;
                editPrograms.hoursCount = Convert.ToInt32(tb_hoursCount.Text);
                editPrograms.disciplineId = (int)(cb_disciplineId.SelectedItem as ComboBoxItem).Tag;
                editPrograms.lessonTypeId = (int)(cb_lessonTypeId.SelectedItem as ComboBoxItem).Tag;
                MainDisciplineProgram._disciplinePrograms.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
            }
            else
            {
                MessageBox.Show("Произошла ошибка!");
                MainWindow.init.OpenPages(MainWindow.pages.disciplineProgram);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
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
    }
}
