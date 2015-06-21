<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="default.aspx.cs" Inherits="RathianPlate._default" %>

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
				<a href="#"><img src="img\header.png"></img></a>
				<ul class="menu">
					<li><a href="#">Login</a></li>
					<li><a href="#">Armor</a></li>
					<li><a href="#">Monsters</a></li>
					<li><a href="#">Hunts</a></li>
					<li><a href="#">Home</a></li>
				</ul>
			</div>
		</div>
		<div class="wrapper">
			<div class="container">
				<asp:Repeater ID="rptHunts" runat="server">
                <HeaderTemplate>
                    <table border="1" width="100%">
                        <tr>
                            <th>Title</th>
                            <th>Monsters</th>
                            <th>Starting Time</th>
                            <th># of Hunters</th>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td> <a href="<%# "Hunts.aspx?query=" + Eval("id") %>"> <%# Eval("Name")%></a></td>
                        <td><%#Eval("Quest") %></td>
                        <td><%#Eval("StartTime")%></td>
                        <td><%#Eval("NumberHunters")%></td>
                    </tr>
                </ItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
			</div>
		</div>
	</body>
</html>
