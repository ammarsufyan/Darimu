using Darimu.ClassFolder;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Darimu
{
    public partial class frm_admin : Form
    {
        // declaration var
        private string id_admin;
        private int borderSize = 2;

        public frm_admin(string id_admin)
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(33, 106, 155);
            this.id_admin = id_admin;
            lihat_data_admin();
            panel_isi_beranda.Visible = true;
        }

        // drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // hide all panel
        private void hide_panel()
        {
            panel_isi_beranda.Visible = false;
            panel_isi_profil_saya.Visible = false;
            panel_isi_faq.Visible = false;
            panel_isi_tentang_kami.Visible = false;
            panel_isi_laporan_pengguna.Visible = false;
            panel_isi_transaksi_pengguna.Visible = false;
            panel_isi_rincian_laporan.Visible = false;
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
            var result = MessageBox.Show("Apakah kamu yakin ingin menutup aplikasi?",
                                         "Keluar Aplikasi",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Stop);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                Application.Exit();
            }
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

        private void lihat_data_admin()
        {
            ArrayList data_admin = ClassAdmin.lihatAdmin(id_admin);
            label_username.Text = data_admin[1].ToString();
            string[] nama_depan_belakang = data_admin[1].ToString().Split(' ');
            label_nama_depan.Text = nama_depan_belakang[0];
            label_nama_belakang.Text = nama_depan_belakang[1];
            label_tanggal_lahir.Text = data_admin[2].ToString();
            label_alamat_email.Text = data_admin[3].ToString();
        }

        // event menu ( 3 row icon )
        private void button_menu_MouseClick(object sender, MouseEventArgs e)
        {
            CollapseMenu();
        }

        // animate all button in left sidebar
        private void button_beranda_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_beranda.Visible = true;
        }

        private void button_faq_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_faq.Visible = true;
        }

        private void button_tentang_kami_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_tentang_kami.Visible = true;
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

        private void button_laporan_MouseEnter(object sender, EventArgs e)
        {
            button_laporan.ForeColor = System.Drawing.Color.Cyan;
            button_laporan.Image = global::Darimu.Properties.Resources.icon_laporan_biru;
        }

        private void button_laporan_MouseLeave(object sender, EventArgs e)
        {
            button_laporan.ForeColor = System.Drawing.Color.White;
            button_laporan.Image = global::Darimu.Properties.Resources.icon_laporan;
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

        private void frm_beranda_setelah_login_Load(object sender, EventArgs e)
        {
            drop_down_laporan.IsMainMenu = true;
            drop_down_laporan.PrimaryColor = Color.FromArgb(16, 53, 78);
        }

        private void button_keluar_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin keluar akun?",
                                         "Keluar Akun",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                hide_panel();
                frm_sebelum_login sebelum_login = new frm_sebelum_login();
                this.Hide();
                sebelum_login.Show();
            }
        }

        private void button_sebelum_faq_MouseClick(object sender, MouseEventArgs e)
        {
            gambar_faq_2.Visible = false;
            button_sebelum_faq.Visible = false;
            gambar_faq_1.Visible = true;
        }

        private void button_sebelum_faq_MouseEnter(object sender, EventArgs e)
        {
            button_sebelum_faq.Image = global::Darimu.Properties.Resources.panah_sebelum_biru;
        }

        private void button_sebelum_faq_MouseLeave(object sender, EventArgs e)
        {
            button_sebelum_faq.Image = global::Darimu.Properties.Resources.panah_sebelum;
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

        private void button_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            drop_down_laporan.Show(button_laporan, button_laporan.Width, 0);
        }

        private void button_profil_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            ClassAdmin.riwayat_laporan_untuk_admin(grid_laporan);
            panel_isi_profil_saya.Visible = true;
        }

        private void laporanPenggunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            ClassAdmin.riwayat_laporan_untuk_admin(grid_laporan);
            panel_isi_laporan_pengguna.Visible = true;
        }

        private void riwayatTransaksiPenggunaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            ClassAdmin.riwayat_transaksi_untuk_admin(grid_transaksi);
            panel_isi_transaksi_pengguna.Visible = true;
        }

        private void button_kerja_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            ClassAdmin.riwayat_laporan_untuk_admin(grid_laporan);
            panel_isi_laporan_pengguna.Visible = true;
        }

        private void button_selesai_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin selesaikan laporan?",
                                         "Selesaikan Laporan",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                string id_laporan = label_nomor_laporan.Text.Trim();
                ClassAdmin.selesaikan_laporan(id_admin, id_laporan);
                ClassAdmin.riwayat_laporan_untuk_admin(grid_laporan);
                hide_panel();
                panel_isi_laporan_pengguna.Visible = true;
            }
        }

        private void button_selesai_laporan_MouseEnter(object sender, EventArgs e)
        {
            button_selesai_laporan.Image = global::Darimu.Properties.Resources.button_selesai_dipencet;

        }

        private void button_selesai_laporan_MouseLeave(object sender, EventArgs e)
        {
            button_selesai_laporan.Image = global::Darimu.Properties.Resources.button_selesai;
        }

        private void button_batal_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_laporan_pengguna.Visible = true;
        }

        private void button_batal_laporan_MouseEnter(object sender, EventArgs e)
        {
            button_batal_laporan.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_laporan_MouseLeave(object sender, EventArgs e)
        {
            button_batal_laporan.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void grid_laporan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id_laporan = grid_laporan.Rows[e.RowIndex].Cells[0].Value.ToString();
                string status_laporan = grid_laporan.Rows[e.RowIndex].Cells[4].Value.ToString();

                ArrayList data_rincian_laporan = ClassAdmin.rincian_laporan_untuk_admin(id_laporan);
                label_nomor_laporan.Text = data_rincian_laporan[0].ToString();
                label_nama_pengguna_laporan.Text = data_rincian_laporan[1].ToString();
                label_subjek_laporan.Text = data_rincian_laporan[2].ToString();
                txt_rincian_laporan.Text = data_rincian_laporan[3].ToString();
                label_tanggal_laporan_dibuat.Text = data_rincian_laporan[4].ToString();
                hide_panel();
                panel_isi_rincian_laporan.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Jika ingin melihat rincian laporan pengguna\nSilakan klik dua kali barisnya, ya. :)",
                                "Silakan Klik Dua Kali Barisnya",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }
    }
}