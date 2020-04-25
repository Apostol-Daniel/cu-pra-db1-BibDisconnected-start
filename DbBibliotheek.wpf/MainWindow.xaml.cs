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
using System.Data;
using System.IO;

namespace DbBibliotheek.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void btnSorteer_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnBoekToevoegen_Click(object sender, RoutedEventArgs e)
        {
        }
        private void btnVerwijderAuteur_Click(object sender, RoutedEventArgs e)
        {
            if (dgAuteur.SelectedIndex > -1) 
            {
            }
        }





    }
}
