using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using System.Data.Odbc; 

namespace SharpGLWPFApplication1
{
    class ODBCClass : IDisposable
    {
        /// <summary>
        /// OdbcConnection : This is the connection
        /// </summary>
        OdbcConnection oConnection;
        /// <summary>
        /// OdbcCommand : This is the command
        /// </summary>
        OdbcCommand oCommand;
        /// <summary>
        /// Constructor: This is the constructor
        /// </summary>
        /// <param name="DataSourceName">string: This is the data source name</param>
        public ODBCClass(string DataSourceName)
        {
            //Instantiate the connection
            oConnection = new OdbcConnection("Dsn=" + DataSourceName);
            try
            {
                //Open the connection
                oConnection.Open();
                //Notify the user that the connection is opened
                Console.WriteLine("The connection is established with the database");

            }
            catch (OdbcException caught)
            {
                Console.WriteLine(caught.Message);
                Console.Read();
            }
        }
        /// <summary>
        /// void: It is used to close the connection if you work within disconnected
        /// mode
        /// </summary>
        public void CloseConnection()
        {
            oConnection.Close();
        }
        /// <summary>
        /// OdbcCommand: This function returns a valid odbc connection
        /// </summary>
        /// <param name="Query">string: This is the SQL query</param>
        /// <returns></returns>
        public OdbcCommand GetCommand(string Query)
        {
            oCommand = new OdbcCommand();
            oCommand.Connection = oConnection;
            oCommand.CommandText = Query;
            return oCommand;
        }
        /// <summary>
        /// void: This method close the actual connection
        /// </summary>
        public void Dispose()
        {
            oConnection.Close();
        }
    }
}
