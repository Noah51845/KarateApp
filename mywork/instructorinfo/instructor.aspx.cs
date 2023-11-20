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
            // Ensure the user is logged in
            if (!User.Identity.IsAuthenticated)
            {
                // Redirect to the login page if not authenticated
                Response.Redirect("~/Logon.aspx");
                return;
            }

            // Check user type
            if (!IsUserTypeAllowed("Instructor"))
            {
                // Redirect to an unauthorized page or display an error message
                Response.Redirect("~/UnauthorizedAccess.aspx");
                return;
            }

            // Continue with page initialization for instructors
            if (!IsPostBack)
            {
                int loggedInInstructorId = GetLoggedInInstructorId();
                if (loggedInInstructorId == -1)
                {
                    // Handle the case where the instructor ID is not found
                    // Authenticate and redirect back to the login page
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Logon.aspx");
                    return;
                }

                //string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
                string conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\matht\\OneDrive\\Documents\\GitHub\\KarateApp\\App_Data\\KarateSchool.mdf; Integrated Security = True; Connect Timeout = 30";

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var instructor = context.Instructors.SingleOrDefault(i => i.InstructorID == loggedInInstructorId);

                    if (instructor != null)
                    {
                        // Display instructor information on the page as needed
                        // For example:
                        Label1.Text = "Welcome, " + instructor.InstructorFirstName;
                        Label2.Text = "Last Name: " + instructor.InstructorLastName;
                        // Add any additional labels or information you want to display for the instructor
                    }
                }
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
                // For simplicity, redirecting to the login page.
                FormsAuthentication.SignOut();
                Response.Redirect("~/Logon.aspx");
                return -1;
            }
        }

        private bool IsUserTypeAllowed(string allowedUserType)
        {
            // Assuming you store the user type in a session variable named "userType"
            if (Session["userType"] != null && Session["userType"].ToString() == allowedUserType)
            {
                return true;
            }
            else
            {
                // If the user type is not allowed, return false
                return false;
            }
        }
    }
}   