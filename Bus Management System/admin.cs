using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace Bus_Management_System
{
    public partial class Admin : Form
    {
        
        public string Aid=null;
        private Int32 checkCurrentSelectedIndex=-1;
        
        //SqlConnection sc = new SqlConnection("Data Source=SAGAR-PC;Initial Catalog=Bus Management System;Integrated Security=True");
      //  SqlCommand cmd;
       // SqlDataReader reader;
        public Admin(string id)
        {
            InitializeComponent();
            Aid = id;
            Name_of_user.Text = "User :"+id;
            T_id_label.Hide();
            route_and_bustime_combo_loader();
           // load_new_ticket_ID();
            dateTimePicker_ticket.MinDate = DateTime.Now.Date;
            dateTimePicker_ticket1.MinDate = DateTime.Now.Date;
            d1.MinDate = DateTime.Now.Date;
        }
        
      

       
        
        private void btn_src_Bus_Click(object sender, EventArgs e)
        {
           
            string search_query = "Select Bus.Bus_number As [Bus Number],Employee.[First Name] as "
                                   +"[Current Driver Name],Seat_plan.[Number of Seat],Garage.[Garage Name],Garage.Place as "
                                   +"[Garage Place],Garage.Contact as [Garage Contact] from ";
            string TableName = "Bus,Employee,Bus_Driver,Seat_plan,Garage where "
                             + "Bus.G_id=Garage.G_id and Bus.Sp_id=Seat_plan.Sp_id "
                              + "and Bus.BD_id= Bus_Driver.BD_id and Bus_Driver.E_id=Employee.E_id ";
            methods m = new methods();
            m.show_table_element(TableName, search_query, BusDataGrid);
        }

        private void btn_src_driver_Click(object sender, EventArgs e)
        {

            //string search_query = "Select Employee.[First Name] from";
            //string TableName = "Employee,Bus_Driver where Employee.E_id= Bus_Driver.E_id and Bus_Driver.Salary=1200;";
            string search_query = "Select Employee.[First Name] ,Employee.[Last Name],"
            + "Employee.Contact,Employee.E_Address as Location ,Bus_Driver.Salary from ";
            string TableName = "Employee,Bus_Driver where Employee.E_id= Bus_Driver.E_id;";
            methods m = new methods();
            m.show_table_element(TableName, search_query, DriverGrid);
        }

        // 
        public void route_and_bustime_combo_loader()
        {
            methods m = new methods();
            string sql = "Select Distinct Road.[Route Name] from Road";
            string member ="Route Name";
            m.combobox_loader(combo_route, sql, member);
            m.combobox_loader(combo_route1, sql, member);
            sql = "Select Distinct Shedule.S_Time from Shedule;";
            member = "S_Time";
            m.combobox_loader(combo_bustime, sql, member);
            m.combobox_loader(combo_bustime1, sql, member);
        }


        private void btn_add_garage_Click(object sender, EventArgs e)
        {
            string sql = null;
            
            

                sql = "Insert into Garage ([Garage Name],Contact,Place) values "
                    + "('" + txtBox_garage_name.Text + "','" + txtbox_contact.Text + "','" + txtBox_Place.Text + "');";
                methods m = new methods();
                m.single_tab_data_Insert(sql);

                string search_query = "Select * from ";
                string TableName = "Garage order by Garage.G_id DESC;";

                m.show_table_element(TableName, search_query, dataGrid_Garage);
                //Select Garage.[Garage Name],Garage.Place,Garage.Contact from Garage order by Garage.G_id DESC;
            
        }



        private void btn_show_garage_Click(object sender, EventArgs e)
        {
            string search_query = "Select * from ";
            string TableName = "Garage ;";
            methods m = new methods();
            m.show_table_element(TableName, search_query, dataGrid_Garage);
        }

        private void checkedListBox_admin_Seat_SelectedIndexChanged(object sender, EventArgs e)
        {
           // Console.WriteLine(checkedListBox_admin_Seat.CheckedItems.Count);
            if (checkedListBox_admin_Seat.CheckedItems.Count > 0)
            {
                //Int32 checkedItemIndex = checkedListBox1.CheckedIndices[0];
                checkedListBox_admin_Seat.SetItemChecked(checkCurrentSelectedIndex, false);
            }

            checkCurrentSelectedIndex = checkedListBox_admin_Seat.SelectedIndex;
        }

        private void btn_checkList_seat_Click(object sender, EventArgs e)
        {

            string sql = null, member = null, S_id = null, sql1 = null;
            methods m = new methods();
            //bool flag = false;
            checkedListBox_admin_Seat.Items.Clear();
            
            sql = "select [Bus Management System].dbo.Shedule.S_id from [Bus Management System].dbo.Shedule where"
                        + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime.Text + "' and"
                        + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                            + "where Road.[Route Name]='" + combo_route.Text + "')";
            member = "S_id";
             S_id = m.id_sender(sql, member);
             if (S_id == null)
             {
                 MessageBox.Show("Wrong Schedule");
                 
             }
            Tag:
            sql = "(select BS_id from [Bus Management System].dbo.Bus_Shedule_manage where " 
                  +"[Bus Management System].dbo.Bus_Shedule_manage.Date='"+dateTimePicker_ticket.Text+"' and "
                  +"[Bus Management System].dbo.Bus_Shedule_manage.S_id="
                  +"(select S_id from [Bus Management System].dbo.Shedule where " 
                  +"[Bus Management System].dbo.Shedule.S_Time='"+combo_bustime.Text+"' and "
                  +"[Bus Management System].dbo.Shedule.Ro_id="
                  +"(select Road.Road_id from Road where Road.[Route Name]='"+combo_route.Text+"')))";
            member = "BS_id";
            
            string BS_id= m.id_sender(sql,member);
            if (m.flag_of_method)
            {
                string[] arr;
            
                Console.WriteLine("HMM:" + BS_id);
                sql= "select  Ticket.[Name Of Seat] from Ticket where Ticket.BS_id='"+BS_id+"'";
                member = "Name Of Seat";
                arr=m.sendarray(sql,member);
                arr.Reverse();
                for (int i = m.counter; i >= 0; i--)
                {
                    Console.WriteLine(""+arr[i]);
                }
                for (int j = 0; j < m.seat.Length;j++)
                {
                    bool bo = false;
                    for (int i = 0; i <= m.counter; i++)
                    {
                        if (arr[i] != m.seat[j])
                        {
                            bo = true;

                        }
                        if (arr[i] == m.seat[j])
                        {
                            bo = false;
                            break;
                        }
                    }
                    if(bo)
                    checkedListBox_admin_Seat.Items.Add(m.seat[j]);
                    
                }
               if (m.counter == -1)
                {
                    for (int j = 0; j < m.seat.Length; j++)
                    {
                        checkedListBox_admin_Seat.Items.Add(m.seat[j]);
                    }
                }
               
            }
            else if(!m.flag_of_method)
            {
                bool insert_flag = false;
               sql = "select [Bus Management System].dbo.Shedule.S_id from [Bus Management System].dbo.Shedule where"
                        + "[Bus Management System].dbo.Shedule.S_Time='"+combo_bustime.Text+"' and"
                        + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                            + "where Road.[Route Name]='"+combo_route.Text+"')";
                member = "S_id";
                //string tes_id = null;
                S_id= m.id_sender(sql, member);
                if (S_id == null)
                {
                    //MessageBox.Show("Wrong Schedule");
                    insert_flag = true;
                }
                if (!insert_flag)
                {
                    sql = "insert into Bus_Shedule_manage (S_id,Date) values ('"+S_id+"','"+dateTimePicker_ticket.Text+"')";
                    m.single_tab_data_Insert(sql);
                    goto Tag;                   
                }

                Console.WriteLine("S_id:" + S_id);
                Console.WriteLine("\n"+dateTimePicker_ticket.Text);
                
                 
               // m.single_tab_data_Insert(sql);

                
            }
          /*  DateTime da = System.DateTime.Now;
            DateTime da1 = Convert.ToDateTime(dateTimePicker_ticket.Text);
            int i = DateTime.Compare(da, da1);
            MessageBox.Show(i.ToString());*/
        }

        private void load_new_ticket_ID()
        {
            string sql = null,member=null;
            sql = "select TOP 1 T_id from Ticket  order by T_id DESC ;";
            member = "T_id";
            methods m = new methods();
            string T_id=m.id_sender(sql,member);
            T_id_label.Text = T_id;
            T_id_label.Show();
            Console.WriteLine("T_id" + T_id);
            
        }
        private void btn_refresh_Click(object sender, EventArgs e)
        {
            //string v=checkedListBox_admin_Seat.SelectedItem.ToString();
            //Console.WriteLine("VALUE::" + v);
            T_id_label.Hide();
            textBox_contact_pass.Clear();
            
            txtbox_name_pass.Clear();
            textBox_total_fare.Clear();
            checkedListBox_admin_Seat.Items.Clear();
            /* string sql = "select [Bus Management System].dbo.Shedule.S_Time from [Bus Management System].dbo.Shedule where"
                         + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime.Text + "' and"
                         + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                             + "where Road.[Route Name]='" + combo_route.Text + "')";
            string member = "S_Time";
            methods m = new methods();
            member =m.id_sender(sql,member);
            Console.WriteLine("date"+member);*/
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            int cou ;
            
            bool bo = true;
            string sql = null,member=null,P_id=null,Ts_id=null,BS_id=null,S_id=null,name_of_seat=null;
            methods m = new methods();
            sql = "select [Bus Management System].dbo.Shedule.S_id from [Bus Management System].dbo.Shedule where"
                        + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime.Text + "' and"
                        + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                            + "where Road.[Route Name]='" + combo_route.Text + "')";
            member = "S_id";
            S_id = m.id_sender(sql, member);
            cou = checkedListBox_admin_Seat.SelectedItems.Count;
            if (cou == 0)
            {
                MessageBox.Show("No Seat Selected");
                bo = false;
            }
            if (S_id == null)
            {
                MessageBox.Show("Wrong Schedule");
                bo = false;
            }
            if (combo_Gender.Text == "------- Please Select -----")
            {
                MessageBox.Show("Please Select a Gender");
                 bo = false;
            }
            if (bo)
            {
                sql = "insert into Passenger (Name,Gender,Contact) values ('"+txtbox_name_pass.Text+"','"+combo_Gender.Text+"','"+textBox_contact_pass.Text+"')";
                m.single_tab_data_Insert(sql);
                sql = "select TOP 1 P_id from Passenger  order by P_id DESC ;";
                member = "P_id";
                P_id = m.id_sender(sql, member);
               // Console.WriteLine("PID::" + P_id);
                sql="select Ts_id from Ticket_seller where Ticket_seller.[User ID]='"+Aid+"'" ;
                member = "Ts_id";
                Ts_id = m.id_sender(sql,member);
                
                sql = "(select BS_id from [Bus Management System].dbo.Bus_Shedule_manage where "
                      + "[Bus Management System].dbo.Bus_Shedule_manage.Date='" + dateTimePicker_ticket.Text + "' and "
                      + "[Bus Management System].dbo.Bus_Shedule_manage.S_id="
                      + "(select S_id from [Bus Management System].dbo.Shedule where "
                      + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime.Text + "' and "
                      + "[Bus Management System].dbo.Shedule.Ro_id="
                      + "(select Road.Road_id from Road where Road.[Route Name]='" + combo_route.Text + "')))";
                member = "BS_id";
                BS_id = m.id_sender(sql, member);
                name_of_seat = checkedListBox_admin_Seat.SelectedItem.ToString();
                sql = "insert into Ticket (Ticket.BS_id,[Name Of Seat],[No of Seat],Ticket.P_id,[Total Fare],Ticket.Ts_id) "
                      +"values ('"+BS_id+"','"+name_of_seat+"','1','"+P_id+"','"+textBox_total_fare.Text+"','"+Ts_id+"')";
                m.single_tab_data_Insert(sql);
                sql = "select Ticket.T_id,Employee.[First Name] as Seller,Passenger.Name as [Passenger Name]"
                      + ",Passenger.Contact as [Passenger Contact],Passenger.Gender as [Passenger Gender] ,Ticket.[Name Of Seat]"
                      + ",Bus_Shedule_manage.Date as [Departure Date] ,Shedule.S_Time as [Schedule Time],Road.[Route Name] from ";
                string sql2= "Ticket,Passenger,Bus_Shedule_manage,Shedule,Road,Ticket_seller,Employee "
                      + "where Ticket.P_id=Passenger.P_id and Ticket.Ts_id=Ticket_seller.Ts_id and "
                      + "Ticket_seller.E_id=Employee.E_id and Ticket.BS_id=Bus_Shedule_manage.BS_id and "
                      + "Bus_Shedule_manage.S_id=Shedule.S_id and Shedule.Ro_id=Road.Road_id order by T_id DESc";
                m.show_table_element(sql2, sql, dataGrid_admin_Ticket);
                load_new_ticket_ID();
                MessageBox.Show("Ticket Purchased Successfully");
                
                 //Console.WriteLine("cou::" + cou);
            }
        }

        private void Search_all_ticket_Click(object sender, EventArgs e)
        {
            string sql = null;
            methods m = new methods();
            sql = "select Ticket.T_id,Employee.[First Name] as Seller,Passenger.Name as [Passenger Name]"
                     + ",Passenger.Contact as [Passenger Contact],Passenger.Gender as [Passenger Gender] ,Ticket.[Name Of Seat]"
                     + ",Bus_Shedule_manage.Date as [Departure Date] ,Shedule.S_Time as [Schedule Time],Road.[Route Name] from ";
            string sql2 = "Ticket,Passenger,Bus_Shedule_manage,Shedule,Road,Ticket_seller,Employee "
                  + "where Ticket.P_id=Passenger.P_id and Ticket.Ts_id=Ticket_seller.Ts_id and "
                  + "Ticket_seller.E_id=Employee.E_id and Ticket.BS_id=Bus_Shedule_manage.BS_id and "
                  + "Bus_Shedule_manage.S_id=Shedule.S_id and Shedule.Ro_id=Road.Road_id order by T_id DESc";
            m.show_table_element(sql2, sql, dataGrid_admin_Ticket);
        }


        private void btn_scr_cencle_Click(object sender, EventArgs e)
        {
            textBox_date_cancle.Clear();
            textBox_contact_cancle.Clear();
            textBox_gender_Cancle.Clear();
            textBox_passenger_canle.Clear();
            textBox_route_cancle.Clear();
            textBox_seat_name_cencle.Clear();
            textBox_time_cancle.Clear();
            textBox_totalfare_cancle.Clear();
            
            string sql = null, member = null, test_1 = null,P_id=null;
            methods m = new methods();
            bool bo = false;
            sql = "select Bus_Shedule_manage.Date from Ticket,Bus_Shedule_manage where "
            + "Ticket.BS_id=Bus_Shedule_manage.BS_id and Ticket.T_id='" + textBox_ticket_no.Text + "'";
            member = "Date";
            test_1 = m.id_sender(sql, member);
           // MessageBox.Show(test_1);
            if (test_1 == null)
            {
                MessageBox.Show("Wrong Ticket No.");

                bo = true;
            }
            //  {
            if (!bo)
            {
                DateTime da = Convert.ToDateTime(test_1);
                DateTime da1 = Convert.ToDateTime(d1.Text);
                int i = DateTime.Compare(da, da1);
             //   MessageBox.Show(""+i);
                if (i == -1||i==1)
                {
                    MessageBox.Show("This Ticket is not valid for Cancellation.");
                    bo = true;
                }
                else
                {
                    sql = "select Ticket.[Name Of Seat] from Ticket where Ticket.T_id='" + textBox_ticket_no.Text + "'";
                    member = "Name Of Seat";
                    test_1 = m.id_sender(sql, member);
                    textBox_seat_name_cencle.Text=test_1;
                    
                    sql = "select Ticket.[Total Fare] from Ticket where Ticket.T_id='" + textBox_ticket_no.Text + "'";
                    member = "Total Fare";
                    test_1 = m.id_sender(sql, member);
                    textBox_totalfare_cancle.Text = test_1;
                    
                    sql = "select Passenger.Name from Ticket,Passenger where Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.P_id=Passenger.P_id";
                    member = "Name";
                    test_1 = m.id_sender(sql, member);
                    textBox_passenger_canle.Text = test_1;

                    sql = "select Passenger.Gender from Ticket,Passenger where Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.P_id=Passenger.P_id";
                    member = "Gender";
                    test_1 = m.id_sender(sql, member);
                    textBox_gender_Cancle.Text = test_1;

                    sql = "select Passenger.Contact from Ticket,Passenger where Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.P_id=Passenger.P_id";
                    member = "Contact";
                    test_1 = m.id_sender(sql, member);
                    textBox_contact_cancle.Text = test_1;

                    sql = "select Bus_Shedule_manage.Date from Ticket,Bus_Shedule_manage where Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.BS_id=Bus_Shedule_manage.BS_id";
                    member = "Date";
                    test_1 = m.id_sender(sql, member);
                    textBox_date_cancle.Text = test_1;

                    sql = "select Shedule.S_Time from Ticket,Bus_Shedule_manage,Shedule where "
                        + "Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.BS_id=Bus_Shedule_manage.BS_id and Bus_Shedule_manage.S_id=Shedule.S_id";
                    member = "S_Time";
                    test_1 = m.id_sender(sql, member);
                    textBox_time_cancle .Text = test_1;

                    sql = "select Road.[Route Name] from Ticket,Bus_Shedule_manage,Shedule,Road where "
                           +" Ticket.T_id='" + textBox_ticket_no.Text + "' and Ticket.BS_id=Bus_Shedule_manage.BS_id and Bus_Shedule_manage.S_id=Shedule.S_id "
                            +"and Shedule.Ro_id=Road.Road_id";
                    member = "Route Name";
                    test_1 = m.id_sender(sql, member);
                    textBox_route_cancle.Text = test_1;

                   /* //delete part

                    sql = "select Ticket.P_id from Ticket where Ticket.T_id='" + textBox_ticket_no.Text + "' ";
                    member = "P_id";
                    P_id = m.id_sender(sql, member);

                    sql = "DELETE FROM Ticket WHERE Ticket.T_id='" + textBox_ticket_no.Text + "'";
                    m.delete_row(sql);

                    sql = "DELETE FROM Passenger WHERE P_id='" + P_id + "'";
                    m.delete_row(sql);
                    MessageBox.Show("Ticket Cancled");*/
                }
            }
        }
        private void btn_cancel_ticket_Click(object sender, EventArgs e)
        {
            string sql=null,member=null,P_id;
            methods m=new methods();
            sql = "select Ticket.P_id from Ticket where Ticket.T_id='" + textBox_ticket_no.Text + "' ";
                    member = "P_id";
                    P_id = m.id_sender(sql, member);

                    sql = "DELETE FROM Ticket WHERE Ticket.T_id='" + textBox_ticket_no.Text + "'";
                    m.delete_row(sql);

                    sql = "DELETE FROM Passenger WHERE P_id='" + P_id + "'";
                    m.delete_row(sql);
                    MessageBox.Show("Ticket Cancled");
         
        }
        private void btn_src_info_ticket_Click(object sender, EventArgs e)
        {
            bool bo = false;
            string sql = null;
            methods m = new methods(); 
            if (comboBox_search_ticket.Text == "-----Pleade Select------")
            {
                MessageBox.Show("Pleade Select Search By");
                bo = true;
            }
            if (!bo)
            {
                if (comboBox_search_ticket.Text == "All")
                {
                    sql = "select Ticket.T_id,Employee.[First Name] as Seller,Passenger.Name as [Passenger Name]"
                      + ",Passenger.Contact as [Passenger Contact],Passenger.Gender as [Passenger Gender] ,Ticket.[Name Of Seat]"
                      + ",Bus_Shedule_manage.Date as [Departure Date] ,Shedule.S_Time as [Schedule Time],Road.[Route Name] from ";
                    string sql2 = "Ticket,Passenger,Bus_Shedule_manage,Shedule,Road,Ticket_seller,Employee "
                          + "where Ticket.P_id=Passenger.P_id and Ticket.Ts_id=Ticket_seller.Ts_id and "
                          + "Ticket_seller.E_id=Employee.E_id and Ticket.BS_id=Bus_Shedule_manage.BS_id and "
                          + "Bus_Shedule_manage.S_id=Shedule.S_id and Shedule.Ro_id=Road.Road_id order by T_id DESc";
                    m.show_table_element(sql2, sql, dataGridView3);
                }
                if (comboBox_search_ticket.Text == "Ticket ID")
                {
                    sql = "select Ticket.T_id,Employee.[First Name] as Seller,Passenger.Name as [Passenger Name]"
                       + ",Passenger.Contact as [Passenger Contact],Passenger.Gender as [Passenger Gender] ,Ticket.[Name Of Seat]"
                       + ",Bus_Shedule_manage.Date as [Departure Date] ,Shedule.S_Time as [Schedule Time],Road.[Route Name] from ";
                    string sql2 = "Ticket,Passenger,Bus_Shedule_manage,Shedule,Road,Ticket_seller,Employee "
                          + "where Ticket.P_id=Passenger.P_id and Ticket.T_id='"+textBox_src_ticket_info.Text+"' and Ticket.Ts_id=Ticket_seller.Ts_id and "
                          + "Ticket_seller.E_id=Employee.E_id and Ticket.BS_id=Bus_Shedule_manage.BS_id and "
                          + "Bus_Shedule_manage.S_id=Shedule.S_id and Shedule.Ro_id=Road.Road_id order by T_id DESc";
                    m.show_table_element(sql2, sql, dataGridView3);
                    MessageBox.Show("Search Complete");
                }
            }
        }


        private void btn_check_schedule_Click(object sender, EventArgs e)
        {
            string sql = null, member = null, S_id = null, sql1 = null;
            methods m = new methods();
            //bool flag = false;
            listBox_avai_seat.Items.Clear();

            sql = "select [Bus Management System].dbo.Shedule.S_id from [Bus Management System].dbo.Shedule where"
                        + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime1.Text + "' and"
                        + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                            + "where Road.[Route Name]='" + combo_route1.Text + "')";
            member = "S_id";
            S_id = m.id_sender(sql, member);
            if (S_id == null)
            {
                MessageBox.Show("Wrong Schedule");

            }
        Tag:
            sql = "(select BS_id from [Bus Management System].dbo.Bus_Shedule_manage where "
                  + "[Bus Management System].dbo.Bus_Shedule_manage.Date='" + dateTimePicker_ticket1.Text + "' and "
                  + "[Bus Management System].dbo.Bus_Shedule_manage.S_id="
                  + "(select S_id from [Bus Management System].dbo.Shedule where "
                  + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime1.Text + "' and "
                  + "[Bus Management System].dbo.Shedule.Ro_id="
                  + "(select Road.Road_id from Road where Road.[Route Name]='" + combo_route1.Text + "')))";
            member = "BS_id";

            string BS_id = m.id_sender(sql, member);
            if (m.flag_of_method)
            {
                string[] arr;

                Console.WriteLine("HMM:" + BS_id);
                sql = "select  Ticket.[Name Of Seat] from Ticket where Ticket.BS_id='" + BS_id + "'";
                member = "Name Of Seat";
                arr = m.sendarray(sql, member);
                arr.Reverse();
                for (int i = m.counter; i >= 0; i--)
                {
                    Console.WriteLine("" + arr[i]);
                }
                for (int j = 0; j < m.seat.Length; j++)
                {
                    bool bo = false;
                    for (int i = 0; i <= m.counter; i++)
                    {
                        if (arr[i] != m.seat[j])
                        {
                            bo = true;

                        }
                        if (arr[i] == m.seat[j])
                        {
                            bo = false;
                            break;
                        }
                    }
                    if (bo)
                        listBox_avai_seat.Items.Add(m.seat[j]);

                }
                if (m.counter == -1)
                {
                    for (int j = 0; j < m.seat.Length; j++)
                    {
                        listBox_avai_seat.Items.Add(m.seat[j]);
                    }
                }

            }
            else if (!m.flag_of_method)
            {
                bool insert_flag = false;
                sql = "select [Bus Management System].dbo.Shedule.S_id from [Bus Management System].dbo.Shedule where"
                         + "[Bus Management System].dbo.Shedule.S_Time='" + combo_bustime1.Text + "' and"
                         + " [Bus Management System].dbo.Shedule.Ro_id=(select Road.Road_id from Road "
                             + "where Road.[Route Name]='" + combo_route1.Text + "')";
                member = "S_id";
                //string tes_id = null;
                S_id = m.id_sender(sql, member);
                if (S_id == null)
                {
                    //MessageBox.Show("Wrong Schedule");
                    insert_flag = true;
                }
                if (!insert_flag)
                {
                    sql = "insert into Bus_Shedule_manage (S_id,Date) values ('" + S_id + "','" + dateTimePicker_ticket1.Text + "')";
                    m.single_tab_data_Insert(sql);
                    goto Tag;
                }

                //Console.WriteLine("S_id:" + S_id);
                //Console.WriteLine("\n" + dateTimePicker_ticket1.Text);


                // m.single_tab_data_Insert(sql);


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void Name_of_user_Click(object sender, EventArgs e)
        {

        }

       

        private void label66_Click(object sender, EventArgs e)
        {

        }

        private void tabPage11_Click(object sender, EventArgs e)
        {

        }

        private void tabControl2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button8_Click_1(object sender, EventArgs e)
        {

        }

        private void button9_Click(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {

        }

        private void label32_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        

        

       

      

       

       

       

       

      

        

      

       

       

       

       
       
      
    }
}
