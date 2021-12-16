<?php
	$dsn="bis_darimu"; // nama koneksi pada file dsn
	$username="sa"; // username sql
	$password="ammar1234"; // pass sql
	
	// koneksi via odbc
	$koneksi=odbc_connect($dsn, $username, $password);
	
	if($koneksi){
		echo "Berhasil";
	}
	else{
		echo "Gagal";
	}
?>