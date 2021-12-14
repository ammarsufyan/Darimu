using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassKoneksi
    {
        SqlConnection sqlcon = new SqlConnection();

        public SqlConnection getSQLCon()
        {
            try
            {
                sqlcon = new SqlConnection("Data Source=MARS-PC; Initial Catalog=db_darimu; User ID=sa; Password=ammar1234");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return sqlcon;
        }
    }
}
