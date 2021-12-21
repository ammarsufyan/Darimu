/*
KELAS : TI 3C 
MATKUL: Sistem Basis Data

** Basis Data Aplikasi Darimu **
Darimu adalah platform finansial Indonesia yang berfokus 
pada saving tracker agar kamu lebih sadar akan pemasukan 
dan pengeluaran keuanganmu.

        ** Anggota **
1. Cherrie Gracila Amanda (11200910000051)
2. Ammar Sufyan           (11200910000054)
3. Riandi Nandaputra      (11200910000062)
*/

-- ========================================================================
-- membuat db_darimu
CREATE DATABASE db_darimu;
DROP DATABASE db_darimu;
-- menggunakan db_darimu
USE db_darimu;

-- ========================================================================
-- membuat tb_admin
 DROP TABLE tb_admin;
CREATE TABLE tb_admin(
	nama_pengguna_admin VARCHAR(100) NOT NULL PRIMARY KEY,
	nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin VARCHAR(100) NOT NULL,
    tempat_tinggal VARCHAR(255) NOT NULL,
    tempat_lahir VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    nik VARCHAR(100) NOT NULL,
    kata_sandi NVARCHAR(255) NOT NULL,
    [status_admin] VARCHAR(100) NULL CHECK ([status_admin] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- memasukkan data admin (di sini ada 3 admin, yaitu Vera Liani, Uli Paris Usada, dan Maman Sitorus)
TRUNCATE TABLE tb_admin;
-- veraliani veraliani1010
-- uliparis uliparis2020
-- mamansitorus mamansitorus3030
INSERT INTO tb_admin(nama_pengguna_admin, nama_lengkap, jenis_kelamin, tempat_tinggal, tempat_lahir, tanggal_lahir, alamat_email, nik, kata_sandi, status_admin)
    VALUES
    ('veraliani', 'Vera Liani', 'Perempuan', 'Jl Ir H Juanda 5 A Plaza Ciputat Mas Bl C/P', 'Jakarta', '1992-04-29', 'vera_liani@gmail.com', '2375780219825719', '3C6C01FFC9CC0282161B4E59E476325EAFAAC99D75A78B1F59B24A0C1F766C56CEB216B336E136CAFE08F95585B7477567A9647CD011E6ED699453A913E84165
', 'Aktif'),
    ('uliparis', 'Uli Paris Usada', 'Perempuan', 'Kpg. Haji No. 518, Salatiga 99947', 'Gorontalo', '1988-04-01', 'uli_ph@gmail.com', '8795200921584695', 'E7749DCF0075979E070B30DB98112FD222F8F299678EF40BBE22EFB060F7A7959B25E06FFF9F256EB672501799FFBAE76FD4A6324702191A06289401F3778B78', 'Aktif'),
    ('mamansitorus', 'Maman Sitorus' , 'Laki-laki', 'Psr. Suryo No. 367, Langsa 82078, Jakarta', 'Papua Barat', '1970-06-10', 'abcdefg@gmail.com', '2221686886311030', 'AD73E20D5A3F8648BA866220371F175CD9DE207FCB477785003EECFBD4F64128EA7229CD9DD28F8AD13C9367BAD0CF991D53D60D39298D08E6F8EF59894F25BB', 'Aktif')    
    
-- ========================================================================
-- membuat tb_pengguna
 DROP TABLE tb_pengguna;
CREATE TABLE tb_pengguna(
    nama_pengguna VARCHAR(100) NOT NULL PRIMARY KEY,
    nama_lengkap VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    kata_sandi NVARCHAR(255) NOT NULL,
    saldo BIGINT NOT NULL,
    tanggal_buka DATETIME NULL,
    tanggal_tutup DATETIME NULL,
    [status_pengguna] VARCHAR(100) NULL CHECK ([status_pengguna] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_transaksi
 DROP TABLE tb_transaksi;
CREATE TABLE tb_transaksi (
    id INT IDENTITY(1,1) NOT NULL,
	id_transaksi AS ('TR-' + RIGHT('0000' + CAST(id AS VARCHAR(20)), 4)) PERSISTED PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    tanggal DATETIME NOT NULL,
    keterangan VARCHAR(255) NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    saldo BIGINT NOT NULL,
    [status_data] VARCHAR(100) NULL CHECK ([status_data] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_tabungan_impian
 DROP TABLE tb_tabungan_impian;
CREATE TABLE tb_tabungan_impian (
	id INT IDENTITY(1,1) NOT NULL,
	id_tabungan_impian AS ('TI-' + RIGHT('0000' + CAST(id AS VARCHAR(20)), 4)) PERSISTED PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    nama_tabungan_impian VARCHAR(100) NOT NULL,
    jenis_impian VARCHAR(100) NOT NULL,
    tautan_gambar VARCHAR(255) NOT NULL,
    saldo_terkumpul BIGINT NOT NULL,
    saldo_impian BIGINT NOT NULL,
    tenggat_waktu DATETIME NOT NULL,
    tanggal_buka DATETIME NOT NULL,
    tanggal_tutup DATETIME NULL,
    [status_tabungan_impian] VARCHAR(100) NULL CHECK ([status_tabungan_impian] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_laporan (untuk menampung data laporan dan dikirim ke admin)
 DROP TABLE tb_laporan;
CREATE TABLE tb_laporan(
    id INT IDENTITY(1,1) NOT NULL,
    id_laporan AS ('LPR-' + RIGHT('0000' + CAST(id AS VARCHAR(20)), 4)) PERSISTED PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NULL,
    subjek_alasan VARCHAR(100) NOT NULL,
    rincian_alasan VARCHAR(255) NOT NULL,
    tanggal_laporan_dibuat DATETIME NOT NULL,
    tanggal_laporan_ditutup DATETIME NULL,
    [status_laporan] VARCHAR(100) NULL CHECK ([status_laporan] IN ('Selesai', 'Belum Selesai')) DEFAULT 'Belum Selesai'
);

-- ========================================================================
-- membuat tb_log_data (untuk melihat apa saja yang dilakukan oleh user (hanya ADMIN!!!!!!!!!!!!!!))
DROP TABLE tb_log_data;
CREATE TABLE tb_log_data(
	id INT IDENTITY(1,1) NOT NULL,
	id_log_data AS ('LOG-' + RIGHT('0000' + CAST(id AS VARCHAR(20)), 4)) PERSISTED,
	nama_pengguna VARCHAR(100) NOT NULL,
	nama_pengguna_admin VARCHAR(100) NOT NULL,
	aktivitas VARCHAR(255) NOT NULL,
	keterangan VARCHAR(255) NOT NULL,
	tanggal_dibuat DATETIME NULL,
	tanggal_ditutup DATETIME NULL,
	[status_log_data] VARCHAR(100) NULL CHECK ([status_log_data] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
	PRIMARY KEY (id_log_data)
);

SELECT * FROM tb_laporan tl

SELECT * FROM tb_pengguna tp 

TRUNCATE TABLE tb_laporan 

INSERT INTO tb_laporan(nama_pengguna, subjek_alasan,rincian_alasan,tanggal_laporan_dibuat)
	VALUES
        ('ammarsufyan', 'Lupa akun', 'Akun saya Hilang Setelah hibernasi', GETDATE()),
		('ammarsufyan', 'Ditimpuk', 'Abis ditimpuk botol', GETDATE()),
		('ammarsufyan', 'BUG', 'BUG A ketemu B', GETDATE()),
		('ammarsufyan', 'Kosong', 'kosong pak', GETDATE());
		
INSERT INTO tb_laporan(nama_pengguna, nama_pengguna_admin, subjek_alasan,rincian_alasan,tanggal_laporan_dibuat, tanggal_laporan_ditutup, status_laporan)
	VALUES
        ('ammarsufyan', 'veraliani', 'Lupa akun', 'Akun saya Hilang Setelah hibernasi', GETDATE(), GETDATE(), 'Selesai'),
		('ammarsufyan', 'veraliani', 'Ditimpuk', 'Abis ditimpuk botol', GETDATE(), GETDATE(), 'Selesai'),
		('ammarsufyan', 'veraliani', 'BUG', 'BUG A ketemu B', GETDATE(), GETDATE(), 'Selesai');