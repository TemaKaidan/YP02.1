﻿using System;
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
using YP02.Models;
using YP02.Pages.listPages;

namespace YP02.Pages.Edit
{
    /// <summary>
    /// Логика взаимодействия для ConsultationEdit.xaml
    /// </summary>
    public partial class ConsultationEdit : Page
    {
        private bool isMenuCollapsed = false;

        public Pages.listPages.Consultation MainConsultation;
        public Models.Consultations consultations;

        Context.DisciplinesContext disciplinesContext = new DisciplinesContext();

        public ConsultationEdit(Pages.listPages.Consultation MainConsultation, Models.Consultations consultations= null)
        {
            InitializeComponent();
            this.MainConsultation = MainConsultation;
            this.consultations = consultations;


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
            db_date.Text = consultations.date.ToString();
            tb_submittedWorks.Text = consultations.submittedWorks;
        }

        private void Edit_Consultation(object sender, RoutedEventArgs e)
        {
            Models.Consultations editConsultations = MainConsultation._consultationsContext.Consultations.
                FirstOrDefault(x => x.id == consultations.id);
            if (editConsultations != null)
            {
                editConsultations.disciplineId = (int)(cb_disciplineId.SelectedItem as ComboBoxItem).Tag;
                editConsultations.date = DateTime.Parse(db_date.Text);
                editConsultations.submittedWorks = tb_submittedWorks.Text;

                MainConsultation._consultationsContext.SaveChanges();
                MainWindow.init.OpenPages(MainWindow.pages.consultation);
            }
            else
            {
                MessageBox.Show("Произошла ошибка!");
                MainWindow.init.OpenPages(MainWindow.pages.consultation);
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