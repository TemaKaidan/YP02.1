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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для DisciplineItem.xaml
    /// </summary>
    public partial class DisciplineItem : UserControl
    {
        private Disciplines _disciplines;
        private readonly TeachersContext _teachersContext;
        public DisciplineItem(Disciplines disciplines)
        {
            InitializeComponent();
            _disciplines = disciplines;

            lb_Name.Content = "Дисциплина: " + disciplines.name;
            TeachersContext _teacher = new TeachersContext();
            var teacher = _teacher.Teachers.FirstOrDefault(g => g.id == disciplines.teacherId);
            lb_teacherId.Content = "Преподователь: " + (teacher != null ? teacher.surname : "Неизвестно ") + " " + (teacher != null ? teacher.name : "Неизвестно ") + " " + (teacher != null ? teacher.lastname : "Неизвестно ");

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
