using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using AngouriMath;

namespace calculator_xamarin
{
    public partial class MainPage : ContentPage
    {
        private List<Button> all_btns = new List<Button>();

        private List<string> all_btns_text = new List<string>() { 
            "Clear", "Bracket", "÷", "7", "8", "9", "×", "4", "5", 
            "6", "-", "1", "2", "3", "+", ".","0", "Equal"
        };

        private string AppTheme;

        public MainPage()
        {
            InitializeComponent();
            all_btns.Add(clear_btn);
            all_btns.Add(bracket_btn);
            all_btns.Add(div_btn);
            all_btns.Add(seven_btn);
            all_btns.Add(eight_btn);
            all_btns.Add(nine_btn);
            all_btns.Add(multi_btn);
            all_btns.Add(four_btn);
            all_btns.Add(five_btn);
            all_btns.Add(six_btn);
            all_btns.Add(subtract_btn);
            all_btns.Add(one_btn);
            all_btns.Add(two_btn);
            all_btns.Add(three_btn);
            all_btns.Add(add_btn);
            all_btns.Add(dot_btn);
            all_btns.Add(zero_btn);
            all_btns.Add(equal_btn);

            Xamarin.Essentials.AppTheme theme = AppInfo.RequestedTheme;
            switch (theme)
            {
                case Xamarin.Essentials.AppTheme.Dark:
                    AppTheme = "dark";
                    break;
                case Xamarin.Essentials.AppTheme.Light:
                    AppTheme = "light";
                    break;
                default:
                    AppTheme = "light";
                    break;
            }
            if(AppTheme == "light")
            {
                Main_number_add.TextColor = Color.Black;
            }
        }
        private string current_opt;
        private string current_opt_below;
        private string equal(string exp)
        {
            string new_expr = "";
            foreach(char i in exp)
            {
                char ii = i;
                if (i == '×')
                {
                    ii = '*';
                }
                else if (i == '÷')
                {
                    ii = '/';
                }
                new_expr += ii;
            }
            try
            {
                Entity expr = new_expr;
                double output = (double)expr.EvalNumerical();
                if((output%1 == 0) == true)
                {
                    int o = Convert.ToInt32(output);
                    return Convert.ToString(o);
                }
                else
                {
                    return Convert.ToString(output);
                }
            }
            catch
            {
                return exp;
            }
        }
        private int no_char = 0;
        private async void back_clicked(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            btn.BackgroundColor = Color.FromHex("#C7C7C7");
            await Task.Delay(60);
            if(no_char > 0)
            {
                current_opt = current_opt.Remove(current_opt.Length - 1, 1);
                current_opt_below = equal(current_opt);
                Main_number_add.Text = current_opt + " ";
                second_number_add.Text = current_opt_below + "  ";
                no_char -= 1;
            }
            btn.BackgroundColor = Color.Transparent;

        }
        private int opt = 0;

        private int bra = 0;
        private void buttons_clicked(object sender, EventArgs args)
        {
            Button btn = (Button)sender;
            int i = 0;
            foreach(var bt in all_btns)
            {
                if(bt.Id == btn.Id) {break;}
                i++;
            }
            
            string btn_text = all_btns_text[i];
            if(btn_text=="Clear" || btn_text=="Bracket" || btn_text == "Equal")
            {
                if (btn_text == "Clear")
                {
                    current_opt = "";
                    opt = 0;
                    no_char = 0;
                }else if (btn_text == "Bracket")
                {
                    if (no_char < 21)
                    {
                        if (bra == 0)
                        {
                            current_opt += "(";
                            bra = 1;
                            opt = 0;
                            no_char += 1;
                        }
                        else
                        {
                            if (opt == 0)
                            {
                                char l = current_opt[current_opt.Length - 1];
                                if (l == '+' || l == '-' || l == '÷' || l == '×' || l == '(')
                                {
                                    opt = 1;
                                }
                                else
                                {
                                    current_opt += ")";
                                    bra = 0;
                                    opt = 0;
                                    no_char += 1;
                                }
                            }
                        }
                    }
                }
                else
                {
                    current_opt = equal(current_opt);
                    current_opt_below = "";
                    opt = 0;
                }
            }
            else
            {
                if (no_char < 21)
                {
                    if (btn_text == "+" || btn_text == "-" || btn_text == "÷" || btn_text == "×")
                    {
                        if (opt == 0)
                        {
                            if (btn_text == "÷" || btn_text == "×")
                            {
                                if (no_char > 0)
                                {
                                    char l = current_opt[current_opt.Length - 1];
                                    if (l != '(')
                                    {
                                        current_opt += btn_text;
                                        opt = 1;
                                        no_char += 1;
                                    }
                                }
                            }
                            else
                            {
                                current_opt += btn_text;
                                opt = 1;
                                no_char += 1;
                            }
                        }
                    }
                    else
                    {
                        current_opt += btn_text;
                        opt = 0;
                        no_char += 1;
                    }
                }
            }
            if (btn_text != "Equal") 
            {
                current_opt_below = equal(current_opt);
            }
            Main_number_add.Text = current_opt + " ";
            second_number_add.Text = current_opt_below + "  ";
        }
    }
}
