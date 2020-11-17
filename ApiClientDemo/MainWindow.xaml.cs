using ApiClientDemo.Views;
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

namespace ApiClientDemo
{

    public partial class MainWindow : Window
    {

        private AnimalWindow AnimalWindow;
        private PeopleWindow PeopleWindow;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Animal_Button(object sender, RoutedEventArgs e)
        {
            AnimalWindow = new AnimalWindow();
            AnimalWindow.Show();
        }

        private void People_Button(object sender, RoutedEventArgs e)
        {
            PeopleWindow = new PeopleWindow();
            PeopleWindow.Show();
        }
    }
}
