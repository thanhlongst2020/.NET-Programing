using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab6
{
    class clsDatabase
    {
        public static SqlConnection con;

        public static bool OpenConnection()
        {
            string conString = "Server=DESKTOP-0BFJ8KL\\SQLEXPRESS;Database=DVDLibrary;User Id=mylogin;Password=mylogin;";
            try
            {
                con = new SqlConnection(conString);
                con.Open();
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool CloseConnection()
        {
            try
            {
                con.Close();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

    }
}
