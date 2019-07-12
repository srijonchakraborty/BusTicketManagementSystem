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
    public partial class LOGIN : Form
    {
        public bool b=false,c=false;
        
        public string id = null; 
        public string u_type = null;
        public string pass = null;

        public LOGIN()
        {
               InitializeComponent();
               usertype_combo_loader();
        }

        public void usertype_combo_loader()
        {
            methods m = new methods();
            string sql = "SELECT DISTINCT Ticket_seller.[Seller Type] FROM Ticket_seller;";
            string member = "Seller Type";
            m.combobox_loader(combo_user_type, sql, member);
        
        }

        private void btn_login_Click(object sender, EventArgs e)
        {


            id = txt_box_userid.Text;
            u_type = combo_user_type.Text;
            pass = txt_box_password.Text;
            string sql = "select * from Ticket_seller where " 
                        +"Ticket_seller.[Seller Type]='"+u_type+"' and Ticket_seller.Ts_Password='"+pass+"'" 
                        +"and Ticket_seller.[User ID]='"+id+"';";

            methods m = new methods();
            c=m.mylogin(sql,c);
            if (c)
            {
                if (u_type == "Admin")
                {
                    b = true;
                    
                    this.Close();
                }
                else {
                    this.Close();
                }
            }
            else
            {
                MessageBox.Show("Login Failed");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string s = null;
           
            Console.WriteLine(s);
        }

      

       
       

       
       

      

       

    }
}
