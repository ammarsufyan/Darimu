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
        private string nama_pengguna, pilihan_bank, jenis_impian, tautan_gambar;
        private bool validasi_button = false;
        private int borderSize = 2;

        public frm_beranda_setelah_login(string nama_pengguna)
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(33, 106, 155);
            panel_isi_beranda.Visible = true;
            tenggat_waktu_impian.MinDate = System.DateTime.Now;
            tenggat_waktu_impian.Value = System.DateTime.Now;
            label_username.Text = nama_pengguna;
            label_saldo.Text = ClassTransaksi.get_saldo(nama_pengguna).ToString();
            this.nama_pengguna = nama_pengguna;
        }

        // drag form
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        // get profile data
        private void ambil_data_profil()
        {
            ArrayList data_pengguna = ClassUser.lihatPengguna(nama_pengguna);
            string[] nama_depan_belakang = data_pengguna[1].ToString().Split(' ');
            txt_ubah_nama_depan.Text = nama_depan_belakang[0];
            txt_ubah_nama_belakang.Text = nama_depan_belakang[1];
            txt_ubah_email.Text = data_pengguna[3].ToString();
            ubah_tanggal_lahir.Text = data_pengguna[2].ToString();

            label_nama_depan.Text = nama_depan_belakang[0];
            label_nama_belakang.Text = nama_depan_belakang[1];
            label_tanggal_lahir.Text = data_pengguna[2].ToString();
            label_alamat_email.Text = data_pengguna[3].ToString();
        }
        // hide all panel
        private void hide_panel()
        {
            panel_isi_beranda.Visible = false;
            panel_isi_faq.Visible = false;
            panel_isi_tentang_kami.Visible = false;
            panel_isi_laporan_saya.Visible = false;
            panel_isi_riwayat_transaksi.Visible = false;
            panel_isi_profil_saya.Visible = false;
            panel_isi_riwayat_tabungan_impian.Visible = false;
            panel_isi_tabungan_impian.Visible = false;
            panel_isi_tambah_tabungan_impian.Visible = false;
            panel_isi_ubah_profil_saya.Visible = false;
            panel_isi_tambah_saldo.Visible = false;
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
            hide_panel();
            panel_isi_tambah_saldo.Visible = true;
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
            drop_down_tabungan.Show(button_tabungan, button_tabungan.Width, 0);
        }

        private void button_beranda_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_beranda.Visible = true;
            button_beranda.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_faq_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_faq.Visible = true;
            button_faq.ForeColor = System.Drawing.Color.Cyan;
        }

        private void button_tentang_kami_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
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
            button_keluar.ForeColor = System.Drawing.Color.Cyan;
        }

        private void frm_beranda_setelah_login_Load(object sender, EventArgs e)
        {
            drop_down_profil.IsMainMenu = true;
            drop_down_profil.PrimaryColor = Color.FromArgb(16, 53, 78);

            drop_down_tabungan.IsMainMenu = true;
            drop_down_tabungan.PrimaryColor = Color.FromArgb(16, 53, 78);
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
            hide_panel();
            panel_isi_profil_saya.Visible = true;
            ambil_data_profil();
        }

        private void button_simpan_MouseEnter(object sender, EventArgs e)
        {
            button_simpan_ubah_profil.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_MouseLeave(object sender, EventArgs e)
        {
            button_simpan_ubah_profil.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void button_batal_MouseEnter(object sender, EventArgs e)
        {
            button_batal_ubah_profil.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_MouseLeave(object sender, EventArgs e)
        {
            button_batal_ubah_profil.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void ubahProfilSayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            panel_isi_ubah_profil_saya.Visible = true;
            ambil_data_profil();
        }

        private void button_BRI_MouseEnter(object sender, EventArgs e)
        {
            button_BRI.Image = global::Darimu.Properties.Resources.pilih_bank_biru;
        }

        private void button_BRI_MouseLeave(object sender, EventArgs e)
        {
            button_BRI.Image = global::Darimu.Properties.Resources.pilih_bank;
        }

        private void button_BCA_MouseEnter(object sender, EventArgs e)
        {
            button_BCA.Image = global::Darimu.Properties.Resources.pilih_bank_biru;
        }

        private void button_BCA_MouseLeave(object sender, EventArgs e)
        {
            button_BCA.Image = global::Darimu.Properties.Resources.pilih_bank;
        }

        private void button_BNI_MouseEnter(object sender, EventArgs e)
        {
            button_BNI.Image = global::Darimu.Properties.Resources.pilih_bank_biru;
        }

        private void button_BNI_MouseLeave(object sender, EventArgs e)
        {
            button_BNI.Image = global::Darimu.Properties.Resources.pilih_bank;
        }

        private void button_simpan_saldo_MouseEnter(object sender, EventArgs e)
        {
            button_simpan_saldo.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_saldo_MouseLeave(object sender, EventArgs e)
        {
            button_simpan_saldo.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void button_batal_saldo_MouseEnter(object sender, EventArgs e)
        {
            button_batal_saldo.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_saldo_MouseLeave(object sender, EventArgs e)
        {
            button_batal_saldo.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void button_BRI_MouseClick(object sender, MouseEventArgs e)
        {
            pilihan_bank = "Setor Saldo Melalui BRI";
            gambar_bank.Image = global::Darimu.Properties.Resources.bank_bri;
        }

        private void button_BCA_MouseClick(object sender, MouseEventArgs e)
        {
            pilihan_bank = "Setor Saldo Melalui BCA";
            gambar_bank.Image = global::Darimu.Properties.Resources.bank_bca;
        }

        private void button_BNI_MouseClick(object sender, MouseEventArgs e)
        {
            pilihan_bank = "Setor Saldo Melalui BNI";
            gambar_bank.Image = global::Darimu.Properties.Resources.bank_bni;
        }

        private void button_batal_saldo_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_beranda.Visible = true;
        }

        private void button_simpan_ubah_profil_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekUbahProfil(txt_ubah_nama_depan, txt_ubah_nama_belakang, txt_ubah_email);

            if (cek == "valid")
            {
                string nama_belakang;

                if (txt_ubah_nama_belakang.Text == "Nama Belakang")
                {
                    nama_belakang = "";
                }
                else
                {
                    nama_belakang = txt_ubah_nama_belakang.Text.Trim();
                }
                string nama_lengkap = txt_ubah_nama_depan.Text.Trim() + " " + nama_belakang;
                string alamat_email = txt_ubah_email.Text.Trim();
                // get the datepicker value
                ubah_tanggal_lahir.Format = DateTimePickerFormat.Custom;
                ubah_tanggal_lahir.CustomFormat = "yyyy/MM/dd";
                string tanggal_lahir = ubah_tanggal_lahir.Value.ToString("yyyy/MM/dd");
                var result = MessageBox.Show("Apakah Anda yakin ingin mengubah?",
                                             "Konfirmasi Ubah Data",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Warning);

                if (result == DialogResult.Yes)
                {
                    if (txt_ubah_email.ReadOnly == true)
                    {
                        string hasil = ClassUser.ubah_data_pengguna_tanpa_email(nama_pengguna, nama_lengkap, tanggal_lahir);
                        MessageBox.Show(hasil, "Sukses Mengubah Data",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        hide_panel();
                        panel_isi_profil_saya.Visible = true;
                        txt_ubah_email.ReadOnly = true;
                        ambil_data_profil();
                    } else
                    {
                        string hasil = ClassUser.ubah_data_pengguna(nama_pengguna, nama_lengkap, tanggal_lahir, alamat_email);
                        if(hasil == "Data Berhasil Diubah")
                        {
                            MessageBox.Show(hasil, "Sukses Mengubah Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            hide_panel();
                            panel_isi_profil_saya.Visible = true;
                            txt_ubah_email.ReadOnly = true;
                            ambil_data_profil();
                        }
                        else
                        {
                            MessageBox.Show(hasil, "Gagal Mengubah Data",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                            hide_panel();
                            panel_isi_profil_saya.Visible = true;
                            txt_ubah_email.ReadOnly = true;
                            ambil_data_profil();
                        }
                    }
                }
            } else
            {
                MessageBox.Show(cek, "Lengkapi Syarat",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information);
            }
        }

        private void label_ubah_email_MouseEnter(object sender, EventArgs e)
        {
            label_ubah_email.ForeColor = Color.Cyan;
        }

        private void label_ubah_email_MouseLeave(object sender, EventArgs e)
        {
            label_ubah_email.ForeColor = Color.Black;
        }

        private void label_ubah_email_MouseClick(object sender, MouseEventArgs e)
        {
            if(txt_ubah_email.ReadOnly == false)
            {
                txt_ubah_email.ReadOnly = true;
            }
            else
            {
                txt_ubah_email.ReadOnly = false;
            }
        }

        private void riwayatTransaksiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            panel_isi_riwayat_transaksi.Visible = true;
            ClassTransaksi.riwayat_transaksi(nama_pengguna, grid_transaksi);
        }

        private void button_simpan_impian_MouseEnter(object sender, EventArgs e)
        {
            button_simpan_impian.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_impian_MouseLeave(object sender, EventArgs e)
        {
            button_simpan_impian.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            hide_panel();
            panel_isi_tambah_tabungan_impian.Visible = true;
        }

        private void button_simpan_impian_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekDaftarImpian(txt_nama_tabungan_impian, txt_saldo_impian, validasi_button);
            if(cek == "valid")
            {
                string nama_tabungan_impian = txt_nama_tabungan_impian.Text.Trim();
                string saldo_impian = txt_saldo_impian.Text.Trim();
                // get the datepicker value
                tenggat_waktu_impian.Format = DateTimePickerFormat.Custom;
                tenggat_waktu_impian.CustomFormat = "yyyy/MM/dd";
                string tenggat_waktu = tenggat_waktu_impian.Value.ToString("yyyy/MM/dd");

                string hasil = ClassTabunganImpian.tambahImpian(nama_pengguna, nama_tabungan_impian, jenis_impian, tautan_gambar, saldo_impian, tenggat_waktu);
                MessageBox.Show(hasil,
                                "Sukses Menambah Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show(cek,
                                "Gagal Menambah Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void default_impian()
        {
            jenis_impian_jalan_jalan.Image = global::Darimu.Properties.Resources.jalan_jalan;
            jenis_impian_elektronik.Image = global::Darimu.Properties.Resources.elektronik;
            jenis_impian_hiburan.Image = global::Darimu.Properties.Resources.hiburan;
            jenis_impian_fashion.Image = global::Darimu.Properties.Resources.fashion;
            jenis_impian_umum.Image = global::Darimu.Properties.Resources.umum;
        }

        private void jenis_impian_umum_MouseClick(object sender, MouseEventArgs e)
        {
            default_impian();
            jenis_impian_umum.Image = global::Darimu.Properties.Resources.umum_biru;
            jenis_impian = "Umum";
            tautan_gambar = "global::Darimu.Properties.Resources.logo_umum";
            validasi_button = true;
        }

        private void jenis_impian_jalan_jalan_MouseClick(object sender, MouseEventArgs e)
        {
            default_impian();
            jenis_impian_jalan_jalan.Image = global::Darimu.Properties.Resources.jalan_jalan_biru;
            jenis_impian = "Jalan-jalan";
            tautan_gambar = "global::Darimu.Properties.Resources.logo_jalan_jalan";
            validasi_button = true;
        }

        private void jenis_impian_elektronik_MouseClick(object sender, MouseEventArgs e)
        {
            default_impian();
            jenis_impian_elektronik.Image = global::Darimu.Properties.Resources.elektronik_biru;
            jenis_impian = "Elektronik";
            tautan_gambar = "global::Darimu.Properties.Resources.logo_elektronik";
            validasi_button = true;
        }

        private void button_batal_ubah_profil_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            panel_isi_profil_saya.Visible = true;
            ambil_data_profil();
        }

        private void button_batal_impian_MouseEnter(object sender, EventArgs e)
        {
            button_batal_impian.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_impian_MouseLeave(object sender, EventArgs e)
        {
            button_batal_impian.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void jenis_impian_fashion_MouseClick(object sender, MouseEventArgs e)
        {
            default_impian();
            jenis_impian_fashion.Image = global::Darimu.Properties.Resources.fashion_biru;
            jenis_impian = "Fashion";
            tautan_gambar = "global::Darimu.Properties.Resources.logo_fashion";
            validasi_button = true;
        }

        private void jenis_impian_hiburan_MouseClick(object sender, MouseEventArgs e)
        {
            default_impian();
            jenis_impian_hiburan.Image = global::Darimu.Properties.Resources.hiburan_biru;
            jenis_impian = "Hiburan";
            tautan_gambar = "global::Darimu.Properties.Resources.logo_hiburan";
            validasi_button = true;
        }

        private void button_simpan_saldo_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekTambahSaldo(txt_isi_saldo, gambar_bank);

            if(cek == "valid")
            {
                long saldo_baru = long.Parse(txt_isi_saldo.Text.Trim());
                label_saldo.Text = ClassTransaksi.isi_saldo(nama_pengguna, saldo_baru, pilihan_bank).ToString();
                MessageBox.Show("Selamat! Anda berhasil tambah saldo",
                                "Tambah Saldo Berhasil",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            } else
            {
                MessageBox.Show(cek,
                                "Lengkapi Syarat!",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }
    }
}