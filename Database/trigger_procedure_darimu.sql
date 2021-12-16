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

        DECLARE @nama_pengguna VARCHAR(100)
        DECLARE @keterangan VARCHAR(100)
        DECLARE @perubahan_saldo BIGINT

        SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
        SET @perubahan_saldo = (SELECT saldo FROM inserted) - (SELECT saldo FROM deleted)
        IF (@perubahan_saldo < 0)
        BEGIN
	       SET @keterangan = 'Tarik Saldo' 
        END
        IF (@perubahan_saldo > 0)
        BEGIN
	        SET @keterangan = 'Setor Saldo'
        END
        IF UPDATE(saldo)
        BEGIN
			EXEC sp_transaksi @nama_pengguna, @perubahan_saldo, @keterangan      
        END
    END

DROP PROCEDURE sp_transaksi;
CREATE PROCEDURE sp_transaksi
	@nama_pengguna VARCHAR(100), @perubahan_saldo BIGINT, @keterangan VARCHAR(100)
AS
    BEGIN
        IF (@perubahan_saldo > 0)
        	BEGIN
	   			INSERT INTO tb_transaksi(nama_pengguna, tanggal, keterangan, debit, kredit)
	   			VALUES(@nama_pengguna, GETDATE(), @keterangan, ABS(@perubahan_saldo), 0)
        	END
		IF (@perubahan_saldo < 0)
			BEGIN
				
	   			INSERT INTO tb_transaksi(nama_pengguna, tanggal, keterangan, debit, kredit)
	   			VALUES(@nama_pengguna, GETDATE(), @keterangan, 0, ABS(@perubahan_saldo))
        	END
    END

-- ========================================================================
-- membuat trigger dan procedure untuk update saldo di tabungan impian
CREATE TRIGGER tr_tabungan_impian
    ON tb_tabungan_impian
    AFTER UPDATE
AS
    BEGIN
        DECLARE @nama_pengguna VARCHAR(100)
        DECLARE @keterangan VARCHAR(100)
        DECLARE @perubahan_saldo BIGINT
        DECLARE @nama_tabungan_impian VARCHAR(255)
        
        SET @nama_pengguna = (SELECT nama_pengguna FROM inserted)
        SET @perubahan_saldo = (SELECT saldo_terkumpul FROM inserted) - (SELECT saldo_terkumpul FROM deleted)
        SET @nama_tabungan_impian = (SELECT nama_tabungan_impian FROM inserted)
 
        IF (@perubahan_saldo < 0)
        BEGIN
	       SET @keterangan = CONCAT('tarik saldo dari tabungan "', @nama_tabungan_impian, '"') 
        END
        IF (@perubahan_saldo > 0)
        BEGIN
	        SET @keterangan = CONCAT('setor saldo ke tabungan "', @nama_tabungan_impian, '"') 
        END
        
        IF UPDATE(saldo_terkumpul)
        BEGIN
			EXEC sp_transaksi @nama_pengguna, @perubahan_saldo, @keterangan
            UPDATE tb_pengguna SET saldo = saldo - @perubahan_saldo WHERE nama_pengguna = @nama_pengguna
        END
        
    END