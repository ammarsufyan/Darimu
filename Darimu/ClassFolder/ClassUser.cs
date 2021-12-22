using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace Darimu.ClassFolder
{
    class ClassUser
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();

        public static string hashKataSandi(string kata_sandi)
        {
            string hashedKataSandi = "";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(ClassGaram.garamDapur(kata_sandi));
            data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);
            hashedKataSandi = BitConverter.ToString(data).Replace("-", "");
            return hashedKataSandi;
        }

        public static string daftarPengguna(string nama_pengguna, string nama_lengkap, string tanggal_lahir, string alamat_email, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE nama_pengguna = '" + nama_pengguna + "' OR alamat_email = '" + alamat_email + "'", sqlcon);
                SqlDataReader dr = sqlcom.ExecuteReader();

                if (dr.Read())
                {
                    string ambil_nama_pengguna = dr.GetString(2);
                    if (ambil_nama_pengguna == nama_pengguna)
                    {
                        hasil = "Nama pengguna sudah terdaftar";
                    }
                    else
                    {
                        hasil = "Email sudah terdaftar";
                    }
                }
                else
                {
                    dr.Close();

                    SqlDataAdapter sqlda = new SqlDataAdapter("INSERT INTO tb_pengguna (nama_pengguna, nama_lengkap, tanggal_lahir, alamat_email, kata_sandi, saldo, tanggal_buka, status_pengguna) VALUES(@nama_pengguna, @nama_lengkap, @tanggal_lahir, @alamat_email, @kata_sandi, 0, GETDATE(), 'Aktif')", sqlcon);
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_lengkap", SqlDbType.VarChar, 100));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tanggal_lahir", SqlDbType.Date));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@alamat_email", SqlDbType.VarChar, 100));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.NVarChar, 255));

                    sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
                    sqlda.SelectCommand.Parameters["@nama_lengkap"].Value = nama_lengkap;
                    sqlda.SelectCommand.Parameters["@tanggal_lahir"].Value = tanggal_lahir;
                    sqlda.SelectCommand.Parameters["@alamat_email"].Value = alamat_email;
                    sqlda.SelectCommand.Parameters["@kata_sandi"].Value = hashKataSandi(kata_sandi);
                    sqlda.SelectCommand.ExecuteNonQuery();

                    hasil = "sukses";
                }
            }
            catch (Exception ex)
            {
                hasil = "Gagal, telah terjadi kesalahan.";
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }
        public static string ubahKataSandi(string nama_pengguna_atau_email, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE nama_pengguna = '" + nama_pengguna_atau_email + "' OR alamat_email = '" + nama_pengguna_atau_email + "'", sqlcon);
                SqlDataReader dr = sqlcom.ExecuteReader();
                if (dr.Read())
                {
                    dr.Close();
                    SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_pengguna SET kata_sandi = @kata_sandi WHERE nama_pengguna = @nama_pengguna_atau_email OR alamat_email = @nama_pengguna_atau_email", sqlcon);
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna_atau_email", SqlDbType.VarChar, 100));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.NVarChar, 255));

                    sqlda.SelectCommand.Parameters["@nama_pengguna_atau_email"].Value = nama_pengguna_atau_email;
                    sqlda.SelectCommand.Parameters["@kata_sandi"].Value = hashKataSandi(kata_sandi);

                    sqlda.SelectCommand.ExecuteNonQuery();
                    hasil = "Kata sandi berhasil diubah";
                    sqlcon.Close();
                }
                else
                {
                    dr.Close();
                    sqlcon.Close();
                    hasil = "Nama pengguna atau email tidak ditemukan";
                }
            }
            catch (Exception ex)
            {
                hasil = "Kata sandi gagal diubah";
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }

        public static string cekMasuk(string nama_pengguna_atau_email, string kata_sandi)
        {
            sqlcon.Open();
            string id_pengguna = "gagal";
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE (nama_pengguna = '" + nama_pengguna_atau_email + "' OR alamat_email = '" + nama_pengguna_atau_email + "') AND kata_sandi = '" + kata_sandi + "' AND status_pengguna = 'Aktif'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();

            if (dr.Read())
            {
                id_pengguna = dr.GetString(1).ToString();
                sqlcon.Close();
                return id_pengguna;
            }

            sqlcon.Close();
            return id_pengguna;
        }

        public static ArrayList lihatPengguna(string id_pengguna)
        {
            sqlcon.Open();
            ArrayList data_pengguna = new ArrayList();
            SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE id_pengguna = '" + id_pengguna + "'", sqlcon);
            SqlDataReader dr = sqlcom.ExecuteReader();
            if (dr.Read())
            {
                data_pengguna.Add(dr.GetString(2));
                data_pengguna.Add(dr.GetString(3));
                data_pengguna.Add(dr.GetDateTime(4).ToString("dd/MM/yyyy"));
                data_pengguna.Add(dr.GetString(5));
                data_pengguna.Add(dr.GetString(6));
                data_pengguna.Add(dr.GetInt64(7));
                data_pengguna.Add(dr.GetDateTime(8));
            }

            sqlcon.Close();
            return data_pengguna;
        }

        public static string ubah_data_pengguna(string id_pengguna, string nama_lengkap, string tanggal_lahir, string alamat_email)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE alamat_email = '" + alamat_email + "'", sqlcon);
                SqlDataReader dr = sqlcom.ExecuteReader();

                if (dr.Read())
                {
                    hasil = "Email sudah terdaftar";
                }
                else
                {
                    dr.Close();

                    SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_pengguna SET nama_lengkap = @nama_lengkap, tanggal_lahir = @tanggal_lahir, alamat_email = @alamat_email WHERE id_pengguna = @id_pengguna", sqlcon);
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_pengguna", SqlDbType.VarChar, 20));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_lengkap", SqlDbType.VarChar, 100));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tanggal_lahir", SqlDbType.Date));
                    sqlda.SelectCommand.Parameters.Add(new SqlParameter("@alamat_email", SqlDbType.VarChar, 100));

                    sqlda.SelectCommand.Parameters["@id_pengguna"].Value = id_pengguna;
                    sqlda.SelectCommand.Parameters["@nama_lengkap"].Value = nama_lengkap;
                    sqlda.SelectCommand.Parameters["@tanggal_lahir"].Value = tanggal_lahir;
                    sqlda.SelectCommand.Parameters["@alamat_email"].Value = alamat_email;
                    sqlda.SelectCommand.ExecuteNonQuery();

                    hasil = "Data Berhasil Diubah";
                }
            }
            catch (Exception ex)
            {
                hasil = "Data Gagal Diubah";
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }

        public static string ubah_data_pengguna_tanpa_email(string id_pengguna, string nama_lengkap, string tanggal_lahir)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_pengguna SET nama_lengkap = @nama_lengkap, tanggal_lahir = @tanggal_lahir WHERE id_pengguna = @id_pengguna", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@id_pengguna", SqlDbType.VarChar, 20));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_lengkap", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tanggal_lahir", SqlDbType.Date));

                sqlda.SelectCommand.Parameters["@id_pengguna"].Value = id_pengguna;
                sqlda.SelectCommand.Parameters["@nama_lengkap"].Value = nama_lengkap;
                sqlda.SelectCommand.Parameters["@tanggal_lahir"].Value = tanggal_lahir;
                sqlda.SelectCommand.ExecuteNonQuery();

                hasil = "Data Berhasil Diubah";
            }
            catch (Exception ex)
            {
                hasil = "Data Gagal Diubah";
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }
    }
}