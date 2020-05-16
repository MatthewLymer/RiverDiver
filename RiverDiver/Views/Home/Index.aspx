<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Standard.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ContentPlaceHolderID="TitleContent" runat="server">
	RiverDiver Dive Charters
</asp:Content>

<asp:Content ContentPlaceHolderID="MainContent" runat="server">
	<div id="main">
		<h2>Welcome to RiverDiver Dive Charters</h2>
		
		<p>
			RiverDiver Dive Charters is a dive charter operation based both in the upper St. Lawrence River, 1000 Islands Region of Ontario and the Toronto Islands area.
			We cater to all types of divers, and we visit many different <a href="charters.aspx">dives &amp; charters</a> which offer a wide range of opportunities
			to our customers.  From pleasure diving to technical dives, there is something for everyone.  To personally discuss all that we exhibit, 
			please <a href="contact.aspx">contact us</a>, we would love to hear from you.
		</p>

		<p>
			Your licensed Captain, Rick Lymer, is an insured dive master with advanced Trimix certification.  He has been  safely operating dive charters since 1992.
		</p>

		<p>
			We welcome you, your friends, students, clubs, and groups a fantastic time visiting Toronto and the 1000 Islands with us.  To book your next dive with
			RiverDiver Dive Charters, please <a href="contact.aspx">contact</a> us and discuss all the possiblities.
		</p>
		
        <%--		
        <p>
			For businesses, RiverDiver <a href="affiliates.aspx">affiliates</a> itself with many dive shops, accommodations, and other establishments that are useful to our customers, 
			if you would want to affiliate yourself or your business with RiverDiver, then please <a href="contact.aspx">talk to us</a>.
		</p>
        --%>

		<%--
		<p>We are a Dive charter operation in the upper St Lawrence River, 1000 Islands Region of Ontario.  All Brockville and Rockport dives sites are accessed by River. We are located in what is referred to as the Caribbean of the North. Unlike the Caribbean the water here is fresh your gear won't need to be rinsed off diving here.  Our mid summer water temperatures are often in the 70's F. with decent visibility.  We offer diving from mid April to November.</p>

		<p>The 1000 Islands Region is a scenic, picturesque gateway for tourism and offers a variety of accommodations and activities for everyone. The river has been a transportation and trade corridor for  generations. The river however can and has been dangerous for navigation with it's currents, islands and shoals. These navigational hazards have also claimed numerous wrecks in this region. Many of these wreck sites are still relatively intact and preserved in the fresh water for divers to enjoy and peek back in time. The wrecks range in age by well over 100 years.</p>

		<p>Our boat RIVERDIVER is a 30 foot by 12 foot beam, twin screw, 12 passenger certified dive boat. RIVERDIVER is a multi level vessel offering generous space and comfort for all weather conditions.  We have an enclosed washroom and a change area aboard to accommodate our passengers.  Riverdiver has all mandatory safety equipment including first aid and oxygen. Generally our dive trips consist of 2 dives with about 1 hour between dives to move to another location and provide sufficient time between dives. Your dive trip generally runs approximately 5 hours. We will also customize excursions to suit your needs.</p>
		--%>
	</div>
	<div id="sidebar">
		<% Html.RenderPartial("SidebarGallery"); %>
	</div>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="PageHead" runat="server">
	<% Html.RenderPartial("SidebarGalleryAssets"); %>
</asp:Content>