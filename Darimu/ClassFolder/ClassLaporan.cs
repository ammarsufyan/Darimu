using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassLaporan
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        public static void riwayat_laporan(string nama_pengguna, DataGridView grid_laporan)
        {
            grid_laporan.Rows.Clear();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_laporan_dikirim WHERE [NAMA PENGGUNA] = '" + nama_pengguna + "'", sqlcon);
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

        public static ArrayList rincian_laporan(string id_laporan)
        {
            sqlcon.Open();
            ArrayList data_rincian_laporan = new ArrayList();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_laporan_diterima WHERE [ID LAPORAN] = '" + id_laporan + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            if (dr.Read())
            {
                data_rincian_laporan.Add(dr.GetString(0));
                data_rincian_laporan.Add(dr.GetString(1));
                data_rincian_laporan.Add(dr.GetString(2));
                data_rincian_laporan.Add(dr.GetString(3));
                data_rincian_laporan.Add(dr.GetString(4));
                data_rincian_laporan.Add(dr.GetDateTime(5).ToString("dd/MM/yyyy"));
                data_rincian_laporan.Add(dr.GetDateTime(6).ToString("dd/MM/yyyy"));
                data_rincian_laporan.Add(dr.GetString(7));
            }

            sqlcon.Close();
            return data_rincian_laporan;
        }

        public static bool buat_laporan(string nama_pengguna, string subjek_alasan, string rincian_alasan)
        {
            sqlcon.Open();
            SqlDataAdapter sqlda = new SqlDataAdapter("INSERT INTO tb_pengguna (nama_pengguna, subjek_alasan, rincian_alasan, tanggal_laporan_dibuat, status_laporan) VALUES(@nama_pengguna, @subjek_alasan, @rincian_alasan, GETDATE(), 'Belum Selesai'", sqlcon);
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@subjek_alasan", SqlDbType.VarChar, 100));
            sqlda.SelectCommand.Parameters.Add(new SqlParameter("@rincian_alasan", SqlDbType.VarChar, 255));

            sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
            sqlda.SelectCommand.Parameters["@subjek_alasan"].Value = subjek_alasan;
            sqlda.SelectCommand.Parameters["@rincian_alasan"].Value = rincian_alasan;
            sqlda.SelectCommand.ExecuteNonQuery();
            sqlcon.Close();

            return true;
        }
    }
}
