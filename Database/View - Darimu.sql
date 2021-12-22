--(VIEW 1) TRANSAKSI
--tanggal | keterangan | debit | kredit | saldo

DROP VIEW view_transaksi;
CREATE VIEW view_transaksi AS
SELECT tbp.nama_pengguna, tbt.tanggal AS 'Tanggal Transaksi', tbt.keterangan AS 'Keterangan Transaksi', tbt.debit AS 'Debit', tbt.kredit AS 'Kredit', tbt.saldo AS 'Saldo'
FROM tb_transaksi AS tbt
INNER JOIN tb_pengguna tbp
ON tbt.nama_pengguna = tbp.nama_pengguna
WHERE tbp.status_pengguna = 'Aktif';

SELECT * FROM view_transaksi;
--SELECT CAST(tanggal AS SMALLDATETIME) AS 'tanggal' FROM tb_transaksi;

--(VIEW 2) LAPORAN (KIRIM)
--nama_pengguna | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan

DROP VIEW view_laporan_dikirim;
CREATE VIEW view_laporan_dikirim AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.nama_pengguna AS 'Nama Pengguna', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.nama_pengguna = tbp.nama_pengguna;

SELECT * FROM view_laporan_dikirim;

--(VIEW 3) LAPORAN (DITERIMA ADMIN)
--nama_pengguna | nama_pengguna_admin | subjek_alasan | rincian_alasan | tanggal_laporan_dibuat | tanggal_laporan_dibuat_ditutup | status_laporan

DROP VIEW view_laporan_diterima;

CREATE VIEW view_laporan_diterima AS
SELECT tbl.id_laporan AS 'ID Laporan', tbp.nama_pengguna AS 'Nama Pengguna', tba.nama_lengkap AS 'Nama Admin', tbl.subjek_alasan AS 'Subjek', tbl.rincian_alasan AS 'Rincian', tbl.tanggal_laporan_dibuat AS 'Tanggal Dibuat', tbl.tanggal_laporan_ditutup AS 'Tanggal Ditutup', tbl.status_laporan AS 'Status Laporan'
FROM tb_laporan AS tbl
INNER JOIN tb_pengguna AS tbp 
ON tbl.nama_pengguna = tbp.nama_pengguna
INNER JOIN tb_admin AS tba 
ON tbl.nama_pengguna_admin = tba.nama_pengguna_admin;

SELECT * FROM view_laporan_diterima;

SELECT * FROM view_laporan_dikirim WHERE [STATUS LAPORAN] = 'Belum Selesai'
