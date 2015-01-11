﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Complex.Domino.Lib
{
    public class SubmissionFactory : EntityFactory
    {
        private int semesterID;
        private int courseID;
        private int assignmentID;
        private int studentID;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public int CourseID
        {
            get { return courseID; }
            set { courseID = value; }
        }

        public int AssignmentID
        {
            get { return assignmentID; }
            set { assignmentID = value; }
        }

        public int StudentID
        {
            get { return studentID; }
            set { studentID = value; }
        }

        public SubmissionFactory(Context context)
            :base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;
            this.studentID = -1;
        }

        public IEnumerable<Assignment> Find()
        {
            return Find(-1, -1);
        }

        public IEnumerable<Assignment> Find(int max, int from)
        {
            return base.Find<Assignment>(max, from);
        }

        protected override string GetTableQuery()
        {
            var from = @"
(SELECT s.*, a.Name AssignmentName, c.ID CourseID, c.Name CourseName, s.ID SemesterID, s.Name SemesterName,
       student.Name StudentName, teacher.Name TeacherName
FROM [Submission] s
INNER JOIN [Assignment] a ON a.ID = s.AssignmentID
INNER JOIN [Course] c ON c.ID = a.CourseID
INNER JOIN [Semester] r ON r.ID = c.SemesterID
INNER JOIN [User] student ON student.ID = s.StudentID
LEFT OUTER JOIN [User] teacher ON teacher.ID = s.TeacherID)
";

            return from;
        }

        protected override void AppendWhereCriteria(StringBuilder sb, System.Data.SqlClient.SqlCommand cmd)
        {
            base.AppendWhereCriteria(sb, cmd);

            if (semesterID > 0)
            {
                AppendWhereCriterion(sb, "SemesterID = @SemesterID");
                cmd.Parameters.Add("@SemesterID", System.Data.SqlDbType.Int).Value = semesterID;
            }

            if (courseID > 0)
            {
                AppendWhereCriterion(sb, "CourseID = @CourseID");
                cmd.Parameters.Add("@CourseID", System.Data.SqlDbType.Int).Value = courseID;
            }

            if (assignmentID > 0)
            {
                AppendWhereCriterion(sb, "AssignmentID = @AssignmentID");
                cmd.Parameters.Add("@AssignmentID", System.Data.SqlDbType.Int).Value = assignmentID;
            }

            if (studentID > 0)
            {
                AppendWhereCriterion(sb, "StudentID = @StudentID");
                cmd.Parameters.Add("@StudentID", System.Data.SqlDbType.Int).Value = studentID;
            }
        }
    }
}