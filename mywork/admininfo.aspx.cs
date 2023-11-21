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
            if (!User.Identity.IsAuthenticated || Session["userID"] == null || Session["userType"] == null)
            {
                // If not authenticated, redirect to the Logon page
                Response.Redirect("~/Logon.aspx", true);
            }
            dbcon = new KarateDataContext(conn);
            ShowAllRecords();
        }

        protected void btnMemAdd_Click(object sender, EventArgs e)
        {
            using (KarateDataContext context = new KarateDataContext(conn))
            {
                var newUser = new NetUser
                {
                    UserName = txtMemUsername.Text,
                    UserPassword = txtMemPassword.Text,
                    UserType = "Member"
                };
                context.NetUsers.InsertOnSubmit(newUser);
                context.SubmitChanges();
                var newMemberRecord = new Member
                {
                    Member_UserID = newUser.UserID,
                    MemberFirstName = txtMemFname.Text,
                    MemberLastName = txtMemLname.Text,
                    MemberEmail = txtMemEmail.Text,
                    MemberPhoneNumber = txtMemPhoneNum.Text,
                    MemberDateJoined = DateTime.Now,
                };
                context.Members.InsertOnSubmit(newMemberRecord);
                context.SubmitChanges();
                ShowAllRecords();
            }
        }

        protected void btnInsAdd_Click(object sender, EventArgs e)
        {
            using (KarateDataContext context = new KarateDataContext(conn))
            {
                var newUser = new NetUser
                {
                    UserName = txtInsUsername.Text,
                    UserPassword = txtInsPassword.Text,
                    UserType = "Instructor"
                };
                context.NetUsers.InsertOnSubmit(newUser);
                context.SubmitChanges();
                var newInstructorRecord = new Instructor
                {
                    InstructorID = newUser.UserID,
                    InstructorFirstName = txtInsFname.Text,
                    InstructorLastName = txtInsLname.Text,
                    InstructorPhoneNumber = txtInsPhoneNum.Text,

                };
                context.Instructors.InsertOnSubmit(newInstructorRecord);
                context.SubmitChanges();
                ShowAllRecords();
            }
        }


        protected void btnMemDelete_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0)
            {
                int index = GridView1.SelectedIndex;

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var deleteMemberRecord = context.Members.Skip(index).FirstOrDefault();
                    var deleteUserRecord = context.NetUsers.FirstOrDefault(item => item.UserID == deleteMemberRecord.Member_UserID);
                    var deleteSessionRecord = context.Sections.FirstOrDefault(item => item.Member_ID == deleteMemberRecord.Member_UserID);
                    
                    
                    if(deleteSessionRecord != null)
                    {
                        context.Sections.DeleteOnSubmit(deleteSessionRecord);
                        context.SubmitChanges();
                    }
                    context.Members.DeleteOnSubmit(deleteMemberRecord);
                    context.SubmitChanges();
                    context.NetUsers.DeleteOnSubmit(deleteUserRecord);
                    context.SubmitChanges();

                    ShowAllRecords();
                }
            }
        }

        protected void btnInsDelete_Click(object sender, EventArgs e)
        {
            if (GridView2.SelectedIndex >= 0)
            {
                int index = GridView2.SelectedIndex;

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var deleteInstructorRecord = context.Instructors.Skip(index).FirstOrDefault();
                    var deleteUserRecord = context.NetUsers.FirstOrDefault(item => item.UserID == deleteInstructorRecord.InstructorID);
                    var deleteSessionRecord = context.Sections.FirstOrDefault(item => item.Instructor_ID == deleteInstructorRecord.InstructorID);

                    
                    if (deleteSessionRecord != null)
                    {
                        context.Sections.DeleteOnSubmit(deleteSessionRecord);
                        context.SubmitChanges();
                    }
                    context.Instructors.DeleteOnSubmit(deleteInstructorRecord);
                    context.SubmitChanges();
                    context.NetUsers.DeleteOnSubmit(deleteUserRecord);
                    context.SubmitChanges();

                    ShowAllRecords();
                }
            }
        }

        protected void btnAddSession_Click(object sender, EventArgs e)
        {
            if (GridView1.SelectedIndex >= 0 && GridView2.SelectedIndex >= 0)
            {
                int index = GridView1.SelectedIndex;

                using (KarateDataContext context = new KarateDataContext(conn))
                {
                    var memberRecord = context.Members.Skip(index).FirstOrDefault();

                    index = GridView2.SelectedIndex;
                    var instructorRecord = context.Instructors.Skip(index).FirstOrDefault();

                    var section = new Section
                    {
                        Member_ID = memberRecord.Member_UserID,
                        Instructor_ID = instructorRecord.InstructorID,
                        SectionFee = Convert.ToDecimal(txtSessionFee.Text),
                        SectionStartDate = Convert.ToDateTime(txtSessionStartDate.Text),
                        SectionName = DropDownSessionType.SelectedItem.ToString(),
                    };
                    context.Sections.InsertOnSubmit(section);
                    context.SubmitChanges();
                }
            }
        }
    }
}