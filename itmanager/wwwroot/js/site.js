// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

	function clickTab(id) {	
		
		let ids = ['#tabSpecification', '#tabDescription', '#tabReview', '#tabInventory','#ltabSpecification', '#ltabDescription', '#ltabReview', '#ltabInventory'];
    
		$.each(ids, function(key, value) {
			console.log(value); 
			$(value).removeClass('active');
		});

		$('#'+id).addClass("active");
		$('#l'+id).addClass("active");
	}
