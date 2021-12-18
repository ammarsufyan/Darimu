<?php
	include "koneksi.php";
	
	// variabel untuk nangkep username password yang dikirim dari c#
	$nama_pengguna = $_GET['nama_pengguna'];
	
	// execute query 
	$query = "SELECT * FROM tb_pengguna WHERE nama_pengguna='$nama_pengguna'";
	$sql = odbc_exec($koneksi, $query);
	$data = odbc_fetch_array($sql);
	
	$response = "";
	
	// variabel untuk menampung nilai yang dicacah
	$nama_pengguna = $data['nama_pengguna'];
	$kata_sandi = $data['kata_sandi'];
	$nama_lengkap = $data['nama_lengkap'];
	$tanggal_lahir = $data['tanggal_lahir'];
	$alamat_email = $data['alamat_email'];
	$saldo = $data['saldo'];
	$tanggal_buka = $data['tanggal_buka'];
	$tanggal_tutup = $data['tanggal_tutup'];
	$status_pengguna = $data['status_pengguna'];
	
	if($kata_sandi_input == $kata_sandi) {
		$response = "Berhasil";
	}
	else { 
		$response = "Gagal"; 
	}
	
	// hasil xml nya
	header('Content-Type: text/xml');
	echo "<?xml version='1.0' encoding='UTF-8'?>";
	echo "<data_pengguna>";
		echo "<response>".$response."</response>";
		echo "<nama_pengguna>".$nama_pengguna."</nama_pengguna>";
		echo "<kata_sandi>".$kata_sandi."</kata_sandi>";
		echo "<nama_lengkap>".$nama_lengkap."</nama_lengkap>";
		echo "<tanggal_lahir>".$tanggal_lahir."</tanggal_lahir>";
		echo "<alamat_email>".$alamat_email."</alamat_email>";
		echo "<saldo>".$saldo."</saldo>";
		echo "<tanggal_buka>".$tanggal_buka."</tanggal_buka>";
		echo "<tanggal_tutup>".$tanggal_tutup."</tanggal_tutup>";
		echo "<status_pengguna>".$status_pengguna."</status_pengguna>";
	echo "</data_pengguna>";
?>