using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
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
            if (currentEquation.Contains("NaN"))
            {
                ResultTextBox.Text = "Error";
                currentEquation = "";
            }
            else
            {
                // Update the text box with the current equation
                ResultTextBox.Text = currentEquation.Replace('*', '×');
            }

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
            if (newOperator == "(" || newOperator == ")")
            {
                currentEquation += newOperator;
                UpdateEquation();
                return;
            }
            if (newOperator == "×")
            {
                newOperator = "*";
            }

            if (currentEquation.Length == 0)
            {
                return;
            }
            string lastChar = currentEquation.Substring(currentEquation.Length - 1);
            if (lastChar == "*" || lastChar == "+" || lastChar == "-" || lastChar == "/")
            {
                return;
            }
            currentEquation += newOperator;
            UpdateEquation();
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            int count1 = currentEquation.Length - currentEquation.Replace("(", "").Length;
            int count2 = currentEquation.Length - currentEquation.Replace(")", "").Length;
            if (count1 != count2)
            {
                currentEquation = Convert.ToString(Double.NaN);
                UpdateEquation();
                return;
            }
            DataTable dt = new DataTable();
            try
            {
                string result = dt.Compute(currentEquation, "").ToString();
                //limit the number of decimal places to 2
                result = Convert.ToDouble(result).ToString("0.##");


                currentEquation = result;
          
                UpdateEquation();
            }
            catch (SyntaxErrorException)
            {
                currentEquation = Convert.ToString(Double.NaN);
                UpdateEquation();
            }

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
            string toggledNumber;
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
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (currentEquation.Length > 0)
            {
                currentEquation = currentEquation.Substring(0, currentEquation.Length - 1);
                UpdateEquation();
            }
        }
    }
}

