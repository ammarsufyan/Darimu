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

        public static long isi_saldo (string nama_pengguna, long saldo, string keterangan)
        {
            long saldo_baru = 0;
            saldo_baru = saldo + get_saldo(nama_pengguna);
            SqlCommand sqlcom;

            sqlcon.Open();
            sqlcom = new SqlCommand("UPDATE tb_pengguna SET saldo = '" + saldo_baru + "' WHERE nama_pengguna = '" + nama_pengguna + "'", sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();

            sqlcon.Open();
            sqlcom = new SqlCommand("UPDATE tb_transaksi SET keterangan = '" + keterangan + "' WHERE id_transaksi = (SELECT TOP 1 id_transaksi FROM tb_transaksi WHERE nama_pengguna = '" + nama_pengguna + "' ORDER BY tanggal DESC);", sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();

            return saldo_baru;
        }

        public static void riwayat_transaksi(string nama_pengguna, DataGridView grid_transaksi)
        {
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_transaksi WHERE nama_pengguna = '" + nama_pengguna + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            while(dr.Read())
            {
                string tanggal = dr.GetDateTime(0).ToString("yyyy/mm/dd");
                string keterangan = dr.GetString(1);
                string debit = dr.GetInt64(2).ToString();
                string kredit = dr.GetInt64(3).ToString();
                string saldo = dr.GetInt64(4).ToString();
                grid_transaksi.Rows.Add(tanggal, keterangan, debit, kredit, saldo);
            }
            sqlcon.Close();
        }
    }
}
