using System;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassValidasi
    {
        static Regex re_username_password = new Regex("[a-zA-Z0-9]{8,16}");
        static Regex re_email = new Regex("^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+\\.[a-zA-Z]{2,6}$");
        public static string cekAngka(string str) {
            string hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++) {
                Boolean cek = Char.IsDigit(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }

            return hasil;
        }

        public string cekHuruf(string str) {
            string hasil = "false";

            char[] data = str.ToCharArray();
            for (int i = 0; i < data.Length; i++) {
                Boolean cek = Char.IsLetter(data[i]);
                if (cek == true)
                {
                    hasil = "true";
                }
            }

            return hasil;
        }

        public static string cekPendaftaran(TextBox nama_depan, TextBox email, TextBox nama_pengguna, TextBox kata_sandi, TextBox konfirmasi_kata_sandi) {
            string hasil;
            string val_nama_depan = nama_depan.Text.Trim();
            string val_nama_pengguna = nama_pengguna.Text.Trim();
            string val_email = email.Text.Trim();
            string val_kata_sandi = kata_sandi.Text.Trim();
            
            if(String.Equals("", val_nama_depan) || String.Equals("Nama Depan", val_nama_depan)) {
                hasil = "MASUKKAN NAMA ANDA (MINIMAL NAMA DEPAN)";
            } else if(String.Equals("", val_nama_pengguna) || String.Equals("Nama Pengguna", val_nama_pengguna) || !re_username_password.IsMatch(val_nama_pengguna)) {
                hasil = "MASUKKAN NAMA PENGGUNA (MINIMAL 8 HURUF TANPA SPASI)";
            } else if(String.Equals("", val_email) || String.Equals("Alamat Email", val_email) || !re_email.IsMatch(val_email)) {
                hasil = "EMAIL HARUS DIISI DENGAN BENAR";
            } else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_username_password.IsMatch(val_kata_sandi)) {
                hasil = "KATA SANDI HARUS DIISI DENGAN BENAR (MINIMAL 8 HURUF TANPA SPASI)";
            } else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text.Trim()))) {
                hasil = "KATA SANDI TIDAK SAMA";
            } else {
                hasil = "valid";
            }

            return hasil;
        }

        public static string cekMasuk(TextBox username, TextBox password)
        {
            string hasil;
            string val_username = username.Text.Trim();
            string val_password = password.Text.Trim();

            if (String.Equals("", val_username) || String.Equals("Nama Pengguna atau Email", val_username) || !re_username_password.IsMatch(val_username)) {
                hasil = "MASUKKAN NAMA PENGGUNA DENGAN BENAR";
            }
            else if (String.Equals("", val_password) || String.Equals("password", val_password) || !re_username_password.IsMatch(val_password)) {
                hasil = "MASUKKAN KATA SANDI DENGAN BENAR";
            }
            else {
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

            if (String.Equals("", val_nama_pengguna_atau_email) || String.Equals("Nama Pengguna atau Email", val_nama_pengguna_atau_email) || !re_username_password.IsMatch(val_nama_pengguna_atau_email)) {
                hasil = "MASUKKAN NAMA PENGGUNA DENGAN BENAR";
            } else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_username_password.IsMatch(val_kata_sandi)) {
                hasil = "MASUKKAN KATA SANDI DENGAN BENAR";
            } else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text))) {
                hasil = "KATA SANDI TIDAK SAMA";
            } else if (String.Equals("", val_captcha) || String.Equals("captcha", val_captcha) || !String.Equals(angka_captcha.ToString(), val_captcha)) {
                hasil = "MASUKKAN CAPTCHA DENGAN BENAR";
            } else {
                hasil = "valid";
            }

            return hasil;
        }
    }
}