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
-- menggunakan db_darimu
USE db_darimu;

-- ========================================================================
--(VIEW 1) TRANSAKSI
--tanggal | keterangan | debit | kredit | saldo

CREATE VIEW view_transaksi AS
SELECT tbp.id_pengguna AS 'ID Pengguna', tbt.tanggal AS 'Tanggal Transaksi', tbt.keterangan AS 'Keterangan Transaksi', tbt.debit AS 'Debit', tbt.kredit AS 'Kredit', tbt.saldo AS 'Saldo'
FROM tb_transaksi AS tbt
INNER JOIN tb_pengguna tbp
ON tbt.id_pengguna = tbp.id_pengguna
WHERE tbp.status_pengguna = 'Aktif';

SELECT * FROM view_transaksi;

-- ========================================================================
--(VIEW 2) LAPORAN (KIRIM)
--ID LAPORAN | id_pengguna | nama_pengguna | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan

CREATE VIEW view_laporan_dikirim AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.id_pengguna AS 'ID Pengguna', tbp.nama_pengguna AS 'Nama Pengguna', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.id_pengguna = tbp.id_pengguna;

-- ========================================================================
--(VIEW 3) LAPORAN (ADMIN SELESAIKAN)
--id_laporan | nama_pengguna | nama_lengkap | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan

CREATE VIEW view_laporan_diterima AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.nama_pengguna AS 'Nama Pengguna', tba.nama_lengkap AS 'Nama Admin', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.id_pengguna = tbp.id_pengguna
INNER JOIN tb_admin AS tba 
ON tbl.id_admin = tba.id_admin;

DROP VIEW view_laporan_dikirim

-- ========================================================================
--(VIEW 4) RIWAYAT TABUNGAN
CREATE VIEW view_riwayat_tabungan_impian AS
SELECT id_tabungan_impian AS 'ID Tabungan', id_pengguna AS 'ID Pengguna', nama_tabungan_impian AS 'Nama Tabungan Impian', jenis_impian AS 'Jenis Impian', saldo_terkumpul AS 'Saldo Terkumpul', saldo_impian AS 'Saldo Impian', tanggal_tutup AS 'Tanggal Tutup' 
FROM tb_tabungan_impian
WHERE status_tabungan_impian = 'Tidak Aktif';

SELECT * FROM view_laporan_dikirim;
SELECT * FROM view_riwayat_tabungan_impian;

DROP VIEW view_riwayat_tabungan_impian 