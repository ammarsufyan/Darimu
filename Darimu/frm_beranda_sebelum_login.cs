using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Darimu
{
    public partial class frm_beranda_sebelum_login : Form
    {
        // declaration var
        private int borderSize = 2;
        private Size formSize;
        int angka = 0;

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
            button_masuk.ForeColor = System.Drawing.Color.White;
        }

        // hide all panel
        private void hide_panel()
        {
            panel_isi_beranda.Visible = false;
            panel_isi_daftar.Visible = false;
            panel_isi_login.Visible = false;
            panel_isi_faq.Visible = false;
            panel_isi_tentang_kami.Visible = false;
            panel_lupa_kata_sandi.Visible = false;
        }

        // create method to generate captcha random
        private void generate_captcha_image()
        {
            Random r = new Random();
            angka = r.Next(100, 10000);
            var image = new Bitmap(this.gambar_captcha.Width, this.gambar_captcha.Height);
            var font = new Font("Cabin", 55, FontStyle.Bold, GraphicsUnit.Pixel);
            var graphics = Graphics.FromImage(image);
            graphics.DrawString(angka.ToString(), font, Brushes.Green, new Point(0, 0));
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
            const string message = "Apakah Anda yakin ingin menutup aplikasi?";
            const string caption = "Keluar Aplikasi";
            var result = MessageBox.Show(message, caption,
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
            const string message = "Maaf, Anda harus masuk terlebih dahulu.";
            const string caption = "Gagal Mengakses Menu Topup";
            var result = MessageBox.Show(message, caption,
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
                this.ClientSize = new System.Drawing.Size(874, 531);
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
                this.ClientSize = new System.Drawing.Size(984, 531);
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
            const string message = "Maaf, Anda harus masuk terlebih dahulu.";
            const string caption = "Gagal Mengakses Menu Profil";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void button_tabungan_MouseClick(object sender, MouseEventArgs e)
        {
            const string message = "Maaf, Anda harus masuk terlebih dahulu.";
            const string caption = "Gagal Mengakses Menu Tabungan";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);
        }

        private void button_masuk_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
            button_masuk.ForeColor = System.Drawing.Color.Cyan;
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

        private void button_beranda_MouseLeave(object sender, EventArgs e)
        {
            button_beranda.ForeColor = System.Drawing.Color.White;
            button_beranda.Image = global::Darimu.Properties.Resources.icon_beranda;
        }

        private void button_beranda_MouseEnter(object sender, EventArgs e)
        {
            button_beranda.ForeColor = System.Drawing.Color.Cyan;
            button_beranda.Image = global::Darimu.Properties.Resources.icon_beranda_biru;
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

        private void button_masuk_MouseEnter(object sender, EventArgs e)
        {
            button_masuk.ForeColor = System.Drawing.Color.Cyan;
            button_masuk.Image = global::Darimu.Properties.Resources.icon_masuk_biru;
        }

        private void button_masuk_MouseLeave(object sender, EventArgs e)
        {
            button_masuk.ForeColor = System.Drawing.Color.White;
            button_masuk.Image = global::Darimu.Properties.Resources.icon_masuk;
        }

        private void icon_topup_MouseEnter(object sender, EventArgs e)
        {
            icon_topup.Image = global::Darimu.Properties.Resources.icon_topup_biru;
        }

        private void icon_topup_MouseLeave(object sender, EventArgs e)
        {
            icon_topup.Image = global::Darimu.Properties.Resources.icon_topup;
        }

        // event beranda
        private void button_nabung_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
            button_masuk.ForeColor = System.Drawing.Color.Cyan;
        }

        /* event login */
        // gotfocus login
        private void txt_username_email_GotFocus(object sender, EventArgs e)
        {
            if (txt_username_email.Text == "username/email")
            {
                txt_username_email.Text = "";
            }
        }

        private void txt_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_password.Text == "password")
            {
                txt_password.Text = "";
            }
        }

        // lostfocus login
        private void txt_username_email_LostFocus(object sender, EventArgs e)
        {
            if (txt_username_email.Text == "")
            {
                txt_username_email.Text = "username/email";
            }
        }

        private void txt_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_password.Text == "")
            {
                txt_password.Text = "password";
            }
        }

        // event label daftar yuk!
        private void label_daftar_yuk_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
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
        private void label_lupa_password_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            generate_captcha_image();
            panel_lupa_kata_sandi.Visible = true;
        }

        private void label_lupa_password_MouseEnter(object sender, EventArgs e)
        {
            label_lupa_password.ForeColor = System.Drawing.Color.Cyan;
        }

        private void label_lupa_password_MouseLeave(object sender, EventArgs e)
        {
            label_lupa_password.ForeColor = System.Drawing.Color.White;
        }

        private void button_submit_masuk_MouseClick(object sender, MouseEventArgs e)
        {
            const string message = "Selamat Anda berhasil masuk!";
            const string caption = "Sukses Masuk";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);

            //frm_beranda_setelah_login p = new frm_beranda_setelah_login();
            this.Hide();
            //p.Visible = true;
        }

        /* event daftar */
        // event label masuk yuk!
        private void label_masuk_yuk_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
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

        private void txt_username_daftar_GotFocus(object sender, EventArgs e)
        {
            if (txt_username_daftar.Text == "Nama Pengguna")
            {
                txt_username_daftar.Text = "";
            }
        }

        private void txt_email_daftar_GotFocus(object sender, EventArgs e)
        {
            if (txt_email_daftar.Text == "Alamat Email")
            {
                txt_email_daftar.Text = "";
            }
        }

        private void txt_password_daftar_GotFocus(object sender, EventArgs e)
        {
            if (txt_password_daftar.Text == "password")
            {
                txt_password_daftar.Text = "";
            }
        }

        private void txt_konfirmasi_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_konfirmasi_password.Text == "konfirmasi password")
            {
                txt_konfirmasi_password.Text = "";
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

        private void txt_username_daftar_LostFocus(object sender, EventArgs e)
        {
            if (txt_username_daftar.Text == "")
            {
                txt_username_daftar.Text = "Nama Pengguna";
            }
        }

        private void txt_email_daftar_LostFocus(object sender, EventArgs e)
        {
            if (txt_email_daftar.Text == "")
            {
                txt_email_daftar.Text = "Alamat Email";
            }
        }

        private void txt_password_daftar_LostFocus(object sender, EventArgs e)
        {
            if (txt_password_daftar.Text == "")
            {
                txt_password_daftar.Text = "password";
            }
        }

        private void txt_konfirmasi_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_konfirmasi_password.Text == "")
            {
                txt_konfirmasi_password.Text = "konfirmasi password";
            }
        }

        private void button_batal_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
        }

        private void button_daftar_MouseClick(object sender, MouseEventArgs e)
        {
            const string message = "Selamat Anda berhasil terdaftar!";
            const string caption = "Sukses Mendaftar";
            var result = MessageBox.Show(message, caption,
                                         MessageBoxButtons.OK,
                                         MessageBoxIcon.Information);

            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
        }

        /* event panel lupa kata sandi */
        private void button_simpan_lupa_sandi_MouseClick(object sender, MouseEventArgs e)
        {
            if(txt_captcha_lupa_password.Text == angka.ToString())
            {
                const string message = "Selamat Anda berhasil membuat password baru!";
                const string caption = "Sukses Membuat Password Baru";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Information);
                hide_panel();
                default_color();
                panel_isi_login.Visible = true;
                button_masuk.ForeColor = System.Drawing.Color.Cyan;
            } else {
                const string message = "Harap isi dengan benar!";
                const string caption = "Lengkapi Semua Syarat!";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButtons.OK,
                                             MessageBoxIcon.Warning);
                generate_captcha_image();
            }
        }

        // gotfocus lupa sandi
        private void txt_username_email_lupa_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_username_email_lupa_password.Text == "username/email")
            {
                txt_username_email_lupa_password.Text = "";
            }
        }

        private void txt_lupa_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_lupa_password.Text == "password")
            {
                txt_lupa_password.Text = "";
            }
        }

        private void txt_konfirmasi_lupa_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_konfirmasi_lupa_password.Text == "konfirmasi password")
            {
                txt_konfirmasi_lupa_password.Text = "";
            }
        }

        private void txt_captcha_lupa_password_GotFocus(object sender, EventArgs e)
        {
            if (txt_captcha_lupa_password.Text == "captcha")
            {
                txt_captcha_lupa_password.Text = "";
            }
        }

        // lostfocus lupa sandi
        private void txt_username_email_lupa_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_username_email_lupa_password.Text == "")
            {
                txt_username_email_lupa_password.Text = "username/email";
            }
        }

        private void txt_lupa_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_lupa_password.Text == "")
            {
                txt_lupa_password.Text = "password";
            }
        }

        private void txt_konfirmasi_lupa_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_konfirmasi_lupa_password.Text == "")
            {
                txt_konfirmasi_lupa_password.Text = "konfirmasi password";
            }
        }

        private void txt_captcha_lupa_password_LostFocus(object sender, EventArgs e)
        {
            if (txt_captcha_lupa_password.Text == "")
            {
                txt_captcha_lupa_password.Text = "captcha";
            }
        }

        // event button batal pada panel lupa sandi
        private void button_batal_lupa_sandi_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            panel_isi_login.Visible = true;
            button_masuk.ForeColor = System.Drawing.Color.Cyan;
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