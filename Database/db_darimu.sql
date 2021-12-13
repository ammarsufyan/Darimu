CREATE DATABASE db_darimu;
USE db_darimu;

CREATE TABLE tb_admin(
	nik VARCHAR(100) NOT NULL PRIMARY KEY,
	nama_lengkap VARCHAR(100) NOT NULL,
    jenis_kelamin VARCHAR(10) NOT NULL,
    tempat_tinggal VARCHAR(255) NOT NULL,
    tempat_lahir VARCHAR(100) NOT NULL,
    tgl_lahir DATE NOT NULL,
    alamat_email VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NOT NULL,
    kata_sandi VARCHAR(100) NOT NULL,
    [status_admin] VARCHAR(255) NULL CHECK ([status_admin] IN ('Aktif', 'Tidak Aktif'))
);

CREATE TABLE tb_pengguna(
    nama_pengguna VARCHAR(100) NOT NULL PRIMARY KEY,
    nama_lengkap VARCHAR(100) NOT NULL,
    saldo BIGINT NOT NULL,
    tgl_buka DATETIME NOT NULL,
    tgl_tutup DATETIME NULL,
    [status_pengguna] VARCHAR(255) NULL CHECK ([status_pengguna] IN ('Aktif', 'Tidak Aktif'))
)

CREATE TABLE tb_tabungan(
	id_tabungan INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    saldo BIGINT NOT NULL,
    tgl_buka DATETIME NOT NULL,
    tgl_tutup DATETIME NULL,
    [status_tabungan] VARCHAR(255) NULL CHECK ([status_tabungan] IN ('Aktif', 'Tidak Aktif'))
);

CREATE TABLE tb_riwayat_tabungan (
    id_riwayat_tabungan VARCHAR(100) NOT NULL PRIMARY KEY,
    id_tabungan INT NOT NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    tanggal DATETIME NOT NULL,
    [status_riwayat_tabungan] VARCHAR(255) NULL CHECK ([status_riwayat_tabungan] IN ('Aktif', 'Tidak Aktif'))
);


CREATE TABLE tb_tabungan_impian(
	id_tabungan_impian INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    id_tabungan INT NOT NULL,
    saldo BIGINT NOT NULL,
    tgl_buka DATETIME NOT NULL,
    tgl_tutup DATETIME NULL,
    [status_tabungan_impian] VARCHAR(255) NULL CHECK ([status_tabungan_impian] IN ('Aktif', 'Tidak Aktif'))
);

CREATE TABLE tb_riwayat_tabungan_impian (
    id_riwayat_tabungan_impian VARCHAR(100) NOT NULL PRIMARY KEY,
    id_tabungan_impian INT NOT NULL,
    debit BIGINT NOT NULL,
    kredit BIGINT NOT NULL,
    tanggal DATETIME NOT NULL,
    [status_riwayat_tabungan_impian] VARCHAR(255) NULL CHECK ([status_riwayat_tabungan_impian] IN ('Aktif', 'Tidak Aktif'))
);

CREATE TABLE tb_laporan(
    id_laporan INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    nama_pengguna VARCHAR(100) NOT NULL,
    nama_pengguna_admin VARCHAR(100) NOT NULL,
    subjek_alasan VARCHAR(255) NOT NULL,
    rincian_alasan VARCHAR(255) NOT NULL,
    tgl_laporan_dibuat DATETIME NOT NULL,
    tgl_laporan_ditutup DATETIME NULL,
    [status_laporan] VARCHAR(255) NULL CHECK ([status_laporan] IN ('Aktif', 'Tidak Aktif'))
);

-- membuat admin baru
INSERT INTO tb_admin(nik, nama_lengkap, jenis_kelamin, tempat_tinggal, tempat_lahir, tgl_lahir, alamat_email, nama_pengguna_admin, kata_sandi, status_admin)
    VALUES
    ('2375780219825719', 'Vera Liani', 'Perempuan', 'Jl Ir H Juanda 5 A Plaza Ciputat Mas Bl C/P', 'Jakarta', '1992-04-29', 'vera_liani@gmail.com', 'vera_liani', 'dahC3ahz', 'Aktif'),
    ('8795200921584695', 'Uli Paris Usada', 'Perempuan', 'Kpg. Haji No. 518, Salatiga 99947', 'Gorontalo', '1988-04-01', 'uli_ph@gmail.com', 'Uli_PH', 'uli123', 'Aktif'),
    ('2221686886311030', 'Maman Sitorus' , 'Laki-laki', 'Psr. Suryo No. 367, Langsa 82078, Jakarta', 'Papua Barat', '1970-06-10', 'abcdefg@gmail.com', 'S_maman', 'maman10670', 'Aktif')