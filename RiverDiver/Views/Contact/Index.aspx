<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Contact Us
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="main">
		<h2>Contact Us</h2>

		<p>
			If you would like to book a charter with us, we would love to hear from you!
		</p>

		<p>
			Please include as much information as possible about your proposed charter.  Also don't forget your own contact details, so we know
			how to reach you.
		</p>

		<p>
			<span class="contact-details">
				Rick Lymer<br/>
				905-706-6677<br/>
				<script type="text/javascript">mailto('ricklymer', 'gmail', 'com');</script>
			</span>
		</p>
	</div>
	<div id="sidebar">
		<% Html.RenderPartial("SidebarGallery"); %>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHead" runat="server">
	<% Html.RenderPartial("SidebarGalleryAssets"); %>
</asp:Content>
