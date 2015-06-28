<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RathianPlate.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Rathian's Plate</title>
    <link href="style.css" rel = stylesheet />
	<link href="http://vvn.nl/sites/all/themes/vvn/favicon.ico" rel="shortcut icon">  
	<meta name="description" content="Rathian Plate is the place for getting your hunting party together or studying your next hunt.">
	<meta name="author" content="Code Panda - www.codepanda.nl">
	<meta charset="UTF-8">
	<script language="javascript" src="javascript/jquery.js"></script>
</head>
<body>
    <div class="header">
			<div class="wrapper">
				<a href="default.aspx"><img src="img\header.png"></img></a>
				<ul class="menu">
					<li><a href="Login.aspx">Login</a></li>
					<li><a href="#">Armor</a></li>
					<li><a href="#">Monsters</a></li>
					<li><a href="Hunts.aspx">Hunts</a></li>
					<li><a href="default.aspx">Home</a></li>
				</ul>
			</div>
		</div>
		<div class="wrapper">
			<div class="container">
                <form id="formLogin" runat="server">
                    <div>
                        <div id ="errMessage" runat="server" style="margin: 5px;">
                            <span>You used the incorrect username or password</span>
                        </div>
                        <p>
			                <label for="tbUsername" style="margin-left: 5px;">Username: </label>
                            <asp:TextBox ID="tbUsername" runat="server" style="margin-left: 5px;"></asp:TextBox>
                        </p>
		                <p>
		                    <label for="tbPassword" style="margin-left: 5px;">Password: </label>
                            <asp:TextBox ID="tbPassword" runat="server" style="margin-left: 5px;" TextMode="Password"></asp:TextBox>
                        </p>
                        <asp:Button ID="btnLogin" runat="server" Text="Log in" OnClick="btnLogin_OnClick" style="margin-left: 5px;"/>
                    </div>
                </form>
            </div>
        </div>
</body>
</html>


                        