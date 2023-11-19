using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KarateApp
{
    public partial class Logon : System.Web.UI.Page
    {
        KarateDataContext dbcon;
        string conn = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\Desktop\\Assignment4\\KarateApp\\App_Data\\KarateSchool.mdf;Integrated Security=True;Connect Timeout=30";

        protected void Page_Load(object sender, EventArgs e)
        {
            dbcon = new KarateDataContext(conn);

            // Check if user is already authenticated
            if (User.Identity.IsAuthenticated)
            {
                // Redirect based on user type
                if (HttpContext.Current.Session["userType"] != null)
                {
                    if (HttpContext.Current.Session["userType"].ToString().Trim() == "Member")
                    {
                        Response.Redirect("mywork/memberinfo/member.aspx");
                    }
                    else if (HttpContext.Current.Session["userType"].ToString().Trim() == "Instructor")
                    {
                        Response.Redirect("mywork/instructorinfo/instructor.aspx");
                    }
                }
            }
        }

        protected void Login1_Authenticate(object sender, AuthenticateEventArgs e)
        {
            try
            {
                string nUserName = Login1.UserName;
                string nPassword = Login1.Password;

                // Search for the current User, validate UserName and Password
                NetUser myUser = dbcon.NetUsers.FirstOrDefault(x =>
                    x.UserName == nUserName &&
                    x.UserPassword == nPassword);

                if (myUser != null)
                {
                    // Add UserID, User type, and User Name to the Session
                    HttpContext.Current.Session["userID"] = myUser.UserID;
                    HttpContext.Current.Session["userType"] = myUser.UserType;
                    HttpContext.Current.Session["userName"] = myUser.UserName;

                    // Redirect based on user type
                    if (myUser.UserType.Trim() == "Member")
                    {
                        FormsAuthentication.RedirectFromLoginPage(nUserName, true);
                        Response.Redirect("mywork/memberinfo/member.aspx");
                    }
                    else if (myUser.UserType.Trim() == "Instructor")
                    {
                        FormsAuthentication.RedirectFromLoginPage(nUserName, true);
                        Response.Redirect("mywork/instructorinfo/instructor.aspx");
                    }
                }
                else
                {
                    // If user authentication fails or user type is not recognized, redirect to Logon page
                    Response.Redirect("~/Logon.aspx", true);
                }
            }
            catch (Exception ex)
            {
                // Log the exception for debugging
                // You can log to a file, database, or use System.Diagnostics.Debug.WriteLine
                System.Diagnostics.Debug.WriteLine($"Exception during authentication: {ex.Message}");
                Response.Redirect("~/Logon.aspx", true);
            }
        }
    }
    }