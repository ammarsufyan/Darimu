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
	nik VARCHAR(100) NOT NULL PRIMARY KEY,
	nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin VARCHAR(10) NOT NULL,
    tempat_tinggal VARCHAR(255) NOT NULL,
    tempat_lahir VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NOT NULL,
    kata_sandi VARCHAR(255) NOT NULL,
    [status_admin] VARCHAR(255) NULL CHECK ([status_admin] IN ('Aktif', 'Tidak Aktif'))
);

-- memasukkan data admin (di sini ada 3 admin, yaitu Vera Liani, Uli Paris Usada, dan Maman Sitorus)
INSERT INTO tb_admin(nik, nama_lengkap, jenis_kelamin, tempat_tinggal, tempat_lahir, tanggal_lahir, alamat_email, nama_pengguna_admin, kata_sandi, status_admin)
    VALUES
    ('2375780219825719', 'Vera Liani', 'Perempuan', 'Jl Ir H Juanda 5 A Plaza Ciputat Mas Bl C/P', 'Jakarta', '1992-04-29', 'vera_liani@gmail.com', 'vera_liani', 'dahC3ahz', 'Aktif'),
    ('8795200921584695', 'Uli Paris Usada', 'Perempuan', 'Kpg. Haji No. 518, Salatiga 99947', 'Gorontalo', '1988-04-01', 'uli_ph@gmail.com', 'Uli_PH', 'uli123', 'Aktif'),
    ('2221686886311030', 'Maman Sitorus' , 'Laki-laki', 'Psr. Suryo No. 367, Langsa 82078, Jakarta', 'Papua Barat', '1970-06-10', 'abcdefg@gmail.com', 'S_maman', 'maman10670', 'Aktif');

-- ========================================================================
-- membuat tb_pengguna
-- DROP TABLE tb_pengguna;
CREATE TABLE tb_pengguna(
    nama_pengguna VARCHAR(100) NOT NULL PRIMARY KEY,
    nama_lengkap VARCHAR(100) NOT NULL,
    tanggal_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    kata_sandi VARCHAR(255) NOT NULL,
    [status_pengguna] VARCHAR(255) NULL CHECK ([status_pengguna] IN ('Aktif', 'Tidak Aktif'))
); 

DROP TABLE tb_pengguna

-- ========================================================================
-- membuat tb_tabungan
-- DROP TABLE tb_tabungan;
CREATE TABLE tb_tabungan(
	id INT IDENTITY(1,1) NOT NULL,
	id_tabungan AS ('TBN-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    nama_pengguna VARCHAR(100) NOT NULL,
    saldo BIGINT NOT NULL,
    tanggal_buka DATETIME NOT NULL,
    tanggal_tutup DATETIME NULL,
    [status_tabungan] VARCHAR(255) NULL CHECK ([status_tabungan] IN ('Aktif', 'Tidak Aktif'))
);

-- ========================================================================
-- membuat tb_riwayat_tabungan
-- DROP TABLE tb_riwayat_tabungan;
CREATE TABLE tb_riwayat_tabungan (
    id INT IDENTITY(1,1) NOT NULL,
	id_riwayat_tabungan AS ('RT-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    id_tabungan VARCHAR(10) NOT NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    tanggal DATETIME NOT NULL,
    [status_riwayat_tabungan] VARCHAR(255) NULL CHECK ([status_riwayat_tabungan] IN ('Aktif', 'Tidak Aktif'))
);

-- ========================================================================
-- membuat tb_tabungan_impian
-- DROP TABLE tb_tabungan_impian;
CREATE TABLE tb_tabungan_impian(
	id INT IDENTITY(1,1) NOT NULL,
	id_tabungan_impian AS ('TBNI-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    id_tabungan VARCHAR(10) NOT NULL,
    nama_tabungan_impian VARCHAR(100) NOT NULL,
    saldo_terkumpul BIGINT NOT NULL,
    saldo_impian BIGINT NOT NULL,
    tenggat_waktu DATETIME NOT NULL,
    tanggal_buka DATETIME NOT NULL,
    tanggal_tutup DATETIME NULL,
    [status_tabungan_impian] VARCHAR(255) NULL CHECK ([status_tabungan_impian] IN ('Aktif', 'Tidak Aktif'))
);

-- ========================================================================
-- membuat tb_riwayat_tabungan_impian
-- DROP TABLE tb_riwayat_tabungan_impian;
CREATE TABLE tb_riwayat_tabungan_impian (
    id INT IDENTITY(1,1) NOT NULL,
	id_riwayat_tabungan_impian AS ('RI-' + RIGHT('000' + CAST(id AS VARCHAR(10)), 3)) PERSISTED,
    id_tabungan_impian VARCHAR(10) NOT NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    tanggal DATETIME NOT NULL,
    [status_riwayat_tabungan_impian] VARCHAR(255) NULL CHECK ([status_riwayat_tabungan_impian] IN ('Aktif', 'Tidak Aktif'))
);

-- ========================================================================
-- membuat tb_laporan (untuk menampung data laporan dan dikirim ke admin)
-- DROP TABLE tb_laporan;
CREATE TABLE tb_laporan(
    id_laporan INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NOT NULL,
    subjek_alasan VARCHAR(255) NOT NULL,
    rincian_alasan VARCHAR(255) NOT NULL,
    tanggal_laporan_dibuat DATETIME NOT NULL,
    tanggal_laporan_ditutup DATETIME NULL,
    [status_laporan] VARCHAR(255) NULL CHECK ([status_laporan] IN ('Aktif', 'Tidak Aktif'))
);

SELECT * FROM tb_pengguna tp 