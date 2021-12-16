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
-- menggunakan db_darimu
USE db_darimu;

-- ========================================================================
-- membuat tb_admin
-- DROP TABLE tb_admin;
CREATE TABLE tb_admin(
	nama_pengguna_admin VARCHAR(100) NOT NULL PRIMARY KEY,
	nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin VARCHAR(10) NOT NULL,
    tempat_tinggal VARCHAR(255) NOT NULL,
    tempat_lahir VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    nik VARCHAR(100) NOT NULL,
    kata_sandi NVARCHAR(255) NOT NULL,
    [status_admin] VARCHAR(255) NULL CHECK ([status_admin] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- memasukkan data admin (di sini ada 3 admin, yaitu Vera Liani, Uli Paris Usada, dan Maman Sitorus)
INSERT INTO tb_admin(nik, nama_lengkap, jenis_kelamin, tempat_tinggal, tempat_lahir, tanggal_lahir, alamat_email, nama_pengguna_admin, kata_sandi, status_admin)
    VALUES
    ('vera_liani', 'Vera Liani', 'Perempuan', 'Jl Ir H Juanda 5 A Plaza Ciputat Mas Bl C/P', 'Jakarta', '1992-04-29', 'vera_liani@gmail.com', '2375780219825719', 'dahC3ahz', 'Aktif'),
    ('Uli_PH', 'Uli Paris Usada', 'Perempuan', 'Kpg. Haji No. 518, Salatiga 99947', 'Gorontalo', '1988-04-01', 'uli_ph@gmail.com', '8795200921584695', 'uli123', 'Aktif'),
    ('S_maman', 'Maman Sitorus' , 'Laki-laki', 'Psr. Suryo No. 367, Langsa 82078, Jakarta', 'Papua Barat', '1970-06-10', 'abcdefg@gmail.com', '2221686886311030', 'maman10670', 'Aktif');

-- ========================================================================
-- membuat tb_pengguna
-- DROP TABLE tb_pengguna;
CREATE TABLE tb_pengguna(
    nama_pengguna VARCHAR(100) NOT NULL PRIMARY KEY,
    nama_lengkap VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    kata_sandi NVARCHAR(255) NOT NULL,
    saldo BIGINT NOT NULL,
    tanggal_buka DATETIME NULL,
    tanggal_tutup DATETIME NULL,
    [status_pengguna] VARCHAR(255) NULL CHECK ([status_pengguna] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_transaksi
CREATE TABLE tb_transaksi (
    id INT IDENTITY(1,1) NOT NULL,
	id_transaksi AS ('TR-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    nama_pengguna VARCHAR(100) NOT NULL,
    tanggal DATETIME NOT NULL,
    keterangan VARCHAR(255) NOT NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    [status_data] VARCHAR(255) NULL CHECK ([status_data] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_tabungan_impian
CREATE TABLE tb_tabungan_impian(
	id INT IDENTITY(1,1) NOT NULL,
	id_tabungan_impian AS ('TI-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    nama_pengguna VARCHAR(10) NOT NULL,
    nama_tabungan_impian VARCHAR(100) NOT NULL,
    saldo_terkumpul BIGINT NOT NULL,
    saldo_impian BIGINT NOT NULL,
    tenggat_waktu DATETIME NOT NULL,
    tanggal_buka DATETIME NOT NULL,
    tanggal_tutup DATETIME NULL,
    [status_tabungan_impian] VARCHAR(255) NULL CHECK ([status_tabungan_impian] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_tabungan_impian
CREATE TABLE tb_tabungan_impian(
	id INT IDENTITY(1,1) NOT NULL,
	id_tabungan_impian AS ('TI-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    nama_pengguna VARCHAR(10) NOT NULL,
    nama_tabungan_impian VARCHAR(100) NOT NULL,
    saldo_terkumpul BIGINT NOT NULL,
    saldo_impian BIGINT NOT NULL,
    tenggat_waktu DATETIME NOT NULL,
    tanggal_buka DATETIME NOT NULL,
    tanggal_tutup DATETIME NULL,
    [status_tabungan_impian] VARCHAR(255) NULL CHECK ([status_tabungan_impian] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);

-- ========================================================================
-- membuat tb_laporan (untuk menampung data laporan dan dikirim ke admin)
--  DROP TABLE tb_laporan;
CREATE TABLE tb_laporan(
    id_laporan INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NOT NULL,
    subjek_alasan VARCHAR(255) NOT NULL,
    rincian_alasan VARCHAR(255) NOT NULL,
    tanggal_laporan_dibuat DATETIME NOT NULL,
    tanggal_laporan_ditutup DATETIME NULL,
    [status_laporan] VARCHAR(255) NULL CHECK ([status_laporan] IN ('Aktif', 'Tidak Aktif')) DEFAULT 'Aktif'
);