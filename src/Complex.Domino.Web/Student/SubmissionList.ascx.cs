﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class SubmissionList : UserControlBase
    {
        private Lib.SubmissionFactory searchObject;

        public int CourseID
        {
            get { return (int)(ViewState["CourseID"] ?? -1); }
            set { ViewState["CourseID"] = value; }
        }

        public int AssignmentID
        {
            get { return (int)(ViewState["AssignmentID"] ?? -1); }
            set { ViewState["AssignmentID"] = value; }
        }

        protected void Page_PreRender(object sender, EventArgs e)
        {
            if (submissionList.Visible)
            {
                submissionList.DataBind();
            }
        }

        protected void SubmissionDataSource_ObjectCreating(object sender, ObjectDataSourceEventArgs e)
        {
            searchObject = new Lib.SubmissionFactory(DatabaseContext);

            searchObject.CourseID = CourseID;
            searchObject.AssignmentID = AssignmentID;
            searchObject.StudentID = DatabaseContext.User.ID;

            e.ObjectInstance = searchObject;
        }

        protected void SubmissionList_ItemCreated(object sender, ListViewItemEventArgs e)
        {
            if (e.Item.ItemType == ListViewItemType.DataItem)
            {
                var submission = (Lib.Submission)e.Item.DataItem;

                var link = (HyperLink)e.Item.FindControl("submissionsLink");
                var label = (Label)e.Item.FindControl("createdDateLabel");

                // mark unread submissions by teacher
                if (submission.IsReply)
                {
                    if (!submission.IsRead)
                    {
                        link.CssClass += " unread";
                    }
                    else if (submission.TeacherID > 0)
                    {
                        link.CssClass += " reply";
                    }

                    label.Text = Resources.Labels.ReplyDate;
                }
                else
                {
                    label.Text = Resources.Labels.SubmissionDate;
                }
            }
        }
    }
}