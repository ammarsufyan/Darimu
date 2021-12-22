using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

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

            if (dr.Read())
            {
                sqlcon.Close();
                return true;
            }

            sqlcon.Close();
            return false;
        }

        public static ArrayList lihatAdmin(string id_admin)
        {
            sqlcon.Open();
            ArrayList data_admin = new ArrayList();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_admin WHERE id_admin = '" + id_admin + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            if (dr.Read())
            {
                data_admin.Add(dr.GetString(2));
                data_admin.Add(dr.GetString(3));
                data_admin.Add(dr.GetDateTime(7).ToString("dd/MM/yyyy"));
                data_admin.Add(dr.GetString(8));
            }

            sqlcon.Close();
            return data_admin;
        }

        public static void riwayat_laporan_untuk_admin(DataGridView grid_laporan)
        {
            grid_laporan.Rows.Clear();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_laporan_dikirim WHERE [STATUS LAPORAN] = 'Belum Selesai'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            while (dr.Read())
            {
                string id_laporan = dr.GetString(0);
                string subjek_laporan = dr.GetString(2);
                string tanggal_dibuat = dr.GetDateTime(4).ToString();
                string tanggal_ditutup;
                try
                {
                    tanggal_ditutup = dr.GetDateTime(5).ToString();
                }
                catch (Exception ex)
                {
                    tanggal_ditutup = "Belum Ditentukan";
                }
                string status = dr.GetString(6);
                grid_laporan.Rows.Add(id_laporan, subjek_laporan, tanggal_dibuat, tanggal_ditutup, status);
            }
            sqlcon.Close();
        }

        public static ArrayList rincian_laporan_untuk_admin(string id_laporan)
        {
            sqlcon.Open();
            ArrayList data_rincian_laporan = new ArrayList();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_laporan_dikirim WHERE [ID LAPORAN] = '" + id_laporan + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            if (dr.Read())
            {
                data_rincian_laporan.Add(dr.GetString(0));
                data_rincian_laporan.Add(dr.GetString(1));
                data_rincian_laporan.Add(dr.GetString(2));
                data_rincian_laporan.Add(dr.GetString(3));
                data_rincian_laporan.Add(dr.GetDateTime(4).ToString("dd/MM/yyyy"));
            }

            sqlcon.Close();
            return data_rincian_laporan;
        }

        public static void selesaikan_laporan(string id_admin, string id_laporan)
        {
            sqlcon.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_laporan SET id_pengguna_admin = @id_pengguna_admin, tanggal_laporan_ditutup = GETDATE(), status_laporan = 'Selesai' WHERE id_laporan = @id_laporan", sqlcon);
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_pengguna_admin", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_laporan", SqlDbType.VarChar, 20));

            sqlda.SelectCommand.Parameters["@id_admin"].Value = id_admin;
            sqlda.SelectCommand.Parameters["@id_laporan"].Value = id_laporan;
            sqlda.SelectCommand.ExecuteNonQuery();
            sqlcon.Close();
        }

        public static void riwayat_transaksi_untuk_admin(DataGridView grid_transaksi)
        {
            grid_transaksi.Rows.Clear();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_transaksi", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            while (dr.Read())
            {
                string nama_pengguna = dr.GetString(0).ToString();
                string tanggal = dr.GetDateTime(1).ToString();
                string keterangan = dr.GetString(2);
                string debit = dr.GetInt64(3).ToString();
                string kredit = dr.GetInt64(4).ToString();
                string saldo = dr.GetInt64(5).ToString();
                grid_transaksi.Rows.Add(tanggal, nama_pengguna, keterangan, debit, kredit, saldo);
            }
            sqlcon.Close();
        }
    }
}
