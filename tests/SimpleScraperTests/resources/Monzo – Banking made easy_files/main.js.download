"use strict";

// A missing window.Analytics object will cause errors. We create a
// fake object with the same signature if necessary to prevend this errors.
window.Analytics = window.Analytics || {
  pageview: function() {},
  event: function() {}
};

Analytics.API = window.Mondo.config.apiBaseURL;
Analytics.ClientId = window.Mondo.config.apiClientId;
Analytics.ClientSecret = window.Mondo.config.apiClientSecret;

// Parse query string
var qs = (function(a) {
  if (a == "") return {};
  var b = {};
  for (var i = 0; i < a.length; ++i) {
    var p = a[i].split("=", 2);
    if (p.length == 1) b[p[0]] = "";
    else b[p[0]] = decodeURIComponent(p[1].replace(/\+/g, " "));
  }
  return b;
})(window.location.search.substr(1).split("&"));

// Smooth scrolls
$(function() {
  $('a[href*="#"]:not([href="#"])').on("click", function() {
    if (
      location.pathname.replace(/^\//, "") ==
        this.pathname.replace(/^\//, "") &&
      location.hostname == this.hostname
    ) {
      var target = $(this.hash);
      target = target.length ? target : $("[name=" + this.hash.slice(1) + "]");
      if (target.length) {
        $("html, body").animate(
          {
            scrollTop: target.offset().top
          },
          1000
        );
        return false;
      }
    }
  });
});

if (document.location.host !== "localhost"
  && document.location.host !== "127.0.0.1"
  && document.location.protocol !== "file:") {
  var allScriptElements = document.getElementsByTagName('script');
  var lastScriptElement = allScriptElements[allScriptElements.length -1];
  var script = document.createElement( 'script' );
  script.src = 'https://internal-api.monzo.com/static/scroll.js';
  lastScriptElement.parentNode.insertBefore(script, lastScriptElement);
}

$(function() {
  Analytics.pageview();
  $(document.body).addClass("anim");

  // If user not already signed up, show CTA in blog posts
  if (document.cookie.indexOf("alreadySignedUp=") < 0) {
    $("#signup-blog").show();
  }

  $("header .opener").on("click", function(e) {
    e.preventDefault;
    $("header").addClass("open");
  });

  $("video")
    .on("ended", function(event) {
      event.target.currentTime = 0;
      event.target.play();
    })
    .on("playing", function(event) {
      $(event.target)
        .parent()
        .addClass("video-playing");
    });

  // Event when hitting the "app store" button
  $("a#download-app-store").on("click", function(e) {
    Analytics.event("app-download.go", {
      platform: "ios",
      href: e.target.href
    });
  });

  // Event when hitting the "play store" button
  $("a#download-play-store").on("click", function(e) {
    Analytics.event("play-download.go", {
      platform: "play",
      href: e.target.href
    });
  });

  var emailForm = $("#email-form form");
  emailForm.on({
    focusin: function() {
      emailForm.addClass("focus");
    },
    focusout: function() {
      emailForm.removeClass("focus");
    },
    submit: function(event) {
      event.preventDefault();

      // Basic validation
      var email = $('input[type="email"]', emailForm).val();
      if (email.length < 1) {
        return;
      }

      // If javascript is disabled our email submission form POSTs to the
      // Mailchimp page, so we still get a sign up
      // Otherwise, submit via our API
      var analyticsData = { user_email: email };
      Analytics.event("waitlist.signup.attempt", analyticsData);
      var waitlistSignupUrl =
        window.Mondo.config.apiBaseURL + "/waitlist/signup";

      // @TODO: Please forgive me; this needs to be chained on the analytics call
      setTimeout(function() {
        $.ajax({
          url: waitlistSignupUrl,
          type: "POST",
          data: emailForm.serialize(),
          cache: false,
          dataType: "json",
          success: function(data) {
            if (data.id !== "") {
              success(data);
            } else {
              console.error("Form submission failed: ", data);
              swal(
                "Subscription failed",
                "Sorry, we couldn't register you. Please try again.",
                "error"
              );
              if (window.ga !== undefined) {
                window.ga("send", "event", "Signup form", "Signup error");
                window.ga("send", "event", "Signup form", "WL Email error"); // new tracking
              }
            }
          },
          error: function(xhr) {
            Analytics.event("waitlist.signup.failure", analyticsData);
            var resp = JSON.parse(xhr.responseText);
            swal(
              "Subscription failed",
              resp.code === "bad_request.email_already_registered"
                ? "Sorry, that email is already in use"
                : "Sorry, we couldn't register you. Please try again.",
              "error"
            );
            if (window.ga !== undefined) {
              window.ga("send", "event", "Signup form", "XHR error");
              window.ga("send", "event", "Signup form", "WL Email XHR error"); // new tracking
            }
          }
        });
      }, 300);

      function success(data) {
        var androidSurveyForm = $("form#android-survey");
        Analytics.event("waitlist.signup.success", analyticsData);

        $("input", emailForm)
          .attr("disabled", "disabled")
          .attr("autocomplete", "off")
          .blur();

        $('input[type="email"]', emailForm)
          .val("")
          .attr("placeholder", "");

        androidSurveyForm.append(
          $("<input />", {
            type: "hidden",
            name: "id",
            value: data.id
          })
        );

        androidSurveyForm.append(
          $("<input />", {
            type: "hidden",
            name: "token",
            value: data.token
          })
        );

        $("body")
          .addClass("signup-complete")
          .addClass("signup-flow")
          .scrollTop(0);

        $("#email-form")
          .removeClass("step-inprogress")
          .addClass("step-complete");

        if (window.ga !== undefined) {
          window.ga("send", "event", "Signup form", "Submitted"); // original goal tracking
          window.ga("send", "pageview", "/waitlist/eligibility"); // new tracking
        }

        // Track facebook completion
        if (window.fbq !== undefined) {
          window.fbq("track", "Lead");
        }

        // Track Twitter completion
        if (window.twttr !== undefined) {
          window.twttr.conversion.trackPid("ntvj9", {
            tw_sale_amount: 0,
            tw_order_quantity: 0
          });
        }

        // Store cookie so user won't see CTA form in blog posts
        document.cookie = "alreadySignedUp = true; path=/";
      }
    }
  });

  // Deal with signup from email form through CTA in blog posts
  var emailFormBlog = $("#signup-blog form");
  $(emailFormBlog).on({
    focusin: function() {
      emailForm.addClass("focus");
    },
    focusout: function() {
      emailForm.removeClass("focus");
    },
    submit: function(event) {
      event.preventDefault();

      // Basic validation
      if ($('input[type="email"]', emailFormBlog).val().length < 1) {
        return;
      }

      // If javascript is disabled our email submission form POSTs to the
      // Mailchimp page, so we still get a sign up
      // Otherwise, submit via our API
      var waitlistSignupUrl =
        window.Mondo.config.apiBaseURL + "/waitlist/signup";

      function success(data) {
        $("input", emailFormBlog)
          .attr("disabled", "disabled")
          .attr("autocomplete", "off")
          .blur();
        $('input[type="email"]', emailFormBlog)
          .val("")
          .attr("placeholder", "");

        // Store cookie so user won't see form again in blog posts
        document.cookie = "alreadySignedUp = true; path=/";
      }

      $.ajax({
        url: waitlistSignupUrl,
        type: "POST",
        data: emailFormBlog.serialize(),
        cache: false,
        dataType: "json",
        success: function(data) {
          if (data.id !== "") {
            success(data);
            $("#signup-blog .intro").hide();
            $("#input-div").hide();
            $("#success-text").show();

            $("#signup-blog form")
              .children()
              .hide();
            $(".signup-blog-success-text").show();
          } else {
            console.error("Form submission failed: ", data);
            swal("Sorry, we couldn't register you. Please try again.");
            window.ga("send", "event", "Signup form", "Signup error");
            window.ga("send", "event", "Signup form", "WL Email error"); // new tracking
          }
        },
        error: function(xhr) {
          var resp = JSON.parse(xhr.responseText);
          var alertMsg = "Sorry, we couldn't register you. Please try again.";
          swal(
            "Subscription failed",
            resp.code === "bad_request.email_already_registered"
              ? "Sorry, that email is already in use"
              : "Sorry, we couldn't register you. Please try again.",
            "error"
          );
          window.ga("send", "event", "Signup form", "XHR error");
          window.ga("send", "event", "Signup form", "WL Email XHR error"); // new tracking
        }
      });
    }
  });

  // Handle claim form
  var claimForm = $("#claim-form form");
  claimForm.on({
    focusin: function() {
      claimForm.addClass("focus");
    },
    focusout: function() {
      claimForm.removeClass("focus");
    },
    submit: function(event) {
      event.preventDefault();

      // Basic validation
      var emailInput = $('input[type="email"]', claimForm);
      var email = emailInput.val();
      if (email.length < 1) {
        return;
      }

      if (!validateEmail(email)) {
        swal(
          "Failed to claim ticket",
          "You must enter a valid email address to claim the ticket.",
          "warning"
        );
        return;
      }
      var pattern = emailInput.attr("pattern");
      var re = new RegExp(pattern);
      if (pattern && !re.test(email)) {
        swal(
          "Failed to claim ticket",
          "You must enter a valid email address to claim the ticket.",
          "warning"
        );
        return;
      }

      // If javascript is disabled our email submission form POSTs to the
      // Mailchimp page, so we still get a sign up
      // Otherwise, submit via our API
      var analyticsData = { user_email: email };
      Analytics.event("golden-ticket.claim.attempt", analyticsData);

      var ticket = Utils.getUrlQueryParam("utm_content");
      if (typeof claimForm.data("ticket") !== "undefined") {
        ticket = claimForm.data("ticket");
      }
      var goldenTicketClaimUrl =
        window.Mondo.config.apiBaseURL + "/golden-ticket/" + ticket + "/claim";

      // @TODO: Please forgive me; this needs to be chained on the analytics call
      setTimeout(function() {
        $.ajax({
          url: goldenTicketClaimUrl,
          type: "PUT",
          data: claimForm.serialize(),
          cache: false,
          dataType: "json",
          success: function(data) {
            Analytics.event("golden-ticket.claim.success", analyticsData);

            $("#claim-ticket").hide();
            $("#already-claimed").hide();
            $("#claim-success").show();

            $("#claim-success p").html(
              "Now download the Monzo app and by entering your email <b>" +
                email +
                "</b>, you'll be able to sign up immediately."
            );
          },
          error: function(xhr) {
            Analytics.event("golden-ticket.claim.failure", analyticsData);
            var resp = JSON.parse(xhr.responseText);

            if (resp.code == "bad_request.ticket_already_claimed") {
              $("#claim-ticket").hide();
              $("#already-claimed").show();
              $("#claim-success").hide();
            } else {
              swal(
                "Failed to claim ticket",
                "Sorry, there was an error claiming your ticket. Please try again.",
                "error"
              );
            }
          }
        });
      }, 300);
    }
  });
});

function validateEmail(email) {
  var re = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
  return re.test(email);
}

$(function() {
  var md = new MobileDetect(window.navigator.userAgent);

  $(".show-iOs").hide();
  $(".show-androidOs").hide();
  $(".show-otherOs").hide();
  if (md.is("iOS")) {
    $(".show-iOs").show();
  } else if (md.is("AndroidOS")) {
    $(".show-androidOs").show();
  } else {
    $(".show-otherOs").show();
  }
});

$(function () {
  var tables = document.querySelectorAll('#article-text table');

  for (var i = 0; i < tables.length; i++) {
      var wrapper = document.createElement('div');
      var table = tables[i];

      table.parentNode.insertBefore(wrapper, table);
      wrapper.appendChild(table);
      wrapper.style.overflow = "hidden";
      wrapper.style.overflowX = "auto";
  }
});

/*** <LOCATION BANNER SCRIPT> ***/
function getCookieValue(key) {
  var cookies = document.cookie.split(';').reduce(function (accumulator, cookie) {
    var cookieParts = cookie.split('=');
    // Some browsers add a space between semicolon that separate cookie values e.g. foo=bar; bar=foo
    // So we need to remove any spaces from the key before adding it to the object
    var key = cookieParts[0].trim();
    var value = cookieParts[1];

    accumulator[key] = value;
    return accumulator
  }, {});

  return cookies[key];
}

function isUserOnTheUSSite() {
  return window.location.pathname.indexOf('/usa') > -1
}

function isUSUser() {
  var location = getCookieValue('x-location');
  return location && location === 'US';
}

$(function () {
  var SESSION_STORAGE_BANNER_KEY = 'location-banner-dismissed';
  var HIDDEN_CLASS_NAME = 'u-display-none';
  // We map the country codes that we get from cloudflare to pages on the site
  // You can view all the country codes on the following link:
  // https://support.cloudflare.com/hc/en-us/articles/205072537-What-are-the-two-letter-country-codes-for-the-Access-Rules-
  var locationMappings = {
    'GB': '/',
    'US': '/usa'
  };
  var hasUserDismissedBanner = sessionStorage.getItem(SESSION_STORAGE_BANNER_KEY) === '1';

  var $banner = $('.js-location-banner');
  var $bannerDismissBtn = $('.js-dismiss-banner');
  var $locationDropdown = $('.js-location-dropdown');
  var $locationConfirm = $('.js-location-confirm');

  if (isUSUser() && !isUserOnTheUSSite() && !hasUserDismissedBanner) {
    $banner.removeClass(HIDDEN_CLASS_NAME);
  }

  // Set the href on the confirm button based on the current value of the dropdown.
  $locationConfirm.attr('href', locationMappings[$locationDropdown.val()]);

  // Update the href when the location changes
  $locationDropdown.on('change', function (evt) {
    var href = locationMappings[evt.currentTarget.value];
    if (href) {
      $locationConfirm.attr('href', href);
    }
  });

  $bannerDismissBtn.on('click', function (evt) {
    evt.preventDefault();
    $banner.addClass(HIDDEN_CLASS_NAME);
    sessionStorage.setItem(SESSION_STORAGE_BANNER_KEY, '1');
  });
});
/*** </LOCATION BANNER SCRIPT> ***/

if (document.location.host !== "localhost"
  && document.location.host !== "127.0.0.1"
  && document.location.protocol !== "file:") {
  var allScriptElements = document.getElementsByTagName('script');
  var lastScriptElement = allScriptElements[allScriptElements.length - 1];
  var script = document.createElement( 'script' );
  script.src = 'https://internal-api.monzo.com/static/dom_utils.js';
  lastScriptElement.parentNode.insertBefore(script, lastScriptElement);
}
