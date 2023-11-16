<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="member.aspx.cs" Inherits="KarateApp.mywork.WebForm1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            height: 550px;
        }
        .auto-style2 {
            height: 550px;
            width: 600px;
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
            <td class="auto-style2">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource2">
                    <Columns>
                        <asp:BoundField DataField="MemberFirstName" HeaderText="MemberFirstName" SortExpression="MemberFirstName" />
                        <asp:BoundField DataField="MemberLastName" HeaderText="MemberLastName" SortExpression="MemberLastName" />
                        <asp:BoundField DataField="SectionName" HeaderText="SectionName" SortExpression="SectionName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:KarateSchoolConnectionString %>" SelectCommand="SELECT Member.MemberFirstName, Member.MemberLastName, Section.SectionName FROM Member INNER JOIN Section ON Member.Member_UserID = Section.Member_ID"></asp:SqlDataSource>
            </td>
            <td class="auto-style1">Hello,
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    </p>
<p>
</p>
<p>
</p>
<p>
</p>
</asp:Content>
