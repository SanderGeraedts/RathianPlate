<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="newHunt.aspx.cs" Inherits="RathianPlate.newHunt" %>

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
                <form id="formNewHunt" runat="server">
                    <div id ="errMessage" runat="server" style="margin: 5px; margin-left: 20px;">
                        Some required fields where not filled in.
                    </div>
                    <div id ="errDescription" runat="server" style="margin: 5px; margin-left: 20px;">
                        The description can only contain 255 characters or less.
                    </div>
                    <label for="tbHours" style="margin-left: 20px;"><b>Time: </b></label><br />
                    <span style="margin-left: 20px">In</span> <asp:TextBox ID="tbHours" runat="server" TextMode="Number" Width="84px"></asp:TextBox> Hours<br/><br />
	                <label for="tbHallId" style="margin-left: 20px;"><b>Hall-Id: </b></label><br />
                    <asp:TextBox ID="tbHallId" runat="server" style="margin-left: 20px;"></asp:TextBox><br/><br />
	                <label for="ddlQuests" style="margin-left: 20px;"><b>Quest: </b></label><br />
                    <asp:DropDownList ID="ddlQuests" runat="server" style="margin-left: 20px; min-width: 170px"></asp:DropDownList><br /><br />
	                <label for="tbDescription" style="margin-left: 20px;"><b>Description: </b></label><br />
                    <asp:TextBox ID="tbDescription" runat="server" style="margin-left: 20px;" TextMode="MultiLine" Width="450px" Height="200px"></asp:TextBox><br />
                    <asp:Button ID="btnCallHunt" runat="server" style="margin-left: 20px;" Text="Submit Hunt" OnClick="btnCallHunt_OnClick" Width="128px"/>
                </form>
            </div>
        </div>
</body>
</html>
