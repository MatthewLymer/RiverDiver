<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Affiliates
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="main">
		<h2>Affiliates</h2>

		<div class="affiliate">
			<h3 id="twobeeztraining-header"><a href="http://2beeztraining.com">2Beez Training</a></h3>
			<p>
				SDI / TDI 5 Star Dive Centre in Whitby, Ontario<br />
				Recreational and Technical dive training<br />
				Air, Nitrox and Trimix<br />
				Equipment Sales and Service
			</p>
		</div>
        
        <div class="affiliate">
			<h3 id="motelroyale-header"><a href="http://www.1000islandsivylea.com">Capricorn Motel Royale</a></h3>
			<p>
                Capricorn Motel Royale<br/>
                108 Ivy Lea Road<br/>
                Ivy Lea Village, Lansdowne<br/>
                Ontario, K0E 1L0
			</p>
		</div>
	</div>
	<div id="sidebar">
		<p>RiverDiver affiliates itself with many different businesses in order to provide excellent service to our customers.</p>
		<p>If you are currently an affiliate, or you wish to affiliate yourself with RiverDiver, please <a href="contact.aspx">contact us</a>.</p>
	</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="PageHead" runat="server">
</asp:Content>
