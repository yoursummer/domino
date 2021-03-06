﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Teacher
{
    public partial class Submission : SubmissionPage
    {
        public static string GetUrl(int assignmentID)
        {
            return GetUrl(assignmentID, -1);
        }

        public static string GetUrl(int assignmentID, int submissionID)
        {
            var url = "~/Teacher/Submission.aspx";
            var query = "";

            if (assignmentID > 0)
            {
                query += "&assignmentID=" + assignmentID.ToString();
            }

            if (submissionID > 0)
            {
                query += "&id=" + submissionID.ToString();
            }

            if (query.Length > 0)
            {
                url += "?" + query.Substring(1);
            }

            return url;
        }

        protected Lib.AssignmentGrade assignmentGrade;
        protected Lib.Submission replySubmission;

        protected override void CreateItem()
        {
            base.CreateItem();

            assignmentGrade = new Lib.AssignmentGrade(DatabaseContext);
            assignmentGrade.Load(Item.AssignmentID, Item.StudentID);

            if (Item.IsExisting)
            {
                Plugins.Visible = true;

                Plugins.SemesterID = Item.SemesterID;
                Plugins.CourseID = Item.CourseID;
                Plugins.AssignmentID = Item.AssignmentID;
                Plugins.SubmissionID = Item.ID;

                Plugins.CreatePluginControls();
            }
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            studentName.Text = Student.Name;
            studentName.NavigateUrl = Teacher.Student.GetUrl(Student.ID);
            studentDescription.Text = Student.Description;
            studentDescription.NavigateUrl = Teacher.Student.GetUrl(Student.ID);
            comments.Text = Item.Comments;
            
            gradeLabel.Text = Util.Enum.ToLocalized(typeof(Resources.Grades), Assignment.GradeType);
            gradeComments.Text = assignmentGrade.Comments;
            grade.Text = assignmentGrade.Grade.ToString();
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            if (sendReply.Checked)
            {
                // This is a reply by a teacher

                replySubmission = new Lib.Submission(Item);

                replySubmission.ID = -1;       // Reset, so a new submission will be created
                replySubmission.TeacherID = DatabaseContext.User.ID;
                replySubmission.Comments = reply.Text;

                var commit = CommitSubmission(replySubmission);

                replySubmission.Name = commit.Hash;
            }

            assignmentGrade.Comments = gradeComments.Text;
            assignmentGrade.Grade = int.Parse(grade.Text);
        }

        protected override void OnOkClick()
        {
            SaveForm();

            // Only create a new submission (reply) if requested.
            // Always save grade

            if (sendReply.Checked)
            {
                replySubmission.Save();
                SendEmail();
            }

            if (markRead.Checked)
            {
                Item.MarkRead();
            }
            else
            {
                Item.MarkUnread();
            }

            assignmentGrade.Save();
            OnRedirect();
        }

        private void SendEmail()
        {
            var body = new StringBuilder(Resources.EmailTemplates.Reply);

            var tokens = new Dictionary<string, string>()
                {
                     { "Name", Student.Description },
                     { "DateTime", Item.CreatedDate.ToString() },
                     { "Assignment", Assignment.Description },
                     { "Url", VirtualPathUtility.ToAbsolute(Web.Student.Submission.GetUrl(replySubmission.AssignmentID, replySubmission.ID)) }
                };

            Util.Email.ReplaceTokens(body, tokens);

            Util.Email.SendFromDomino(
                Student,
                Resources.EmailTemplates.ReplySubject,
                body.ToString());
        }

        protected void SendReply_CheckedChanged(object sender, EventArgs e)
        {
            CommentsRow3.Visible = sendReply.Checked;
            CommentsRow4.Visible = sendReply.Checked;
        }
    }
}