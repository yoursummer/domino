﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web
{
    public class SubmissionPage : EntityPage<Lib.Submission>
    {
        protected Panel emptyPanel;
        protected Panel filesPanel;
        protected Label formLabel;
        protected Label cancelLabel;
        protected LinkButton ok;
        protected Label semesterDescription;
        protected Label courseDescription;
        protected Label assignmentDescription;
        protected Controls.FileBrowser fileBrowser;

        private Lib.Assignment assignment;
        private Lib.GitHelper git;
        private Lib.User student;

        protected int AssignmentID
        {
            get { return Util.Url.ParseInt(Request.QueryString[Constants.RequestAssignmentID]); }
        }

        protected override void CreateItem()
        {
            base.CreateItem();

            Item.AssignmentID = this.AssignmentID;

            assignment = new Lib.Assignment(DatabaseContext);
            assignment.Load(Item.AssignmentID);

            // If the submission already exists we can take the student from
            // it.
            // TODO: need to extend logic in case of reply by teacher

            if (Item.IsExisting)
            {
                student = new Lib.User(DatabaseContext);
                student.Load(Item.StudentID);
            }
            else
            {
                // TODO: add reply-to logic here
                student = DatabaseContext.User;
            }

            // Initialize the git helper class with current session info

            git = new Lib.GitHelper()
            {
                SessionGuid = SessionGuid,
                Author = DatabaseContext.User,
                Student = student,
                Assignment = assignment,
                Submission = Item,
            };

            fileBrowser.PrefixPath = git.GetAssignmentPrefixPath();
            fileBrowser.BasePath = git.GetAssignmentPath();
        }

        protected override void UpdateForm()
        {
            base.UpdateForm();

            formLabel.Text = Item.IsExisting ?
                Resources.Labels.Submission :
                Resources.Labels.NewSubmission;

            cancelLabel.Text = Item.IsExisting ?
                Resources.Labels.Ok :
                Resources.Labels.Cancel;

            ok.Visible = !Item.IsExisting;

            var semester = new Lib.Semester(DatabaseContext);
            semester.Load(assignment.SemesterID);

            var course = new Lib.Course(DatabaseContext);
            course.Load(assignment.CourseID);

            semesterDescription.Text = semester.Description;
            courseDescription.Text = course.Description;
            assignmentDescription.Text = assignment.Description;

            fileBrowser.AllowDelete = !Item.IsExisting;
            fileBrowser.AllowUpload = !Item.IsExisting;

            if (Item.IsExisting)
            {
                // Check out the current submission to view files
                // TODO: might need to be replace with smarter solution
                // that read file contents from git directly
                git.CheckOutSubmission();

                SwitchViewToFiles();
            }
            else
            {
                // Make sure the git repo is checked out, the assigment exists
                // and it is the tip of the branch
                git.EnsureAssignmentExists();

                // If this a first time visit to the page the user can choose whether to
                // keep existing files in the submission folder or delete them
                if (git.IsAssignmentEmpty())
                {
                    SwitchViewToFiles();
                }
            }
        }

        protected override void SaveForm()
        {
            base.SaveForm();

            var comments = Item.Comments;

            if (String.IsNullOrWhiteSpace(comments))
            {
                comments = "(no comments were given)";   // TODO
            }

            // Commit changes into git
            var commit = git.CommitSubmission(comments);

            Item.StudentID = DatabaseContext.User.ID;
            Item.Name = commit.Hash;
        }

        protected void NewSubmissionKeep_Click(object sender, EventArgs e)
        {
            SwitchViewToFiles();
        }

        protected void NewSubmissionEmpty_Click(object sender, EventArgs e)
        {
            git.EmptyAssignment();
            SwitchViewToFiles();
        }

        private void SwitchViewToFiles()
        {
            if (emptyPanel != null)
            {
                emptyPanel.Visible = false;
            }

            filesPanel.Visible = true;
        }

        
    }
}