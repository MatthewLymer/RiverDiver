<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl" %>

<link href="/css/jquery.lightbox.css" rel="stylesheet" type="text/css" />
<script src="/js/jquery.lightbox.js" type="text/javascript"></script>
<script type="text/javascript">
	// <![CDATA[

	$(document).ready(function(){
		$("#sidebar .gallery a.image").lightBox();
	});

	// ]]>
</script>