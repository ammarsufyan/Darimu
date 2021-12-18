using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassTransaksi
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        public static long get_saldo(string nama_pengguna)
        {
            long ambil_saldo = 0;

            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT saldo FROM tb_pengguna WHERE nama_pengguna = '" + nama_pengguna + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            if (dr.Read())
            {
                ambil_saldo = dr.GetInt64(0);
            }

            sqlcon.Close();
            return ambil_saldo;
        }
    }
}
