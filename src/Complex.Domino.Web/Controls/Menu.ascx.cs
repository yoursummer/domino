﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Complex.Domino.Web.Controls
{
    public partial class Menu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AdminUsers.NavigateUrl = Admin.UserList.GetUrl();
            AdminSemesters.NavigateUrl = Admin.SemesterList.GetUrl();
            AdminCourses.NavigateUrl = Admin.CourseList.GetUrl();
            AdminAssignments.NavigateUrl = Admin.AssignmentList.GetUrl();

            TeacherCourses.NavigateUrl = Teacher.CourseList.GetUrl();

            StudentMenu.NavigateUrl = Student.Default.GetUrl();
            StudentCourses.NavigateUrl = Student.Courses.GetUrl();
            StudentAssignments.NavigateUrl = Student.Assignments.GetUrl();
            StudentSubmissions.NavigateUrl = Student.Submissions.GetUrl();
        }
    }
}