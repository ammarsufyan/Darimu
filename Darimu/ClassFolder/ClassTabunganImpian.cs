using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Darimu.ClassFolder
{
    class ClassTabunganImpian
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        public static string tambahImpian(string nama_pengguna, string nama_tabungan_impian, string jenis_impian, string tautan_gambar, string saldo_impian, string tenggat_waktu)
        {
            sqlcon.Open();
            string hasil = "";
            SqlDataAdapter sqlda = new SqlDataAdapter("INSERT INTO tb_tabungan_impian(nama_pengguna, nama_tabungan_impian, jenis_impian, tautan_gambar, saldo_terkumpul, saldo_impian, tenggat_waktu, tanggal_buka, status_tabungan_impian) VALUES(@nama_pengguna, @nama_tabungan_impian, @jenis_impian, @tautan_gambar, 0, @saldo_impian, @tenggat_waktu, GETDATE(), 'Aktif')", sqlcon);
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_tabungan_impian", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@jenis_impian", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tautan_gambar", SqlDbType.VarChar, 255));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@saldo_impian", SqlDbType.BigInt));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tenggat_waktu", SqlDbType.DateTime));

            sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
            sqlda.SelectCommand.Parameters["@nama_tabungan_impian"].Value = nama_tabungan_impian;
            sqlda.SelectCommand.Parameters["@jenis_impian"].Value = jenis_impian;
            sqlda.SelectCommand.Parameters["@tautan_gambar"].Value = tautan_gambar;
            sqlda.SelectCommand.Parameters["@saldo_impian"].Value = saldo_impian;
            sqlda.SelectCommand.Parameters["@tenggat_waktu"].Value = tenggat_waktu;
            sqlda.SelectCommand.ExecuteNonQuery();

            sqlcon.Close();
            hasil = "Selamat! Impian Anda telah terdaftar";

            return hasil;
        }
    }
}