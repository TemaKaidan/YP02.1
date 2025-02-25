using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace YP02
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static MainWindow init;

        public MainWindow()
        {
            InitializeComponent();
            init = this;
            OpenPages(pages.authorization);
        }

        public enum pages
        {
            authorization,
            main,
            student
        }

        public void OpenPages(pages _pages)
        {
            this.MinHeight = 800;
            this.MinWidth = 950;
            this.Height = 850;
            this.Width = 1000;

            switch (_pages)
            {
                case pages.authorization:
                    frame.Navigate(new Pages.Authorization());
                    break;

                case pages.main:
                   frame.Navigate(new Pages.Main());
                    break;

                //case pages.student:
                //    frame.Navigate(new Pages.Student());
                //    break;
            }
        }
    }
}