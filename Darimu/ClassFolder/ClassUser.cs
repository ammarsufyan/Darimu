using System;
using System.Data;
using System.Data.SqlClient;

namespace Darimu.ClassFolder
{
    class ClassUser
    {
        SqlConnection sqlcon = new ClassKoneksi().getSQLCon();

        public string daftarUser(string nama_pengguna, String nama_lengkap, string tanggal_lahir, string alamat_email, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("INSERT INTO tb_pengguna (nama_pengguna, nama_lengkap, tanggal_lahir, alamat_email, kata_sandi, status_pengguna) VALUES(@nama_pengguna, @nama_lengkap, @tanggal_lahir, @alamat_email, HASHBYTES('SHA2_256', @kata_sandi), 'Aktif')", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_lengkap", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@tanggal_lahir", SqlDbType.Date));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@alamat_email", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.VarChar, 100));


                sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
                sqlda.SelectCommand.Parameters["@nama_lengkap"].Value = nama_lengkap;
                sqlda.SelectCommand.Parameters["@tanggal_lahir"].Value = tanggal_lahir;
                sqlda.SelectCommand.Parameters["@alamat_email"].Value = alamat_email;
                sqlda.SelectCommand.Parameters["@kata_sandi"].Value = kata_sandi;

                sqlda.SelectCommand.ExecuteNonQuery();
                hasil = "Data berhasil disimpan";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasil = ex.Message;
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }

        public string loginUser(string nama_pengguna, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("SELECT * FROM tb_pengguna WHERE nama_pengguna = @nama_pengguna AND kata_sandi = @kata_sandi", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.VarChar, 100));

                sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
                sqlda.SelectCommand.Parameters["@kata_sandi"].Value = kata_sandi;
                sqlda.SelectCommand.ExecuteNonQuery();
                hasil = "Sukses masuk";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasil = ex.Message;
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }

        public string ubahPassword(string nama_pengguna, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_pengguna SET kata_sandi = HASHBYTES('SHA2_256', @kata_sandi) WHERE nama_pengguna = @nama_pengguna", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.VarChar, 100));

                sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
                sqlda.SelectCommand.Parameters["@kata_sandi"].Value = kata_sandi;

                sqlda.SelectCommand.ExecuteNonQuery();
                hasil = "Kata sandi berhasil diubah";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                hasil = ex.Message;
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }
    }
}