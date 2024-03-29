﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;


namespace DataAccessLayer
{
    public class SqlDataAccess : IDatabaseAccessProvider
    {
        private SqlConnection _SqlConnection;
        private SqlDataAdapter _SqlAdapter;
        private SqlCommandBuilder _SqlCommandBuilder;

        private string _connectionString;

        public SqlDataAccess(string connectionString)
        {
            this._SqlConnection = new SqlConnection(connectionString);
            _connectionString = connectionString;
        }

        public void OpenConnection()
        {
            this._SqlConnection.Open();
        }

        public void CloseConnection()
        {
            this._SqlConnection.Close();
        }

        public DataTable GetTable(string tableName)
        {
            DataTable dataTable = new DataTable(tableName);
            this._SqlAdapter = new SqlDataAdapter(string.Format("SELECT * FROM {0};", (object)tableName), this._SqlConnection);
            this._SqlCommandBuilder = new SqlCommandBuilder(this._SqlAdapter);
            this._SqlAdapter.Fill(dataTable);
            return dataTable;
        }

        public DataTable Query(string queryString)
        {
            DataTable dataTable = new DataTable();
            this._SqlAdapter = new SqlDataAdapter(queryString, this._SqlConnection);
            this._SqlCommandBuilder = new SqlCommandBuilder(this._SqlAdapter);
            this._SqlAdapter.Fill(dataTable);
            return dataTable;
        }

        public void UpdateTable(DataTable table)
        {
            ((SqlDataAdapter)this._SqlAdapter).Update(table);
        }

        public DataSet ExecuteStoredProcedure(string procName, Dictionary<string, object> parameters)
        {
            DataSet dataSet;

            try
            {
                this._SqlConnection.Open();

                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in parameters)
                    {
                        var parameter = new SqlParameter(pair.Key, pair.Value);
                        SetSqlType(ref parameter, pair.Value);
                        command.Parameters.Add(parameter);
                    }
                }

                this._SqlAdapter = new SqlDataAdapter { SelectCommand = command };
                dataSet = new DataSet();
                this._SqlAdapter.Fill(dataSet);
            }
            finally
            {
                this._SqlConnection.Close();
            }
            return dataSet;
        }

        public ResponseModel ExecuteStoredProcedureWithReturnObject(string procName, Dictionary<string, object> parameters)
        {
            var result = new ResponseModel();

            try
            {
                this._SqlConnection.Open();

                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                foreach (KeyValuePair<string, object> pair in parameters)
                {
                    var parameter = new SqlParameter(pair.Key, pair.Value);
                    SetSqlType(ref parameter, pair.Value);
                    command.Parameters.Add(parameter);
                }

                var rowsAffected = command.ExecuteScalar();
                int Id = rowsAffected != null ? Convert.ToInt32(rowsAffected) : 0;
                if(Id != null)
                {
                    result.setId(Id);
                }
                result.success = true;
                result.message = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                result.success = false;
                result.message = ex.Message;
            }
            finally
            {
                this._SqlConnection.Close();
            }
            return result;
        }

        public ResponseModel ExecuteUpdateStoredProcedureWithReturnObject(string procName, Dictionary<string, object> parameters)
        {
            var result = new ResponseModel();

            try
            {
                this._SqlConnection.Open();

                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                foreach (KeyValuePair<string, object> pair in parameters)
                {
                    var parameter = new SqlParameter(pair.Key, pair.Value);
                    SetSqlType(ref parameter, pair.Value);
                    command.Parameters.Add(parameter);
                }

                var rowsAffected = command.ExecuteNonQuery(); // Use ExecuteNonQuery to get the number of rows affected

                result.success = rowsAffected > 0; // Check if rows were updated

                if (result.success)
                {
                    result.message = $"{rowsAffected} rows updated successfully.";
                }
                else
                {
                    result.message = "No rows were updated.";
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                result.success = false;
                result.message = ex.Message;
            }
            finally
            {
                this._SqlConnection.Close();
            }
            return result;
        }


        public bool ExecuteNonQueryStoredProcedure(string procName, Dictionary<string, object> parameters)
        {
            bool result;

            try
            {
                this._SqlConnection.Open();
                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in parameters)
                    {
                        var parameter = new SqlParameter(pair.Key, pair.Value);
                        SetSqlType(ref parameter, pair.Value);
                        command.Parameters.Add(parameter);
                    }
                }

                SqlParameter returnParameter = command.Parameters.Add("RetVal", SqlDbType.Int);
                returnParameter.Direction = ParameterDirection.ReturnValue;

                int rowsAffected = command.ExecuteNonQuery();
                int returnValue = (int)returnParameter.Value;

                // Check the return value to determine success or failure
                result = returnValue == 1;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                result = false;
            }
            finally
            {
                this._SqlConnection.Close();
            }

            return result;
        }


        public int ExecuteScalarStoredProcedure(string procName, Dictionary<string, object> parameters)
        {
            int result = 0;

            try
            {
                if (this._SqlConnection == null)
                {
                    // Initialize the SqlConnection object
                    this._SqlConnection = new SqlConnection(this._connectionString);
                }

                this._SqlConnection.Open();
                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in parameters)
                    {
                        var parameter = new SqlParameter(pair.Key, pair.Value);
                        SetSqlType(ref parameter, pair.Value);
                        command.Parameters.Add(parameter);
                    }
                }

                var objResult = command.ExecuteScalar();
                result = objResult != null ? Convert.ToInt32(objResult) : 0;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                result = 0;
            }
            finally
            {
                this._SqlConnection.Close();
            }

            return result;
        }




        public object ExecuteStoredProcedureWithReturnMessage(string procName, Dictionary<string, object> parameters)
        {
            var result = new ResponseModel();

            try
            {
                this._SqlConnection.Open();
                var command = new SqlCommand(procName, this._SqlConnection) { CommandType = CommandType.StoredProcedure };

                if (parameters != null)
                {
                    foreach (KeyValuePair<string, object> pair in parameters)
                    {
                        var parameter = new SqlParameter(pair.Key, pair.Value);
                        SetSqlType(ref parameter, pair.Value);
                        command.Parameters.Add(parameter);
                    }
                }

                int rowsAffected = command.ExecuteNonQuery();
               
                result.success = rowsAffected > 0;
                result.message = "";
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                Debug.WriteLine(ex.StackTrace);
                result.message = ex.Message;
                result.success = false;
            }
            finally
            {
                this._SqlConnection.Close();
            }
            return result;
        }

        private void SetSqlType(ref SqlParameter parameter, object value)
        {
            if (value is long)
            {
                parameter.SqlDbType = SqlDbType.BigInt;
            }
            else if (value is bool)
            {
                parameter.SqlDbType = SqlDbType.SmallInt;
            }
            else if (value is string)
            {
                parameter.SqlDbType = SqlDbType.NVarChar;
            }
            else if (value is byte[])
            {
                parameter.SqlDbType = SqlDbType.Image;
            }
            else if (value is int)
            {
                parameter.SqlDbType = SqlDbType.Int;
            }
            else if (value is DateTime)
            {
                parameter.SqlDbType = SqlDbType.DateTime2;
            }
        }
        public void Dispose()
        {
            if (this._SqlConnection == null)
                return;
            this._SqlConnection.Close();
        }
    }
}
