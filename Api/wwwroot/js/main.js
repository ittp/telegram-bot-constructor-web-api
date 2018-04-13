
$(document).ready(function(){
	
	"use strict";
	
	// LOADER
	$("body").imagesLoaded().always(function(instance){
		$(".loader").delay(300).fadeOut(500);
	});
	
	
	// OFFCANVAS NAVIGATION
	$("#navigation").navigation({
		offCanvasSide: "right",
		overlayColor: "rgba(81, 210, 194, 0.9)"
	});
	
	/* Open the offcanvas panel */
	$(".offcanvas-btn").on("click touchstart", function(e){
		e.preventDefault();
		$("#navigation").data("navigation").toggleOffcanvas();
	});
	
	/* Close offcanvas panel after clicking on a link */
	$("#navigation").find(".nav-menu").find("a").on("click", function(){
		$("#navigation").data("navigation").toggleOffcanvas();
	});
	
	
	// APPLY STYLES ON SCROLL
	if($(window).scrollTop() > 10){
		$(".top-bar").addClass("top-bar-onscroll");
	}
	$(window).scroll(function(){
		if($(window).scrollTop() > 10){
			$(".top-bar").addClass("top-bar-onscroll");
		}
		else{
			$(".top-bar").removeClass("top-bar-onscroll");
		}
	});
	
	
	// OWL CAROUSEL
	$(".carousel-screens").owlCarousel({
		autoplay: true,
		autoplaySpeed: 1000,
		center: true,
		loop: true,
		nav: true,
		navSpeed: 1000,
		margin: 100,
		navText: ["<i class='fa fa-angle-left' aria-hidden='true'></i>", "<i class='fa fa-angle-right' aria-hidden='true'></i>"],
		responsive: {
			0:{
				items: 1
			},
			768:{
				items: 2
			},
			992:{
				items: 3
			}
		}
	});
	
	$(".carousel-testimoniails").owlCarousel({
		autoplay: true,
		autoplaySpeed: 1000,
		loop: true,
		nav: true,
		navSpeed: 1000,
		items: 1,
		navText: ["<i class='fa fa-angle-left' aria-hidden='true'></i>", "<i class='fa fa-angle-right' aria-hidden='true'></i>"]
	});
	
	
	// FORM VALIDATION
	$("#contact-form").validate({
		errorLabelContainer: $("#error-container"),
		rules: {
            name: {
                required: true,
                minlength: 2,
                lettersonly: true
            },
            email: {
                required: true,
                minlength: 6,
                email: true
            },
			subject: {
                required: true,
                minlength: 2
            },
			message: {
                required: true,
                minlength: 6
            }
		},
		messages: {
            name: {
                required: "Please enter your name",
                minlength: "Minimum 2 characters",
                lettersonly: "Only letters please!"
            },
            email: {
                required: "Please enter your email address",
                minlength: "Minimum 6 characters",
                email: "That's an invalid email"
            },
			subject: {
                required: "Please enter the subject",
                minlength: "Minimum 2 characters"
            },
			message: {
                required: "Please enter your message",
                minlength: "Minimum 6 characters"
            }
		},
		success: function(label) {
            label.addClass("valid").html("<span class='text-capitalize'>" + $(label).attr("for") + ": Ok</span>");
        },
		submitHandler: function(element) {

            var ajaxform = $(element),
                url = ajaxform.attr('action'),
                type = ajaxform.attr('method'),
                data = {};

            $(ajaxform).find('[name="submit"]').html('<i class="fa fa-circle-o-notch fa-spin fa-fw"></i> Sending...');


            ajaxform.find('[name]').each(function(index, value) {
                var field = $(this),
                    name = field.attr('name'),
                    value = field.val();

                data[name] = value;

            });

            $.ajax({
                url: url,
                type: type,
                data: data,
                success: function(response) {
                    if (response.type == 'success') {
                        $("#contact-form").after("<div class='alert alert-success mt-3' role='alert'><a href='#' class='close' data-dismiss='alert'>&times;</a>" + response.text + "</div>");
                        $(ajaxform).each(function() {
                            this.reset();
                            $(this).find('[name="submit"]').html('<i class="fa fa-paper-plane fa-fw"></i> Sent');
                        }).find('.valid').each(function() {
                            $(this).remove('label.valid');
                        })
                    } else if (response.type == 'error') {
                        $("#contact-form").after("<div class='alert alert-danger mt-3' role='alert'><a href='#' class='close' data-dismiss='alert'>&times;</a>" + response.text + "</div>");
                        $(ajaxform).find('[name="submit"]').text("Send the message");
                    }
                }
            });

            return false;
        }
	});
	
});