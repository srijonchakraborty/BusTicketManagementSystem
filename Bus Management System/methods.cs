using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace Bus_Management_System
{
    class methods
    {

      
        public static SqlConnection sc = new SqlConnection("Data Source=SAGAR-PC;Initial Catalog=Bus Management System;Integrated Security=True");
        public SqlCommand cmd;
        public bool flag_of_method = false;
        public int counter = 0;
        public string[] seat =
	                    {
	                        "A1",
	                        "A2",
	                        "A3",
	                        "A4",
                            "B1",
	                        "B2",
	                        "B3",
	                        "B4",

                            "C1",
	                        "C2",
	                        "C3",
	                        "C4",
                            "D1",
	                        "D2",
	                        "D3",
	                        "D4",

                            "E1",
	                        "E2",
	                        "E3",
	                        "E4",
                            "F1",
	                        "F2",
	                        "F3",
	                        "F4",

                            "G1",
	                        "G2",
	                        "G3",
	                        "G4",
                            "H1",
	                        "H2",
	                        "H3",
	                        "H4",
                            
                            "I1",
	                        "I2",
	                        "I3",
	                        "I4",
                            "J1",
	                        "J2",
	                        "J3",
	                        "J4"
	                    };
           

        public void show_table_element(string Tab_and_Last,string que, DataGridView d)
        {

           
            SqlDataReader reader;
            try
            {
                DataTable table = new DataTable(); //for data table we have to include namespace System.Data
                cmd = new SqlCommand(que+" "+" " + Tab_and_Last, sc);
                sc.Open();
                reader = cmd.ExecuteReader();
                table.Load(reader);
                d.DataSource = table;
                reader.Close();
                sc.Close();
                d.ReadOnly = true;
               // MessageBox.Show("Search Complete");
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        public void combobox_loader(ComboBox cb,string sql,string member)
        {
            string pc=null ;
            string[] tqr=new string[50];
            int ic=0;
            cmd = new SqlCommand(""+sql,sc); 
            SqlDataAdapter objDA = new SqlDataAdapter(cmd);
            objDA.SelectCommand.CommandText = cmd.CommandText.ToString();
            DataTable table = new DataTable();
            objDA.Fill(table);
            cb.DataSource = table;
            cb.DisplayMember = member;
            /*
             * 
             * console printing*/
            foreach (DataRow row in table.Rows)
            {
                 pc= (row[member].ToString());
                 //Console.WriteLine(pc);  
                    //send files
                 tqr[ic] = pc;
                 ic++;
                
            }
            ic--;
            for (int i = ic; i >= 0; i--)
            {
                Console.WriteLine("LAST:"+tqr[i]);  
            }
        }

        public bool mylogin(string sql,bool b)
        {
            DataSet DS = new DataSet();

            cmd = new SqlCommand("" + sql, sc);
            SqlDataAdapter objDA = new SqlDataAdapter(cmd);
            objDA.SelectCommand.CommandText = cmd.CommandText.ToString();
            DS.Tables.Clear();
            objDA.Fill(DS);
            if(DS.Tables[0].Rows.Count> 0)
            { return b = true; }

            return b;
        }
        public void single_tab_data_Insert(string sql)
        {
            sc.Open();
            try
            {
                cmd = new SqlCommand("" + sql, sc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex+"Illegal data insert attempt.");
            }
            sc.Close();
        }

        public string id_sender(string sql,string member)
        {
            string pc = null;
            flag_of_method = false;
            string[] tqr = new string[50];
            int ic = 0;
            cmd = new SqlCommand("" + sql, sc);
            SqlDataAdapter objDA = new SqlDataAdapter(cmd);
            objDA.SelectCommand.CommandText = cmd.CommandText.ToString();
            DataTable table = new DataTable();
           objDA.Fill(table);

           if (table.Rows.Count > 0)
           {
               flag_of_method = true;
           }
            /*
             * 
             * console printing*/
            foreach (DataRow row in table.Rows)
            {
                pc = (row[member].ToString());
                //Console.WriteLine(pc);  
                //send files
                tqr[ic] = pc;
                ic++;
               
            }
            ic--;
            for (int i = ic; i >= 0; i--)
            {
                Console.WriteLine("mytest:" + tqr[i]);
            }
            return pc;
        }
        public string[] sendarray(string sql,string member)
        {
            
            string pc = null;
            string[] tqr = new string[50];
           
            cmd = new SqlCommand("" + sql, sc);
            SqlDataAdapter objDA = new SqlDataAdapter(cmd);
            objDA.SelectCommand.CommandText = cmd.CommandText.ToString();
            DataTable table = new DataTable();
            objDA.Fill(table);
            /*
             * 
             * console printing*/
            foreach (DataRow row in table.Rows)
            {
                pc = (row[member].ToString());
                //Console.WriteLine(pc);  
                //send files
                tqr[counter] = pc;
                counter++;

            }
            counter--;
            Console.WriteLine("coun:" + counter);
           /* for (int i = ic; i >= 0; i--)
            {
                Console.WriteLine("LAST:" + tqr[i]);
            }*/
           
                return tqr;
        }
        public void delete_row(string sql)
        {
            sc.Open();
            try
            {
                cmd = new SqlCommand("" + sql, sc);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex + "Illegal data delete attempt.");
            }
            sc.Close();
        }
    }
}
