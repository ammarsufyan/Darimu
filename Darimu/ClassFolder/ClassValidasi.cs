using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace Darimu.ClassFolder
{
    class ClassValidasi
    {
        static Regex re_nama_pengguna = new Regex("^[a-zA-Z0-9_]{8,16}$");
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
                hasil = "Nama belum ada nih! Minimal nama depan ya";
            }
            else if (!re_huruf.IsMatch(val_nama_depan) || !re_huruf.IsMatch(val_nama_belakang))
            {
                hasil = "Nama harus huruf";
            }
            else if (String.Equals("", val_nama_pengguna) || String.Equals("nama pengguna", val_nama_pengguna) || !re_nama_pengguna.IsMatch(val_nama_pengguna))
            {
                hasil = "Nama pengguna belum diisi :) \nMinimal 8 huruf dan maksimal 16 huruf \nTanpa karakter spesial, kecuali (_)";
            }
            else if (String.Equals("", val_email) || String.Equals("Alamat Email", val_email) || !re_email.IsMatch(val_email))
            {
                hasil = "Email harus diisi dengan benar. Coba cek lagi!";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_kata_sandi.IsMatch(val_kata_sandi))
            {
                hasil = "Kata sandi harus diisi dengan benar \nMinimal 8 karakter dan maksimal 16 karakter \nSupaya akun kamu makin aman!";
            }
            else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text)))
            {
                hasil = "Yah, kata sandinya tidak sama :(";
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

            if (String.Equals("", val_username) || String.Equals("nama pengguna atau email", val_username))
            {
                hasil = "Isi nama pengguna atau email dengan benar ya!";
            }
            else if (String.Equals("", val_password) || String.Equals("password", val_password) || !re_nama_pengguna.IsMatch(val_password))
            {
                hasil = "Isi kata sandi dengan benar ya!";
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

            if (String.Equals("", val_nama_pengguna_atau_email) || String.Equals("nama pengguna atau email", val_nama_pengguna_atau_email))
            {
                hasil = "Isi nama pengguna atau emailmu yuk!";
            }
            else if (String.Equals("", val_kata_sandi) || String.Equals("password", val_kata_sandi) || !re_kata_sandi.IsMatch(val_kata_sandi))
            {
                hasil = "Isi kata sandi dengan benar ya :)";
            }
            else if (!(String.Equals(val_kata_sandi, konfirmasi_kata_sandi.Text)))
            {
                hasil = "Yah, kata sandinya tidak sama :(";
            }
            else if (String.Equals("", val_captcha) || String.Equals("captcha", val_captcha) || !String.Equals(angka_captcha.ToString(), val_captcha))
            {
                hasil = "Captcha masih salah nih. Coba lagi yuk!";
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
                hasil = "Isi saldomu dulu yuk!";
            }
            else if (!re_saldo.IsMatch(val_tambah_saldo))
            {
                hasil = "Isi saldo dengan angka ya :)";
            }
            else if (long.Parse(val_tambah_saldo) < 50000)
            {
                hasil = "Minimal isi saldo Rp50.000";
            }
            else if (bank.Image == null)
            {
                hasil = "Banknya belum dipilih nih!";
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

            if (String.Equals("", val_nama_depan) || !re_huruf.IsMatch(val_nama_depan))
            {
                hasil = "Isi nama depanmu dengan benar ya!";
            }
            else if (!re_huruf.IsMatch(val_nama_belakang))
            {
                hasil = "Isi nama belakangmu dengan benar ya!";
            }
            else if (String.Equals("", val_email) || !re_email.IsMatch(val_email))
            {
                hasil = "Isi emailmu dengan benar yuk!";
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
                hasil = "Isi nama impianmu dengan benar ya! :D";
            }
            else if (String.Equals("", val_saldo_impian) || !re_saldo.IsMatch(val_saldo_impian))
            {
                hasil = "Isi saldo impianmu dengan benar yuk! :D";
            }
            else if (validasi_button == false)
            {
                hasil = "Jenis impianmu belum dipilih nih!";
            }
            else if (long.Parse(val_saldo_impian) < 10000)
            {
                hasil = "Minimal saldo impianmu Rp10.000, ya.";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }

        public static string cekTambahSaldoImpian(TextBox tambah_saldo_impian)
        {
            string hasil = "";
            string val_saldo_impian = tambah_saldo_impian.Text.Trim();

            if (String.Equals("Masukkan Nominal Di sini", val_saldo_impian) || String.Equals("", val_saldo_impian))
            {
                hasil = "Isi saldomu dulu yuk!";
            }
            else if (!re_saldo.IsMatch(val_saldo_impian))
            {
                hasil = "Isi saldo dengan angka ya :)";
            }
            else if (long.Parse(val_saldo_impian) < 10000)
            {
                hasil = "Minimal isi saldo Rp10.000";
            }
            else
            {
                hasil = "valid";
            }
            return hasil;
        }

        public static string cekBuatLaporan(TextBox subjek_laporan, TextBox rincian_laporan)
        {
            string hasil = "";
            string val_subjek_laporan = subjek_laporan.Text.Trim();
            string val_rincian_laporan = rincian_laporan.Text.Trim();

            if (String.Equals("Ketik di sini...", val_subjek_laporan) || String.Equals("", val_subjek_laporan))
            {
                hasil = "Isi subjek laporanmu dengan benar ya!";
            }
            else if (String.Equals("Ketik di sini...", val_rincian_laporan) || String.Equals("", val_rincian_laporan))
            {
                hasil = "Isi rincian laporanmu dengan benar ya!";
            }
            else
            {
                hasil = "valid";
            }

            return hasil;
        }
    }
}