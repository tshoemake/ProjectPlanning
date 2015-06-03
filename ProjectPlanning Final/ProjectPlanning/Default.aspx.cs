using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Drawing;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Globalization;
using Business;
using DataAccess;



namespace ProjectPlanning
{
    public partial class Default : Page
    {

        
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings ["connstring"].ToString());

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {
                Bind();
                AddExistingData();
                //calEnd.Visible = false;
            }
        }
            protected void Bind()
        {
            DataTable dt = new DataTable();
            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
            using (var command = new SqlCommand("dbo.sp_calendar", conn) { CommandType = CommandType.StoredProcedure }) 
            {
               conn.Open();
               command.ExecuteNonQuery();
               SqlDataAdapter da = new SqlDataAdapter(command);
               da.Fill(dt);
               Grd.DataSource = dt;
               Grd.DataBind();
               conn.Close();
            }

            Grd.Rows[1].Cells[30].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[1].Cells[31].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[1].Cells[32].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[3].Cells[32].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[5].Cells[32].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[8].Cells[32].Attributes["style"] += "background-color:Gray;";
            Grd.Rows[10].Cells[32].Attributes["style"] += "background-color:Gray;";

        }

            protected void AddExistingData()
            {
                DataTable dt = new DataTable();
                //using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["connstring"].ToString()))
                using (var command = new SqlCommand("dbo.sp_add_existing_data", conn) { CommandType = CommandType.StoredProcedure })
                {
                    conn.Open();
                    command.ExecuteNonQuery();
                    SqlDataAdapter da = new SqlDataAdapter(command);
                    da.Fill(dt);
                    //Grd.DataSource = dt;
                    //Grd.DataBind();

                    foreach (DataRow row in dt.Rows)
                    {
                         string startMon = "";
                         string startDay = "";
                         string startYr = "";
                         string endMon = "";
                         string endDay = "";
                         string endYr = ""; 
                        string  projCode = "";

                        foreach (DataColumn col in dt.Columns)
                        {

                            //save start_date, end_date, and code values for current row
                            if (col.ColumnName == "start_date")
                              {
                                  string startDate = row["start_date"].ToString();

                                  //separate date values in startDate.. startMon, startDay, startYr
                                   startMon = startDate.Substring(5,2);
                                   startDay =startDate.Substring(8, 2);
                                   startYr = startDate.Substring(0,4);
                              }

                            else if (col.ColumnName == "end_date")
                            {
                                string endDate = row["end_date"].ToString();

                                endMon = endDate.Substring(5, 2);
                                endDay = endDate.Substring(8, 2);
                                endYr = endDate.Substring(0, 4);
                            }

                            else if (col.ColumnName == "code")
                            {
                                projCode = row["code"].ToString();
                            }
                        }

                        //startdate use new vals in row index and cell index  =  Grd.Rows[startMon].Cells[startDay].Attributes["style"] += "background: linear-gradient(to top, white 80%, green 20%);";
                        Grd.Rows[Convert.ToInt32(startMon)-1].Cells[Convert.ToInt32(startDay)+1].Attributes["style"] += "background: linear-gradient(to top, white 80%, green 20%);";

                        //enddate use new vals in row index and cell index  =  Grd.Rows[endMon].Cells[endDay].Attributes["style"] += "background: linear-gradient(to top, white 80%, red 20%);";
                        Grd.Rows[Convert.ToInt32(endMon)-1].Cells[Convert.ToInt32(endDay)+1].Attributes["style"] += "background: linear-gradient(to top, white 80%, red 20%);";
                        
                        //use proj code val to insert new val = Grd.Rows[10].Cells[32].Value? = projCode;
                        Grd.Rows[Convert.ToInt32(startMon)-1].Cells[Convert.ToInt32(startDay)+1].Text = projCode;
                        Grd.Rows[Convert.ToInt32(endMon)-1].Cells[Convert.ToInt32(endDay) + 1].Text = projCode;
                       
                    }

                    conn.Close();
                }

            }
          
        protected void Grd_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                

                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    //Highlight rows with mouseover
                    //e.Row.Attributes["onmouseover"] = "onMouseOver('" + (e.Row.RowIndex + 1) + "')";
                    //e.Row.Attributes["onmouseout"] = "onMouseOut('" + (e.Row.RowIndex + 1) + "')";

                    LinkButton _singleClickButton = (LinkButton)e.Row.Cells[0].Controls[0];
                    string _jsSingle = ClientScript.GetPostBackClientHyperlink(_singleClickButton, "");
                    // Add events to each editable cell  
                    for (int columnIndex = 2; columnIndex < e.Row.Cells.Count; columnIndex++)
                    {

                        if (
                            ((columnIndex == 30) && (e.Row.Cells[1].Text.Contains("Feb")))
                               || ((columnIndex == 31) && (e.Row.Cells[1].Text.Contains("Feb")))
                               || ((columnIndex == 32) && (e.Row.Cells[1].Text.Contains("Feb")))
                               || ((columnIndex == 32) && (e.Row.Cells[1].Text.Contains("Apr")))
                               || ((columnIndex == 32) && (e.Row.Cells[1].Text.Contains("Jun")))
                               || ((columnIndex == 32) && (e.Row.Cells[1].Text.Contains("Sep")))
                               || ((columnIndex == 32) && (e.Row.Cells[1].Text.Contains("Nov")))
                            )

                            //e.Row.Cells[e.Row.Cells.Count - 1]
                        { continue;  }
                        else
                            {
                                // Add the column index as the event argument parameter  
                                string js = _jsSingle.Insert(_jsSingle.Length - 2, columnIndex.ToString());
                                // Add this javascript to the onclick Attribute of the cell  
                                e.Row.Cells[columnIndex].Attributes["onclick"] = js;
                                // Add a cursor style to the cells  
                                e.Row.Cells[columnIndex].Attributes["style"] += "cursor:pointer;cursor:hand;";
                            }
                        
                    }
                }
            }

        }

        private int SelectedRowIndex
        {
           get
           {
              if (ViewState["SelectedRowIndex"] == null)
              {
                 return 0;
              }
              else
              {
                 return (int)ViewState["SelectedRowIndex"];
              }
           }
           set
           {
              ViewState["SelectedRowIndex"] = value;
           }
        }

        private int SelectedColumnIndex
        {
            get
            {
                if (ViewState["SelectedColumnIndex"] == null)
                {
                    return 0;
                }
                else
                {
                    return (int)ViewState["SelectedColumnIndex"];
                }
            }
            set
            {
                ViewState["SelectedColumnIndex"] = value;
            }
        }

        protected void Grd_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.ToString() == "ColumnClick")
            {
                //clear values
                txtProjName.Text = "";
                txtProjCode.Text = "";
                txtStartDate.Text = "";
                txtEndDate.Text = "";

                //Change button text to "Update"
                btnUpdate.Text = "Update Existing";

                //Clear out project resources and available resources
                for (int i = lbxProjResources.Items.Count - 1; i >= 0; i--)
                {
                    lbxProjResources.Items.RemoveAt(i);
                }

                for (int i = lbxOpenResources.Items.Count - 1; i >= 0; i--)
                {
                    lbxOpenResources.Items.RemoveAt(i);
                }

                

                //Begin 
                foreach (GridViewRow r in Grd.Rows)
                {
                    if (r.RowType == DataControlRowType.DataRow)
                    {
                        for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                        {
                            //r.Cells[columnIndex].Attributes["style"] += "background-color:White;";
                            Grd.Rows[1].Cells[30].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[1].Cells[31].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[1].Cells[32].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[3].Cells[32].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[5].Cells[32].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[8].Cells[32].Attributes["style"] += "background-color:Gray;";
                            Grd.Rows[10].Cells[32].Attributes["style"] += "background-color:Gray;";
                        }
                    }
                }


                this.SelectedRowIndex = Convert.ToInt32(e.CommandArgument.ToString());
                this.SelectedColumnIndex = Convert.ToInt32(Request.Form["__EVENTARGUMENT"].ToString());
                lblSelectedColumn.Text = SelectedColumnIndex.ToString();
                lblSelectedRow.Text = SelectedRowIndex.ToString();
                lblSelectedColumnTitle.Text = Grd.Columns[SelectedColumnIndex].HeaderText;
                lblSelectedColumnValue.Text = Grd.Rows[SelectedRowIndex].Cells[1].Text;

                //Acquire date of cell clicked
                string monthNum = ConvertMonthNum(Grd.Rows[SelectedRowIndex].Cells[1].Text);
                string dayNum = (Grd.Columns[SelectedColumnIndex].HeaderText.Length == 1) ? "0" + Grd.Columns[SelectedColumnIndex].HeaderText : Grd.Columns[SelectedColumnIndex].HeaderText;
                string clickDate = monthNum + "/" + dayNum + "/" + "2015";

                /*Populate modal with data*/
                //Check to see if data exists for  current cell, if so display data in modal.
                if (Grd.Rows[SelectedRowIndex].Cells[SelectedColumnIndex].Text != "&nbsp;")
                {
                    btnInsert.Visible = false;
                    btnUpdate.Visible = true;
                    string dbProjID = "";
                    string dbProjName = "";
                    string dbProjCode = Grd.Rows[SelectedRowIndex].Cells[SelectedColumnIndex].Text;
                    string dbStartDate = "";
                    string dbEndDate = "";
                    List<String> dbProjResources = new List<string>();
                    List<String> dbProjOpenResources = new List<string>();

                    //Acquire data from database
                    PPBL ProjPlanBL = new PPBL();
                    dbProjName = ProjPlanBL.GetProjName(dbProjCode);
                    dbStartDate = ProjPlanBL.GetProjStart(dbProjCode);
                    dbEndDate = ProjPlanBL.GetProjEnd(dbProjCode);
                    dbProjID = ProjPlanBL.GetProjID(dbProjCode);

                    //Add project resources to lbxProjResources listbox
                    dbProjResources = ProjPlanBL.GetProjResources(dbProjID);
                    foreach (string Resource in dbProjResources) // Loop through List with foreach.
	                {
                        lbxProjResources.Items.Add(Resource);
                    }

                    //Add available project resources to lbxOpenResources listbox
                    dbProjOpenResources = ProjPlanBL.GetProjOpenResources(clickDate);
                    foreach (string Resource in dbProjOpenResources) // Loop through List with foreach.
	                {
                        lbxOpenResources.Items.Add(Resource);
                    }

                    //Fill modal textboxes with acquired data
                    txtProjName.Text = dbProjName;
                    txtProjCode.Text = dbProjCode;
                    txtStartDate.Text = dbStartDate;
                    txtEndDate.Text = dbEndDate;
                }
                
                //If no data in current cell, list available resources
                else
                {
                    //Show Insert btn
                    btnInsert.Visible = true;
                    btnUpdate.Visible = false;

                    //Change button text to "Insert"
                    btnInsert.Text = "Insert New";

                    //Populate listbox with available resources
                    PPBL ProjPlanBL = new PPBL();
                    List<String> dbProjOpenResources = new List<string>();
                     dbProjOpenResources = ProjPlanBL.GetProjOpenResources(clickDate);
                     foreach (string Resource in dbProjOpenResources) // Loop through List with foreach.
                    {
                        lbxOpenResources.Items.Add(Resource);
                    }

                    //Make cell clicked the Starting Date
                    txtStartDate.Text = clickDate;
                }
            }
                //Open modal 
                Popup(true);
        }


        protected void btnInsert_Click(object sender, EventArgs e)
        {
            int selectedRowIndex = this.SelectedRowIndex;
            int selectedColumnIndex = this.SelectedColumnIndex;

            if (Grd.Rows[selectedRowIndex].Cells[selectedColumnIndex].Text == "&nbsp;")
            {

                //validation checks
                if (txtProjName.Text == "" ||
                    txtProjCode.Text == "" ||
                    txtStartDate.Text == "" ||
                    txtEndDate.Text == "")
                { //throw error 
                }
                else
                {

                    //Get date of cell clicked
                    string monthNum = ConvertMonthNum(Grd.Rows[SelectedRowIndex].Cells[1].Text);
                    string dayNum = (Grd.Columns[SelectedColumnIndex].HeaderText.Length == 1) ? "0" + Grd.Columns[SelectedColumnIndex].HeaderText : Grd.Columns[SelectedColumnIndex].HeaderText;
                    string clickDate = monthNum + "/" + dayNum + "/" + "2015";

                    //Populate listbox with available resources
                    PPBL ProjPlanBL = new PPBL();
                    List<String> dbProjOpenResources = new List<string>();
                    dbProjOpenResources = ProjPlanBL.GetProjOpenResources(clickDate);
                    foreach (string Resource in dbProjOpenResources) // Loop through List with foreach.
                    {
                        lbxOpenResources.Items.Add(Resource);
                    }

                    //Make cell clicked the Starting Date
                    txtStartDate.Text = clickDate;

                    //Find selected resources for project
                    string selectedItem = "";
                    if (lbxOpenResources.Items.Count > 0)
                    {
                        for (int i = 0; i < lbxOpenResources.Items.Count; i++)
                        {
                            if (lbxOpenResources.Items[i].Selected)
                            {
                                selectedItem += lbxOpenResources.Items[i].Text + ",";
                            }
                        }
                        //selectedItem = selectedItem.TrimEnd(',');
                    }

                    //insert data into Projects table
                    PPBL.InsertProject(txtProjName.Text, txtProjCode.Text, txtStartDate.Text, txtEndDate.Text);

                    //insert data into Assignments table
                    PPBL.InsertAssignments(txtProjName.Text, txtProjCode.Text, selectedItem);


                    //parse details to add to gridview 
                    string projCode = txtProjCode.Text;
                    string endDate = txtEndDate.Text;
                    string endMon = endDate.Substring(0, 2);
                    string endDay = endDate.Substring(3, 2);
                    //string endYr = endDate.Substring(6, 4);

                    //add new proj details to gridview
                    Grd.Rows[selectedRowIndex].Cells[selectedColumnIndex].Attributes["style"] += "background: linear-gradient(to top, white 80%, green 20%);";
                    Grd.Rows[Convert.ToInt32(endMon) - 1].Cells[Convert.ToInt32(endDay) + 1].Attributes["style"] += "background: linear-gradient(to top, white 80%, red 20%);";

                    //use proj code val to insert new val = Grd.Rows[10].Cells[32].Value? = projCode;
                    Grd.Rows[Convert.ToInt32(selectedRowIndex)].Cells[Convert.ToInt32(selectedColumnIndex)].Text = projCode;
                    Grd.Rows[Convert.ToInt32(endMon) - 1].Cells[Convert.ToInt32(endDay) + 1].Text = projCode;
                }


            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //update existing db record
            int selectedRowIndex = this.SelectedRowIndex;
            int selectedColumnIndex = this.SelectedColumnIndex;

                //validation checks
                if (txtProjName.Text == "" ||
                    txtProjCode.Text == "" ||
                    txtStartDate.Text == "" ||
                    txtEndDate.Text == "")
                { //throw error 
                }
                
                /*projects table*/
                string db_start ="";
                string db_end = "";
                DateTime dt_start;
                DateTime dt_end;
                string dbProjCode = Grd.Rows[selectedRowIndex].Cells[selectedColumnIndex].Text;
                PPBL ProjPlanBL = new PPBL();
                    
                    //Find existing project's start date
                    db_start = ProjPlanBL.GetProjDate(dbProjCode, "start");
                    dt_start = DateTime.Parse(db_start);
                    db_start = dt_start.ToShortDateString();
                    
                    //Find existing project's end date
                    db_end = ProjPlanBL.GetProjDate(dbProjCode, "end");
                    dt_end = DateTime.Parse(db_end);
                    db_end = dt_end.ToShortDateString();

            //Date validation
            if (txtStartDate.Text != db_start || txtEndDate.Text != db_end)
            {
                //get resource id's

                if (txtStartDate.Text != db_start)
                { 
                    //check current project startdate against resource's project's start time and end time
                    //e.g. current proj 01/02 01/04, resource on project starting 01/03 cuasing error
                    //throw error with resource name and project start/end date conflicts
                }
                else if (txtEndDate.Text != db_end)
                {
                    //check current project enddate against resource's project's start time and end time
                    //e.g. current proj 01/02 01/04, resource on project starting 01/03 cuasing error
                    //throw error with resource name and project start/end date conflicts
                }

            }

            //If date's check out, Update Projects table
            else
            {
                PPBL.UpdateProject(dbProjCode, txtProjName.Text,  txtProjCode.Text, txtStartDate.Text, txtEndDate.Text);
            }

             /*assignments table*/
            //Retrieve Open Listbox Vals, if available

            if (lbxOpenResources.SelectedIndex != -1)
            {
                string selectedItem = "";
                if (lbxOpenResources.Items.Count > 0)
                {
                    for (int i = 0; i < lbxOpenResources.Items.Count; i++)
                    {
                        if (lbxOpenResources.Items[i].Selected)
                        {
                            selectedItem += lbxOpenResources.Items[i].Text + ",";
                        }
                    }
                }


                //Update Assignments table
                PPBL.UpdateAssignments(txtProjName.Text, txtProjCode.Text, selectedItem);
            }

                Bind();
                AddExistingData();
                Popup(false);
            
        }

        protected void btnReload_Click(object sender, EventArgs e)
        {
            Bind();
            AddExistingData();
        }

        protected override void Render(HtmlTextWriter writer)
        {
            foreach (GridViewRow r in Grd.Rows)
            {
                if (r.RowType == DataControlRowType.DataRow)
                {
                    for (int columnIndex = 0; columnIndex < r.Cells.Count; columnIndex++)
                    {
                        Page.ClientScript.RegisterForEventValidation(r.UniqueID + "$ctl00", columnIndex.ToString());
                    }
                }
            }
            base.Render(writer);
        }

        void Popup(bool isDisplay)
        {
            StringBuilder builder = new StringBuilder();
            //calStart.Visible = false;
            if (isDisplay)
            {
                builder.Append(@"<script  type='text/javascript'> ShowPopup(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "ShowPopup", builder.ToString());
            }
            else
            {
                builder.Append(@"<script type='text/javascript'> HidePopup(); </script>");
                Page.ClientScript.RegisterStartupScript(this.GetType(), "HidePopup", builder.ToString());
            }
        }

        protected string ConvertMonthNum(string month)
        {
            string result = "";
            switch (month)
            {
                case "Jan":
                    result = "01";
                    break;
                case "Feb":
                    result = "02";
                    break;
                case "Mar":
                    result = "03";
                    break;
                case "Apr":
                    result = "04";
                    break;
                case "May":
                    result = "05";
                    break;
                case "Jun":
                    result = "06";
                    break;
                case "Jul":
                    result = "07";
                    break;
                case "Aug":
                    result = "08";
                    break;
                case "Sep":
                    result = "09";
                    break;
                case "Oct":
                    result = "10";
                    break;
                case "Nov":
                    result = "11";
                    break;
                case "Dec":
                    result = "12";
                    break;
            }
            return result;
        }

        //protected void CalStart_Click(object sender, EventArgs e)
        //{

        //    if (calStart.Visible = false)
        //    {
        //        calStart.Visible = true;
        //    }

        //    else
        //    {
        //        calStart.Visible = false;
        //    }

        //}

        //protected void CalEnd_Click(object sender, EventArgs e)
        //{

        //    if (calEnd.Visible == false)
        //    {
        //        calEnd.Visible = true;
        //    }

        //    else
        //    {
        //        calEnd.Visible = false;
        //    }

        //}


    }
}