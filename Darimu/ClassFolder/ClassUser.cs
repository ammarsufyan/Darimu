using System;
using System.Data;
using System.Data.SqlClient;

namespace Darimu.ClassFolder
{
    class ClassUser
    {
        static SqlConnection sqlcon = new ClassKoneksi().getSQLCon();
        
        public static string hashPassword(string password)
        {
            string hashedPassword = "";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(ClassGaram.garamDapur(password));
            data = new System.Security.Cryptography.SHA512Managed().ComputeHash(data);

            hashedPassword = BitConverter.ToString(data).Replace("-", "");
            return hashedPassword;
        }

        public static string daftarUser(string nama_pengguna, String nama_lengkap, string tanggal_lahir, string alamat_email, string kata_sandi)
        {
            string hasil = "";
            try {
                sqlcon.Open();
                SqlCommand sqlcom = new SqlCommand("SELECT * FROM tb_pengguna WHERE alamat_email = '" + alamat_email + "'", sqlcon);
                SqlDataReader dr = sqlcom.ExecuteReader();

                if(dr.Read()){
                    hasil = "Email sudah terdaftar";
                } else {
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
                    sqlda.SelectCommand.Parameters["@kata_sandi"].Value = hashPassword(kata_sandi);
                    sqlda.SelectCommand.ExecuteNonQuery();

                    hasil = "Data berhasil disimpan";
                }
            }
            catch (Exception ex)
            {
                hasil = ex.Message;
            }
            finally
            {
                sqlcon.Close();
            }
            return hasil;
        }
        public static string ubahPassword(string nama_pengguna, string kata_sandi)
        {
            string hasil = "";
            try
            {
                sqlcon.Open();
                SqlDataAdapter sqlda = new SqlDataAdapter("UPDATE tb_pengguna SET kata_sandi = @kata_sandi WHERE nama_pengguna = @nama_pengguna", sqlcon);
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@nama_pengguna", SqlDbType.VarChar, 100));
                sqlda.SelectCommand.Parameters.Add(new SqlParameter("@kata_sandi", SqlDbType.NVarChar, 255));

                sqlda.SelectCommand.Parameters["@nama_pengguna"].Value = nama_pengguna;
                sqlda.SelectCommand.Parameters["@kata_sandi"].Value = hashPassword(kata_sandi);

                sqlda.SelectCommand.ExecuteNonQuery();
                hasil = "Kata sandi berhasil diubah";
            }
            catch (Exception ex)
            {
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