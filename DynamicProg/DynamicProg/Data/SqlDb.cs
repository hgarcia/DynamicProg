using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using LaTrompa;

namespace DynamicProg.Data
{
    ///<summary>
    /// Wraps access to the database.
    ///</summary>
    public class SqlDb
    {
        private static string _connectionString;
        private static int _timeOut = 30;


        /// <summary>
        /// Will look in the web.config or app.config file for a Connection string entry with the key of "ConnectionString".
        /// </summary>
        /// <exception cref="ArgumentNullException"></exception>
        public SqlDb()
            : this(Utils.GetConnectionString("ConnectionString"))
        {

        }

        ///<summary>
        /// Constructor that specify a connection time out
        /// Will look in the web.config or app.config file for a Connection string entry with the key of "ConnectionString".
        ///</summary>
        ///<param name="timeOut">Time out in seconds, the default is 30 seconds</param>
        /// <exception cref="ArgumentNullException"></exception>
        public SqlDb(int timeOut)
            : this(Utils.GetConnectionString("ConnectionString"), timeOut)
        {

        }

        ///<summary>
        /// Constructor
        ///</summary>
        ///<param name="connectionString">Connection string to the database</param>
        ///<exception cref="ArgumentNullException"></exception>
        public SqlDb(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentNullException("connectionString");
            }
            _connectionString = connectionString;
        }

        ///<summary>
        /// Constructor that specify a connection time out
        ///</summary>
        ///<param name="connectionString">Connection string to the database</param>
        ///<param name="timeOut">Time out in seconds, the default is 30 seconds</param>
        ///<exception cref="ArgumentNullException"></exception>
        public SqlDb(string connectionString, int timeOut)
            : this(connectionString)
        {
            _timeOut = timeOut;
        }

        ///<summary>
        /// Sets the name of a stored procedure
        ///</summary>
        ///<param name="spName">The name of the stored procedure to run</param>
        ///<returns>A <see cref="SqlDbCommand"/> implementation</returns>
        public SqlDbCommand StoredProcedure(string spName)
        {
            return new SqlDbCommand(spName, CommandType.StoredProcedure);
        }

        ///<summary>
        ///Sets a raw sql query (we recommend using named parameters like @parameterName in the query
        ///</summary>
        /// <example>
        /// var sql = @"SELECT * FROM Lookup WHERE Lookup = @lookup";
        /// var parameterValue = "LicensorExclusivities";
        /// var lookups = new <c>SqlDb</c>.Query(sql).AddParameter("lookup",parameterValue).GetDataSet();
        /// </example>
        ///<param name="commandText">The sql query</param>
        ///<returns>A <see cref="SqlDbCommand"/> implementation</returns>
        public SqlDbCommand Query(string commandText)
        {
            return new SqlDbCommand(commandText, CommandType.Text);
        }

        ///<summary>
        ///Sets a CommandText of an specific type
        ///</summary>
        ///<param name="commandText">the command text</param>
        ///<param name="type">The command type</param>
        ///<returns>A <see cref="SqlDbCommand"/> implementation</returns>
        public SqlDbCommand Query(string commandText, CommandType type)
        {
            return new SqlDbCommand(commandText, type);
        }


        ///<summary>
        /// The SqlDbCommand to run
        ///</summary>
        public class SqlDbCommand
        {
            private readonly string _commandText;
            private readonly CommandType _type;
            private List<SqlParameter> _parameters = new List<SqlParameter>();

            ///<summary>
            ///Constructor
            ///</summary>
            ///<param name="commandTxt">The command text</param>
            ///<param name="type">The command type</param>
            ///<exception cref="ArgumentNullException"></exception>
            public SqlDbCommand(string commandTxt, CommandType type)
            {
                if (string.IsNullOrEmpty(commandTxt))
                {
                    throw new ArgumentNullException("commandTxt");
                }
                _commandText = commandTxt;
                _type = type;
            }

            ///<summary>
            /// Add parameters property
            ///</summary>
            ///<param name="name">Name of the parameter</param>
            ///<param name="value">Value of the parameter</param>
            ///<returns>A <see cref="SqlDbCommand"/> implementation</returns>
            public SqlDbCommand AddParameter(string name, object value)
            {
                _parameters.Add(new SqlParameter(name, value));
                return this;
            }

            ///<summary>
            /// Adds a bunch of parameters at once
            ///</summary>
            ///<param name="parameters">The parameters to add to the command</param>
            ///<returns>A <see cref="SqlDbCommand"/> implementation</returns>
            /// <exception cref="ArgumentNullException"><c>parameters</c> is null.</exception>
            public SqlDbCommand AddParameters(List<SqlParameter> parameters)
            {
                if (parameters == null)
                {
                    throw new ArgumentNullException("parameters");
                }
                _parameters = parameters;
                return this;
            }

