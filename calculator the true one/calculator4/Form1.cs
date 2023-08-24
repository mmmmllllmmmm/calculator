using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Windows.Forms;

namespace calculator4
{
    public partial class Form1 : Form
    {

        string numhold = "";
        // Stack numbers = new Stack();
        Stack operations = new Stack();
        //  Queue postfix = new Queue();
        Stack idit = new Stack();

        int j = 0;

        string[] posfix = new string[40];
        string[] bow = new string[40];
        int bb;
        string[] rebow = new string[40];
        int rebb = 0;
        //string[] reposfix = new string[20];
        int i = 0;
        string result_operation;
        double azal = 0;
        bool opin = true;
        bool hamza =false;
        char previous;
        public Form1()
        {
            InitializeComponent();
        }
        private void res(string[] array, int both)
        {
            for (int result = 0; result < both; result++)
            {
                if (array[result] == "+" || array[result] == "-" || array[result] == "*" || array[result] == "/" || array[result] == "%")
                {
                    result_operation = array[result];
                    switch (result_operation)
                    {
                        case "+":
                            {
                                double i1 = double.Parse(idit.Pop().ToString());
                                double i2 = double.Parse(idit.Pop().ToString());
                                azal = i2 + i1;
                                break;
                            }
                        case "-":
                            {
                                double i1 = double.Parse(idit.Pop().ToString());
                                double i2 = double.Parse(idit.Pop().ToString());
                                azal = i2 - i1;
                                break;
                            }

                        case "*":
                            {
                                double i1 = double.Parse(idit.Pop().ToString());
                                double i2 = double.Parse(idit.Pop().ToString());
                                azal = i2 * i1;
                                break;
                            }
                        case "/":
                            {
                                double i1 = double.Parse(idit.Pop().ToString());
                                double i2 = double.Parse(idit.Pop().ToString());
                                azal = i2 / i1;
                                break;
                            }
                        case "%":
                            {
                                double i1 = double.Parse(idit.Pop().ToString());
                                double i2 = double.Parse(idit.Pop().ToString());
                                azal = i2 % i1;
                                break;

                            }
                    }
                    idit.Push(azal);
                }
                else
                {
                    idit.Push(double.Parse(array[result]));
                }
            }
        }
        private void priority(string received)
        {
            if (operations.Count == 0)
            {
                operations.Push(received);
            }
            else
            {
                if (numhold != "")
                {
                    posfix[i] = numhold;
                    i++;
                    numhold = "";
                }
                if (operations.Peek().ToString() == "*" || operations.Peek().ToString() == "/"||operations.Peek().ToString() == "%")
                {
                    if (operations.Contains("("))
                    {

                        if (received == "*" || received == "/"||received=="%")
                        {
                            posfix[i] = operations.Pop().ToString();
                            i++;
                        }
                        else
                        {
                            while (operations.Peek().ToString() != "(")

                            {
                                posfix[i] = operations.Pop().ToString();
                                i++;
                            }
                        }
                        operations.Push(received);
                    }
                    else
                    {
                        if (received == "+" || received == "-")
                            while (operations.Count != 0)

                            {
                                posfix[i] = operations.Pop().ToString();
                                i++;
                            }
                        else
                        {
                            posfix[i] = operations.Pop().ToString();
                            i++;
                        }
                        operations.Push(received);

                    }
                    hamza = false;
                }
            }
            }
        private void button12_Click(object sender, EventArgs e)
        {
            Button pr = (Button)sender;
            if (textBox1.Text == "0" && pr.Text != ".")
            {
                textBox1.Clear();
                numhold += pr.Text;
                textBox1.Text += pr.Text;
            }
            else if (pr.Text == ".")
            {
                if (!numhold.Contains("."))
                {
                    if (textBox1.Text == "0" && numhold == "")
                    {
                        numhold += 0 + pr.Text;
                        textBox1.Text += pr.Text;
                    }
                    else
                    {
                        numhold += pr.Text;
                        textBox1.Text += pr.Text;
                    }
                }
            }
            else if(hamza==true)
            {
                priority("*");
                    numhold += pr.Text;
                    textBox1.Text += pr.Text;
                    hamza = false;   
            }
            else
            {
                numhold += pr.Text;
                textBox1.Text += pr.Text;
            }
            opin = true;
        }
        private void button17_Click(object sender, EventArgs e)
        {
            if (i != 0)
            {
                previous = textBox1.Text[textBox1.Text.Length - 2];
            }
            string check;
            textBox1.SelectionStart = textBox1.Text.Length - 1;
            textBox1.SelectionLength = textBox1.MaxLength;
            check = textBox1.SelectedText;
            if (textBox1.Text.Length > 0 && textBox1.Text != "0")
            {
                if (check == "+" || check == "-" || check == "*" || check == "/"||check=="%")
                {
                    operations.Pop();
                        while (posfix[i - 1] == "+" || posfix[i - 1] == "-" || posfix[i - 1] == "*" || posfix[i - 1] == "/"||posfix[i-1]=="%")
                        {
                            operations.Push(posfix[i - 1]);
                            posfix[i - 1] = null;
                            i--;
                        }
                        if (posfix[i - 1] == ")")//في حال كان اخر شي في المصفوفه هو غلقه القوس يافاااااااااااااااااااضل+ 
                        {
                            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                        }
                        else
                        {
                            numhold = posfix[i - 1];
                            posfix[i - 1] = null;
                            i--;
                            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                        }
                    opin = true;
                    }
                    else if (check == ")" )
                    {
                    if (numhold=="")
                    {
                        posfix[i - 1] = null;
                        i--;
                        operations.Push("(");
                        while (posfix[i - 1] == "+" || posfix[i - 1] == "-" || posfix[i - 1] == "*" || posfix[i - 1] == "/" || posfix[i - 1] == "%")
                        {
                            operations.Push(posfix[i - 1]);
                            posfix[i - 1] = null;
                            i--;
                        }
                        numhold = posfix[i - 1];
                        posfix[i - 1] = null;
                        i--;
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                    else
                    {
                       while(!textBox1.Text.EndsWith("("))
                        {
                            textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                        }
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                        numhold = ""; 
                    }
                    }
                    else if (check == "(")// في خطا عند الحذف الكبير
                    {
                        if(previous=='+'||previous=='*'||previous=='-'||previous=='/'||previous=='%')
                        {
                        operations.Pop();
                        posfix[i - 1] = null;
                        i--;
                        }
                        else 
                        {
                         operations.Pop();
                         operations.Pop();
                         posfix[i - 1] = null;
                        i--;
                        while (posfix[i - 1] == "+" || posfix[i - 1] == "-" || posfix[i - 1] == "*" || posfix[i - 1] == "/" || posfix[i - 1] == "%")
                        {
                            operations.Push(posfix[i - 1]);
                            posfix[i - 1] = null;
                            i--;
                        }
                        numhold = posfix[i - 1];
                         posfix[i - 1] = null;
                         i--;
                        }
                        textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                    }
                    else
                    {
                        if (i != 0)
                        {
                            if (posfix[i - 1] == ")")
                            {
                                operations.Pop();
                                numhold = numhold.Remove(numhold.Length - 1, 1);
                                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                            }
                            else
                            {
                                numhold = numhold.Remove(numhold.Length - 1, 1);
                                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                            }
                        }
                        else
                        {
                            if (numhold.Length == 1)
                            {
                                numhold = "";
                                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                            }
                            else
                            {
                                numhold = numhold.Remove(numhold.Length - 1, 1);
                                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                            }
                        }
                    }
                }
                if (textBox1.Text.Length == 0 || textBox1.Text == "0")
                {
                    textBox1.Text = "0";
                } 
        }
        private void button18_Click(object sender, EventArgs e)
        {
            textBox1.Text = "0";
            numhold = ""; 
            label2.Text = "";
            idit.Clear();
            operations.Clear();
            posfix = new string[40];
            i = 0;
            result_operation = "";
            azal = 0;
            opin = true;
            bow = new string[40];
            rebow = new string[40];
            bb = 0;
            rebb = 0;
            z = 0;
            x = 0;
        }
        private void button16_Click(object sender, EventArgs e)
        {
            Button op = (Button)sender;
            if(textBox1.Text=="0"&&op.Text=="(")
            {
                textBox1.Clear();
            }
            if (op.Text == "(")
            {
                 if (numhold != "")
                {
                    priority("*");
                }
                opin = true;  
            }
            if (opin)
            {
                if(textBox1.Text=="0")
                {
                    numhold = "0";
                }
                if (numhold != "")
                {
                    posfix[i] = numhold;
                    i++;
                }
                if (op.Text == "(")
                {
                    if(i!=0)
                    {
                        if (posfix[i - 1] == ")" && hamza)
                             priority("*");
                    }
                    operations.Push(op.Text);
                    posfix[i] = op.Text;
                    i++;
                    hamza = false;
                }
                else if (operations.Count == 0 || operations.Peek().ToString() == "(")
                {
                    operations.Push(op.Text);
                    hamza = false;
                }
                else if (op.Text == ")")
                {
                    while (operations.Peek().ToString() != "(")
                    {
                        posfix[i] = operations.Pop().ToString();
                        i++;
                    }
                    operations.Pop();
                    posfix[i] = op.Text;
                    i++;
                    hamza = true;
                }
                else if (operations.Peek().ToString() == "+" || operations.Peek().ToString() == "-")
                {                  
                    if (op.Text == "+" || op.Text == "-")
                    {
                        posfix[i] = operations.Pop().ToString();
                        i++;
                        operations.Push(op.Text);
                    }
                    else
                    {
                        operations.Push(op.Text);
                    }
                    hamza = false;
                }
                else if (operations.Peek().ToString() == "*" || operations.Peek().ToString() == "/"||operations.Peek().ToString()=="%")
                {
                    if (operations.Contains("("))
                    {
                        if(op.Text=="*"||op.Text=="/"||op.Text=="%")
                        {
                            posfix[i] = operations.Pop().ToString();
                            i++;
                        }
                        else
                        {
                            while (operations.Peek().ToString()!="(")

                            {
                                posfix[i] = operations.Pop().ToString();
                                i++;
                            }
                        }
                        operations.Push(op.Text);
                    }
                    else
                    {
                        if(op.Text== "+" || op.Text == "-")
                        while (operations.Count != 0)
                        {
                            posfix[i] = operations.Pop().ToString();
                            i++;
                        }
                        else
                        {
                            posfix[i] = operations.Pop().ToString();
                            i++;
                        }
                        operations.Push(op.Text);
                    }
                    hamza = false;
                }
                numhold = "";
                    textBox1.Text += op.Text;
                    opin = false;
                if (op.Text == ")")
                    opin = true;
            }
        }
        private void button19_Click(object sender, EventArgs e)
        {
            label2.Text = textBox1.Text;
            if (numhold != "")
            {
                posfix[i] = numhold;
                i++;
            }
            while (operations.Count != 0)
            {
                posfix[i] = operations.Pop().ToString();
                i++;
            }
            for (int b = 0; b < i; b++)
            {
                if(posfix[b]!="(")
                {
                    if (posfix[b] == ")")
                        continue;
                    if (posfix[b] != null)
                    {
                        bow[bb] = posfix[b];
                        bb++;
                    }
                }
                else
                {
                    while(posfix[b+1]!=")")
                    {
                        b++;
                        rebow[rebb] = posfix[b];
                        rebb++;
                    }
                    res(rebow,rebb);//////////////////////////////////hamza
                    bow[bb] = idit.Pop().ToString();
                    bb++;
                    while(rebb!=0)
                    {
                        rebb--;
                        rebow[rebb] = null;

                    }
                }
            }
            res(bow,bb);// 
            comboBox1.Items.Add(textBox1.Text);
            textBox1.Text = idit.Pop().ToString();
            numhold = textBox1.Text;
            idit.Clear();
            operations.Clear();
            posfix = new string[20];
            i = 0;
            result_operation = "";
            azal = 0;
            opin = true;
            bow = new string[20];
            rebow = new string[20];
            bb = 0;
            rebb = 0;
        }
        
        double z;
        int x;
        private void button26_Click_1(object sender, EventArgs e)
        {
            Button math = (Button)sender;
            x = numhold.Length;
            while (x != 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
                x--;
            }
            switch (math.Text)
            {
                case "-+":
                    {
                        z = double.Parse(numhold);
                        z *= -1;
                        numhold = z.ToString();
                        textBox1.Text += "(" + numhold + ")";
                        break;
                    }
                case "√":
                    {
                        z = Math.Sqrt(double.Parse(numhold));
                        numhold = z.ToString();
                        textBox1.Text += numhold;
                        break;
                    }
                case "X^2":
                    { 
                        z = Math.Pow(double.Parse(numhold), 2);
                        numhold = z.ToString();
                        textBox1.Text += numhold;
                        break;
                    }
                case "X^3":
                    {
                        z = Math.Pow(double.Parse(numhold), 3);
                        numhold = z.ToString();
                        textBox1.Text += numhold;
                        break;
                    }
                case "1/X":
                    {
                        z = 1.0/ double.Parse(numhold);
                        numhold = z.ToString();
                        textBox1.Text += numhold;
                        break;
                    }
            }
        }

        private void clearHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
        }
    }
}