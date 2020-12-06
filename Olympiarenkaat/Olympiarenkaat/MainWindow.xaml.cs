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

namespace Olympiarenkaat
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            n1.penni = new Pen(Brushes.Blue, 4);
            n2.penni = new Pen(Brushes.Black, 4);
            n3.penni = new Pen(Brushes.Red, 4);
            n4.penni = new Pen(Brushes.Yellow, 4);
            n5.penni = new Pen(Brushes.Green, 4);
        }
        
        private void move_Click(object sender, RoutedEventArgs e)
        {
            Canvas.SetLeft(s1, 300);
            Canvas.SetTop(s1, 50);

            Canvas.SetLeft(s2, 10);
            Canvas.SetTop(s2, 20);

            Canvas.SetLeft(s3, 100);
            Canvas.SetTop(s3, 30);

            Canvas.SetLeft(s4, 400);
            Canvas.SetTop(s4, 90);

            Canvas.SetLeft(s5, 500);
            Canvas.SetTop(s5, 10);

            n1.SetXY(90, 90);
            n2.SetXY(80, 10);
            n3.SetXY(-190, 30);
            n4.SetXY(210, 70);
            n5.SetXY(-200, 60);

        }
    }
}
