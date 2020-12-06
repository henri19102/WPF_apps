using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace NotePad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo("sv-SE");
            file.Header = NotePad.Properties.Resources.HF;
            OpenItem.Header = NotePad.Properties.Resources.HO;
            SaveItem.Header = NotePad.Properties.Resources.HS;
            PrintItem.Header = NotePad.Properties.Resources.HP;
            edit.Header = NotePad.Properties.Resources.Hedit;
            copy.Header = NotePad.Properties.Resources.Hcopy;
            paste.Header = NotePad.Properties.Resources.Hpaste;
            cut.Header = NotePad.Properties.Resources.Hcut;
            format.Header = NotePad.Properties.Resources.Hformat;
            FontItem.Header = NotePad.Properties.Resources.Hfont;
            test.Text = NotePad.Properties.Resources.testT;

        }

        private void OpenItem_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = "Document"; 
            dlg.DefaultExt = ".txt"; 
            dlg.Filter = "Text documents (.txt)|*.txt"; 

            Nullable<bool> result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
                textBox.Text += File.ReadAllText(filename);
            }
        }

        private void SaveItem_Click(object sender, RoutedEventArgs e)
        {
            string fileText = textBox.Text;

            Microsoft.Win32.SaveFileDialog dialog = new SaveFileDialog()
            {
                Filter = "Text Files(*.txt)|*.txt|All(*.*)|*"
            };

            if (dialog.ShowDialog() == true)
            {
                File.WriteAllText(dialog.FileName, fileText);
            }
        }

        private void PrintItem_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog printDialog = new PrintDialog();
            if ((bool)printDialog.ShowDialog().GetValueOrDefault())
            {
                FlowDocument flowDocument = new FlowDocument();
                foreach (string line in textBox.Text.Split('\n'))
                {
                    Paragraph myParagraph = new Paragraph();
                    myParagraph.Margin = new Thickness(0);
                    myParagraph.Inlines.Add(new Run(line));
                    flowDocument.Blocks.Add(myParagraph);
                }
                DocumentPaginator paginator = ((IDocumentPaginatorSource)flowDocument).DocumentPaginator;
                printDialog.PrintDocument(paginator, "Title");
            }
        }

        private void FontItem_Click(object sender, RoutedEventArgs e)
        {
            Window1 myWindow = new Window1();
            
            
            if (myWindow.ShowDialog() == true)
            {
                if (string.IsNullOrEmpty(myWindow.textBox2.Text))
                {
                    textBox.FontSize = 12.0;
                }
                else
                {
                    double fon = Convert.ToDouble(myWindow.textBox2.Text);
                    textBox.FontSize = fon;
                }

                }
        }
    }
}
