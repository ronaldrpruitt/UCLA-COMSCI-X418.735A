<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Orders.aspx.cs" Inherits="GatesWebSite_ronpruitt.Orders" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <h1>Order Data:</h1>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CellPadding="4" DataKeyNames="OrderId" DataSourceID="SqlDataSource1" Width="916px" BackColor="White" BorderColor="#3366CC" BorderStyle="None" BorderWidth="1px" Height="308px">
                <Columns>
                    <asp:BoundField DataField="OrderId" HeaderText="Order Id" ReadOnly="True" SortExpression="OrderId" />
                    <asp:BoundField DataField="CustomerID" HeaderText="Customer ID" SortExpression="CustomerID" />
                    <asp:BoundField DataField="InventoryId" HeaderText="Inventory Id" SortExpression="InventoryId" />
                    <asp:BoundField DataField="OrderDate" HeaderText="Order Date" SortExpression="OrderDate" DataFormatString="{0:MM-dd-yyyy}" />
                </Columns>
                <FooterStyle BackColor="#99CCCC" ForeColor="#003399" />
                <HeaderStyle BackColor="#003399" Font-Bold="True" ForeColor="#CCCCFF" />
                <PagerStyle BackColor="#99CCCC" ForeColor="#003399" HorizontalAlign="Left" />
                <RowStyle BackColor="White" ForeColor="#003399" />
                <SelectedRowStyle BackColor="#009999" Font-Bold="True" ForeColor="#CCFF99" />
                <SortedAscendingCellStyle BackColor="#EDF6F6" />
                <SortedAscendingHeaderStyle BackColor="#0D4AC4" />
                <SortedDescendingCellStyle BackColor="#D6DFDF" />
                <SortedDescendingHeaderStyle BackColor="#002876" />
            </asp:GridView>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT * FROM [Orders]"></asp:SqlDataSource>
            <br />
        </div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Default.aspx">Go Back to Gates Cell Phone(Main Menu)</asp:HyperLink>
    </form>
</body>
</html>
