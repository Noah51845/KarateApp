using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateApp.mywork
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Assuming you have some way to identify the logged-in member, replace "loggedInMemberId" with the actual member ID.
                int loggedInMemberId = GetLoggedInMemberId();
                string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var member = context.Members.SingleOrDefault(m => m.Member_UserID == loggedInMemberId);

                    if (member != null)
                    {
                        Label1.Text = member.MemberFirstName;
                        Label2.Text = member.MemberLastName;
                    }
                }
            }
        }
        private int GetLoggedInMemberId()
        {
            // Assuming you store the member ID in a session variable named "LoggedInMemberId"
            if (Session["LoggedInMemberId"] != null && Session["LoggedInMemberId"] is int)
            {
                return (int)Session["LoggedInMemberId"];
            }
            else
            {
                // If the member ID is not found in the session, handle it accordingly.
                // You might want to redirect the user to the login page or take other actions.
                // For simplicity, returning -1 as an indication of an error.
                return -1;
            }
        }
    }
}