using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Bus_Management_System
{
    public partial class splash : Form
    {
        public bool done = false;

        public splash()
        {
            InitializeComponent();
            delay();
        }

        public void delay()
        {
            this.Show();
            Thread.Sleep(4000);
            done = true;
            this.Close();
            
        }

        private void splash_Load(object sender, EventArgs e)
        {

        }
    

    
    
    
    }
}
