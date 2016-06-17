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
using System.Windows.Shapes;

namespace RVSampleApp
{
    /// <summary>
    /// Interaction logic for CustomControlWindow.xaml
    /// </summary>
    public partial class CustomControlWindow : Window
    {
        public CustomControlWindow()
        {
            InitializeComponent();
        }

        private void closeButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void setGradientButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int red1 = int.Parse(rbox1.Text);
                int green1 = int.Parse(gbox1.Text);
                int blue1 = int.Parse(bbox1.Text);
                int red2 = int.Parse(rbox2.Text);
                int green2 = int.Parse(gbox2.Text);
                int blue2 = int.Parse(bbox2.Text);
                LinearGradientBrush lgb = new LinearGradientBrush(
                    Color.FromRgb((byte)red1, (byte)green1, (byte)blue1),
                    Color.FromRgb((byte)red2, (byte)green2, (byte)blue2),
                    new Point(0.5, 0),
                    new Point(0.5, 1));
                customControlTest.ccBorder.Background = lgb;
            }
            catch(ArgumentNullException)
            {
                MessageBox.Show("RGB values cannot be empty!", "RGB Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch(FormatException)
            {
                MessageBox.Show("RGB values can only be integers from 0 to 255!", "RGB Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void setBorderButton_Click(object sender, RoutedEventArgs e)
        {
            double borderThickness = 0.0;
            try
            {
                borderThickness = double.Parse(borderThicknessTB.Text);
                if(borderThickness < 0)
                {
                    MessageBox.Show("Border thickness cannot be less than 0.", "Border Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if(allBorderRB.IsChecked == true) //the IsChecked property *has* to check against a boolean literal
                {
                    customControlTest.ccBorder.BorderThickness = new Thickness(borderThickness);
                }
                else if(leftBorderRB.IsChecked == true)
                {
                    customControlTest.ccBorder.BorderThickness = new Thickness(borderThickness, 0, 0, 0);
                }
                else if(rightBorderRB.IsChecked == true)
                {
                    customControlTest.ccBorder.BorderThickness = new Thickness(0, 0, borderThickness, 0);
                }
                else if(topBorderRB.IsChecked == true)
                {
                    customControlTest.ccBorder.BorderThickness = new Thickness(0, borderThickness, 0, 0);
                }
                else if(bottomBorderRB.IsChecked == true)
                {
                    customControlTest.ccBorder.BorderThickness = new Thickness(0, 0, 0, borderThickness);
                }
            }
            catch(FormatException)
            {
                MessageBox.Show("Value of thickness must be in a numeric form.", "Thickness Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
