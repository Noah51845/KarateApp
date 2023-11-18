using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateApp.mywork
{
    public partial class instructor : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Assuming you have some way to identify the logged-in instructor, replace "loggedInInstructorId" with the actual instructor ID.
                int loggedInInstructorId = GetLoggedInInstructorId();

                if (loggedInInstructorId != -1)
                {
                    string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";

                    using (KarateDataContext context = new KarateDataContext(conn))
                    {
                        var instructor = context.Instructors.SingleOrDefault(i => i.InstructorID == loggedInInstructorId);

                        if (instructor != null)
                        {
                            // Display instructor information on the page as needed
                            // For example:
                            Label1.Text = instructor.InstructorFirstName;
                            Label2.Text = instructor.InstructorLastName;
                        }
                    }
                }
                else
                {
                    // Handle the case where the instructor ID is not found in the session.
                    // You might want to redirect the user to the login page or take other actions.
                    Response.Redirect("~/Logon.aspx", true);
                }
            }
            else
            {
                // If the user is not authenticated, redirect to the login page.
                Response.Redirect("~/Logon.aspx", true);
            }
        }

        private int GetLoggedInInstructorId()
        {
            // Assuming you store the instructor ID in a session variable named "userID"
            if (Session["userID"] != null && Session["userID"] is int)
            {
                return (int)Session["userID"];
            }
            else
            {
                // If the instructor ID is not found in the session, handle it accordingly.
                // You might want to redirect the user to the login page or take other actions.
                // For simplicity, returning -1 as an indication of an error.
                return -1;
            }
        }
    }
}