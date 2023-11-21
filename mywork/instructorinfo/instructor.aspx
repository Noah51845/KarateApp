<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="instructor.aspx.cs" Inherits="KarateApp.mywork.instructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
    .auto-style1 {
        height: 386px;
    }
    .auto-style2 {
        height: 421px;
    }
    .auto-style3 {
        height: 421px;
        width: 1135px;
    }
    .auto-style4 {
        width: 1135px;
    }
    .auto-style5 {
        height: 386px;
        width: 1135px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <p>
    <br />
</p>
<p>
    <table style="width:100%;">
        <tr>
            <td class="auto-style3">
                <asp:GridView ID="GridView1" runat="server">
                </asp:GridView>
            </td>
            <td class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;
                <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="auto-style4">&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="auto-style5"></td>
            <td class="auto-style1"></td>
        </tr>
    </table>
</p>
<p>
</p>
<p>
</p>
</asp:Content>
