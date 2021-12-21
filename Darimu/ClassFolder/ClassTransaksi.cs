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

        public static long isi_saldo_impian(string nama_pengguna, long saldo_terkumpul, string keterangan)
        {
            long saldo_lama = 0;
            long saldo_baru = 0;
            saldo_lama = get_saldo(nama_pengguna); 
            saldo_baru = saldo_lama - saldo_terkumpul;

            if(saldo_baru < 0)
            {
                saldo_baru = saldo_lama; 
                return saldo_baru;
            }
            else
            {
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
        }

        public static void riwayat_transaksi(string nama_pengguna, DataGridView grid_transaksi)
        {
            grid_transaksi.Rows.Clear();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_transaksi WHERE nama_pengguna = '" + nama_pengguna + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            while(dr.Read())
            {
                string tanggal = dr.GetDateTime(3).ToString();
                string keterangan = dr.GetString(4);
                string debit = dr.GetInt64(5).ToString();
                string kredit = dr.GetInt64(6).ToString();
                string saldo = dr.GetInt64(7).ToString();
                grid_transaksi.Rows.Add(tanggal, keterangan, debit, kredit, saldo);
            }
            sqlcon.Close();
        }
    }
}
