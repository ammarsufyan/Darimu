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
        public static bool cekAdmin(string nama_pengguna_admin_atau_email, string kata_sandi)
        {
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_admin WHERE nama_pengguna_admin = '" + nama_pengguna_admin_atau_email + "' OR alamat_email = '" + nama_pengguna_admin_atau_email + "'AND kata_sandi = '" + kata_sandi + "' AND status_admin = 'Aktif'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            if(dr.Read())
            {
                sqlcon.Close();
                return true;
            }

            sqlcon.Close();
            return false;
        }
    }
}
