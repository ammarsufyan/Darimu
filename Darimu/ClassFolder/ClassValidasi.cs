using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassValidasi
    {
        static Regex re_nama_pengguna = new Regex("^[a-zA-Z0-9]{8,16}$");
        static Regex re_kata_sandi = new Regex(@"^[a-zA-Z0-9!@#$%^{}()\[\]\\/:,._`~'&*+\-?]{8,16}$"); 
        static Regex re_huruf = new Regex(@"^[a-z\sA-Z]+$");
        static Regex re_saldo = new Regex("^[0-9]+$");
        static Regex re_email = new Regex("^[a-zA-Z0-9._]+@[a-zA-Z0-9.]+\\.[a-zA-Z]{2,6}$");

        public static string cekPendaftaran(TextBox nama_depan, TextBox nama_belakang, TextBox email, TextBox nama_pengguna, TextBox kata_sandi, TextBox konfirmasi_kata_sandi)
        {
            string hasil;
            string val_nama_depan = nama_depan.Text.Trim();
            string val_nama_belakang = nama_belakang.Text.Trim();
            string val_nama_pengguna = nama_pengguna.Text.Trim();
            string val_email = email.Text.Trim();
            string val_kata_sandi = kata_sandi.Text;

            if (String.Equals("", val_nama_depan) || String.Equals("Nama Depan", val_nama_depan))
            {
                hasil = "Masukkan nama Anda (MINIMAL NAMA DEPAN)";
            }
            else if (!re_huruf.IsMatch(val_nama_depan) || !re_huruf.IsMatch(val_nama_belakang))
            {
                hasil = "Nama harus huruf";
            }
            else if (String.Equals("", val_nama_pengguna) || String.Equals("Nama Pengguna", val_nama_pengguna) || !re_nama_pengguna.IsMatch(val_nama_pengguna))
            {
                hasil = "Masukkan nama pengguna (MINIMAL 8 HURUF TANPA KARAKTER SPESIAL)";
            }
            else if (String.Equals("", val_email) || String.Equals("Alamat Email", val_email) || !re_email.IsMatch(val_email))
            {
                hasil = "Email harus diisi dengan benar";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_kata_sandi.IsMatch(val_kata_sandi))
            {
                hasil = "Kata sandi harus diisi dengan benar (MINIMAL 8 HURUF TANPA KARAKTER SPESIAL)";
            }
            else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text)))
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
            string val_password = password.Text;

            if (String.Equals("", val_username) || String.Equals("Nama Pengguna atau Email", val_username))
            {
                hasil = "Masukkan nama pengguna atau email dengan benar";
            }
            else if (String.Equals("", val_password) || String.Equals("password", val_password) || !re_nama_pengguna.IsMatch(val_password))
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
            string val_kata_sandi = kata_sandi.Text;
            string val_captcha = captcha.Text.Trim();

            if (String.Equals("", val_nama_pengguna_atau_email) || String.Equals("Nama Pengguna atau Email", val_nama_pengguna_atau_email))
            {
                hasil = "Masukkan nama pengguna atau email dengan benar";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_kata_sandi.IsMatch(val_kata_sandi))
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

        public static string cekTambahSaldo(TextBox tambah_saldo, PictureBox bank)
        {
            string hasil = "";
            string val_tambah_saldo = tambah_saldo.Text.Trim();

            if (String.Equals("", val_tambah_saldo))
            {
                hasil = "Mohon diisi saldonya terlebih dahulu";
            } 
            else if (!re_saldo.IsMatch(val_tambah_saldo))
            {
                hasil = "Saldo hanya dapat diisi oleh angka saja";
            }
            else if (long.Parse(val_tambah_saldo) < 50000)
            {
                hasil = "Minimal isi saldo Rp50.000";
            } else if (bank.Image == null)
            {
                hasil = "Silakan pilih bank dahulu";
            }
            else
            {
                hasil = "valid";
            }
            return hasil;
        }
        public static string cekUbahProfil(TextBox nama_depan, TextBox nama_belakang, TextBox email)
        {
            string hasil = "";
            string val_nama_depan = nama_depan.Text.Trim();
            string val_nama_belakang = nama_belakang.Text.Trim();
            string val_email = email.Text.Trim();

            if(String.Equals("", val_nama_depan) || !re_huruf.IsMatch(val_nama_depan)) {
                hasil = "Mohon isi nama depan dengan benar";
            } else if(!re_huruf.IsMatch(val_nama_belakang)) {
                hasil = "Mohon isi nama belakang dengan benar";
            } else if(String.Equals("", val_email) || !re_email.IsMatch(val_email)) {
                hasil = "Mohon isi email dengan benar";
            } 
            else
            {
                hasil = "valid";
            }

            return hasil;
        }

        public static string cekDaftarImpian(TextBox nama_impian, TextBox saldo_impian, bool validasi_button)
        {
            string hasil = "";
            string val_nama_impian = nama_impian.Text.Trim();
            string val_saldo_impian = saldo_impian.Text.Trim();

            if (String.Equals("", val_nama_impian) || !re_huruf.IsMatch(val_nama_impian))
            {
                hasil = "Mohon isi nama impian dengan benar";
            }
            else if (String.Equals("", val_saldo_impian) || !re_saldo.IsMatch(val_saldo_impian))
            {
                hasil = "Mohon isi saldo impian dengan benar";
            }
            else if (validasi_button == false)
            {
                hasil = "Mohon pilih jenis impianmu :)";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }
    }
}