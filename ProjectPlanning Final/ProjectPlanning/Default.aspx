<%@ Page Language="C#" AutoEventWireup="true" Inherits="ProjectPlanning.Default" Codebehind="Default.aspx.cs" %>


<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>PHS : Project Planning</title>
    <link rel="stylesheet" href="/Scripts/ui/1.11.2/themes/smoothness/jquery-ui.css">
    <script src="Scripts/jquery-1.11.1"></script>
    <script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>
    <script src="Scripts/ui/1.11.2/jquery-ui.js"></script>
    
    <style type="text/css">
        
        #mask
        {
            position: fixed;
            left: 0px;
            top: 0px;
            z-index: 4;
            opacity: 0.4;
            -ms-filter: "progid:DXImageTransform.Microsoft.Alpha(Opacity=40)"; /* first!*/
            filter: alpha(opacity=40); /* second!*/
            background-color: gray;
            display: none;
            width: 100%;
            height: 100%;
        }

        #Grd table, tr, td
        {
            border:1px solid #c0c0c0;
        }

        body
    {
        font-family: Arial;
        font-size: 10pt;
    }
    td
    {
        cursor: pointer;
    }
    .hover_row
    {
        background-color: #A1DCF2;
    }

    </style>
    <%--<script src="Scripts/jquery-1.8.2.min.js" type="text/javascript"></script>--%>
    
       <script type="text/javascript" language="javascript">

           function ShowPopup() {
            $('#mask').show();
            $('#<%=pnlpopup.ClientID %>').show();
        }
        function HidePopup() {
            $('#mask').hide();
            $('#<%=pnlpopup.ClientID %>').hide();
        }


           $('.btnClose').on('click', function () {
               HidePopup();
           });

           $(function () {
               $("[id*=Grd] td").hover(function () {
                   $("td", $(this).closest("tr")).addClass("hover_row");
               }, function () {
                   $("td", $(this).closest("tr")).removeClass("hover_row");
               });
           });

           //$(function () {
           //    $("#datepicker").datepicker();
           //});

           //$("#calEnd").show();

           //function onMouseOver(rowIndex) {
           //    var gv = document.getElementById("Grd");
           //    var rowElement = gv.rows[rowIndex];
           //    rowElement.style.backgroundColor = "#c8e4b6";
           //    //rowElement.cells[0].style.backgroundColor = "green";
           //}

           //function onMouseOut(rowIndex) {
           //    var gv = document.getElementById("Grd");
           //    var rowElement = gv.rows[rowIndex];
           //    rowElement.style.backgroundColor = "#fff";
           //    //rowElement.cells[0].style.backgroundColor = "#fff";
           //}
    </script>


     
