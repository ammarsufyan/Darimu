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
DROP TRIGGER tr_transaksi_saldo;
CREATE TRIGGER tr_transaksi_saldo
    ON tb_pengguna
    AFTER UPDATE 
AS
    BEGIN
        IF UPDATE(saldo)
        BEGIN
--	        DECLARE @saldo_before BIGINT
--	        DECLARE @saldo_after BIGINT
	        DECLARE @nama_pengguna VARCHAR(100)
	        DECLARE @keterangan VARCHAR(100)
	        DECLARE @perubahan_saldo BIGINT
--	        SET @saldo_before = (SELECT saldo FROM deleted)
--	        SET @saldo_after = (SELECT saldo FROM inserted)
	        SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
	        SET @perubahan_saldo = (SELECT saldo FROM inserted) - (SELECT saldo FROM deleted)
	        PRINT CONCAT('Perubahan Saldo : ', @perubahan_saldo)
--	        PRINT CONCAT('Saldo Sebelum : ', @saldo_before) 
--	        PRINT CONCAT('Saldo Sesudah : ', @saldo_after) 
	        IF (@perubahan_saldo < 0)
	        BEGIN
		       SET @keterangan = 'Tarik Saldo' 
	        END
	        IF (@perubahan_saldo > 0)
	        BEGIN
		        SET @keterangan = 'Setor Saldo'
	        END
	        DECLARE @saldo_saat_ini BIGINT
		    SET @saldo_saat_ini = (SELECT saldo FROM tb_pengguna WHERE nama_pengguna = @nama_pengguna)
			EXEC sp_transaksi @nama_pengguna, @perubahan_saldo, @keterangan, @saldo_saat_ini
        END
    END

DROP PROCEDURE sp_transaksi;
CREATE PROCEDURE sp_transaksi
	@nama_pengguna VARCHAR(100), @perubahan_saldo BIGINT, @keterangan VARCHAR(100), @saldo_saat_ini BIGINT
AS
    BEGIN
	    
        IF (@perubahan_saldo > 0)
        	BEGIN
	   			INSERT INTO tb_transaksi(nama_pengguna, tanggal, keterangan, debit, kredit, saldo)
	   			VALUES(@nama_pengguna, GETDATE(), @keterangan, ABS(@perubahan_saldo), 0, @saldo_saat_ini)
        	END
		IF (@perubahan_saldo < 0)
			BEGIN
				
	   			INSERT INTO tb_transaksi(nama_pengguna, tanggal, keterangan, debit, kredit, saldo)
	   			VALUES(@nama_pengguna, GETDATE(), @keterangan, 0, ABS(@perubahan_saldo), @saldo_saat_ini)
        	END
    END

-- ========================================================================
-- membuat trigger dan procedure untuk update saldo di tabungan impian
DROP TRIGGER tr_tabungan_impian;
CREATE TRIGGER tr_tabungan_impian
    ON tb_tabungan_impian
    AFTER UPDATE
AS
    BEGIN
        IF UPDATE(saldo_terkumpul)
        BEGIN
	        DECLARE @nama_pengguna VARCHAR(100)
	        DECLARE @keterangan VARCHAR(100)
	        DECLARE @perubahan_saldo BIGINT
	        DECLARE @nama_tabungan_impian VARCHAR(255)
	        
	        SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
	        SET @perubahan_saldo = (SELECT saldo_terkumpul FROM inserted) - (SELECT saldo_terkumpul FROM deleted)
	        SET @nama_tabungan_impian = (SELECT nama_tabungan_impian FROM inserted)
	        
	        PRINT CONCAT('Perubahan Saldo : ', @perubahan_saldo)
	 
	        IF (@perubahan_saldo < 0)
	        BEGIN
		       SET @keterangan = CONCAT('withdraw dari tabungan "', @nama_tabungan_impian, '"') 
	        END
	        IF (@perubahan_saldo > 0)
	        BEGIN
		        SET @keterangan = CONCAT('transfer ke tabungan "', @nama_tabungan_impian, '"') 
	        END
	        
	        DECLARE @saldo_saat_ini BIGINT
		    SET @saldo_saat_ini = (SELECT saldo_terkumpul FROM tb_tabungan_impian WHERE id_tabungan_impian = (SELECT id_tabungan_impian FROM inserted))
			EXEC sp_transaksi @nama_pengguna, @perubahan_saldo, @keterangan, @saldo_saat_ini
			UPDATE tb_pengguna SET saldo = saldo - @perubahan_saldo WHERE nama_pengguna = @nama_pengguna
        END
        
    END

-- ========================================================================
-- membuat trigger dan procedure untuk hapus pengguna
DROP TRIGGER  tr_hapus_pengguna;
CREATE TRIGGER tr_hapus_pengguna
	ON tb_pengguna
	INSTEAD OF DELETE 	
AS 
BEGIN
	DECLARE @nama_pengguna VARCHAR(100)
	SET @nama_pengguna = (SELECT nama_pengguna FROM deleted)
	PRINT @nama_pengguna
	EXEC sp_hapus_pengguna @nama_pengguna 
END


DROP PROCEDURE sp_hapus_pengguna;
CREATE PROCEDURE sp_hapus_pengguna
	@nama_pengguna VARCHAR(100)
AS 
BEGIN
	UPDATE tb_pengguna SET status_pengguna = 'Tidak Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_pengguna SET tanggal_tutup = GETDATE() WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Tidak Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_tabungan_impian SET tanggal_tutup = GETDATE() WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_transaksi SET status_data = 'Tidak Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_laporan SET status_laporan = 'Tidak Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_laporan SET tanggal_laporan_ditutup = GETDATE() WHERE nama_pengguna = @nama_pengguna
END


-- ========================================================================
-- membuat trigger dan procedure untuk mengembalikan akun pengguna
DROP TRIGGER  tr_mengembalikan_pengguna;
CREATE TRIGGER tr_mengembalikan_pengguna
	ON tb_pengguna
	AFTER UPDATE
AS 
BEGIN
	DECLARE @nama_pengguna VARCHAR(100)
	SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
	IF UPDATE(status_pengguna)
	BEGIN
		EXEC sp_mengembalikan_pengguna @nama_pengguna	
	END
END


DROP PROCEDURE sp_mengembalikan_pengguna;
CREATE PROCEDURE sp_mengembalikan_pengguna
	@nama_pengguna VARCHAR(100)
AS 
BEGIN
	UPDATE tb_pengguna SET tanggal_tutup = NULL WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_tabungan_impian SET status_tabungan_impian = 'Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_tabungan_impian SET tanggal_tutup = NULL WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_transaksi SET status_data = 'Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_laporan SET status_laporan = 'Aktif' WHERE nama_pengguna = @nama_pengguna
	UPDATE tb_laporan SET tanggal_laporan_ditutup = NULL WHERE nama_pengguna = @nama_pengguna
END