            ///<summary>
            /// Equivalent to use ExecuteScalar in a <see cref="SqlCommand"/>
            ///</summary>
            ///<typeparam name="T">The type of the object to get from the database</typeparam>
            ///<returns>An object of type T</returns>
            public T GetUniqueResult<T>()
            {
                T result;
                SqlConnection connection;
                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand
                                             {
                                                 CommandText = _commandText,
                                                 Connection = connection,
                                                 CommandType = _type,
                                                 CommandTimeout = _timeOut
                                             })
                    {
                        command.Parameters.AddRange(_parameters.ToArray());
                        result = (T)command.ExecuteScalar();
                    }
                }
                return result;
            }

            ///<summary>
            /// Equivalent to use ExecuteScalar().ToString() in a <see cref="SqlCommand"/>
            ///</summary>
            ///<returns>A to string representation of an object</returns>
            public string GetUniqueResultToString()
            {
                var o = GetUniqueResult<object>();
                if (o != null)
                {
                    return o.ToString();
                }
                return string.Empty;
            }

            ///<summary>
            /// Returns the table with index 0 from a dataset
            ///</summary>
            ///<returns>A <see cref="DataTable"/></returns>
            public DataTable GetDataTable()
            {
                return GetDataTable(0);
            }

            ///<summary>
            /// Returns a table with the given index
            ///</summary>
            ///<param name="tableIndex">The index for the table</param>
            ///<returns>A <see cref="DataTable"/></returns>
            public DataTable GetDataTable(int tableIndex)
            {
                return GetDataSet().Tables[tableIndex];
            }

            ///<summary>
            /// Returns a <see cref="DataSet"/>
            ///</summary>
            ///<returns>A <see cref="DataSet"/></returns>
            public DataSet GetDataSet()
            {
                return GetDataSet("tableName");
            }

            ///<summary>
            /// Returns a <see cref="DataSet"/>
            ///</summary>
            /// <param name="dataSetName">The name of the table</param>
            ///<returns>A <see cref="DataSet"/></returns>
            /// <exception cref="ArgumentNullException">Argument is null.</exception>
            public DataSet GetDataSet(string dataSetName)
            {
                if (String.IsNullOrEmpty(dataSetName))
                {
                    throw new ArgumentNullException("dataSetName");
                }
                var result = new DataSet(dataSetName);
                SqlConnection connection;
                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand
                                             {
                                                 CommandText = _commandText,
                                                 Connection = connection,
                                                 CommandType = _type
                                             })
                    {
                        command.Parameters.AddRange(_parameters.ToArray());
                        using (var adapter = new SqlDataAdapter(command))
                        {
                            adapter.Fill(result);
                        }
                    }
                }
                return result;
            }
            ///<summary>
            /// Equivalent to use ExecuteNonQuery
            ///</summary>
            ///<returns>Number of records affected</returns>
            public int NonQuery()
            {
                int result;
                SqlConnection connection;
                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand
                                             {
                                                 CommandText = _commandText,
                                                 Connection = connection,
                                                 CommandType = _type,
                                                 CommandTimeout = _timeOut
                                             })
                    {
                        command.Parameters.AddRange(_parameters.ToArray());
                        result = command.ExecuteNonQuery();
                    }
                }
                return result;
            }

            ///<summary>
            /// Runs a stored procedure with output parameters.
            ///</summary>
            ///<param name="outParameterNames">The names of the parameters returned by the
            ///procedure</param>
            ///<typeparam name="T">The type of the returning parameter, if varies use a common
            ///interface or base class for all of them</typeparam>
            ///<returns>An implementation of <c>IDictionary</c> where the first item is the
            ///parameter name and the second the value</returns>
            /// <exception cref="ArgumentNullException"></exception>
            /// <exception cref="InvalidOperationException">If <see cref="CommandType"/> is not
            /// StoredProcedure.</exception>
            /// <exception cref="IndexOutOfRangeException">If the number of parameter names is
            /// higher than the parameters returned by the stored procedure</exception>
            /// <exception cref="InvalidCastException">If the output parameter can't be casted
            /// to T</exception>
            public IDictionary<string, T> GetOutParameters<T>(string[] outParameterNames)
            {
                if (_type != CommandType.StoredProcedure)
                {
                    throw new InvalidOperationException(
                        "This method can only be call when run against a Stored Procedure");
                }
                if (outParameterNames == null)
                {
                    throw new ArgumentNullException("outParameterNames");
                }

                IDictionary<string, T> results = new Dictionary<string, T>(outParameterNames.Length);
                SqlConnection connection;
                using (connection = new SqlConnection(_connectionString))
                {
                    connection.Open();
                    using (var command = new SqlCommand
                                             {
                                                 CommandText = _commandText,
                                                 Connection = connection,
                                                 CommandType = _type,
                                                 CommandTimeout = _timeOut
                                             })
                    {
                        command.Parameters.AddRange(_parameters.ToArray());
                        command.ExecuteNonQuery();
                        foreach (var k in outParameterNames)
                        {
                            results.Add(k, (T)command.Parameters[k].Value);
                        }
                    }
                }
                return results;
            }
        }
    }
}