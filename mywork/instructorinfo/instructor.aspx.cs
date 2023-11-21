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
                Response.Redirect("~/Logon.aspx");
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

                string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
                //string conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\matht\\OneDrive\\Documents\\GitHub\\KarateApp\\App_Data\\KarateSchool.mdf; Integrated Security = True; Connect Timeout = 30";

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var instructor = context.Instructors.SingleOrDefault(i => i.InstructorID == loggedInInstructorId);

                    if (instructor != null)
                    {
                        var instructorInfo = (from Instructor in context.Instructors
                                              join Section in context.Sections on Instructor.InstructorID equals Section.Instructor_ID
                                              join Member in context.Members on Section.Member_ID equals Member.Member_UserID
                                              where instructor.InstructorID == loggedInInstructorId
                                              select new
                                              {
                                                  Instructor.InstructorFirstName,
                                                  Instructor.InstructorLastName,
                                                  Section.SectionName,
                                                  Member.MemberFirstName,
                                                  Member.MemberLastName,
                                                  // Add other properties you need
                                              }).FirstOrDefault();

                        if (instructorInfo != null)
                        {
                            // Display instructor information on the page
                            Label1.Text = "Welcome, " + instructorInfo.InstructorFirstName;
                            Label2.Text = instructorInfo.InstructorLastName;

                            // Bind instructor information to GridView
                            GridView1.DataSource = new List<object> { instructorInfo }; // Creating a list with a single item for simplicity
                            GridView1.DataBind();
                        }
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
            if (Session["userType"] != null)
            {
                string userType = Session["userType"].ToString().Trim();
                return userType == allowedUserType || userType == "Administrator";
            }
            else
            {
                // If the user type is not allowed, return false
                return false;
            }
        }
    }
}