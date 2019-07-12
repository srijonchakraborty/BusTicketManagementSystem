using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Bus_Management_System
{
    static class Program
    {
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
          LOGIN logon = new LOGIN();
           
            splash sp = new splash();
           
            if (sp.done)
            {
                

                Application.Run(logon);

            }  

            

            if (logon.c )
            {
                if (logon.c && logon.b)
                {
                    string nid = logon.id;

                    Application.Run(new Admin(nid));
                }
                else {
                    string nid = logon.id;
                    Application.Run(new ticket_purchase(nid));
                }
                
            }

            
            //Application.Run(new LOGIN());
           // Application.Run(new ticket_purchase());
          // Application.Run(new Admin("Admin1"));
        }
    }
}
