﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Student
{
    public partial class Assignment : EntityPage<Lib.Assignment>
    {
        public static string GetUrl(int assignmentID)
        {
            return String.Format("~/Student/Assignment.aspx?ID={0}", assignmentID);
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            TitleLabel.Text = Item.Description;
            SemesterDescription.Text = Item.SemesterDescription;
            CourseDescription.Text = Item.CourseDescription;
            AssignmentDescription.Text = Item.Description;

            if (!String.IsNullOrWhiteSpace(Item.Url))
            {
                Url.Text = Item.Url;
                Url.NavigateUrl = Item.Url;
            }
            else
            {
                UrlRow.Visible = false;
            }

            if (!String.IsNullOrWhiteSpace(Item.Comments))
            {
                Comments.Text = Item.Comments;
            }
            else
            {
                CommentsPanel.Visible = false;
            }

            NewSubmission.NavigateUrl = Submission.GetUrl(Item.ID);

            SubmissionList.CourseID = Item.CourseID;
            SubmissionList.AssignmentID = Item.ID;
        }
    }
}