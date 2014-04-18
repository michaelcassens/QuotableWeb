using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace QuotableWeb
{
    public class DataAccess
    {
        string myConnectionString = ConfigurationManager.ConnectionStrings["External"].ToString();

        public DataAccess() { }

        public SqlConnection createConnection()
        {
            SqlConnection myConnection = new SqlConnection(myConnectionString);
            myConnection.Open();
            return myConnection;
        }

        public DataSet getQuery(string myQuery, params SqlParameter[] myParameters)
        {
            DataSet myDataSet = new DataSet();
            SqlCommand myCommand = new SqlCommand(myQuery);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddRange(myParameters);

            SqlConnection myConnection = createConnection();
            myCommand.Connection = myConnection;

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(myDataSet);
            myConnection.Close();
            return myDataSet;
        }

        public DataSet getQuery(string myQuery)
        {
            DataSet myDataSet = new DataSet();
            SqlCommand myCommand = new SqlCommand(myQuery);
            myCommand.CommandType = CommandType.StoredProcedure;

            SqlConnection myConnection = createConnection();
            myCommand.Connection = myConnection;

            SqlDataAdapter myAdapter = new SqlDataAdapter(myCommand);
            myAdapter.Fill(myDataSet);
            myConnection.Close();
            return myDataSet;
        }

        public void nonQuery(string myQuery)
        {
            SqlCommand myCommand = new SqlCommand(myQuery);
            SqlConnection myConnection = createConnection();
            myCommand.Connection = myConnection;
            myCommand.ExecuteNonQuery();
            myConnection.Close();

        }

        public int nonQuery(string myQuery, params SqlParameter[] myParameters)
        {
            SqlCommand myCommand = new SqlCommand(myQuery);
            myCommand.CommandType = CommandType.StoredProcedure;
            myCommand.Parameters.AddRange(myParameters);
            SqlConnection myConnection = createConnection();
            myCommand.Connection = myConnection;
            int test = myCommand.ExecuteNonQuery();
            myConnection.Close();

            return test;

        }

    }
}