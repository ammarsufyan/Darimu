using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Darimu.ClassFolder
{
    class ClassAdmin
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        public static bool cek_admin(string nama_pengguna_admin)
        {
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_admin WHERE nama_pengguna_admin = '" + nama_pengguna_admin + "' AND status_admin = 'Aktif'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            if(dr.Read())
            {
                return true;
            }
            sqlcon.Close();

            return false;
        }
    }
}
