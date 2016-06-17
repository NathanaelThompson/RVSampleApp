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

namespace RVSampleApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            textOutBox.Document.Blocks.Clear();
            rtbIn.Document.Blocks.Clear();
        }

        private void customCntrlButton_Click(object sender, RoutedEventArgs e)
        {
            var secondWindow = new CustomControlWindow();
            secondWindow.Show();
        }

        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void generateButton_Click(object sender, RoutedEventArgs e)
        {
            textOutBox.Document.Blocks.Clear();
            string textIn = new TextRange(rtbIn.Document.ContentStart, rtbIn.Document.ContentEnd).Text;
            StringBuilder prevText = new StringBuilder();
            StringBuilder highlightText = new StringBuilder();
            bool noHighlightFlag = true;

            for (int i = 0; i < textIn.Length; i++)
            {
                if(textIn[i] != '#' && textIn[i] != '@' && textIn[i] != '\\') //if the current char isn't a delimiter
                {
                    prevText.Append(textIn[i]); //and it to the previously scanned chars
                }
                else if(textIn[i] == '#' || textIn[i] == '@') //if we have found one of the special chars
                {
                    //range for previously found output
                    TextRange blRange = new TextRange(textOutBox.Document.ContentEnd, textOutBox.Document.ContentEnd);
                    blRange.Text = prevText.ToString();
                    blRange.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);

                    int j = i;
                    do //we need the loop to execute at least once, the char at position i will break out of the while loop immediately otherwise
                    {
                        highlightText.Append(textIn[j]); //append until a special char is found
                        j++;
                    } //an ugly condition, but it works
                    while (j < textIn.Length && textIn[j] != ' ' && textIn[j] != '\r' && textIn[j] != '#' && textIn[j] != '@');
                    
                    if (highlightText.ToString().Length > 1) //if a special char was not found immediately, change the color
                    {
                        TextRange trToAppend = new TextRange(textOutBox.Document.ContentEnd, textOutBox.Document.ContentEnd);
                        trToAppend.Text = highlightText.ToString() + " ";
                        trToAppend.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Blue);
                    }
                    else //otherwise, don't change color
                    {
                        TextRange trToAppend = new TextRange(textOutBox.Document.ContentEnd, textOutBox.Document.ContentEnd);
                        highlightText.Append(textIn[j]);
                        trToAppend.Text = highlightText.ToString();
                        trToAppend.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black); 
                    }
                    prevText.Clear();
                    highlightText.Clear();
                    noHighlightFlag = false;
                    i = j;
                }
            }
            if(noHighlightFlag || prevText.ToString() != "")//if there has been no change to the text, simply add it to the output
            {
                TextRange trToAppend = new TextRange(textOutBox.Document.ContentEnd, textOutBox.Document.ContentEnd);
                trToAppend.Text = prevText.ToString();
                trToAppend.ApplyPropertyValue(TextElement.ForegroundProperty, Brushes.Black);
                
            }
        }
    }
}
