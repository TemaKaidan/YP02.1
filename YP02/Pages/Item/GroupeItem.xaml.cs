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
    /// Логика взаимодействия для GroupeItem.xaml
    /// </summary>
    public partial class GroupeItem : UserControl
    {
        Pages.listPages.Group MainGrope;
        Models.StudGroups studGroups;

        public GroupeItem(StudGroups studGroups, Group MainGrope)
        {
            InitializeComponent();
            this.studGroups = studGroups;
            this.MainGrope = MainGrope;

            lb_Name.Content = "Наименование группы: " + studGroups.name;
        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("При удалении все связанные данные также будут удалены!", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                MainGrope._studgroupsContext.StudGroups.Remove(studGroups);
                MainGrope._studgroupsContext.SaveChanges();
                (this.Parent as Panel).Children.Remove(this);
            }
        }
    }
}