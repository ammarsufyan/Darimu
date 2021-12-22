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
    public partial class frm_setelah_login : Form
    {
        // declaration var
        private string nama_pengguna, pilihan_bank, jenis_impian, tautan_gambar, id_impian, keterangan_impian, keterangan_hapus_impian;
        private bool validasi_button = false;
        private int borderSize = 2;
        private long saldo_impian = 0;
        private long saldo_terkumpul = 0;
        private Image logo_tambah;

        public frm_setelah_login(string nama_pengguna)
        {
            InitializeComponent();
            CollapseMenu();
            this.Padding = new Padding(borderSize);
            this.BackColor = Color.FromArgb(33, 106, 155);
            this.nama_pengguna = nama_pengguna;
            label_saldo.Text = ClassTransaksi.get_saldo(nama_pengguna).ToString();
            ambil_data_profil();
            panel_isi_beranda.Visible = true;
            logo_tambah = Darimu.Properties.Resources.logo_tambah;
            tenggat_waktu_impian.MinDate = System.DateTime.Now;
            tenggat_waktu_impian.Value = System.DateTime.Now;
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
            label_username.Text = data_pengguna[1].ToString();
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

            panel_isi_profil_saya.Visible = false;
            panel_isi_ubah_profil_saya.Visible = false;

            panel_isi_tabungan_impian.Visible = false;
            panel_isi_tambah_saldo.Visible = false;
            panel_isi_tambah_tabungan_impian.Visible = false;
            panel_isi_saldo_tabungan_impian.Visible = false;
            panel_isi_riwayat_transaksi.Visible = false;

            panel_isi_laporan_saya.Visible = false;
            panel_isi_tambah_laporan.Visible = false;
            panel_isi_rincian_laporan.Visible = false;
        }

        private void default_impian(PictureBox logo, Label nama_impian, Label saldo_impian, Label saldo_terkumpul, Label tenggat_waktu, Button hapus, Button topup, Button selesai)
        {
            logo.Image = logo_tambah;
            logo.Cursor = System.Windows.Forms.Cursors.Hand;
            nama_impian.Text = "Nama Impianmu Di sini";
            saldo_impian.Text = "0";
            saldo_terkumpul.Text = "0";
            tenggat_waktu.Text = "00/00/0000";
            hapus.Visible = false;
            topup.Visible = false;
            selesai.Visible = false;
        }

        private void tampil_button_isi_impian(Button hapus, Button topup, Button selesai, Label saldo_terkumpul, Label saldo_impian)
        {
            hapus.Visible = true;
            topup.Visible = true;

            if (saldo_terkumpul.Text.Trim() == saldo_impian.Text.Trim())
            {
                selesai.Visible = true;
            }
            else
            {
                selesai.Visible = false;
            }
        }

        private void tampil_impian()
        {
            ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
            if (isi_impian.Count <= 0)
            {
                default_impian(logo_impian_1, label_nama_impian_1, label_saldo_impianmu_1, label_saldo_terkumpulmu_1, label_tenggat_waktu_1, button_hapus_impian_1, button_topup_impian_1, button_selesai_impian_1);
                default_impian(logo_impian_2, label_nama_impian_2, label_saldo_impianmu_2, label_saldo_terkumpulmu_2, label_tenggat_waktu_2, button_hapus_impian_2, button_topup_impian_2, button_selesai_impian_2);
                default_impian(logo_impian_3, label_nama_impian_3, label_saldo_impianmu_3, label_saldo_terkumpulmu_3, label_tenggat_waktu_3, button_hapus_impian_3, button_topup_impian_3, button_selesai_impian_3);
            }
            else
            {
                if (isi_impian.Count >= 6)
                {
                    label_nama_impian_1.Text = isi_impian[1].ToString();
                    set_logo_impian(isi_impian[2].ToString(), logo_impian_1);
                    label_saldo_terkumpulmu_1.Text = isi_impian[3].ToString();
                    label_saldo_impianmu_1.Text = isi_impian[4].ToString();
                    label_tenggat_waktu_1.Text = isi_impian[5].ToString();
                    tampil_button_isi_impian(button_hapus_impian_1, button_topup_impian_1, button_selesai_impian_1, label_saldo_terkumpulmu_1, label_saldo_impianmu_1);
                }
                else
                {
                    default_impian(logo_impian_1, label_nama_impian_1, label_saldo_impianmu_1, label_saldo_terkumpulmu_1, label_tenggat_waktu_1, button_hapus_impian_1, button_topup_impian_1, button_selesai_impian_1);
                }

                if (isi_impian.Count >= 11)
                {
                    label_nama_impian_2.Text = isi_impian[7].ToString();
                    set_logo_impian(isi_impian[8].ToString(), logo_impian_2);
                    label_saldo_terkumpulmu_2.Text = isi_impian[9].ToString();
                    label_saldo_impianmu_2.Text = isi_impian[10].ToString();
                    label_tenggat_waktu_2.Text = isi_impian[11].ToString();
                    tampil_button_isi_impian(button_hapus_impian_2, button_topup_impian_2, button_selesai_impian_2, label_saldo_terkumpulmu_2, label_saldo_impianmu_2);
                }
                else
                {
                    default_impian(logo_impian_2, label_nama_impian_2, label_saldo_impianmu_2, label_saldo_terkumpulmu_2, label_tenggat_waktu_2, button_hapus_impian_2, button_topup_impian_2, button_selesai_impian_2);
                }

                if (isi_impian.Count >= 16)
                {
                    label_nama_impian_3.Text = isi_impian[13].ToString();
                    set_logo_impian(isi_impian[14].ToString(), logo_impian_3);
                    label_saldo_terkumpulmu_3.Text = isi_impian[15].ToString();
                    label_saldo_impianmu_3.Text = isi_impian[16].ToString();
                    label_tenggat_waktu_3.Text = isi_impian[17].ToString();
                    tampil_button_isi_impian(button_hapus_impian_3, button_topup_impian_3, button_selesai_impian_3, label_saldo_terkumpulmu_3, label_saldo_impianmu_3);
                }
                else
                {
                    default_impian(logo_impian_3, label_nama_impian_3, label_saldo_impianmu_3, label_saldo_terkumpulmu_3, label_tenggat_waktu_3, button_hapus_impian_3, button_topup_impian_3, button_selesai_impian_3);
                }
            }
        }

        private void set_logo_impian(string tautan_logo, PictureBox logo)
        {
            logo.Cursor = System.Windows.Forms.Cursors.Default;
            if (tautan_logo == "global::Darimu.Properties.Resources.logo_umum")
            {
                logo.Image = global::Darimu.Properties.Resources.logo_umum;
            }
            else if (tautan_logo == "global::Darimu.Properties.Resources.logo_jalan_jalan")
            {
                logo.Image = global::Darimu.Properties.Resources.logo_jalan_jalan;
            }
            else if (tautan_logo == "global::Darimu.Properties.Resources.logo_elektronik")
            {
                logo.Image = global::Darimu.Properties.Resources.logo_elektronik;
            }
            else if (tautan_logo == "global::Darimu.Properties.Resources.logo_fashion")
            {
                logo.Image = global::Darimu.Properties.Resources.logo_fashion;
            }
            else if (tautan_logo == "global::Darimu.Properties.Resources.logo_hiburan")
            {
                logo.Image = global::Darimu.Properties.Resources.logo_hiburan;
            }
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
            panel_isi_tambah_tabungan_impian.Visible = true;
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
            var result = MessageBox.Show("Apakah kamu yakin ingin keluar akun?",
                                         "Keluar Akun",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                hide_panel();
                frm_sebelum_login p = new frm_sebelum_login();
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
            txt_isi_saldo.Text = "";
            gambar_bank.Image = null;
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
                var result = MessageBox.Show("Apakah kamu yakin ingin mengubah data?",
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
                    }
                    else
                    {
                        string hasil = ClassUser.ubah_data_pengguna(nama_pengguna, nama_lengkap, tanggal_lahir, alamat_email);
                        if (hasil == "Data Berhasil Diubah")
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
            }
            else
            {
                MessageBox.Show(cek, "Lengkapi dulu semua isiannya :)",
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
            if (txt_ubah_email.ReadOnly == false)
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
            if (cek == "valid")
            {
                var result = MessageBox.Show("Apakah kamu yakin ingin menambah impian?",
                                             "Konfirmasi Tambah Impian",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);

                if (result == DialogResult.Yes)
                {
                    string nama_tabungan_impian = txt_nama_tabungan_impian.Text.Trim();
                    string saldo_impian = txt_saldo_impian.Text.Trim();
                    // get the datepicker value
                    tenggat_waktu_impian.Format = DateTimePickerFormat.Custom;
                    tenggat_waktu_impian.CustomFormat = "yyyy/MM/dd";
                    string tenggat_waktu = tenggat_waktu_impian.Value.ToString("yyyy/MM/dd");

                    bool berhasil = ClassTabunganImpian.tambahImpian(nama_pengguna, nama_tabungan_impian, jenis_impian, tautan_gambar, saldo_impian, tenggat_waktu);

                    if (berhasil)
                    {
                        MessageBox.Show("Selamat! Impianmu telah terdaftar :D",
                                        "Sukses Menambah Impian",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                        hide_panel();
                        txt_nama_tabungan_impian.Text = "";
                        txt_saldo_impian.Text = "";
                        default_impian();
                        jenis_impian = "";
                        tautan_gambar = "";
                        validasi_button = false;
                        tampil_impian();
                        panel_isi_tabungan_impian.Visible = true;
                    }
                    else
                    {
                        MessageBox.Show("Maaf, maksimum tabungan impianmu hanya 3 nih :(",
                                        "Gagal Menambah Impian",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                }
            }
            else
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

        private void TabunganImpiantoolStripMenuItem_Click(object sender, EventArgs e)
        {
            ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
            if (isi_impian.Count <= 0)
            {
                tampil_impian();
                MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                "Tidak Ada Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
            else
            {
                tampil_impian();
            }

            hide_panel();
            panel_isi_tabungan_impian.Visible = true;
        }

        private void button_hapus_impian_1_MouseEnter(object sender, EventArgs e)
        {
            button_hapus_impian_1.Image = global::Darimu.Properties.Resources.icon_hapus_biru;
        }

        private void button_hapus_impian_1_MouseLeave(object sender, EventArgs e)
        {
            button_hapus_impian_1.Image = global::Darimu.Properties.Resources.icon_hapus;
        }

        private void button_selesai_impian_1_MouseEnter(object sender, EventArgs e)
        {
            button_selesai_impian_1.Image = global::Darimu.Properties.Resources.icon_selesai_biru;
        }

        private void button_selesai_impian_1_MouseLeave(object sender, EventArgs e)
        {
            button_selesai_impian_1.Image = global::Darimu.Properties.Resources.icon_selesai;
        }

        private void button_topup_impian_1_MouseEnter(object sender, EventArgs e)
        {
            button_topup_impian_1.Image = global::Darimu.Properties.Resources.icon_topup_biru;
        }

        private void button_topup_impian_1_MouseLeave(object sender, EventArgs e)
        {
            button_topup_impian_1.Image = global::Darimu.Properties.Resources.icon_topup;
        }

        private void button_hapus_impian_1_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menghapus impian?",
                                         "Hapus Impian",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Stop);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[1].ToString();
                    long saldo_baru = long.Parse(isi_impian[3].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[0].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Selamat! Impianmu berhasil terhapus.",
                                    "Sukses Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }

        private void button_simpan_isi_saldo_impian_MouseEnter(object sender, EventArgs e)
        {
            button_simpan_isi_saldo_impian.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_isi_saldo_impian_MouseLeave(object sender, EventArgs e)
        {
            button_simpan_isi_saldo_impian.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void button_batal_isi_saldo_impian_MouseEnter(object sender, EventArgs e)
        {
            button_batal_isi_saldo_impian.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_isi_saldo_impian_MouseLeave(object sender, EventArgs e)
        {
            button_batal_isi_saldo_impian.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void button_hapus_impian_2_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menghapus impian?",
                                         "Hapus Impian",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Stop);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[7].ToString();
                    long saldo_baru = long.Parse(isi_impian[9].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[6].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Selamat! Impianmu berhasil terhapus.",
                                    "Sukses Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }

        private void button_hapus_impian_2_MouseEnter(object sender, EventArgs e)
        {
            button_hapus_impian_2.Image = global::Darimu.Properties.Resources.icon_hapus_biru;
        }

        private void button_hapus_impian_2_MouseLeave(object sender, EventArgs e)
        {
            button_hapus_impian_2.Image = global::Darimu.Properties.Resources.icon_hapus;
        }

        private void button_hapus_impian_3_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menghapus impian?",
                                         "Hapus Impian",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Stop);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[13].ToString();
                    long saldo_baru = long.Parse(isi_impian[15].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[12].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Selamat! Impianmu sudah terhapus.",
                                    "Sukses Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menghapus Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }
        private void button_hapus_impian_3_MouseEnter(object sender, EventArgs e)
        {
            button_hapus_impian_3.Image = global::Darimu.Properties.Resources.icon_hapus_biru;
        }

        private void button_hapus_impian_3_MouseLeave(object sender, EventArgs e)
        {
            button_hapus_impian_3.Image = global::Darimu.Properties.Resources.icon_hapus;
        }

        private void button_topup_impian_2_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
            id_impian = isi_impian[6].ToString();
            saldo_terkumpul = long.Parse(isi_impian[9].ToString());
            saldo_impian = long.Parse(isi_impian[10].ToString());
            label_nama_impian_isi_saldo.Text = isi_impian[7].ToString();
            keterangan_impian = "Isi Saldo Impian " + isi_impian[7].ToString();
            panel_isi_saldo_tabungan_impian.Visible = true;
        }

        private void button_topup_impian_2_MouseEnter(object sender, EventArgs e)
        {
            button_topup_impian_2.Image = global::Darimu.Properties.Resources.icon_topup_biru;
        }

        private void button_topup_impian_2_MouseLeave(object sender, EventArgs e)
        {
            button_topup_impian_2.Image = global::Darimu.Properties.Resources.icon_topup;
        }

        private void button_topup_impian_3_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
            id_impian = isi_impian[12].ToString();
            saldo_terkumpul = long.Parse(isi_impian[15].ToString());
            saldo_impian = long.Parse(isi_impian[16].ToString());
            label_nama_impian_isi_saldo.Text = isi_impian[13].ToString();
            keterangan_impian = "Isi Saldo Impian" + isi_impian[13].ToString();
            panel_isi_saldo_tabungan_impian.Visible = true;
        }

        private void button_topup_impian_3_MouseEnter(object sender, EventArgs e)
        {
            button_topup_impian_3.Image = global::Darimu.Properties.Resources.icon_topup_biru;
        }

        private void button_topup_impian_3_MouseLeave(object sender, EventArgs e)
        {
            button_topup_impian_3.Image = global::Darimu.Properties.Resources.icon_topup;
        }

        private void button_selesai_impian_1_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menyelesaikan impian?",
                                        "Selesaikan Impian",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[1].ToString();
                    long saldo_baru = long.Parse(isi_impian[3].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[0].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Hore! Impianmu sudah selesai :D",
                                    "Sukses Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }

        private void button_selesai_impian_2_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menyelesaikan impian?",
                                       "Selesaikan Impian",
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[7].ToString();
                    long saldo_baru = long.Parse(isi_impian[9].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[6].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Selamat! Impianmu sudah selesai.",
                                    "Sukses Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }

        private void button_selesai_impian_2_MouseEnter(object sender, EventArgs e)
        {
            button_selesai_impian_2.Image = global::Darimu.Properties.Resources.icon_selesai_biru;
        }

        private void button_selesai_impian_2_MouseLeave(object sender, EventArgs e)
        {
            button_selesai_impian_2.Image = global::Darimu.Properties.Resources.icon_selesai;
        }

        private void button_selesai_impian_3_MouseClick(object sender, MouseEventArgs e)
        {
            var result = MessageBox.Show("Apakah kamu yakin ingin menyelesaikan impian?",
                                         "Selesaikan Impian",
                                         MessageBoxButtons.YesNo,
                                         MessageBoxIcon.Information);

            // If the yes button was pressed ...
            if (result == DialogResult.Yes)
            {
                try
                {
                    ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
                    keterangan_hapus_impian = "Ambil saldo dari impian " + isi_impian[13].ToString();
                    long saldo_baru = long.Parse(isi_impian[15].ToString());
                    label_saldo.Text = ClassTabunganImpian.hapusImpian(nama_pengguna, isi_impian[12].ToString(), saldo_baru, keterangan_hapus_impian).ToString();
                    MessageBox.Show("Selamat! Impianmu sudah selesai.",
                                    "Sukses Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Kamu belum mempunyai impian. Buat dulu yuk! :)",
                                    "Gagal Menyelesaikan Impian",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    tampil_impian();
                }
            }
        }

        private void button_selesai_impian_3_MouseEnter(object sender, EventArgs e)
        {
            button_selesai_impian_3.Image = global::Darimu.Properties.Resources.icon_selesai_biru;
        }

        private void button_selesai_impian_3_MouseLeave(object sender, EventArgs e)
        {
            button_selesai_impian_3.Image = global::Darimu.Properties.Resources.icon_selesai;
        }

        private void txt_isi_saldo_impian_GotFocus(object sender, EventArgs e)
        {
            if (txt_isi_saldo_impian.Text == "Masukkan Nominal Di sini")
            {
                txt_isi_saldo_impian.Text = "";
            }
        }

        private void txt_isi_saldo_impian_LostFocus(object sender, EventArgs e)
        {
            if (txt_isi_saldo_impian.Text == "")
            {
                txt_isi_saldo_impian.Text = "Masukkan Nominal Di sini";
            }
        }

        private void logo_impian_1_MouseClick(object sender, MouseEventArgs e)
        {
            if (logo_impian_1.Image == logo_tambah)
            {
                hide_panel();
                tampil_impian();
                panel_isi_tambah_tabungan_impian.Visible = true;
            }
        }

        private void logo_impian_2_MouseClick(object sender, MouseEventArgs e)
        {
            if (logo_impian_2.Image == logo_tambah)
            {
                hide_panel();
                tampil_impian();
                panel_isi_tambah_tabungan_impian.Visible = true;
            }
        }

        private void logo_impian_3_MouseClick(object sender, MouseEventArgs e)
        {
            if (logo_impian_3.Image == logo_tambah)
            {
                hide_panel();
                tampil_impian();
                panel_isi_tambah_tabungan_impian.Visible = true;
            }
        }

        private void button_batal_impian_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            txt_nama_tabungan_impian.Text = "";
            txt_saldo_impian.Text = "";
            default_impian();
            jenis_impian = "";
            tautan_gambar = "";
            validasi_button = false;
            tampil_impian();
            panel_isi_tabungan_impian.Visible = true;
        }

        private void laporanSayaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            hide_panel();
            ClassLaporan.riwayat_laporan(nama_pengguna, grid_laporan);
            panel_isi_laporan_saya.Visible = true;
        }

        private void grid_laporan_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                string id_laporan = grid_laporan.Rows[e.RowIndex].Cells[0].Value.ToString();
                string status_laporan = grid_laporan.Rows[e.RowIndex].Cells[4].Value.ToString();

                if (status_laporan == "Selesai")
                {
                    ArrayList data_rincian_laporan = ClassLaporan.rincian_laporan(id_laporan);
                    label_nomor_laporan.Text = data_rincian_laporan[0].ToString();
                    label_nama_pengguna_laporan.Text = data_rincian_laporan[1].ToString();
                    label_nama_admin_laporan.Text = data_rincian_laporan[2].ToString();
                    label_subjek_laporan.Text = data_rincian_laporan[3].ToString();
                    txt_rincian_laporan.Text = data_rincian_laporan[4].ToString();
                    label_tanggal_laporan_dibuat.Text = data_rincian_laporan[5].ToString();
                    label_tanggal_laporan_ditutup.Text = data_rincian_laporan[6].ToString();
                    label_status_laporan.Text = data_rincian_laporan[7].ToString();
                    hide_panel();
                    panel_isi_rincian_laporan.Visible = true;
                }
                else
                {
                    MessageBox.Show("Maaf, jadi buat kamu menunggu :(",
                                    "Laporanmu Sedang Kami Atasi",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Jika ingin melihat rincian laporanmu\nSilakan klik dua kali barisnya, ya. :)",
                                "Silakan Klik Dua Kali Barisnya",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void txt_isi_subjek_laporan_GotFocus(object sender, EventArgs e)
        {
            if (txt_isi_subjek_laporan.Text == "Ketik di sini...")
            {
                txt_isi_subjek_laporan.Text = "";
            }
        }

        private void txt_isi_subjek_laporan_LostFocus(object sender, EventArgs e)
        {
            if (txt_isi_subjek_laporan.Text == "")
            {
                txt_isi_subjek_laporan.Text = "Ketik di sini...";
            }
        }

        private void txt_isi_rincian_laporan_GotFocus(object sender, EventArgs e)
        {
            if (txt_isi_rincian_laporan.Text == "Ketik di sini...")
            {
                txt_isi_rincian_laporan.Text = "";
            }
        }

        private void txt_isi_rincian_laporan_LostFocus(object sender, EventArgs e)
        {
            if (txt_isi_rincian_laporan.Text == "")
            {
                txt_isi_rincian_laporan.Text = "Ketik di sini...";
            }
        }

        private void button_simpan_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekBuatLaporan(txt_isi_subjek_laporan, txt_isi_rincian_laporan);
            if (cek == "valid")
            {
                var result = MessageBox.Show("Apakah kamu yakin ingin membuat laporan?",
                                "Konfirmasi Buat Laporan",
                                MessageBoxButtons.YesNo,
                                MessageBoxIcon.Information);

                // If the yes button was pressed ...
                if (result == DialogResult.Yes)
                {
                    string subjek_laporan = txt_isi_subjek_laporan.Text.Trim();
                    string rincian_laporan = txt_isi_rincian_laporan.Text.Trim();
                    bool berhasil = ClassLaporan.buat_laporan(nama_pengguna, subjek_laporan, rincian_laporan);
                    if (berhasil)
                    {
                        MessageBox.Show("Maaf, ya, kalau ngerepotin kamu. :(",
                                        "Berhasil Membuat Laporan",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Yah, gagal buat laporan. :(",
                                        "Gagal Membuat Laporan",
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Information);
                    }
                    ClassLaporan.riwayat_laporan(nama_pengguna, grid_laporan);
                    txt_isi_subjek_laporan.Text = "Ketik di sini...";
                    txt_isi_rincian_laporan.Text = "Ketik di sini...";
                    hide_panel();
                    panel_isi_laporan_saya.Visible = true;
                }
            }
            else
            {
                MessageBox.Show(cek,
                                "Gagal Membuat Laporan",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void button_simpan_laporan_MouseEnter(object sender, EventArgs e)
        {
            button_simpan_laporan.Image = global::Darimu.Properties.Resources.button_simpan_dipencet;
        }

        private void button_simpan_laporan_MouseLeave(object sender, EventArgs e)
        {
            button_simpan_laporan.Image = global::Darimu.Properties.Resources.button_simpan;
        }

        private void button_batal_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            txt_isi_subjek_laporan.Text = "Ketik di sini...";
            txt_isi_rincian_laporan.Text = "Ketik di sini...";
            ClassLaporan.riwayat_laporan(nama_pengguna, grid_laporan);
            hide_panel();
            panel_isi_laporan_saya.Visible = true;
        }

        private void button_batal_laporan_MouseEnter(object sender, EventArgs e)
        {
            button_batal_laporan.Image = global::Darimu.Properties.Resources.button_batal_dipencet;
        }

        private void button_batal_laporan_MouseLeave(object sender, EventArgs e)
        {
            button_batal_laporan.Image = global::Darimu.Properties.Resources.button_batal;
        }

        private void label_kembali_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            ClassLaporan.riwayat_laporan(nama_pengguna, grid_laporan);
            txt_isi_subjek_laporan.Text = "Ketik di sini...";
            txt_isi_rincian_laporan.Text = "Ketik di sini...";
            hide_panel();
            panel_isi_laporan_saya.Visible = true;
        }

        private void label_kembali_laporan_MouseEnter(object sender, EventArgs e)
        {
            label_kembali_laporan.ForeColor = Color.Cyan;
        }

        private void label_kembali_laporan_MouseLeave(object sender, EventArgs e)
        {
            label_kembali_laporan.ForeColor = Color.White;
        }

        private void label_buat_laporan_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            label_isi_nama_pengguna_laporan.Text = nama_pengguna;
            panel_isi_tambah_laporan.Visible = true;
        }

        private void label_buat_laporan_MouseEnter(object sender, EventArgs e)
        {
            label_buat_laporan.ForeColor = Color.Cyan;
        }

        private void label_buat_laporan_MouseLeave(object sender, EventArgs e)
        {
            label_buat_laporan.ForeColor = Color.White;
        }

        private void button_batal_isi_saldo_impian_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            tampil_impian();
            panel_isi_tabungan_impian.Visible = true;
            txt_isi_saldo_impian.Text = "Masukkan Nominal Di sini";
        }

        private void button_topup_impian_1_MouseClick(object sender, MouseEventArgs e)
        {
            hide_panel();
            ArrayList isi_impian = ClassTabunganImpian.lihatImpian(nama_pengguna);
            id_impian = isi_impian[0].ToString();
            saldo_terkumpul = long.Parse(isi_impian[3].ToString());
            saldo_impian = long.Parse(isi_impian[4].ToString());
            label_nama_impian_isi_saldo.Text = isi_impian[1].ToString();
            keterangan_impian = "Isi Saldo Impian " + isi_impian[1].ToString();
            panel_isi_saldo_tabungan_impian.Visible = true;
        }

        private void button_simpan_isi_saldo_impian_MouseClick(object sender, MouseEventArgs e)
        {
            string cek = ClassValidasi.cekTambahSaldoImpian(txt_isi_saldo_impian);
            if (cek == "valid")
            {
                long saldo_baru = long.Parse(txt_isi_saldo_impian.Text.Trim());
                label_saldo.Text = ClassTabunganImpian.tambahSaldoImpian(nama_pengguna, saldo_baru, saldo_terkumpul, saldo_impian, id_impian, keterangan_impian).ToString();
                hide_panel();
                tampil_impian();
                panel_isi_tabungan_impian.Visible = true;
                txt_isi_saldo_impian.Text = "Masukkan Nominal Di sini";
            }
            else
            {
                MessageBox.Show(cek,
                                "Gagal Menambah Saldo Impian",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
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

            if (cek == "valid")
            {
                var result = MessageBox.Show("Apakah kamu yakin ingin menambah saldo?",
                                             "Konfirmasi Tambah Saldo",
                                             MessageBoxButtons.YesNo,
                                             MessageBoxIcon.Information);

                // If the yes button was pressed ...
                if (result == DialogResult.Yes)
                {
                    long saldo_baru = long.Parse(txt_isi_saldo.Text.Trim());
                    label_saldo.Text = ClassTransaksi.isi_saldo(nama_pengguna, saldo_baru, pilihan_bank).ToString();
                    MessageBox.Show("Selamat! Kamu berhasil tambah saldo :D",
                                    "Tambah Saldo Berhasil",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    txt_isi_saldo.Text = "";
                    gambar_bank.Image = null;
                    hide_panel();
                    panel_isi_beranda.Visible = true;
                }
            }
            else
            {
                MessageBox.Show(cek,
                                "Lengkapi dulu semua isiannya :)",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }

        }
    }
}