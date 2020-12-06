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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        long num1 = 0;
        long num2 = 0;
        long total = 0;
        string operation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Nums(object sender, RoutedEventArgs e)
        {

            if (operation == "")
            {
                num1 = (num1 * 10) + Convert.ToInt64(((Button)sender).Content);
                disp.Text = num1.ToString();
            }
            else
            {
                num2 = (num2 * 10) + Convert.ToInt64(((Button)sender).Content);
                disp.Text = num2.ToString();
            }

        }

        private void Calculate(object sender, RoutedEventArgs e)
        {
            switch (operation)
            {
                case "+":
                    total = num1 + num2;
                    disp.Text = total.ToString();
                    num1 = 0;
                    num2 = 0;
                    total = 0;
                    operation = "";
                    break;

                case "-":
                    total = num1 - num2;
                    disp.Text = total.ToString();
                    num1 = 0;
                    num2 = 0;
                    total = 0;
                    operation = "";
                    break;

                case "*":
                    total = num1 * num2;
                    disp.Text = total.ToString();
                    num1 = 0;
                    num2 = 0;
                    total = 0;
                    operation = "";
                    break;

                case "/":
                    total = num1 / num2;
                    disp.Text = total.ToString();
                    num1 = 0;
                    num2 = 0;
                    total = 0;
                    operation = "";
                    break;
            }
        }

        private void Sum(object sender, RoutedEventArgs e)
        {
            operation = "+";
        }

        private void Minus(object sender, RoutedEventArgs e)
        {
            operation = "-";
        }

        private void Multiply(object sender, RoutedEventArgs e)
        {
            operation = "*";
        }

        private void Divide(object sender, RoutedEventArgs e)
        {
            operation = "/";
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            num1 = 0;
            num2 = 0;
            total = 0;
            operation = "";
            disp.Text = total.ToString();
        }
    }

}
