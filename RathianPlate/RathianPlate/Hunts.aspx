<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Hunts.aspx.cs" Inherits="RathianPlate.Hunts" %>

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
			    <div id="NoQuery" runat="server">
                    <form runat="server">
                        <asp:Button ID="btnNewHunt" runat="server" Text="Call a Hunt" OnClick="btnNewHunt_Click" />
                    </form>
                    <h1>Your Hunt log</h1>
			        <asp:Repeater ID="rptJoinedHunts" runat="server">
                        <HeaderTemplate>
                            <table border="1" width="100%">
                                <tr>
                                    <th>Title</th>
                                    <th>Monsters</th>
                                    <th>Starting Time</th>
                                    <th># of Hunters</th>
                                    <th>Hall-Id</th>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td> <a href="<%# "Hunts.aspx?query=" + Eval("id") %>">Select</a></td>
                                <td><%#Eval("Quest") %></td>
                                <td><%#Eval("StartTime")%></td>
                                <td><%#Eval("NumberHunters")%></td>
                                <td><%#Eval("HallId")%></td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
			    </div>
                <div id="YesQuery" runat="server">
                    <form id="formHunt" runat="server">
                        <asp:Button ID="btnJoinHunt" runat="server" Text="Join Hunt" OnClick="btnJoinHunt_OnClick"/><br /><br />
                        <b>Quest Name:</b> <asp:Label ID="lblQuest" runat="server" Text="Label"></asp:Label><br /><br />

                        <b>Description:</b><br/>
                        <asp:Label ID="lblDescription" runat="server" Text="
                            "></asp:Label><br /><br/>

                        <b>Hall Id: </b><asp:Label ID="lblHallId" runat="server" Text="Label"></asp:Label><br />
                        <b>Starting Time: </b><asp:Label ID="lblStartTime" runat="server" Text="Label"></asp:Label>
                    
                        <asp:Repeater ID="rptHuntersHunt" runat="server">
                            <HeaderTemplate>
                                <h3>Hunters:</h3>
                                <ul>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <li><a href="Hunter.aspx?query=<%#Eval("Id") %>"><%#Eval("Name") %></a></li>
                            </ItemTemplate>
                            <FooterTemplate>
                                </ul>
                            </FooterTemplate>
                        </asp:Repeater>
                        <asp:Repeater ID="rptMessagesHunt" runat="server">
                            <HeaderTemplate>
                                <h3>Messages:</h3>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <div style="margin-bottom: 10px;">
                                    <b><%#Eval("Hunter.Name") %></b><br />
                                    <%#Eval("Text") %><br />
                                    <i><%#Eval("SentOn") %></i><br />
                                </div>
                            </ItemTemplate>
                        </asp:Repeater>
                        <asp:TextBox ID="tbMessage" runat="server"></asp:TextBox><asp:Button ID="btnMessage" runat="server" Text="Send" OnClick="btnMessage_OnClick"/>
                    </form>
                </div>
			</div>
		</div>
	</body>
</html>
