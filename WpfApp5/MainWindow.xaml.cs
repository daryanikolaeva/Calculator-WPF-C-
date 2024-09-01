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

namespace WpfApp5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public string oper;
        public string num1;
        public bool num2;

        public string oper1;
        public string num11;
        public bool num22;
        public int znak=0;
 


        public MainWindow()
        {
            num2 = false; //проверка будет ли введено второе число
          
            InitializeComponent();
            textBlock1.PreviewTextInput += TextBox_InputNumbers;
            textBlock1.PreviewTextInput += TextBox_InputGeneralOperations;
            textBlock1.PreviewKeyDown += TextBox_Result;


        }

        private void Button_Click_Numbers(object sender, RoutedEventArgs e) //кнопки числа
        {
            if (num2 == true) //если подтвержден ввод второго числа, то текстбокс обнуляем для дальнейшего ввода
            {
                num2 = false;
                textBlock1.Text = "0";
            }
            Button B = (Button)sender;
            if (textBlock1.Text == "0") //если значение равно 0, то вводится текст кнопками и в текстблоке изменяется
                textBlock1.Text = Convert.ToString(B.Content);
            else
                textBlock1.Text = textBlock1.Text + B.Content; // если не 0, то добавляются цифры (чтобы первым не стоял 0)
        }

        private void Button_Click_AC(object sender, RoutedEventArgs e) //кнопка стирания
        {
            textBlock1.Text = "0";
        }

        private void Button_Click_GeneralOperations(object sender, RoutedEventArgs e) //кнопки основных операций 
        {
            Button B = (Button)sender; //присваиваем значение из кнопки
            oper = Convert.ToString(B.Content); //добавляем его в операцию 
            num1 = textBlock1.Text; //текст с текстбокса
            num2 = true; // второе число подтверждено

        }

        private void Button_Click_Result(object sender, RoutedEventArgs e) //кнопка пробел
        {
            double new_num1, new_num2; //новые переменные для счета
            new_num1 = Convert.ToDouble(num1); //число из num1
            new_num2 = Convert.ToDouble(textBlock1.Text); //число из текстбокса
            double res = 0;

            if (oper == "+")
            {
                res = new_num1 + new_num2;
                textBlock1.Text = Convert.ToString(res);
            }
            if (oper == "-")
            {
                res = new_num1 - new_num2;
                textBlock1.Text = Convert.ToString(res);
            }
            if (oper == "*")
            {
                res = new_num1 * new_num2;
                textBlock1.Text = Convert.ToString(res);
            }
            if (oper == "/")
            {
                if (new_num2 == 0)

                {
                    textBlock1.Text = "error";
                    MessageBox.Show("Нельзя делить на 0");
                }
                else
                {
                    res = new_num1 / new_num2;
                    textBlock1.Text = Convert.ToString(res);
                }
            }

            oper = "=";
            num2 = true;

        }

        private void Button_Click_Change(object sender, RoutedEventArgs e) //изменение знака
        {
            double new_num1, res;
            new_num1 = Convert.ToDouble(textBlock1.Text);
            res = -new_num1;
            textBlock1.Text = Convert.ToString(res);

        }

        private void Button_Click_Dot(object sender, RoutedEventArgs e) //кнопка точка и проверка ввода нескольких 
        {
            if (!textBlock1.Text.Contains(","))
                textBlock1.Text = textBlock1.Text + ",";
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (!Int32.TryParse(e.Text, out val) && e.Text != "+" && e.Text != "-" && e.Text != "/" && e.Text != "*" && e.Text != "," && e.Text != "=")
            {
                e.Handled = true; //если не цифра или математический знак, то отклоняем ввод
            }
            if (textBlock1.Text.Contains(",") && e.Text == ",")
                e.Handled = true;
        }


        

        private void TextBox_InputGeneralOperations(object sender, TextCompositionEventArgs e)
        {
            if (e.Text == "+" || e.Text == "-" || e.Text == "*" || e.Text == "/")
            {
                string p = e.Text;
                oper1 = p; //добавляем его в операцию 
                
                num11 = textBlock1.Text; //текст с текстбокса
                num22 = true;
                textBlock1.Clear();// второе число подтверждено
            }
        }
        private void TextBox_InputNumbers(object sender, TextCompositionEventArgs e)
        {
            int val;
            if (e.Text == "-" && num22 == false) //первое число отрицательное
                znak = 1;
            if (Int32.TryParse(e.Text, out val))
            {
                if (num22 == true) //если подтвержден ввод второго числа, то текстбокс обнуляем для дальнейшего ввода
                {
                    num22 = false;
                    textBlock1.Clear();
                }

                if (textBlock1.Text == "0") //если значение равно 0, то вводится текст кнопками и в текстблоке изменяется
                    textBlock1.Clear();
               
            }
        }
        private void TextBox_Result(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {

                double new_num1, new_num2; //новые переменные для счета
                if (znak==1)
                    new_num1 = -Convert.ToDouble(num11); //число из num1
                else
                    new_num1 = Convert.ToDouble(num11); //число из num1
                new_num2 = Convert.ToDouble(textBlock1.Text); //число из текстбокса
                textBlock1.Clear();
                double res = 0;

                if (oper1 == "+")
                {
                    res = new_num1 + new_num2;
        
                    textBlock1.Text = Convert.ToString(res);

                }
                if (oper1 == "-")
                {
                    res = new_num1 - new_num2;
                    textBlock1.Text = Convert.ToString(res);
                }
                if (oper1 == "*")
                {
                    res = new_num1 * new_num2;
                    textBlock1.Text = Convert.ToString(res);
                }
                if (oper1 == "/")
                {
                    if (new_num2 == 0)

                    {
                        textBlock1.Text = "error";
                        MessageBox.Show("Нельзя делить на 0");
                    }
                    else
                    {
                        res = new_num1 / new_num2;
                        textBlock1.Text = Convert.ToString(res);
                    }
                }

                oper1 = "=";
                num22 = true;
            }
        }



    }

}
