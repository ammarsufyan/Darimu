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
SELECT tbp.nama_pengguna, tbt.tanggal AS 'Tanggal Transaksi', tbt.keterangan AS 'Keterangan Transaksi', tbt.debit AS 'Debit', tbt.kredit AS 'Kredit', tbt.saldo AS 'Saldo'
FROM tb_transaksi AS tbt
INNER JOIN tb_pengguna tbp
ON tbt.nama_pengguna = tbp.nama_pengguna
WHERE tbp.status_pengguna = 'Aktif';

SELECT * FROM view_transaksi;

-- ========================================================================
--(VIEW 2) LAPORAN (KIRIM)
--nama_pengguna | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan

CREATE VIEW view_laporan_dikirim AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.nama_pengguna AS 'Nama Pengguna', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.nama_pengguna = tbp.nama_pengguna;

-- ========================================================================
--(VIEW 3) LAPORAN (ADMIN SELESAIKAN)
--nama_pengguna | nama_pengguna_admin | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan
DROP VIEW view_laporan_diterima;
CREATE VIEW view_laporan_diterima AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.nama_pengguna AS 'Nama Pengguna', tba.nama_lengkap AS 'Nama Admin', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.nama_pengguna = tbp.nama_pengguna
INNER JOIN tb_admin AS tba 
ON tbl.nama_pengguna_admin = tba.nama_pengguna_admin;


-- ========================================================================
--(VIEW 4) RIWAYAT TABUNGAN
DROP VIEW  view_riwayat_tabungan_impian;
CREATE VIEW view_riwayat_tabungan_impian AS
SELECT id_tabungan_impian AS 'ID',nama_pengguna AS 'NAMA PENGGUNA',nama_tabungan_impian AS 'NAMA TABUNGAN IMPIAN', jenis_impian AS 'JENIS IMPIAN', saldo_terkumpul AS 'SALDO TERKUMPUL', saldo_impian AS 'SALDO IMPIAN', tanggal_tutup AS 'TANGGAL TUTUP' 
FROM tb_tabungan_impian
WHERE status_tabungan_impian = 'Tidak Aktif';

SELECT * FROM view_riwayat_tabungan_impian ORDER BY [TANGGAL TUTUP] DESC;