</head>
<body>
<form id="form1" runat="server">
        <div>
            <table>
                <tr>
                    <td>
                        <asp:HiddenField ID="hdnID" runat="server" />
                        <asp:Button ID="btnReload" runat="server" Text="Reload Page" OnClick="btnReload_Click" />
                        <asp:GridView ID="Grd" runat="server" AutoGenerateColumns="False"  style="table-layout:fixed;" Width="1500px" Height="800px"
                            OnRowDataBound="Grd_RowDataBound"  OnRowCommand="Grd_RowCommand"   
                            CssClass="grid" BorderColor="Black" BorderStyle="Solid">
                            <%--DataSourceID="SqlDataSource1"--%>
                            <Columns>
                                <asp:ButtonField CommandName="ColumnClick" Visible="false" />  
                                <asp:BoundField DataField="Month" HeaderText="Month" SortExpression="Month" HeaderStyle-Width="60px" />
                                <asp:BoundField DataField="1" HeaderText="1" SortExpression="1"  />
                                <asp:BoundField DataField="2" HeaderText="2" SortExpression="2"  />
                                <asp:BoundField DataField="3" HeaderText="3" SortExpression="3" />
                                <asp:BoundField DataField="4" HeaderText="4" SortExpression="4" />
                                <asp:BoundField DataField="5" HeaderText="5" SortExpression="5" />
                                <asp:BoundField DataField="6" HeaderText="6" SortExpression="6" />
                                <asp:BoundField DataField="7" HeaderText="7" SortExpression="7" />
                                <asp:BoundField DataField="8" HeaderText="8" SortExpression="8" />
                                <asp:BoundField DataField="9" HeaderText="9" SortExpression="9" />
                                <asp:BoundField DataField="10" HeaderText="10" SortExpression="10" />
                                <asp:BoundField DataField="11" HeaderText="11" SortExpression="11" />
                                <asp:BoundField DataField="12" HeaderText="12" SortExpression="12" />
                                <asp:BoundField DataField="13" HeaderText="13" SortExpression="13" />
                                <asp:BoundField DataField="14" HeaderText="14" SortExpression="14" />
                                <asp:BoundField DataField="15" HeaderText="15" SortExpression="15" />
                                <asp:BoundField DataField="16" HeaderText="16" SortExpression="16" />
                                <asp:BoundField DataField="17" HeaderText="17" SortExpression="17" />
                                <asp:BoundField DataField="18" HeaderText="18" SortExpression="18" />
                                <asp:BoundField DataField="19" HeaderText="19" SortExpression="19" />
                                <asp:BoundField DataField="20" HeaderText="20" SortExpression="20" />
                                <asp:BoundField DataField="21" HeaderText="21" SortExpression="21" />
                                <asp:BoundField DataField="22" HeaderText="22" SortExpression="22" />
                                <asp:BoundField DataField="23" HeaderText="23" SortExpression="23" />
                                <asp:BoundField DataField="24" HeaderText="24" SortExpression="24" />
                                <asp:BoundField DataField="25" HeaderText="25" SortExpression="25" />
                                <asp:BoundField DataField="26" HeaderText="26" SortExpression="26" />
                                <asp:BoundField DataField="27" HeaderText="27" SortExpression="27" />
                                <asp:BoundField DataField="28" HeaderText="28" SortExpression="28" />
                                <asp:BoundField DataField="29" HeaderText="29" SortExpression="29" />
                                <asp:BoundField DataField="30" HeaderText="30" SortExpression="30" />
                                <asp:BoundField DataField="31" HeaderText="31" SortExpression="31" />
                                <%--<asp:TemplateField HeaderText="" SortExpression="">
                               <%-- <ItemTemplate>
                                    <asp:LinkButton ID="LinkButtonEdit" runat="server" CommandName="ShowPopup"
                                    CommandArgument='<%#Eval("Month") %>'>Edit</asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            </Columns>
                        </asp:GridView>
                    </td>
                    <td valign="top">
                        <table>
                            <tr>
                                <td>
                                    <strong>
                                    Selected Row Index:</strong>
                                    <asp:Label ID="lblSelectedRow" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <strong>
                                    Selected Column Index:</strong>
                                    <asp:Label ID="lblSelectedColumn" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                    Selected Column Title:</strong>
                                    <asp:Label ID="lblSelectedColumnTitle" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <strong>
                                    Selected Column Value:</strong>
                                    <asp:Label ID="lblSelectedColumnValue" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
         <div id="mask">
    </div>
      <asp:Panel ID="pnlpopup" runat="server"  BackColor="White" Height="400px"
            Width="800px" Style="z-index:111;background-color: White; position: absolute; left: 35%; top: 12%; border: outset 2px gray;padding:5px;display:none">
            <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #0924BC">
                    <td colspan="3" style="color:White; font-weight: bold; font-size: 1.2em; padding:3px"
                        align="center">
                        Project Details <a id="closebtn" style="color: white; float: right;text-decoration:none" class="btnClose" OnClick="HidePopup()"  href="#">X</a>
                    </td>
                </tr>
                <tr>
                    <td colspan="3"  text-align="center">
                        <asp:Label ID="LabelValidate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right" >
                        Project Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjName" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Project Code:
                    </td>
                    <td>
                        <asp:TextBox ID="txtProjCode" MaxLength="5" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Resources:
                    </td>
                    <td>
                        <%--<asp:Label ID="lblProjResources" runat="server"></asp:Label>--%>
                        <asp:Label ID="lblProj" runat="server" style="color: red; font-size:120%;" Text="Project Resources" /><br />
                        <asp:ListBox ID="lbxProjResources" runat="server" SelectionMode="Multiple" width="50%" /> <br />
                        <asp:Label ID="lblOpen" runat="server" style="color: green; font-size:120%;" Text="Open Resources" /><br />
                        <asp:ListBox ID="lbxOpenResources" runat="server" SelectionMode="Multiple" width="50%" />
                    </td>
                    <%--<td>
                        <asp:ListBox ID="lbxOpenResources" runat="server" SelectionMode="Multiple">
                       </asp:ListBox>
                    </td>--%>
                </tr>
                <tr>
                    <td align="right">
                        StartDate:
                    </td>
                    <td>
                        <asp:TextBox ID="txtStartDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        EndDate:
                    </td>
                    <td>
                        <asp:TextBox ID="txtEndDate" runat="server" />
                    </td>
                </tr>
                <tr>
               <%-- <tr>
                    <td align="right">
                        StartDate:
                    </td>
                    <td>
                        <input type="text" id="datepicker1">
                        <asp:Button ID="calStartButton" runat="server" OnClick="CalStart_Click" Text="Pick Date" />
                        <asp:Calendar ID="calStart" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        EndDate:
                    </td>
                    <td>
                        <asp:Button ID="calEndButton" runat="server" OnClick="CalEnd_Click" Text="Pick Date" />
                       <asp:Calendar ID="calEnd" runat="server" />
                    </td>
                </tr>
                <tr>--%>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="btnInsert" runat="server" Text="Insert" OnClick="btnInsert_Click" />
                        <asp:Button ID="btnUpdate" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <asp:Button ID="btnCancel" CommandName="Cancel" runat="server" Text="Cancel" class="btnClose" OnClientClick="HidePopup()" value="Cancel" />
                    </td>
                </tr>
            </table>
        </asp:Panel>

   <%-- <table width="100%" style="width: 100%; height: 100%;" cellpadding="0" cellspacing="5">
                <tr style="background-color: #0924BC">
                    <td colspan="2" style="color:White; font-weight: bold; font-size: 1.2em; padding:3px"
                        align="center">
                        Project Details <a id="closebtn" style="color: white; float: right;text-decoration:none" class="btnClose" OnClick="HidePopup()"  href="#">X</a>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        StartDate:
                    </td>
                    <td>
                        <input type="text" id="datepicker">
                        <%--<asp:Button ID="calStartButton" runat="server" OnClick="CalStart_Click" Text="Pick Date" />
                        <asp:Calendar ID="calStart" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        EndDate:
                    </td>
                    <td>
                        <asp:Button ID="calEndBtn" runat="server" OnClick="CalEnd_Click" Text="Pick Date" />
                       <asp:Calendar ID="calEnd" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Button ID="Button2" CommandName="Update" runat="server" Text="Update" OnClick="btnUpdate_Click" />
                        <input type="button" class="btnClose" onclick="HidePopup()" value="Cancel" />
                    </td>
                </tr>
            </table>--%>
    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:connstring %>" SelectCommand="sp_calendar" SelectCommandType="StoredProcedure"></asp:SqlDataSource>--%>
        <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>--%>
</form>

</body>
    </html>
