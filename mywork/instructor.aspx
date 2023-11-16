<%@ Page Title="" Language="C#" MasterPageFile="~/Site1.Master" AutoEventWireup="true" CodeBehind="instructor.aspx.cs" Inherits="KarateApp.mywork.instructor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            margin-bottom: 514px;
        }
        .auto-style2 {
            height: 243px;
        }
        .auto-style3 {
            height: 243px;
            width: 1225px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <table class="auto-style1">
        <tr>
            <td class="auto-style3">
                <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
                    <Columns>
                        <asp:BoundField DataField="InstructorFirstName" HeaderText="InstructorFirstName" SortExpression="InstructorFirstName" />
                        <asp:BoundField DataField="InstructorLastName" HeaderText="InstructorLastName" SortExpression="InstructorLastName" />
                        <asp:BoundField DataField="SectionName" HeaderText="SectionName" SortExpression="SectionName" />
                    </Columns>
                </asp:GridView>
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:KarateSchoolConnectionString %>" SelectCommand="SELECT Instructor.InstructorFirstName, Instructor.InstructorLastName, Section.SectionName FROM Instructor INNER JOIN Instructor AS Instructor_1 ON Instructor.InstructorID = Instructor_1.InstructorID CROSS JOIN Section"></asp:SqlDataSource>
            </td>
            <td class="auto-style2">Hello
                <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;<asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
            </td>
        </tr>
    </table>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
    <p>
        &nbsp;</p>
</asp:Content>
