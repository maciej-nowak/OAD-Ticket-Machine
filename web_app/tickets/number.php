<!DOCTYPE HTML>
<html>
<head>
	<meta http-equiv="Content-type" content="text/html; charset=utf-8">
	<title>APO - biletomat</title>
	<link rel="stylesheet" href="css/style.css">
</head>
<body>

<div class=ramka>
<?php 
	echo '<p></br></br></br></br><div class="odstep"><b>WYBRANY BILET</b></div></p>';
	$ile = $_GET['cash'];
	echo '<div class="odstep2">';
	
	if($ile == 3.00)
	{echo '<p><a class="button2"><b>DO 10 MINUT</b></br><font color="yellow">NORMALNY</font></br><b>STREFA A+B+C                3.00ZL</b></a>';}
	
	else if ($ile == 1.50)
	{echo '	<a class="button3"><b>DO 10 MINUT</b></br><font color="yellow">ULGOWY</font></br><b>STREFA A+B+C                1.50ZL</b></a>';}
	
	else if ($ile == 4.60)
	{echo '<a class="button2"><b>DO 40 MINUT lub 1 PRZEJAZD</b></br><font color="yellow">NORMALNY</font></br><b>STREFA A+B+C                4.60ZL</b></a>';}
	
	else if ($ile == 2.30)
	{echo '<a class="button3"><b>DO 40 MINUT lub 1 PRZEJAZD</b></br><font color="yellow">ULGOWY</font></br><b>STREFA A+B+C                2.30ZL</b></a>';}
	
	else if ($ile == 13.60)
	{echo '<a class="button2"><b>24-GODZINNY</b></br><font color="yellow">NORMALNY</font></br><b>STREFA A+B+C                13.60ZL</b></a>';}
	
	else if ($ile == 6.80)
	{echo '	<a class="button3"><b>24-GODZINNY</b></br><font color="yellow">ULGOWY</font></br><b>STREFA A+B+C                6.80ZL</b></a>';}
	
	else if ($ile == 21.00)
	{echo '<a class="button2"><b>48-GODZINNY</b></br><font color="yellow">NORMALNY</font></br><b>STREFA A+B+C                21.00ZL</b></a>';}
	
	else if ($ile == 10.50)
	{echo '	<a class="button3"><b>48-GODZINNY</b></br><font color="yellow">ULGOWY</font></br><b>STREFA A+B+C                10.50ZL</b></a>';}
	
	else if ($ile == 27.00)
	{echo '<a class="button2"><b>72-GODZINNY</b></br><font color="yellow">NORMALNY</font></br><b>STREFA A+B+C                27.00ZL</b></a>';}
	
	else if ($ile == 13.50)
	{echo '<a class="button3"><b>72-GODZINNY</b></br><font color="yellow">ULGOWY</font></br><b>STREFA A+B+C                13.50ZL</b></a>';}
	
	echo '</div></br></br></br></br>';
	
	echo 
	'<p><div class="odstep"><b>LICZBA BILETOW</b></div></p>
	
	<form action="payment.php" method="GET">	
		<div class="odstep3"><input type="text" value=1 name="count"/>
		<input type="hidden" name="cash" value="' . $ile . '">	
		<input type=submit value="ZATWIERDZ"/></div>	
	</form>';

?>
</div>
</body>
</html>