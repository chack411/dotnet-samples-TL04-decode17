using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChackLib;

namespace WinFormOCR
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
                return;

            Uri image = new Uri(textBox1.Text);

            // Setup for OcrLib
            // 1) Go to https://www.microsoft.com/cognitive-services/en-us/computer-vision-api 
            //    Sign up for computer vision api
            // 2) Add environment variable "Vision_API_Subscription_Key" and set Computer vision key as value
            //    e.g. Vision_API_Subscription_Key=123456789abcdefghijklmnopqrstuvw

            Task<string> task = Task.Run(() => OcrLib.DoOcr(image));
            task.Wait();

            textBox2.Text = task.Result;
        }
    }
}
