﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class Semester : Entity, IDatabaseTableObject
    {
        private DateTime startDate;
        private DateTime endDate;

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

        public Semester()
        {
            InitializeMembers();
        }

        public Semester(Context context)
            : base(context)
        {
            InitializeMembers();
        }

        private void InitializeMembers()
        {
            this.startDate = new DateTime(DateTime.Now.Year, 1, 1);
            this.endDate = new DateTime(DateTime.Now.Year, 12, 31);
        }

        public override void LoadFromDataReader(SqlDataReader reader)
        {
            base.LoadFromDataReader(reader);

            this.startDate = reader.GetDateTime("StartDate");
            this.endDate = reader.GetDateTime("EndDate");
        }

        public override void Load(int id)
        {
            var sql = @"
SELECT *
FROM [Semester]
WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;

                Context.ExecuteCommandSingleObject(cmd, this);
            }
        }

        protected override void Create(string columns, string values)
        {
            var sql = @"
INSERT [Semester]
    ({0}, StartDate, EndDate)
VALUES
    ({1}, @StartDate, @EndDate)

SELECT @@IDENTITY
";

            sql = String.Format(sql, columns, values);

            using (var cmd = Context.CreateCommand(sql))
            {
                AppendCreateModifyCommandParameters(cmd);
                ID = Context.ExecuteCommandScalar(cmd);
            }
        }

        protected override void Modify(string columns)
        {
            var sql = @"
UPDATE [Semester]
SET {0}
    StartDate = @StartDate,
    EndDate = @EndDate
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

            cmd.Parameters.Add("@StartDate", SqlDbType.DateTime).Value = startDate;
            cmd.Parameters.Add("@EndDate", SqlDbType.DateTime).Value = endDate;
        }

        public override void Delete(int id)
        {
            var sql = "DELETE Semester WHERE ID = @ID";

            using (var cmd = Context.CreateCommand(sql))
            {
                cmd.Parameters.Add("@ID", SqlDbType.Int).Value = id;
                Context.ExecuteCommandNonQuery(cmd);
            }
        }
    }
}
