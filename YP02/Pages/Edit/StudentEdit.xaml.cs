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
using YP02.Models;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для StudentEdit.xaml
    /// </summary>
    public partial class StudentEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Student MainStudent;
        public Models.Students students;
        Context.StudGroupsContext studGroupsContext = new StudGroupsContext();

        public StudentEdit(Pages.listPages.Student MainStudent, Models.Students students = null)
        {
            InitializeComponent();
            this.MainStudent = MainStudent;
            this.students = students;

            tb_surname.Text = students.surname;
            tb_name.Text = students.name;
            tb_lastname.Text = students.lastname;

            foreach (Models.StudGroups studGroups in studGroupsContext.StudGroups)
            {
                ComboBoxItem item = new ComboBoxItem();
                item.Content = studGroups.name;
                item.Tag = studGroups.id;
                if (studGroups.id == students.studGroupId)
                {
                    item.IsSelected = true;
                }
                cb_groupe.Items.Add(item);
            }

            db_dateOfRemand.Text = students.dateOfRemand.ToString();
        }

        private void Edit_Student(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(tb_surname.Text) || !Regex.IsMatch(tb_surname.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите фамилию студента\n(буквенные значения русского алфавита)");
                return;
            }
            if (string.IsNullOrEmpty(tb_name.Text) || !Regex.IsMatch(tb_name.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите имя студента\n(буквенные значения русского алфавита)");
                return;
            }
            if (string.IsNullOrEmpty(tb_lastname.Text) || !Regex.IsMatch(tb_lastname.Text, "[а-яА-Я]"))
            {
                MessageBox.Show("Введите отчество студента\n(буквенные значения русского алфавита)");
                return;
            }
            if (string.IsNullOrEmpty(cb_groupe.Text))
            {
                MessageBox.Show("Введите группу студента");
                return;
            }
            if (string.IsNullOrEmpty(db_dateOfRemand.Text))
            {
                MessageBox.Show("Введите дату отчисления\n(dd.mm.yyyy)");
                return;
            }

            Models.Students editstudents = MainStudent._studentsContext.Students.FirstOrDefault(x => x.id == students.id);
            if (editstudents != null)
            {
                editstudents.surname = tb_surname.Text;
                editstudents.name = tb_name.Text;
                editstudents.lastname = tb_lastname.Text;
                editstudents.studGroupId = (int)(cb_groupe.SelectedItem as ComboBoxItem).Tag;
                editstudents.dateOfRemand = DateTime.Parse(db_dateOfRemand.Text);
                MainStudent._studentsContext.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.student);
            }
            else
            {
                MessageBox.Show("Произошла ошибка!");
                MainWindow.init.OpenPages(MainWindow.pages.student);
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
