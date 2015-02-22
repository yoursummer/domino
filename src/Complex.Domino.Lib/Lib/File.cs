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
    public class File : Entity, IDatabaseTableObject
    {
        private int semesterID;
        private int courseID;
        private int assignmentID;
        private int pluginID;
        private string mimeType;

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

        public int PluginID
        {
            get { return pluginID; }
            set { pluginID = value; }
        }

        public string MimeType
        {
            get { return mimeType; }
            set { mimeType = value; }
        }

        public File()
        {
            InitializeMembers();
        }

        public File(Context context)
            : base(context)
        {
            InitializeMembers();
        }
        
        private void InitializeMembers()
        {
            this.semesterID = -1;
            this.courseID = -1;
            this.assignmentID = -1;
            this.pluginID = -1;
            this.mimeType = null;
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            this.semesterID = reader.GetInt32("SemesterID");
            this.courseID = reader.GetInt32("CourseID");
            this.assignmentID = reader.GetInt32("AssignmentID");
            this.pluginID = reader.GetInt32("PluginID");
            this.mimeType = reader.GetString("MimeType");

            base.LoadFromDataReader(reader);
        }

        protected override void OnLoad(int id)
        {
            var sql = @"
SELECT f.ID, f.PluginID, p.SemesterID, p.CourseID, p.AssignmentID,
       f.Name, f.Description, f.Hidden, f.ReadOnly, f.CreatedDate, f.ModifiedDate, f.Comments,
       f.MimeType
FROM [File] f
INNER JOIN [Plugin] p ON p.ID = f.PluginID
WHERE f.ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void OnCreate(string columns, string values)
        {
            var sql = @"
INSERT [File]
    (PluginID, MimeType, Blob, {0})
VALUES
    (@PluginID, @MimeType, NULL, {1})

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
UPDATE [File]
SET Plugin = @PluginID,
    CouignmentID = @AssignmentID,
    MimeType = @MimeType,
    {0}
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

            cmd.Parameters.Add("@PluginID", SqlDbType.Int).Value = pluginID > 0 ? (object)pluginID : DBNull.Value;
            cmd.Parameters.Add("@MimeType", SqlDbType.VarChar).Value = mimeType;
        }

        protected override void OnDelete(int id)
        {
            var sql = "DELETE [File] WHERE ID = @ID";

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
            else if (Context.User.Roles.ContainsKey(this.courseID))
            {
                if (Context.User.Roles[this.courseID].RoleType == UserRoleType.Teacher)
                {
                    // Teacher
                    return new Access(true, true, true, false);
                }
                else if (Context.User.Roles[this.courseID].RoleType == UserRoleType.Student)
                {
                    // Student
                    return new Access(false, true, false, false);
                }
            }

            return Access.None;
        }
    }
}
