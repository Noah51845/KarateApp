using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace KarateApp.mywork
{
    public partial class WebForm1 : System.Web.UI.Page
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
            if (!IsUserTypeAllowed("Member"))
            {
                // Redirect to an unauthorized page or display an error message
                Response.Redirect("~/Logon.aspx");
                return;
            }

            // Continue with page initialization for members
            if (!IsPostBack)
            {
                int loggedInMemberId = GetLoggedInMemberId();
                if (loggedInMemberId == -1)
                {
                    // Handle the case where member ID is not found
                    // Authenticate and redirect back to the login page
                    FormsAuthentication.SignOut();
                    Response.Redirect("~/Logon.aspx");
                    return;
                }

                //string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";
                string conn = "Data Source = (LocalDB)\\MSSQLLocalDB; AttachDbFilename = C:\\Users\\matht\\OneDrive\\Documents\\GitHub\\KarateApp\\App_Data\\KarateSchool.mdf; Integrated Security = True; Connect Timeout = 30"; 

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var query = from Member in context.Members
                                join Section in context.Sections on Member.Member_UserID equals Section.Member_ID
                                join Instructor in context.Instructors on Section.Instructor_ID equals Instructor.InstructorID
                                where Member.Member_UserID == loggedInMemberId
                                select new
                                {
                                    Member.MemberFirstName,
                                    Member.MemberLastName,
                                    Member.MemberDateJoined,
                                    Section.SectionName,
                                    Instructor.InstructorFirstName,
                                    Instructor.InstructorLastName,
                                    Section.SectionFee,
                                };

                    var memberWithSections = query.ToList();

                    if (memberWithSections.Any())
                    {
                        Label1.Text = "Welcome, " + memberWithSections.First().MemberFirstName;
                        Label2.Text = memberWithSections.First().MemberLastName;

                        // Calculate total amount spent on sections
                        decimal totalAmount = memberWithSections.Sum(section => section.SectionFee);

                        // Display the total amount
                        Label3.Text = "Total Amount Spent on Sections: $" + totalAmount;

                        // Bind member information to GridView
                        GridView1.DataSource = memberWithSections;
                        GridView1.DataBind();

                    }
                }    }
        }
        private int GetLoggedInMemberId()
        {
            // Assuming you store the member ID in a session variable named "userID"
            if (Session["userID"] != null && Session["userID"] is int)
            {
                return (int)Session["userID"];
            }
            else
            {
                // If the member ID is not found in the session, handle it accordingly.
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