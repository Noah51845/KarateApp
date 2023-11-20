using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateApp.mywork
{
    public partial class admininfo : System.Web.UI.Page
    {
        
        //string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
        string conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\matht\\OneDrive\\Documents\\GitHub\\KarateApp\\App_Data\\KarateSchool.mdf; Integrated Security = True; Connect Timeout = 30";

        KarateDataContext dbcon;

        public void ShowAllRecords()
        {
            var memberRecords = from item in dbcon.Members select new { item.MemberFirstName, item.MemberLastName, item.MemberPhoneNumber, item.MemberDateJoined };
            GridView1.DataSource = memberRecords;
            GridView1.DataBind();

            var instructorRecords = from item in dbcon.Instructors select new { item.InstructorFirstName, item.InstructorLastName };
            GridView2.DataSource = instructorRecords;
            GridView2.DataBind();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(conn);
            ShowAllRecords();
        }
    }
}