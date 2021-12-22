using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassTabunganImpian
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        public static bool tambahImpian(string id_pengguna, string id_jenis_impian, string nama_tabungan_impian, string saldo_impian, string tenggat_waktu)
        {
            bool berhasil = false;
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT COUNT(*) FROM tb_tabungan_impian WHERE id_pengguna = '" + id_pengguna + "' AND status_tabungan_impian = 'Aktif'", sqlcon);
            Int32 count = Convert.ToInt32(sqlcom.ExecuteScalar());
            if (count > 2)
            {
                sqlcon.Close();
            }
            else
            {
                SqlDataAdapter sqlda = new SqlDataAdapter("INSERT INTO tb_tabungan_impian(id_pengguna, id_jenis_impian, nama_tabungan_impian, saldo_terkumpul, saldo_impian, tenggat_waktu, tanggal_buka, status_tabungan_impian) VALUES(@id_pengguna, @id_jenis_impian, @nama_tabungan_impian, 0, @saldo_impian, @tenggat_waktu, GETDATE(), 'Aktif')", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_pengguna", SqlDbType.VarChar, 20));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_jenis_impian", SqlDbType.VarChar, 20));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_tabungan_impian", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@saldo_impian", SqlDbType.BigInt));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tenggat_waktu", SqlDbType.DateTime));

                sqlda.SelectCommand.Parameters["@id_pengguna"].Value = id_pengguna;
                sqlda.SelectCommand.Parameters["@id_jenis_impian"].Value = id_jenis_impian;
                sqlda.SelectCommand.Parameters["@nama_tabungan_impian"].Value = nama_tabungan_impian;
                sqlda.SelectCommand.Parameters["@saldo_impian"].Value = saldo_impian;
                sqlda.SelectCommand.Parameters["@tenggat_waktu"].Value = tenggat_waktu;
                sqlda.SelectCommand.ExecuteNonQuery();

                sqlcon.Close();
                berhasil = true;
            }

            return berhasil;
        }

        public static ArrayList lihatImpian(string id_pengguna)
        {
            ArrayList isi_impian = new ArrayList();

            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_isi_tabungan_impian WHERE [ID PENGGUNA] = '" + id_pengguna + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            while (dr.Read())
            {
                isi_impian.Add(dr.GetString(0));
                isi_impian.Add(dr.GetString(2));
                isi_impian.Add(dr.GetString(3));
                isi_impian.Add(dr.GetInt64(4).ToString());
                isi_impian.Add(dr.GetInt64(5).ToString());
                isi_impian.Add(dr.GetDateTime(6).ToString("dd/MM/yyyy"));
            }

            sqlcon.Close();

            return isi_impian;
        }

        public static long tambahSaldoImpian(string id_pengguna, long isi_saldo_tabungan_impian, long saldo_terkumpul, long saldo_impian, string id_tabungan_impian, string keterangan_impian)
        {
            saldo_terkumpul += isi_saldo_tabungan_impian;
            long saldo_baru = ClassTransaksi.isi_saldo_impian(id_pengguna, isi_saldo_tabungan_impian, saldo_terkumpul, saldo_impian, keterangan_impian);
            if (saldo_impian < saldo_terkumpul)
            {
                MessageBox.Show("Wah, kamu tidak bisa topup melebihi saldo impian :).",
                                "Gagal Menambah Saldo Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else if (saldo_baru < 0)
            {
                MessageBox.Show("Maaf, duit kamu kurang nih. :(",
                                "Gagal Menambah Saldo Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
                saldo_baru = ClassTransaksi.get_saldo(id_pengguna);
            }
            else
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_tabungan_impian SET saldo_terkumpul = @saldo_terkumpul WHERE id_tabungan_impian = '" + id_tabungan_impian + "' AND status_tabungan_impian = 'Aktif'", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@saldo_terkumpul", SqlDbType.BigInt));
                sqlda.SelectCommand.Parameters["@saldo_terkumpul"].Value = saldo_terkumpul;
                sqlda.SelectCommand.ExecuteNonQuery();
                sqlcon.Close();

                MessageBox.Show("Selamat! Anda berhasil tambah saldo impian",
                               "Tambah Saldo Impian Berhasil",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            return saldo_baru;
        }

        public static long hapusImpian(string id_pengguna, string id_tabungan_impian, long saldo_terkumpul, string keterangan)
        {
            long saldo_baru = ClassTransaksi.isi_saldo(id_pengguna, saldo_terkumpul, keterangan);

            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Tidak Aktif', tanggal_tutup = GETDATE() WHERE id_tabungan_impian = '" + id_tabungan_impian + "'", sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            return saldo_baru;
        }

        public static void riwayat_impian(string id_pengguna, DataGridView grid_riwayat_impian)
        {
            grid_riwayat_impian.Rows.Clear();
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM view_riwayat_tabungan_impian WHERE [ID PENGGUNA] = '" + id_pengguna + "' ORDER BY [TANGGAL TUTUP] DESC;", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            while (dr.Read())
            {
                string nama_impian = dr.GetString(1);
                string jenis_impian = dr.GetString(2);
                string saldo_terkumpul = dr.GetInt64(3).ToString();
                string saldo_impian = dr.GetInt64(4).ToString();
                string tanggal_ditutup = dr.GetDateTime(5).ToString();
                string status;

                if (dr.GetInt64(3) >= dr.GetInt64(4))
                {
                    status = "Berhasil";
                }
                else
                {
                    status = "Gagal";
                }
                
                grid_riwayat_impian.Rows.Add(tanggal_ditutup,nama_impian,jenis_impian,saldo_terkumpul,saldo_impian,status);
            }
            sqlcon.Close();
        }
    }
}