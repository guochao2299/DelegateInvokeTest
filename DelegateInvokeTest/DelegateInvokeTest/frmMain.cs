using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DelegateInvokeTest
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }
               
        private void btnCalc_Click(object sender, EventArgs e)
        {
            int opt1st = 0;
            int opt2st = 0;

            if (string.IsNullOrEmpty(txtNo1.Text) || !Int32.TryParse(txtNo1.Text, out opt1st))
            {
                return;
            }

            if (string.IsNullOrEmpty(txtNo2.Text) || !Int32.TryParse(txtNo2.Text, out opt2st))
            {
                return;
            }

            Func<int, int, float> operation = GetMathDelegate(comboOpt.SelectedIndex);
            txtResult.Text = Convert.ToString(operation(opt1st, opt2st));
            //txtResult.Text = Convert.ToString(DoTheOperation(opt1st, opt2st, comboOpt.SelectedIndex));
        }

        private Func<int, int, float> GetMathDelegate(int optType)
        {
            Func<int, int, float> result = null;

            switch (optType)
            {
                case 0:
                    result= MathCalcLib.MathLib.Add;
                    break;

                case 1:

                    if (MathCalcLib.MathLib.IsSupportSubOpt)
                    {
                        System.Reflection.MethodInfo mInfo = typeof(MathCalcLib.MathLib).GetMethod("Sub", System.Reflection.BindingFlags.Public| System.Reflection.BindingFlags.Static);
                        if (mInfo == null)
                        {
                            MessageBox.Show("MathCalcLib.MathLib类中不包含静态函数Sub");
                        }

                        Delegate d= Delegate.CreateDelegate(typeof(Func<int, int, float>), mInfo);
                        result = d as Func<int, int, float>;
                        break;
                    }
                    else
                    {
                        result = delegate (int a, int b)
                          {
                              return 0;
                          };
                    }
                    break;

                case 2:
                    result = MathCalcLib.MathLib.Multy;
                    break;

                case 3:
                    result= MathCalcLib.MathLib.Div;
                    break;
            }

            return result;                
        }

        private float DoTheOperation(int a,int b,int optType)
        {
            switch(optType)
            {
                case 0:
                    return MathCalcLib.MathLib.Add(a , b);

                case 1:
                    //if (MathCalcLib.MathLib.IsSupportSubOpt)
                    //{
                    //    return MathCalcLib.MathLib.Sub(a, b);
                    //}

                    if (MathCalcLib.MathLib.IsSupportSubOpt)
                    {
                        System.Reflection.MethodInfo mInfo = typeof(MathCalcLib.MathLib).GetMethod("Sub", System.Reflection.BindingFlags.Static);
                        if(mInfo==null)
                        {
                            MessageBox.Show("MathCalcLib.MathLib类中不包含静态函数Sub");
                        }

                        return (float)mInfo.Invoke(null, new object[] { a, b });
                    }
                    break;

                case 2:
                    return MathCalcLib.MathLib.Multy(a, b);

                case 3:
                    return MathCalcLib.MathLib.Div(a, b);
            }

            throw new Exception("未识别的操作符");
        }
    }
}
