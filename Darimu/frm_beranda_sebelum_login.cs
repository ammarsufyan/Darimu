using Darimu.ClassFolder;
using System;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace Darimu
{
    public partial class frm_beranda_sebelum_login : Form
    {
        // declaration var
        int kesempatan_masuk = 3;
        int angka = 0;
        public static string nama_pengguna, kata_sandi, nama_lengkap, tanggal_lahir, alamat_email, saldo, tanggal_buka, tanggal_tutup, status_pengguna;
        private int borderSize = 2;

        public frm_beranda_sebelum_login()
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(33, 106, 155);
            panel_isi_beranda.Visible = true;
        }

        // drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // change all button and label color to white
        private void default_color()
        {
            button_beranda.ForeColor = System.Drawing.Color.White;
            button_profil.ForeColor = System.Drawing.Color.White;
            button_tabungan.ForeColor = System.Drawing.Color.White;
            button_faq.ForeColor = System.Drawing.Color.White;
            button_tentang_kami.ForeColor = System.Drawing.Color.White;
            button_masuk_sidebar.ForeColor = System.Drawing.Color.White;
        }

        // hide all panel
        private void hide_panel()
        {
            panel_isi_beranda.Visible = false;
            panel_isi_daftar.Visible = false;
            panel_isi_masuk.Visible = false;
            panel_isi_faq.Visible = false;
            panel_isi_tentang_kami.Visible = false;
            panel_lupa_kata_sandi.Visible = false;
        }

        // create method to clear all text field
        private void clear_text()
        {
            // login
            txt_nama_pengguna_atau_email_masuk.Text = "Nama Pengguna atau Email";
            txt_kata_sandi_masuk.Text = "password";

            // daftar
            txt_nama_depan.Text = "Nama Depan";
            txt_nama_belakang.Text = "Nama Belakang";
            txt_nama_pengguna_daftar.Text = "Nama Pengguna";
            txt_email_daftar.Text = "Alamat Email";
            txt_kata_sandi_daftar.Text = "password";
            txt_konfirmasi_kata_sandi.Text = "konfirmasi password";

            // lupa password
            txt_nama_pengguna_atau_email_lupa_kata_sandi.Text = "Nama Pengguna atau Email";
            txt_lupa_kata_sandi.Text = "password";
            txt_konfirmasi_lupa_kata_sandi.Text = "konfirmasi password";
            txt_captcha_lupa_kata_sandi.Text = "captcha";
        }

        // create method to generate captcha random
        private void generate_captcha_image()
        {
            Random r = new Random();
            angka = r.Next(1000, 9999);
            var image = new Bitmap(this.gambar_captcha.Width, this.gambar_captcha.Height);
            var font = new Font("Cabin", 55, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(angka.ToString(), font, Brushes.DeepSkyBlue, new Point(0, 0));
            gambar_captcha.Image = image;
        }

        /* event title bar */
        // drag title bar
        private void panel_title_bar_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        // event button title bar
        private void button_minimize_click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void button_exit_click(object sender, EventArgs e)
        {
            var result = MessageBox.Show("Apakah Anda yakin ingin menutup aplikasi?",
                                         "Keluar Aplikasi",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Stop);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
        }

        private void icon_topup_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Maaf, Anda harus masuk terlebih dahulu.",
                            "Gagal Mengakses Menu Topup",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        // create method to collapse and uncollapse menu
        private void CollapseMenu()
        {
            if (this.panel_menu.Width > 200) //Collapse menu
            {
                panel_menu.Width = 100;
                logo_darimu.Visible = false;
                label_nama_logo.Visible = false;
                button_menu.Location = new System.Drawing.Point(33, 36);
                this.ClientSize = new System.Drawing.Size(890, 570);
                foreach (Button menuButton in panel_menu.Controls.OfType<Button>())
                {
                    menuButton.Text = "";
                    menuButton.ImageAlign = ContentAlignment.MiddleCenter;
                    menuButton.Padding = new Padding(0);
                }
            }
            else // uncollapse menu
            {
                panel_menu.Width = 210;
                logo_darimu.Visible = true;
                label_nama_logo.Visible = true;
                button_menu.Location = new System.Drawing.Point(148, 36);
                this.ClientSize = new System.Drawing.Size(1000, 570);
                foreach (Button menuButton in panel_menu.Controls.OfType<Button>())
                {
                    menuButton.Text = menuButton.Tag.ToString();
                    menuButton.ImageAlign = ContentAlignment.MiddleLeft;
                    menuButton.Padding = new Padding(10, 0, 0, 0);
                }
            }
        }

        // event menu ( 3 row icon )
        private void button_menu_MouseClick(object sender, MouseEventArgs e)
        {
            CollapseMenu();
        }

        // animate all button in left sidebar
        private void button_profil_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Maaf, Anda harus masuk terlebih dahulu.",
                            "Gagal Mengakses Profil",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void button_tabungan_MouseClick(object sender, MouseEventArgs e)
        {
            MessageBox.Show("Maaf, Anda harus masuk terlebih dahulu.",
                            "Gagal Mengakses Menu Tabungan",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void button_masuk_sidebar_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_masuk.Visible = true;
            button_masuk_sidebar.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_beranda_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_beranda.Visible = true;
            button_beranda.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_faq_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_faq.Visible = true;
            button_faq.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_tentang_kami_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_tentang_kami.Visible = true;
            button_tentang_kami.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_beranda_MouseEnter(object sender, EventArgs e)
        {
            button_beranda.ForeColor = System.Drawing.Color.Cyan;
            button_beranda.Image = global::Darimu.Properties.Resources.icon_beranda_biru;
        }

        private void button_beranda_MouseLeave(object sender, EventArgs e)
        {
            button_beranda.ForeColor = System.Drawing.Color.White;
            button_beranda.Image = global::Darimu.Properties.Resources.icon_beranda;
        }

        private void button_profil_MouseEnter(object sender, EventArgs e)
        {
            button_profil.ForeColor = System.Drawing.Color.Cyan;
            button_profil.Image = global::Darimu.Properties.Resources.icon_profil_biru;
        }

        private void button_profil_MouseLeave(object sender, EventArgs e)
        {
            button_profil.ForeColor = System.Drawing.Color.White;
            button_profil.Image = global::Darimu.Properties.Resources.icon_profil;
        }

        private void button_tabungan_MouseEnter(object sender, EventArgs e)
        {
            button_tabungan.ForeColor = System.Drawing.Color.Cyan;
            button_tabungan.Image = global::Darimu.Properties.Resources.icon_tabungan_biru;
        }

        private void button_tabungan_MouseLeave(object sender, EventArgs e)
        {
            button_tabungan.ForeColor = System.Drawing.Color.White;
            button_tabungan.Image = global::Darimu.Properties.Resources.icon_tabungan;
        }

        private void button_faq_MouseEnter(object sender, EventArgs e)
        {
            button_faq.ForeColor = System.Drawing.Color.Cyan;
            button_faq.Image = global::Darimu.Properties.Resources.icon_faq_biru;
        }

        private void button_faq_MouseLeave(object sender, EventArgs e)
        {
            button_faq.ForeColor = System.Drawing.Color.White;
            button_faq.Image = global::Darimu.Properties.Resources.icon_faq;
        }

        private void button_tentang_kami_MouseEnter(object sender, EventArgs e)
        {
            button_tentang_kami.ForeColor = System.Drawing.Color.Cyan;
            button_tentang_kami.Image = global::Darimu.Properties.Resources.icon_tentang_kami_biru;
        }

        private void button_tentang_kami_MouseLeave(object sender, EventArgs e)
        {
            button_tentang_kami.ForeColor = System.Drawing.Color.White;
            button_tentang_kami.Image = global::Darimu.Properties.Resources.icon_tentang_kami;
        }

        private void button_menu_MouseEnter(object sender, EventArgs e)
        {
            button_menu.ForeColor = System.Drawing.Color.Cyan;
            button_menu.Image = global::Darimu.Properties.Resources.icon_menu_biru;
        }

        private void button_menu_MouseLeave(object sender, EventArgs e)
        {
            button_menu.ForeColor = System.Drawing.Color.White;
            button_menu.Image = global::Darimu.Properties.Resources.icon_menu;
        }

        private void button_masuk_sidebar_MouseEnter(object sender, EventArgs e)
        {
            button_masuk_sidebar.ForeColor = System.Drawing.Color.Cyan;
            button_masuk_sidebar.Image = global::Darimu.Properties.Resources.icon_masuk_biru;
        }

        private void button_masuk_sidebar_MouseLeave(object sender, EventArgs e)
        {
            button_masuk_sidebar.ForeColor = System.Drawing.Color.White;
            button_masuk_sidebar.Image = global::Darimu.Properties.Resources.icon_masuk;
        }

        private void icon_topup_MouseEnter(object sender, EventArgs e)
        {
            icon_topup.Image = global::Darimu.Properties.Resources.icon_topup_biru;
        }

        private void icon_topup_MouseLeave(object sender, EventArgs e)
        {
            icon_topup.Image = global::Darimu.Properties.Resources.icon_topup;
        }

        private void button_minimize_MouseEnter(object sender, EventArgs e)
        {
            button_minimize.Image = global::Darimu.Properties.Resources.icon_minimize_biru;
        }

        private void button_minimize_MouseLeave(object sender, EventArgs e)
        {
            button_minimize.Image = global::Darimu.Properties.Resources.minimize;
        }

        private void button_exit_MouseEnter(object sender, EventArgs e)
        {
            button_exit.Image = global::Darimu.Properties.Resources.exit_pressed;
        }

        private void button_exit_MouseLeave(object sender, EventArgs e)
        {
            button_exit.Image = global::Darimu.Properties.Resources.exit;
        }
        // event beranda
        private void button_nabung_yuk_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            clear_text();
            panel_isi_masuk.Visible = true;
            button_masuk_sidebar.ForeColor = System.Drawing.Color.Cyan;
        }

        /* event login */
        // gotfocus login
        private void txt_nama_pengguna_atau_email_masuk_GotFocus(object sender, EventArgs e)
        {
            if (txt_nama_pengguna_atau_email_masuk.Text == "Nama Pengguna atau Email")
            {
                txt_nama_pengguna_atau_email_masuk.Text = "";
            }
        }

        private void txt_kata_sandi_masuk_GotFocus(object sender, EventArgs e)
        {
            if (txt_kata_sandi_masuk.Text == "password")
            {
                txt_kata_sandi_masuk.Text = "";
            }
        }

        // lostfocus login
        private void txt_nama_pengguna_atau_email_masuk_LostFocus(object sender, EventArgs e)
        {
            if (txt_nama_pengguna_atau_email_masuk.Text == "")
            {
                txt_nama_pengguna_atau_email_masuk.Text = "Nama Pengguna atau Email";
            }
        }

        private void txt_kata_sandi_masuk_LostFocus(object sender, EventArgs e)
        {
            if (txt_kata_sandi_masuk.Text == "")
            {
                txt_kata_sandi_masuk.Text = "password";
            }
        }

        // event label daftar yuk!
        private void label_daftar_yuk_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            clear_text();
            panel_isi_daftar.Visible = true;
        }
        private void label_daftar_yuk_MouseEnter(object sender, EventArgs e)
        {
            label_daftar_yuk.ForeColor = System.Drawing.Color.Cyan;
        }

        private void label_daftar_yuk_MouseLeave(object sender, EventArgs e)
        {
            label_daftar_yuk.ForeColor = System.Drawing.Color.White;
        }

        // event label lupa password
        private void label_lupa_kata_sandi_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            clear_text();
            generate_captcha_image();
            panel_lupa_kata_sandi.Visible = true;
        }

        private void label_lupa_kata_sandi_MouseEnter(object sender, EventArgs e)
        {
            label_lupa_kata_sandi.ForeColor = System.Drawing.Color.Cyan;
        }

        private void label_lupa_kata_sandi_MouseLeave(object sender, EventArgs e)
        {
            label_lupa_kata_sandi.ForeColor = System.Drawing.Color.White;
        }

        private void button_masuk_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekMasuk(txt_nama_pengguna_atau_email_masuk, txt_kata_sandi_masuk);

            if (cek == "valid")
            {
                kesempatan_masuk = kesempatan_masuk - 1;

                string nama_pengguna_atau_email_input = txt_nama_pengguna_atau_email_masuk.Text.Trim();
                string kata_sandi_input = ClassUser.hashPassword(txt_kata_sandi_masuk.Text.Trim());

                if (ClassUser.cekMasuk(nama_pengguna_atau_email_input, kata_sandi_input))
                {
                    MessageBox.Show("Selamat Anda berhasil masuk!",
                                    "Sukses Masuk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);

                    hide_panel();
                    default_color();
                    clear_text();
                    frm_beranda_setelah_login p = new frm_beranda_setelah_login(nama_pengguna_atau_email_input);
                    this.Hide();
                    p.Show();
                }
                else if (kesempatan_masuk <= 0)
                {
                    MessageBox.Show("Maaf, Anda sudah gagal 3 kali untuk mencoba masuk",
                                    "Keluar Aplikasi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show("Nama pengguna, email atau kata sandi salah" + "\nKesempatan Anda sisa " + kesempatan_masuk + " kesempatan",
                                    "Gagal Masuk",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Exclamation);
                    clear_text();
                }
            }
        }

            /* event daftar */
            // event label masuk yuk!
            private void label_masuk_yuk_MouseClick(object sender, MouseEventArgs e)
            {
                hide_panel();
                default_color();
                clear_text();
                panel_isi_masuk.Visible = true;
            }
            private void label_masuk_yuk_MouseEnter(object sender, EventArgs e)
            {
                label_masuk_yuk.ForeColor = System.Drawing.Color.Cyan;
            }

            private void label_masuk_yuk_MouseLeave(object sender, EventArgs e)
            {
                label_masuk_yuk.ForeColor = System.Drawing.Color.White;
            }


            // gotfocus daftar
            private void txt_nama_depan_GotFocus(object sender, EventArgs e)
            {
                if (txt_nama_depan.Text == "Nama Depan")
                {
                    txt_nama_depan.Text = "";
                }
            }

            private void txt_nama_belakang_GotFocus(object sender, EventArgs e)
            {
                if (txt_nama_belakang.Text == "Nama Belakang")
                {
                    txt_nama_belakang.Text = "";
                }
            }

            private void txt_nama_pengguna_daftar_GotFocus(object sender, EventArgs e)
            {
                if (txt_nama_pengguna_daftar.Text == "Nama Pengguna")
                {
                    txt_nama_pengguna_daftar.Text = "";
                }
            }

            private void txt_email_daftar_GotFocus(object sender, EventArgs e)
            {
                if (txt_email_daftar.Text == "Alamat Email")
                {
                    txt_email_daftar.Text = "";
                }
            }

            private void txt_kata_sandi_daftar_GotFocus(object sender, EventArgs e)
            {
                if (txt_kata_sandi_daftar.Text == "password")
                {
                    txt_kata_sandi_daftar.Text = "";
                }
            }

            private void txt_konfirmasi_kata_sandi_GotFocus(object sender, EventArgs e)
            {
                if (txt_konfirmasi_kata_sandi.Text == "konfirmasi password")
                {
                    txt_konfirmasi_kata_sandi.Text = "";
                }
            }

            // lostfocus daftar
            private void txt_nama_depan_LostFocus(object sender, EventArgs e)
            {
                if (txt_nama_depan.Text == "")
                {
                    txt_nama_depan.Text = "Nama Depan";
                }
            }

            private void txt_nama_belakang_LostFocus(object sender, EventArgs e)
            {
                if (txt_nama_belakang.Text == "")
                {
                    txt_nama_belakang.Text = "Nama Belakang";
                }
            }

            private void txt_nama_pengguna_daftar_LostFocus(object sender, EventArgs e)
            {
                if (txt_nama_pengguna_daftar.Text == "")
                {
                    txt_nama_pengguna_daftar.Text = "Nama Pengguna";
                }
            }

            private void txt_email_daftar_LostFocus(object sender, EventArgs e)
            {
                if (txt_email_daftar.Text == "")
                {
                    txt_email_daftar.Text = "Alamat Email";
                }
            }

            private void txt_kata_sandi_daftar_LostFocus(object sender, EventArgs e)
            {
                if (txt_kata_sandi_daftar.Text == "")
                {
                    txt_kata_sandi_daftar.Text = "password";
                }
            }

            private void txt_konfirmasi_kata_sandi_LostFocus(object sender, EventArgs e)
            {
                if (txt_konfirmasi_kata_sandi.Text == "")
                {
                    txt_konfirmasi_kata_sandi.Text = "konfirmasi password";
                }
            }

            private void button_batal_MouseClick(object sender, MouseEventArgs e)
            {
                hide_panel();
                default_color();
                panel_isi_masuk.Visible = true;
            }

            private void button_daftar_MouseClick(object sender, MouseEventArgs e)
            {
                string cek = ClassValidasi.cekPendaftaran(txt_nama_depan, txt_nama_belakang, txt_email_daftar, txt_nama_pengguna_daftar, txt_kata_sandi_daftar, txt_konfirmasi_kata_sandi);
                
                if (cek == "valid")
                {
                    string nama_belakang;

                    if (txt_nama_belakang.Text == "Nama Belakang")
                    {
                        nama_belakang = "";
                    }
                    else
                    {
                        nama_belakang = txt_nama_belakang.Text.Trim();    
                    }
                    string nama_lengkap = txt_nama_depan.Text.Trim() + " " + nama_belakang;
                    string nama_pengguna = txt_nama_pengguna_daftar.Text.Trim();
                    string alamat_email = txt_email_daftar.Text.Trim();
                    string kata_sandi = txt_kata_sandi_daftar.Text.Trim();
                    //get the datepicker value
                    tanggal_lahir_daftar.Format = DateTimePickerFormat.Custom;
                    tanggal_lahir_daftar.CustomFormat = "yyyy/MM/dd";
                    tanggal_lahir_daftar.ShowUpDown = true;
                    string tanggal_lahir = tanggal_lahir_daftar.Value.ToString("yyyy/MM/dd");

                    string hasil = ClassUser.daftarUser(nama_pengguna, nama_lengkap, tanggal_lahir, alamat_email, kata_sandi);
                    if(hasil == "Selamat! Anda telah terdaftar")
                    {
                        MessageBox.Show(hasil, "Sukses Mendaftar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                        hide_panel();
                        default_color();
                        clear_text();
                        panel_isi_masuk.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show(hasil, "Gagal Mendaftar",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show(cek, "Gagal Mendaftar",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }

            }

            /* event panel lupa kata sandi */
            private void button_simpan_lupa_sandi_MouseClick(object sender, MouseEventArgs e)
            {
                string cek = ClassValidasi.cekLupaKataSandi(txt_nama_pengguna_atau_email_lupa_kata_sandi, txt_lupa_kata_sandi, txt_konfirmasi_lupa_kata_sandi, txt_captcha_lupa_kata_sandi, angka);

                if (cek == "valid")
                {
                    string hasil = ClassUser.ubahPassword(txt_nama_pengguna_atau_email_lupa_kata_sandi.Text, txt_lupa_kata_sandi.Text);
                    MessageBox.Show(hasil, "Sukses Membuat Password Baru",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    hide_panel();
                    default_color();
                    clear_text();
                    panel_isi_masuk.Visible = true;
                    button_masuk_sidebar.ForeColor = System.Drawing.Color.Cyan;
                }
                else
                {
                    MessageBox.Show(cek, "Lengkapi Semua Syarat!",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                    generate_captcha_image();
                }
            }

            // gotfocus lupa sandi
            private void txt_nama_pengguna_atau_email_lupa_kata_sandi_GotFocus(object sender, EventArgs e)
            {
                if (txt_nama_pengguna_atau_email_lupa_kata_sandi.Text == "Nama Pengguna atau Email")
                {
                    txt_nama_pengguna_atau_email_lupa_kata_sandi.Text = "";
                }
            }

            private void txt_lupa_kata_sandi_GotFocus(object sender, EventArgs e)
            {
                if (txt_lupa_kata_sandi.Text == "password")
                {
                    txt_lupa_kata_sandi.Text = "";
                }
            }

            private void txt_konfirmasi_lupa_kata_sandi_GotFocus(object sender, EventArgs e)
            {
                if (txt_konfirmasi_lupa_kata_sandi.Text == "konfirmasi password")
                {
                    txt_konfirmasi_lupa_kata_sandi.Text = "";
                }
            }

            private void txt_captcha_lupa_kata_sandi_GotFocus(object sender, EventArgs e)
            {
                if (txt_captcha_lupa_kata_sandi.Text == "captcha")
                {
                    txt_captcha_lupa_kata_sandi.Text = "";
                }
            }

            private void button_berikutnya_faq_MouseClick(object sender, MouseEventArgs e)
            {
                gambar_faq_1.Visible = false;
                gambar_faq_2.Visible = true;
                button_sebelum_faq.Visible = true;
            }

            private void button_berikutnya_faq_MouseEnter(object sender, EventArgs e)
            {
                button_berikutnya_faq.Image = global::Darimu.Properties.Resources.panah_berikutnya_biru;
            }

            private void button_berikutnya_faq_MouseLeave(object sender, EventArgs e)
            {
                button_berikutnya_faq.Image = global::Darimu.Properties.Resources.panah_berikutnya;
            }

            private void button_sebelum_faq_MouseEnter(object sender, EventArgs e)
            {
                button_sebelum_faq.Image = global::Darimu.Properties.Resources.panah_sebelum_biru;
            }

            private void button_sebelum_faq_MouseLeave(object sender, EventArgs e)
            {
                button_sebelum_faq.Image = global::Darimu.Properties.Resources.panah_sebelum;
            }

            private void button_sebelum_faq_MouseClick(object sender, MouseEventArgs e)
            {
                gambar_faq_2.Visible = false;
                button_sebelum_faq.Visible = false;
                gambar_faq_1.Visible = true;
            }

            // lostfocus lupa sandi
            private void txt_nama_pengguna_atau_email_lupa_kata_sandi_LostFocus(object sender, EventArgs e)
            {
                if (txt_nama_pengguna_atau_email_lupa_kata_sandi.Text == "")
                {
                    txt_nama_pengguna_atau_email_lupa_kata_sandi.Text = "Nama Pengguna atau Email";
                }
            }

            private void txt_lupa_kata_sandi_LostFocus(object sender, EventArgs e)
            {
                if (txt_lupa_kata_sandi.Text == "")
                {
                    txt_lupa_kata_sandi.Text = "password";
                }
            }

            private void txt_konfirmasi_lupa_kata_sandi_LostFocus(object sender, EventArgs e)
            {
                if (txt_konfirmasi_lupa_kata_sandi.Text == "")
                {
                    txt_konfirmasi_lupa_kata_sandi.Text = "konfirmasi password";
                }
            }

            private void txt_captcha_lupa_kata_sandi_LostFocus(object sender, EventArgs e)
            {
                if (txt_captcha_lupa_kata_sandi.Text == "")
                {
                    txt_captcha_lupa_kata_sandi.Text = "captcha";
                }
            }

            // event button batal pada panel lupa sandi
            private void button_batal_lupa_sandi_MouseClick(object sender, MouseEventArgs e)
            {
                hide_panel();
                default_color();
                panel_isi_masuk.Visible = true;
                button_masuk_sidebar.ForeColor = System.Drawing.Color.Cyan;
            }

            // event button refresh pada panel lupa sandi
            private void label_refresh_captcha_lupa_sandi_MouseClick(object sender, MouseEventArgs e)
            {
                generate_captcha_image();
            }

            private void label_refresh_captcha_lupa_sandi_MouseEnter(object sender, EventArgs e)
            {
                label_refresh_captcha_lupa_sandi.ForeColor = System.Drawing.Color.Cyan;
            }

            private void label_refresh_captcha_lupa_sandi_MouseLeave(object sender, EventArgs e)
            {
                label_refresh_captcha_lupa_sandi.ForeColor = Color.FromArgb(194, 194, 194);
            }
        }
    }