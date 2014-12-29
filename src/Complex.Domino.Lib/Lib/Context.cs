﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Complex.Domino.Lib
{
    public class Context : IDisposable
    {
        #region Private member variables

        private bool isValid;

        private int userId;
        private UserRoleType userRole;
        private string username;

        private SqlConnection databaseConnection;
        private SqlTransaction databaseTransaction;

        #endregion

        private string ConnectionString
        {
            get { return ConfigurationManager.ConnectionStrings["Domino"].ConnectionString; }
        }

        public int UserID
        {
            get { return userId; }
            set { userId = value; }
        }

        public UserRoleType UserRole
        {
            get { return userRole; }
            set { userRole = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        #region Contructors and initializers

        private Context()
        {
            InitializeMembers();
        }

        public static Context Create()
        {
            return new Context();
        }

        private void InitializeMembers()
        {
            this.isValid = true;
            this.userId = -1;
            this.username = null;
            this.databaseConnection = null;
            this.databaseTransaction = null;
        }

        public void Dispose()
        {

            if (databaseTransaction != null)
            {
                CommitTransaction();
            }

            if (databaseConnection != null)
            {
                CloseConnection();
            }

            isValid = false;
        }

        #endregion
        #region Connection and transaction functions

        private void EnsureOpenConnection()
        {
            if (!isValid)
            {
                throw new InvalidOperationException();
            }

            if (databaseConnection == null)
            {
                OpenConnection();
            }
        }

        public SqlConnection OpenConnection()
        {
            databaseConnection = new SqlConnection(ConnectionString);
            databaseConnection.Open();

            return databaseConnection;
        }

        private void CloseConnection()
        {
            if (databaseConnection != null)
            {
                if (databaseConnection.State != ConnectionState.Closed)
                {
                    databaseConnection.Close();
                }
                databaseConnection.Dispose();
                databaseConnection = null;
            }
        }

        private void EnsureOpenTransaction()
        {
            if (!isValid)
            {
                throw new InvalidOperationException();
            }

            if (databaseTransaction == null)
            {
                BeginTransaction();
            }
        }

        private SqlTransaction BeginTransaction()
        {
            EnsureOpenConnection();

            if (databaseTransaction == null)
            {
                databaseTransaction = databaseConnection.BeginTransaction();
            }

            return databaseTransaction;
        }

        /// <summary>
        /// Commits the current SQL Server transaction.
        /// </summary>
        public void CommitTransaction()
        {
            if (!isValid)
            {
                throw new InvalidOperationException();
            }

            if (databaseTransaction != null)
            {
                databaseTransaction.Commit();
                databaseTransaction.Dispose();
                databaseTransaction = null;
            }
        }

        /// <summary>
        /// Rolls back the current SQL Server transaction.
        /// </summary>
        public void RollbackTransaction()
        {
            if (!isValid)
            {
                throw new InvalidOperationException();
            }

            if (databaseTransaction != null)
            {
                databaseTransaction.Rollback();
                databaseTransaction.Dispose();
                databaseTransaction = null;
            }
        }

        #endregion
        #region Command helpers

        public SqlCommand CreateStoredProcedureCommand(string sql)
        {
            var cmd = new SqlCommand()
            {
                CommandText = sql,
                CommandTimeout = 30,        // TODO: from settings
                CommandType = CommandType.StoredProcedure,
            };

            return cmd;
        }

        #endregion
        #region Query execution helpers

        private void PrepareCommand(SqlCommand cmd)
        {
            EnsureOpenConnection();
            EnsureOpenTransaction();

            cmd.Connection = databaseConnection;
            cmd.Transaction = databaseTransaction;
        }

        public IEnumerable<T> ExecuteCommandReader<T>(SqlCommand cmd)
            where T : IDatabaseTableObject, new()
        {
            PrepareCommand(cmd);

            var dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.CloseConnection);

            return dr.AsEnumerable<T>();
        }

        public T ExecuteCommandSingleObject<T>(SqlCommand cmd)
            where T : IDatabaseTableObject, new()
        {
            PrepareCommand(cmd);

            using (var dr = cmd.ExecuteReader(CommandBehavior.SequentialAccess | CommandBehavior.SingleRow))
            {
                var o = dr.AsSingleObject<T>();

                if (o is ContextObject)
                {
                    ((ContextObject)(object)o).Context = this;
                }

                return o;
            }
        }

        public void ExecuteCommandNonQuery(SqlCommand cmd)
        {
            PrepareCommand(cmd);

            cmd.ExecuteNonQuery();
        }

        public string[] SplitQuery(string sql)
        {
            int i = 0;
            var res = new List<string>();

            while (true)
            {
                // Find the next GO statement
                var j = sql.IndexOf("GO", i, StringComparison.InvariantCultureIgnoreCase);

                if (j < 0)
                {
                    AddToScriptIfValid(res, sql.Substring(i));
                    break;
                }
                else
                {
                    AddToScriptIfValid(res, sql.Substring(i, j - i));
                }

                i = j + 2;
            }

            return res.ToArray();
        }

        private void AddToScriptIfValid(List<string> script, string sql)
        {
            if (!String.IsNullOrWhiteSpace(sql))
            {
                script.Add(sql.Trim());
            }
        }

        #endregion
    }
}
