/// <reference path="jquery-1.6.1.js" />

function mailto(acct, domain, tld){
	document.write('<a href="mailto:'+acct+'@'+domain+'.'+tld+'">'+acct+'@'+domain+'.'+tld+'</a>');
}

$(document).ready(function(){
	//var hostSelector = (window.location.protocol + '//' + window.location.hostname).replace(/\//g, '\\\/').replace(/:/g, '\\\:');
	//$('a[href^=http]').not('[href^=' + hostSelector + ']').attr('target', '_blank');
	$('a[href^=http]').attr('target', '_blank');
});