<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Our Vessels
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
	<div id="main">
		<h2>Our Vessel</h2>

		<p>
			RiverDiver Dive Charters currently operates the vessel "Run Time", which is a Transport Canada certified vessel operated by an endorsed Captain.  
			The vessel has an enclosed head (washroom), change area, first aid supplies, oxygen, and all mandatory safety equipment.
		</p>

		<h3>Run Time</h3>
		<p>
		    Run Time is a Rosborough workboat and can accommodate up to 8 passengers.  
		    This vessel can also be trailered, this allows us to give our customers a 
		    wider range of options for their diving destinations.
		</p>

		<p>For more information please <a href="contact.aspx">contact us</a>.</p>
	</div>
	<div id="sidebar">
		<% Html.RenderPartial("SidebarGallery"); %>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHead" runat="server">
	<% Html.RenderPartial("SidebarGalleryAssets"); %>
</asp:Content>