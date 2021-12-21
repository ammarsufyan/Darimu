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
        public static bool tambahImpian(string nama_pengguna, string nama_tabungan_impian, string jenis_impian, string tautan_gambar, string saldo_impian, string tenggat_waktu)
        {
            bool berhasil = false;
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT COUNT(*) FROM tb_tabungan_impian WHERE nama_pengguna = '" + nama_pengguna + "' AND status_tabungan_impian = 'Aktif'", sqlcon);
            Int32 count = Convert.ToInt32(sqlcom.ExecuteScalar());
            if(count > 2)
            {
                sqlcon.Close();
            } else
            {
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
                berhasil = true;
            }

            return berhasil;
        }

        public static ArrayList lihatImpian(string nama_pengguna) {
            ArrayList isi_impian = new ArrayList();
            
            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_tabungan_impian WHERE nama_pengguna = '" + nama_pengguna + "' AND status_tabungan_impian = 'Aktif'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            while (dr.Read())
            {
                isi_impian.Add(dr.GetString(1));
                isi_impian.Add(dr.GetString(3));
                isi_impian.Add(dr.GetString(5));
                isi_impian.Add(dr.GetInt64(6).ToString());
                isi_impian.Add(dr.GetInt64(7).ToString());
                isi_impian.Add(dr.GetDateTime(8).ToString("dd/MM/yyyy"));
            } 

            sqlcon.Close();

            return isi_impian;
        }

        public static long tambahSaldoImpian(string nama_pengguna, long isi_saldo_terkumpul, long saldo_impian, string id_tabungan_impian, string keterangan_impian)
        {
            long saldo_baru = ClassTransaksi.isi_saldo_impian(nama_pengguna, isi_saldo_terkumpul, keterangan_impian);
            if (saldo_impian < isi_saldo_terkumpul)
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
            } 
            else
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_tabungan_impian SET saldo_terkumpul = @saldo_terkumpul WHERE id_tabungan_impian = '" + id_tabungan_impian + "' AND status_tabungan_impian = 'Aktif'", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@saldo_terkumpul", SqlDbType.BigInt));
                sqlda.SelectCommand.Parameters["@saldo_terkumpul"].Value = isi_saldo_terkumpul;
                sqlda.SelectCommand.ExecuteNonQuery();
                sqlcon.Close();

                MessageBox.Show("Selamat! Anda berhasil tambah saldo impian",
                               "Tambah Saldo Impian Berhasil",
                               MessageBoxButtons.OK,
                               MessageBoxIcon.Information);
            }
            return saldo_baru;
        }

        public static long hapusImpian(string nama_pengguna, string id_tabungan_impian, long saldo_terkumpul, string keterangan)
        {
            long saldo_baru = ClassTransaksi.isi_saldo(nama_pengguna, saldo_terkumpul, keterangan);

            sqlcon.Open();
            SqlCommand sqlcom = new SqlCommand("UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Tidak Aktif', tanggal_tutup = GETDATE() WHERE id_tabungan_impian = '" + id_tabungan_impian + "'", sqlcon);
            sqlcom.ExecuteNonQuery();
            sqlcon.Close();
            return saldo_baru;
        }
    }
}