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

namespace Rekenmachine
{
    public partial class MainWindow : Window
    {
        private double currentNumber = 0;
        private double result = 0;
        private string currentOperator = "";
        private bool isNewNumber = true;
        private StringBuilder equationBuilder = new StringBuilder();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Number_Click(object sender, RoutedEventArgs e)
        {
            string number = (string)((Button)sender).Content;

            if (isNewNumber)
            {
                ResultTextBox.Text = number;
                isNewNumber = false;
            }
            else
            {
                if (ResultTextBox.Text == "0")
                    ResultTextBox.Text = number;
                else
                    ResultTextBox.Text += number;
            }

            equationBuilder.Append(number);
            EquationTextBlock.Text = equationBuilder.ToString();
        }

        private void Decimal_Click(object sender, RoutedEventArgs e)
        {
            if (!ResultTextBox.Text.Contains("."))
            {
                ResultTextBox.Text += ".";
                equationBuilder.Append(".");
                EquationTextBlock.Text = equationBuilder.ToString();
            }
        }

        private void Operator_Click(object sender, RoutedEventArgs e)
        {
            string newOperator = (string)((Button)sender).Content;

            if (!isNewNumber)
            {
                Equals_Click(null, null);
                isNewNumber = true;
            }

            equationBuilder.Append(" ");
            equationBuilder.Append(newOperator);
            equationBuilder.Append(" ");
            EquationTextBlock.Text = equationBuilder.ToString();

            currentOperator = newOperator;
            currentNumber = double.Parse(ResultTextBox.Text);
        }

        private void Equals_Click(object sender, RoutedEventArgs e)
        {
            double newNumber = double.Parse(ResultTextBox.Text);

            switch (currentOperator)
            {
                case "+":
                    result = result + newNumber;
                    break;
                case "-":
                    result = result - newNumber;
                    break;
                case "×":
                    result = result * newNumber;
                    break;
                case "÷":
                    if (newNumber != 0)
                        result = result / newNumber;
                    else
                        MessageBox.Show("Cannot divide by zero.");
                    break;
                default:
                    result = newNumber;
                    break;
            }

            ResultTextBox.Text = result.ToString();
            isNewNumber = true;
            equationBuilder.Clear();
            equationBuilder.Append(result.ToString());
            EquationTextBlock.Text = equationBuilder.ToString();
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            ResultTextBox.Text = "0";
            currentNumber = 0;
            result = 0;
            currentOperator = "";
            isNewNumber = true;
            equationBuilder.Clear();
            EquationTextBlock.Text = "0";
        }

        private void Negate_Click(object sender, RoutedEventArgs e)
        {
            if (ResultTextBox.Text != "0")
            {
                double number = double.Parse(ResultTextBox.Text);
                ResultTextBox.Text = (-number).ToString();
            }
        }
    }
}