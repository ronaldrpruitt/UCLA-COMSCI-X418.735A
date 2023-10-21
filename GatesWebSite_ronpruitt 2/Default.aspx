<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="GatesWebSite_ronpruitt.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h1>Gates Cell Phones (Main Menu)</h1>
        </div>
        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Inventory.aspx">Go to Inventory</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Customers.aspx">Go to Customers</asp:HyperLink>
        <br />
        <br />
        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Orders.aspx">Go to Orders</asp:HyperLink>

    </form>
</body>
</html>
