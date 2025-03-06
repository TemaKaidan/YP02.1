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
        Pages.listPages.Discipline MainDiscipline;
        Models.Disciplines disciplines;

        public DisciplineItem(Disciplines disciplines, Discipline MainDiscipline)
        {
            InitializeComponent();
            this.disciplines = disciplines;
            this.MainDiscipline = MainDiscipline;

            lb_Name.Content = "Дисциплина: " + disciplines.name;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {
            MainWindow.init.OpenPages(MainWindow.pages.disciplineEdit, null, null, null, disciplines);
        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainDiscipline._disciplinesContext.Disciplines.Remove(disciplines);
                MainDiscipline._disciplinesContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}
