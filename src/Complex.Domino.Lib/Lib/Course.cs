﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    [Serializable]
    public class Course : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private string semesterName;
        private string semesterDescription;
        private DateTime startDate;
        private DateTime endDate;
        private string url;
        private GradeType gradeType;

        public int SemesterID
        {
            get { return semesterID; }
            set { semesterID = value; }
        }

        public string SemesterName
        {
            get { return semesterName; }
        }

        public string SemesterDescription
        {
            get { return semesterDescription; }
        }

        public DateTime StartDate
        {
            get { return startDate; }
            set { startDate = value; }
        }

        public DateTime EndDate
        {
            get { return endDate; }
            set { endDate = value; }
        }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public GradeType GradeType
        {
            get { return gradeType; }
            set { gradeType = value; }
        }

        public string FullName
        {
            get { return String.Format("{0}/{1}", SemesterName, Name); }
        }

        public Course()
        {
            InitializeMembers();
        }

        public Course(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.semesterName = null;
            this.semesterDescription = null;
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
            this.url = String.Empty;
            this.gradeType = Lib.GradeType.Unknown;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.semesterName = reader.GetString("SemesterName");
            this.semesterDescription = reader.GetString("SemesterDescription");
            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
            this.url = reader.GetString("Url");
            this.gradeType = (Lib.GradeType)reader.GetInt32("GradeType");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT c.*, r.Name SemesterName, r.Description SemesterDescription
FROM [Course] c
INNER JOIN [Semester] r ON r.ID = c.SemesterID
WHERE c.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandAsSingleObject(cmd, this);
            }
        }

        protected override void OnCreate(string columns, string values)
        {
            var sql = @"
INSERT [Course]
    (SemesterID, {0}, StartDate, EndDate, Url, GradeType)
VALUES
    (@SemesterID, {1}, @StartDate, @EndDate, @Url, @GradeType)

SELECT @@IDENTITY
";

            sql = String.Format(sql, columns, values);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                ID = Context.ExecuteCommandScalar(cmd);
            }
        }

        protected override void OnModify(string columns)
        {
            var sql = @"
UPDATE [Course]
SET SemesterID = @SemesterID,
    {0},
    StartDate = @StartDate,
    EndDate = @EndDate,
    Url = @Url,
    GradeType = @GradeType
WHERE ID = @ID";

            sql = String.Format(sql, columns);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override void AppendCreateModifyCommandParameters(SqlCommand cmd)
        {
            base.AppendCreateModifyCommandParameters(cmd);

            cmd.Parameters.Add("@SemesterID", SqlDbType.Int).Value = semesterID;
            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
            cmd.Parameters.Add("@Url", SqlDbType.NVarChar).Value = url;
            cmd.Parameters.Add("@GradeType", SqlDbType.Int).Value = gradeType;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE Course WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }

        protected override Access GetAccess()
        {
            if (Context.User.IsInAdminRole())
            {
                return Access.All;
            }
            else if (Context.User.Roles.ContainsKey(this.ID))
            {
                if (Context.User.Roles[this.ID].RoleType == UserRoleType.Teacher)
                {
                    // Teacher
                    return new Access(true, true, true, false);
                }
                else
                {
                    // Student
                    return new Access(false, true, false, false);
                }
            }

            return Access.None;
        }
    }
}
