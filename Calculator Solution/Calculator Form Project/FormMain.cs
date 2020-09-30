using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator_Form_Project
{
    public partial class FormMain : Form
    {
        public struct ButtonStruct
        {
            public char Content;
            public bool isBold;
            public bool isNumber;
            public bool isDecimalSeparator;
            public bool isPlusMinusSign;
            public ButtonStruct(char Content, bool isBold, bool isNumber = true, bool isDecimalSeparator = false, bool isPlusMinusSign = false)
            {
                this.Content = Content;
                this.isBold = isBold;
                this.isNumber = isNumber;
                this.isDecimalSeparator = isDecimalSeparator;
                this.isPlusMinusSign = isPlusMinusSign;
            }
            public override string ToString()
            {
                return base.ToString();
            }

        }
        //private char[,] buttons =new char[6,4];
        private ButtonStruct[,] buttons =
        {
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false),new ButtonStruct('C',false),new ButtonStruct('<',false)},
            {new ButtonStruct(' ',false),new ButtonStruct(' ',false,false),new ButtonStruct(' ',false,false),new ButtonStruct('/',false)},
            {new ButtonStruct('7',true,true),new ButtonStruct('8',true,true),new ButtonStruct('9',true,true),new ButtonStruct('X',false)},
            {new ButtonStruct('4',true,true),new ButtonStruct('5',true,true),new ButtonStruct('6',true,true),new ButtonStruct('-',false)},
            {new ButtonStruct('1',true,true),new ButtonStruct('2',true,true),new ButtonStruct('3',true,true),new ButtonStruct('+',false)},
            {new ButtonStruct('±',false,false,false,true),new ButtonStruct('0',true),new ButtonStruct('.',false,false,true),new ButtonStruct('=',false,false)},

        };
        public FormMain()
        {
            InitializeComponent();
        }
        private RichTextBox resultBox;
        private void FormMain_Load(object sender, EventArgs e)
        {
            makebuttons(buttons);
            makeResultsBox();
        }
        private void makeResultsBox()
        {
            resultBox = new RichTextBox();
            resultBox.ReadOnly = true;
            resultBox.SelectionAlignment = HorizontalAlignment.Right;
            resultBox.Font = new Font("Segoe UI", 22);
            resultBox.Width = this.Width - 16;
            resultBox.Height = 50;
            resultBox.Top = 20;
            resultBox.Text = "0";
            resultBox.TabStop = false;
            resultBox.TextChanged += resultBox_textChanged;
            this.Controls.Add(resultBox);
        }

        private void resultBox_textChanged(object sender, EventArgs e)
        {
            int newSize = 22 - (15 - resultBox.TextLength);
            if(newSize>8&&newSize<23)
            {
                int delta = 15 - resultBox.Text.Length;
                resultBox.Font = new Font("Serge UI", delta);
            }
            else
                resultBox.Font = new Font("Serge UI", 22);

        }

        private void makebuttons(ButtonStruct[,] buttons)
        {
            int buttonWidth = 82;
            int buttonHeight = 60;
            int posX = 0;
            int posY = 101;
            for (int i = 0; i < buttons.GetLength(0); i++)
            {
                posX = 0;
                for (int j = 0; j < buttons.GetLength(1); j++)
                {
                    Button newButton = new Button();
                    newButton.Text = buttons[i, j].Content.ToString();
                    newButton.Font = new Font("segoe UI", 16);
                    ButtonStruct bs = buttons[i, j];
                    if (bs.isBold)
                    {
                        newButton.Font = new Font(newButton.Font, FontStyle.Bold);
                    }
                    //newButton.Text = buttons[i,j].ToString();
                    newButton.Width = buttonWidth;
                    newButton.Height = buttonHeight;
                    newButton.Left= posX;
                    newButton.Top = posY;
                    newButton.Tag = bs;
                    newButton.Click+=Button_click;
                    posX += buttonWidth;
                    this.Controls.Add(newButton);

                }
                posY += buttonHeight;
            }
        }

        private void Button_click(object sender, EventArgs e)
        {
            Button clickedButton = (Button)sender;
            ButtonStruct bs = (ButtonStruct)clickedButton.Tag;
            if ((resultBox.Text == "0")&&(bs.isDecimalSeparator==false))
                resultBox.Text = null;
            if (bs.isNumber)
            {
                
                resultBox.Text += clickedButton.Text;
            }
            else
            {
                if (bs.isDecimalSeparator)
                {
                    if (!resultBox.Text.Contains(bs.Content))
                        resultBox.Text += clickedButton.Text;
                        
                }
                if (bs.isPlusMinusSign)
                {
                    if (!resultBox.Text.Contains(bs.Content))
                        resultBox.Text = "-" + resultBox.Text;
                    else
                        resultBox.Text=resultBox.Text.Replace("-", " ");
                }
                else
                {
                    switch (bs.Content)
                    {
                        case 'C':
                            resultBox.Text = "0";
                            break;
                        case '<':
                            resultBox.Text = resultBox.Text.Remove(resultBox.Text.Length-1);
                            if ((resultBox.Text.Length==0)||(resultBox.Text=="-0")|| (resultBox.Text == "-"))
                            {
                                resultBox.Text = "0";
                            }
                            break;
                        default:
                            break;


                    }
                        
                }
            }
            
        }
    }
}
