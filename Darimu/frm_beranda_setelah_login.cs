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
    public partial class frm_beranda_setelah_login : Form
    {
        // declaration var
        private int borderSize = 2;
        private Size formSize;
        int angka = 0;

        public frm_beranda_setelah_login()
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
            button_keluar.ForeColor = System.Drawing.Color.White;
        }

        // hide all panel
        private void hide_panel()
        {
            panel_isi_beranda.Visible = false;
            panel_isi_faq.Visible = false;
            panel_isi_tentang_kami.Visible = false;
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
            drop_down_profil.Show(button_profil, button_profil.Width, 0);
        }

        private void button_tabungan_MouseClick(object sender, MouseEventArgs e)
        {
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

        private void button_keluar_MouseEnter(object sender, EventArgs e)
        {
            button_keluar.ForeColor = System.Drawing.Color.Cyan;
            button_keluar.Image = global::Darimu.Properties.Resources.icon_keluar_biru;
        }

        private void button_keluar_MouseLeave(object sender, EventArgs e)
        {
            button_keluar.ForeColor = System.Drawing.Color.White;
            button_keluar.Image = global::Darimu.Properties.Resources.icon_keluar;
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
        private void button_nabung_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            default_color();
            button_keluar.ForeColor = System.Drawing.Color.Cyan;
        }

        private void frm_beranda_setelah_login_Load(object sender, EventArgs e)
        {
            drop_down_profil.IsMainMenu = true;
            drop_down_profil.PrimaryColor = Color.FromArgb(16, 53, 78);
        }
    }
}