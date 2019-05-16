using Microsoft.AspNet.SignalR;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using uStora.Web.Hubs;

namespace uStora.Web.Models
{
    public class NotificationComponent
    {
        public void RegisterNotification(DateTime currentTime)
        {
            string conStr = ConfigurationManager.ConnectionStrings["uStoraConnection"].ConnectionString;
            FeedbackRegister(conStr, currentTime);
            UserRegister(conStr, currentTime);
        }
        private void FeedbackRegister(string conStr, DateTime currentTime)
        {
            string sqlCommand = @"SELECT  [ID],[Name],[CreatedDate]
                                FROM [dbo].[Feedbacks]
                                where [CreatedDate] > @CreatedDate";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlCommand, con);
                cmd.Parameters.AddWithValue("@CreatedDate", currentTime);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += sqlDep_OnChange;
                cmd.ExecuteReader();
            }
        }

        private void UserRegister(string conStr, DateTime currentTime)
        {
            string sqlUser = @"SELECT [Id] ,[FullName],[CreatedDate]
                FROM [dbo].[ApplicationUsers] where [CreatedDate] > @CreatedDate";

            using (SqlConnection con = new SqlConnection(conStr))
            {
                SqlCommand cmd = new SqlCommand(sqlUser, con);
                cmd.Parameters.AddWithValue("@CreatedDate", currentTime);
                if (con.State != ConnectionState.Open)
                {
                    con.Open();
                }
                cmd.Notification = null;
                SqlDependency sqlDep = new SqlDependency(cmd);
                sqlDep.OnChange += user_OnChange;
                cmd.ExecuteReader();
            }
        }
        public void sqlDep_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= sqlDep_OnChange;

                var feedbackHub = GlobalHost.ConnectionManager.GetHubContext<FeedbackHub>();
                feedbackHub.Clients.All.newfeedback("feedback");

                RegisterNotification(DateTime.Now);
            }
        }

        public void user_OnChange(object sender, SqlNotificationEventArgs e)
        {
            if (e.Info == SqlNotificationInfo.Insert)
            {
                SqlDependency sqlDep = sender as SqlDependency;
                sqlDep.OnChange -= user_OnChange;

                var userHub = GlobalHost.ConnectionManager.GetHubContext<UserHub>();
                userHub.Clients.All.newuser("user");

                RegisterNotification(DateTime.Now);
            }
        }
    }
}