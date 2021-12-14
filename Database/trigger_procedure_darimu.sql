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
-- membuat trigger dan procedure untuk membuat tabungan otomatis
-- setelah user berhasil mendaftar pada aplikasi Darimu
--DROP TRIGGER tr_tabungan;
CREATE TRIGGER tr_tabungan
	ON tb_pengguna
	AFTER INSERT
AS
	BEGIN
    	DECLARE @nama_pengguna VARCHAR(100)	
		SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
        EXEC sp_tabungan_insert @nama_pengguna
	END

--DROP PROCEDURE sp_tabungan_insert;
CREATE PROCEDURE sp_tabungan_insert
    @nama_pengguna VARCHAR(100)
AS
    BEGIN
        INSERT INTO tb_tabungan(nama_pengguna, saldo, tanggal_buka, status_tabungan)
            VALUES(@nama_pengguna, 0, GETDATE(), 'Aktif')
    END

-- ========================================================================
-- membuat trigger dan procedure untuk update saldo di tabungan
CREATE TRIGGER tr_tabungan_update_saldo
    ON tb_riwayat_tabungan
    AFTER INSERT
AS
    BEGIN
        DECLARE @saldo BIGINT, id_tabungan INT
        SET @saldo = (SELECT saldo FROM inserted)
        SET @id_tabungan = (SELECT id_tabungan FROM inserted)
        EXEC sp_tabungan_update_saldo @saldo @id_tabungan
    END

CREATE PROCEDURE sp_tabungan_update_saldo
    @saldo BIGINT, @id_tabungan INT
AS
    BEGIN
        UPDATE INTO tb_tabungan SET saldo = @saldo WHERE id_tabungan = @id_tabungan
    END

-- ========================================================================
-- membuat trigger dan procedure untuk update saldo di tabungan impian
CREATE TRIGGER tr_tabungan_impian_update_saldo
    ON tb_riwayat_tabungan_impian
    AFTER INSERT
AS
    BEGIN
        DECLARE @saldo BIGINT, id_tabungan INT
        SET @saldo = (SELECT saldo FROM inserted)
        SET @id_tabungan = (SELECT id_tabungan FROM inserted)
        EXEC sp_tabungan_impian_update_saldo @saldo @id_tabungan
    END

CREATE PROCEDURE sp_tabungan_impian_update_saldo
    @saldo BIGINT, @id_tabungan INT
AS
    BEGIN
        UPDATE INTO tb_tabungan SET saldo = @saldo WHERE id_tabungan = @id_tabungan
    END