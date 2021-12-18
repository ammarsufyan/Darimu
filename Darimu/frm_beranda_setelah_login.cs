using Darimu.ClassFolder;
using System;
using System.Collections;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml;

namespace Darimu
{
    public partial class frm_beranda_setelah_login : Form
    {
        // declaration var
        public string nama_pengguna;
        private int borderSize = 2;
        private Size formSize;
        int angka = 0;

        public frm_beranda_setelah_login(string nama_pengguna)
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(33, 106, 155);
            panel_isi_beranda.Visible = true;
            label_username.Text = nama_pengguna;
            label_saldo.Text = ClassTransaksi.get_saldo(nama_pengguna).ToString();
            this.nama_pengguna = nama_pengguna;
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
            panel_isi_laporan_saya.Visible = false;
            panel_isi_riwayat_tabungan.Visible = false;
            panel_isi_profil_saya.Visible = false;
            panel_isi_riwayat_tabungan_impian.Visible = false;
            panel_isi_tabungan_impian.Visible = false;
            panel_isi_tambah_tabungan_impian.Visible = false;
            panel_isi_ubah_profil_saya.Visible = false;
            panel_isi_ubah_tabungan_impian.Visible = false;
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

        private void button_keluar_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah Anda yakin ingin keluar akun?",
                                         "Keluar Akun",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                hide_panel();
                default_color();
                frm_beranda_sebelum_login p = new frm_beranda_sebelum_login();
                this.Hide();
                p.Show();
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

        private void icon_topup_Click(object sender, EventArgs e)
        {

        }

        private void profilSayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList data_pengguna = ClassUser.lihatPengguna(nama_pengguna);
            string[] nama_depan_belakang = data_pengguna[1].ToString().Split(' ');
            label_nama_depan.Text = nama_depan_belakang[0];
            label_nama_belakang.Text = nama_depan_belakang[1];
            label_alamat_email.Text = data_pengguna[3].ToString();
            label_tanggal_lahir.Text = data_pengguna[2].ToString();

            hide_panel();
            panel_isi_profil_saya.Visible = true;


            // XmlDocument doc = new XmlDocument();
            // doc.Load(@"http://localhost:81/webservice/xml/darimu/lihat_pengguna.php?nama_pengguna" + nama_pengguna);
            // XmlElement root = doc.DocumentElement;
            // XmlNodeList nodes = root.SelectNodes("/datapengguna");

            // String res = "";

            // foreach (XmlNode node in nodes)
            // {
            //     // Respon apakah gagal atau tidak
            //     res = node["response"].InnerText.Trim();

            //     // respon yang digunakan untuk menampilkan data profil
            //     nama_lengkap = node["nama_lengkap"].InnerText.Trim();
            //     alamat_email = node["alamat_email"].InnerText.Trim();
            //     tanggal_lahir = node["tanggal_lahir"].InnerText.Trim();
            // }

            // if (res.Equals("Berhasil"))
            // {
            //     string[] nama_depan_belakang = nama_lengkap.Split(' ');
            //     label_nama_depan.Text = nama_depan_belakang[0];
            //     label_nama_belakang.Text = nama_depan_belakang[1];
            //     label_alamat_email.Text = alamat_email;
            //     label_tanggal_lahir.Text = tanggal_lahir;
            // }
            // else
            // {
            //     MessageBox.Show("Gagal mengambil data profil",
            //                     "Gagal Mengakses Profil",
            //                     MessageBoxButtons.OK,
            //                     MessageBoxIcon.Information);
            // }
        }

        private void button_simpan_MouseEnter(object sender, EventArgs e)
        {
            button_simpan.BackColor = System.Drawing.Color.White;
            button_simpan.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_MouseLeave(object sender, EventArgs e)
        {
            button_simpan.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void button_batal_MouseEnter(object sender, EventArgs e)
        {
            button_batal.BackColor = System.Drawing.Color.White;
            button_batal.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_MouseLeave(object sender, EventArgs e)
        {
            button_batal.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void ubahProfilSayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            panel_isi_ubah_profil_saya.Visible = true;
        }
    }
}