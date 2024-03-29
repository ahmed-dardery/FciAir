﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI
{
    public partial class CustomerPage : Form
    {

        private int CustomerID = 0;

        string CustomerTicketsCond ;
        string CustomerFlightCols = "FlightID,Source,Destination,DepartTime,ArriveTime";
        string CustomerFlightTicketsCols = "TicketID,Tickets.FlightID,Source,Destination,DepartTime,ArriveTime,Price,AgeGroup,Class,BookDate";
        int FlightID,nAdult=1,nChild,nInfant,totalPrice,seats,totalN,maxSeat;

        const int AdultPrice = 1000, ChildPrice = 500, InfantPrice = 350;

        public CustomerPage(int id)
        {
            InitializeComponent();
            CustomerID = id;
            CustomerTicketsCond = "CustomerID = " + CustomerID;
            this.Icon = UI.Properties.Resources.myico;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchBy = cboSearch.Text;
            string req = txtSearchBar.Text;

            try
            {
                if (cboSearch.Text.Equals("DepartTime") || cboSearch.Text.Equals("ArriveTime"))
                    Logic.LoadListData(Program.dbms.SearchByTime("Flights", searchBy, dtpFrom.Value, dtpTo.Value, CustomerFlightCols), listFlights);
                else if (req != "")
                    Logic.LoadListData(Program.dbms.SearchBy("Flights", searchBy, req, CustomerFlightCols), listFlights);
            }
            catch (SqlException ex)
            {
                var reason = DBMS.ParseException(ex);
                if (reason == DBMS.SqlErrorNumber.NotInteger)
                {
                    MessageBox.Show(Logic.ErrorMessages.IntegerOnly, "Search", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void CustomerPage_Load(object sender, EventArgs e)
        {
            cboClass.SelectedIndex = 0;

            List<string> cols;

            cols = Program.dbms.GetTableColumns("Flights",CustomerFlightCols);
            Logic.LoadListColumns(cols, listFlights);
            Logic.LoadListColumns(cols, cboSearch);

            Logic.LoadListData(Program.dbms.GetTableData("Flights","1=1" ,CustomerFlightCols), listFlights);

            const string flightJOINticket = "Tickets JOIN Flights ON Tickets.FlightID = Flights.FlightID";

            cols = Program.dbms.GetTableColumns(flightJOINticket, CustomerFlightTicketsCols);
            Logic.LoadListColumns(cols, listVMyFlights);

            Logic.LoadListData(Program.dbms.GetTableData(flightJOINticket, CustomerTicketsCond, CustomerFlightTicketsCols), listVMyFlights);


            lblCustomerID.Text = CustomerID.ToString();
            List<object> userData = Program.dbms.GetTableData("Customers", $"CustomerID = { CustomerID }")[0];
            lbluserCustomer.Text = userData[6].ToString();

            txtMyFirstName.Text = userData[1].ToString();
            txtMyLastName.Text = userData[2].ToString();
            txtMyPassport.Text = userData[3].ToString();
            txtMyNationality.Text = userData[4].ToString();
            dtpMyBirthdate.Value = (DateTime)userData[5];
            txtMyUsername.Text = userData[6].ToString();

            GroupValue_Changed(null, null);
        }

        private void txtSearchBar_TextChanged(object sender, EventArgs e)
        {
                if (txtSearchBar.Text == "") Logic.LoadListData(Program.dbms.GetTableData("Flights", "1=1", CustomerFlightCols), listFlights);

        }

        private void listVFlights_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFlights.SelectedItems.Count > 0)
            {
                var selected = listFlights.SelectedItems[0].SubItems;
                FlightID = int.Parse(selected[0].Text);
                seats = Program.dbms.GetFlightReservedSeats(FlightID);
                maxSeat = Program.dbms.GetFlightMaxSeats(FlightID);
            }
        }

        private void btnSaveInfo_Click(object sender, EventArgs e)
        {
            bool correct = Program.dbms.CheckPasswordCustomer(CustomerID, txtMyOldPassword.Text);
            if (!correct)
            {
                MessageBox.Show("You have entered your previous password incorrectly", "Incorrect Password", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                string pass = txtMyNewPassword.Text == "" ? txtMyOldPassword.Text : txtMyNewPassword.Text;
                try
                {
                    Program.dbms.UpdateCustomer(CustomerID, txtMyPassport.Text, txtMyFirstName.Text, txtMyLastName.Text, txtMyUsername.Text, pass,txtMyNationality.Text,dtpMyBirthdate.Value);
                    lbluserCustomer.Text = txtMyUsername.Text;

                }
                catch (SqlException ex)
                {
                    var reason = DBMS.ParseException(ex);
                    switch (reason)
                    {
                        case DBMS.SqlErrorNumber.Duplication:
                            MessageBox.Show(Logic.ErrorMessages.DuplicateUsername, "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            break;
                        case DBMS.SqlErrorNumber.Truncation:
                            MessageBox.Show(Logic.ErrorMessages.Truncation, "Edit Info", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            break;
                        default:
                            MessageBox.Show(ex.Message);
                            break;
                    }
                }
            }
            txtMyOldPassword.Clear();
            txtMyNewPassword.Clear();
        }

        private void TextMyInfos_TextChanged(object sender, EventArgs e)
        {
            if (txtMyFirstName.Text=="" || txtMyLastName.Text=="" || txtMyPassport.Text == "" || txtMyNationality.Text == "" || txtMyUsername.Text == "" || txtMyOldPassword.Text=="" || dtpMyBirthdate.Value > DateTime.Now)
            {
                btnSaveInfo.Enabled = false;
            }
            else
            {
                btnSaveInfo.Enabled = true;
            }
        }

        private void CustomerPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            Program.homePage.Show();
        }

        private void btnBackAdmin_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btnDeleteAcc_Click(object sender, EventArgs e)
        {
            int[] ID;
            DialogResult result = MessageBox.Show("Do you want to delete the account?", "Delete Account", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.Yes)
            {
                result = MessageBox.Show("Are you sure?", "Confirm", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    ID = new int[1]; ID[0] = CustomerID;
                    Program.dbms.DeleteCustomer(ID);
                    this.Close();
                }
            }
        }

        private void cboSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtSearchBar.Text = dtpFrom.Text = dtpTo.Text = "";
            if (cboSearch.Text.Equals("DepartTime") || cboSearch.Text.Equals("ArriveTime"))
            {
                lblFrom.Visible = lblTo.Visible = true;
                dtpFrom.Visible = dtpTo.Visible = true;
                txtSearchBar.Visible = false;
            }
            else
            {
                lblFrom.Visible = lblTo.Visible = false;
                dtpFrom.Visible = dtpTo.Visible = false;
                txtSearchBar.Visible = true;
            }
            Logic.LoadListData(Program.dbms.GetTableData("Flights", "1=1", CustomerFlightCols), listFlights);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            this.CustomerPage_Load(null, null);

        }
        
        public void GroupValue_Changed(object sender,EventArgs e)
        {
            nAdult = (int)adultNumber.Value;
            nChild = (int)childNumber.Value;
            nInfant = (int)infantNumber.Value;
            totalPrice = nAdult * AdultPrice + nChild * ChildPrice + nInfant * InfantPrice;
            Price.Text = totalPrice.ToString();
            totalN = nAdult + nChild + nInfant;
        }
        private void BtnBookFlight_Click(object sender, EventArgs e)
        {
            if (listFlights.SelectedItems.Count == 0)
                MessageBox.Show("You must select a flight","Flight not selected",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);

            else if (nAdult == 0)
                MessageBox.Show("There must be one adult at least", "Age Group Error", MessageBoxButtons.OK, MessageBoxIcon.Information);

            else if (cboClass.SelectedIndex < 0)
                MessageBox.Show("Please select the class","Class Error",MessageBoxButtons.OK,MessageBoxIcon.Warning);

            else if (seats + totalN > maxSeat)
                MessageBox.Show($"This flight does not have enough available seats for your resevation! Sorry about this", "Maximum size", MessageBoxButtons.OK, MessageBoxIcon.Warning);


            else
            {
                //Get the name of the class with out the 'class' identifier.
                string cbo = cboClass.Text.Substring(0, cboClass.Text.IndexOf(" "));

                for (int i = 0; i < nAdult; ++i)
                    Program.dbms.InsertTicket(FlightID, AdultPrice, "Adult", CustomerID, cbo, DateTime.Now);
                for (int i = 0; i < nChild; ++i)
                    Program.dbms.InsertTicket(FlightID, ChildPrice, "child", CustomerID, cbo, DateTime.Now);
                for (int i = 0; i < nInfant; ++i)
                    Program.dbms.InsertTicket(FlightID, InfantPrice, "infant", CustomerID, cbo, DateTime.Now);

                MessageBox.Show($"Regestration is successful!","Done", MessageBoxButtons.OK, MessageBoxIcon.Information);

                adultNumber.Value = 1;
                childNumber.Value = 0;
                infantNumber.Value = 0;
                GroupValue_Changed(null, null);
            }

        }
    }
}
