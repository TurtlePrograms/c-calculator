using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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


namespace Rekenmachine
{
    public partial class MainWindow : Window
    {
        private string currentEquation = "";

        public MainWindow()
        {
            InitializeComponent();
        }

        private void UpdateEquation()
        {
            ResultTextBox.Text = currentEquation;
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Content;

            currentEquation += number;
            UpdateEquation();
        }

        private string[] SplitEquation()
        {
            string[] numbers = currentEquation.Split('×', '+', '-', '/');
            return numbers;
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            string currentNumber = SplitEquation().Last();
            if (!currentNumber.Contains('.'))
            {
                currentEquation += ".";
                UpdateEquation();
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string newOperator = (string)((Button)sender).Content;

            currentEquation += newOperator;
            UpdateEquation();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            Console.WriteLine(currentEquation);
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            currentEquation = "";
            UpdateEquation();
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            // Regular expression to match the last number in the string
            string pattern = @"(?<![\d.])-?\d+(\.\d+)?$";
            Match match = Regex.Match(currentEquation, pattern);
            string toggledNumber = "";
            if (match.Success)
            {
                string lastNumber = match.Value;
                if (lastNumber.StartsWith("-"))
                {
                    toggledNumber = lastNumber.Substring(1); // Remove the negative sign
                }
                else
                {
                    toggledNumber = "-" + lastNumber; // Add the negative sign
                }
                // Handle edge cases to avoid combining numbers incorrectly
                currentEquation = currentEquation.Substring(0, match.Index) + toggledNumber;
            }


            UpdateEquation();
        
        }

    
    }
}

