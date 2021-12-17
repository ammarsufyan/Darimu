using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassValidasi
    {
        static Regex re_username_password = new Regex("[a-zA-Z0-9]{8,16}");
        static Regex re_email = new Regex("^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+\\.[a-zA-Z]{2,6}$");
        public static string cekAngka(string str)
        {
            string hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                Boolean cek = Char.IsDigit(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }

            return hasil;
        }

        public static string cekHuruf(string str)
        {
            string hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++)
            {
                Boolean cek = Char.IsLetter(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }

            return hasil;
        }

        public static string cekPendaftaran(TextBox nama_depan, TextBox email, TextBox nama_pengguna, TextBox kata_sandi, TextBox konfirmasi_kata_sandi)
        {
            string hasil;
            string val_nama_depan = nama_depan.Text.Trim();
            string cek_huruf_nama_depan = cekHuruf(val_nama_depan);
            string val_nama_pengguna = nama_pengguna.Text.Trim();
            string val_email = email.Text.Trim();
            string val_kata_sandi = kata_sandi.Text.Trim();

            if (String.Equals("", val_nama_depan) || String.Equals("Nama Depan", val_nama_depan))
            {
                hasil = "Masukkan nama Anda (MINIMAL NAMA DEPAN)";
            }
            else if (cek_huruf_nama_depan.Equals("false"))
            {
                hasil = "Nama harus huruf";
            }
            else if (String.Equals("", val_nama_pengguna) || String.Equals("Nama Pengguna", val_nama_pengguna) || !re_username_password.IsMatch(val_nama_pengguna))
            {
                hasil = "Masukkan nama pengguna (MINIMAL 8 HURUF TANPA SPASI)";
            }
            else if (String.Equals("", val_email) || String.Equals("Alamat Email", val_email) || !re_email.IsMatch(val_email))
            {
                hasil = "Email harus diisi dengan benar";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_username_password.IsMatch(val_kata_sandi))
            {
                hasil = "Kata sandi harus diisi dengan benar (MINIMAL 8 HURUF TANPA SPASI)";
            }
            else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text.Trim())))
            {
                hasil = "Kata sandi tidak sama";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }

        public static string cekMasuk(TextBox username, TextBox password)
        {
            string hasil;
            string val_username = username.Text.Trim();
            string val_password = password.Text.Trim();

            if (String.Equals("", val_username) || String.Equals("Nama Pengguna atau Email", val_username) || !re_username_password.IsMatch(val_username))
            {
                hasil = "Masukkan nama pengguna dengan benar";
            }
            else if (String.Equals("", val_password) || String.Equals("password", val_password) || !re_username_password.IsMatch(val_password))
            {
                hasil = "Masukkan kata sandi dengan benar";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }

        public static string cekLupaKataSandi(TextBox nama_pengguna_atau_email, TextBox kata_sandi, TextBox konfirmasi_kata_sandi, TextBox captcha, int angka_captcha)
        {
            string hasil;
            string val_nama_pengguna_atau_email = nama_pengguna_atau_email.Text.Trim();
            string val_kata_sandi = kata_sandi.Text.Trim();
            string val_captcha = captcha.Text.Trim();

            if (String.Equals("", val_nama_pengguna_atau_email) || String.Equals("Nama Pengguna atau Email", val_nama_pengguna_atau_email) || !re_username_password.IsMatch(val_nama_pengguna_atau_email))
            {
                hasil = "Masukkan nama pengguna dengan benar";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_username_password.IsMatch(val_kata_sandi))
            {
                hasil = "Masukkan kata sandi dengan benar";
            }
            else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text)))
            {
                hasil = "Kata sandi tidak sama";
            }
            else if (String.Equals("", val_captcha) || String.Equals("captcha", val_captcha) || !String.Equals(angka_captcha.ToString(), val_captcha))
            {
                hasil = "Masukkan captcha dengan benar";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }
    }
}