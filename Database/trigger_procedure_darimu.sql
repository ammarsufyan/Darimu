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
-- membuat trigger dan procedure untuk update saldo di tabungan
CREATE TRIGGER tr_transaksi_saldo
    ON tb_pengguna
    AFTER UPDATE 
AS
    BEGIN
        IF UPDATE(saldo)
        BEGIN
	        DECLARE @id_pengguna VARCHAR(20)
	        DECLARE @perubahan_saldo BIGINT
	        SET @id_pengguna = (SELECT id_pengguna FROM inserted)
	        SET @perubahan_saldo = (SELECT saldo FROM inserted) - (SELECT saldo FROM deleted)
	        DECLARE @saldo_saat_ini BIGINT
		    SET @saldo_saat_ini = (SELECT saldo FROM tb_pengguna WHERE id_pengguna = @id_pengguna)
			EXEC sp_transaksi @id_pengguna, @perubahan_saldo, @saldo_saat_ini
        END
    END

CREATE PROCEDURE sp_transaksi
	@id_pengguna VARCHAR(20), @perubahan_saldo BIGINT, @saldo_saat_ini BIGINT
AS
    BEGIN
        IF (@perubahan_saldo > 0)
        	BEGIN
	   			INSERT INTO tb_transaksi(id_pengguna, tanggal, debit, kredit, saldo)
	   			VALUES(@id_pengguna, GETDATE(), ABS(@perubahan_saldo), 0, @saldo_saat_ini)
        	END
		IF (@perubahan_saldo < 0)
			BEGIN
	   			INSERT INTO tb_transaksi(id_pengguna, tanggal, debit, kredit, saldo)
	   			VALUES(@id_pengguna, GETDATE(), 0, ABS(@perubahan_saldo), @saldo_saat_ini)
        	END
    END

-- ========================================================================
-- membuat trigger dan procedure untuk hapus pengguna
CREATE TRIGGER tr_hapus_pengguna
	ON tb_pengguna
	INSTEAD OF DELETE 	
AS 
BEGIN
	DECLARE @id_pengguna VARCHAR(20)
	SET @id_pengguna = (SELECT id_pengguna FROM deleted)
	EXEC sp_hapus_pengguna @id_pengguna 
END

CREATE PROCEDURE sp_hapus_pengguna
	@id_pengguna VARCHAR(20)
AS 
BEGIN
	UPDATE tb_pengguna SET status_pengguna = 'Tidak Aktif' WHERE id_pengguna = @id_pengguna
	UPDATE tb_pengguna SET tanggal_tutup = GETDATE() WHERE id_pengguna = @id_pengguna
	UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Tidak Aktif' WHERE id_pengguna = @id_pengguna
	UPDATE tb_tabungan_impian SET tanggal_tutup = GETDATE() WHERE id_pengguna = @id_pengguna
	UPDATE tb_transaksi SET status_data = 'Tidak Aktif' WHERE id_pengguna = @id_pengguna
END

DELETE tb_pengguna WHERE id_pengguna = 'USER-0001';
UPDATE tb_pengguna SET status_pengguna = 'Aktif' WHERE id_pengguna = 'USER-0001';

-- ========================================================================
-- membuat trigger dan procedure untuk mengembalikan akun pengguna
CREATE TRIGGER tr_mengembalikan_pengguna
	ON tb_pengguna
	AFTER UPDATE
AS 
BEGIN
	DECLARE @id_pengguna VARCHAR(20)
	SET @id_pengguna = (SELECT id_pengguna FROM inserted)
	IF UPDATE(status_pengguna)
	BEGIN
		EXEC sp_mengembalikan_pengguna @id_pengguna	
	END
END

CREATE PROCEDURE sp_mengembalikan_pengguna
	@id_pengguna VARCHAR(20)
AS 
BEGIN
	UPDATE tb_pengguna SET tanggal_tutup = NULL WHERE id_pengguna = @id_pengguna
	UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Aktif' WHERE id_pengguna = @id_pengguna
	UPDATE tb_tabungan_impian SET tanggal_tutup = NULL WHERE id_pengguna = @id_pengguna
	UPDATE tb_transaksi SET status_data = 'Aktif' WHERE id_pengguna = @id_pengguna
END