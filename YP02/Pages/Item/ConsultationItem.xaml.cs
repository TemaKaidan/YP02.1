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

namespace YP02.Pages.Item
{
    /// <summary>
    /// Логика взаимодействия для ConsultationItem.xaml
    /// </summary>
    public partial class ConsultationItem : UserControl
    {
        private Consultations _consultations;
        private readonly ConsultationsContext _consultationsContext;
        private readonly DisciplinesContext _disciplinesContext = new DisciplinesContext();
        public ConsultationItem(Consultations consultations)
        {
            InitializeComponent();
            _consultations = consultations;


            lb_Discipline.Content = "Дисцилина: " + _disciplinesContext.Disciplines.FirstOrDefault(x => x.id == _consultations.disciplineId).name;
            lb_Date.Content = "Дата: " + consultations.date;

        }

        private void Click_Edit(object sender, RoutedEventArgs e)
        {

        }

        private void Click_Delete(object sender, RoutedEventArgs e)
        {

        }
    }
}
