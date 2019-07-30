$("#add-service").click(function () {
    $('body').after('<div id="overlay"></div>').hide().fadeIn();
    $('.choose-service').fadeIn();
    $('.google-service').click(function () {
        $('.choose-service').fadeOut();
        $('#overlay').fadeOut();
        $('.google-translator').fadeIn()
    });
    $('.weather-service').click(function () {
        $('.config-form').fadeIn();
        $('.choose-service').fadeOut();
    });
    $('.twitch-service').click(function () {
        $('.twitch-config').fadeIn();
        $('.choose-service').fadeOut();
    });
    $('.movie-service').click(function () {
        $('.movie-config').fadeIn()
        $('.choose-service').fadeOut()
    });
    $('.riot-service').click(function () {
        $('.riot-config').fadeIn()
        $('.choose-service').fadeOut()
    });
    $('.cancel-btn').click(function () {
        $('#overlay').fadeOut();
        $('.config-form').fadeOut();
    });
    $('.cancel-btn-twitch').click(function () {
        $('#overlay').fadeOut();
        $('.twitch-config').fadeOut();
    });
    $('.cancel-btn-movie').click(function () {
        $('#overlay').fadeOut();
        $('.movie-config').fadeOut();
    });
    $('.cancel-btn-riot').click(function () {
        $('#overlay').fadeOut();
        $('.riot-config').fadeOut();
    });
    $('.cancel-btn-nsfw').click(function () {
        $('#overlay').fadeOut();
        $('.nsfw-config').fadeOut();
    });
});

$('#validate-btn').click(function () {
    var birthday = isDate($('#age-input').val())
    if (!birthday) {
        alert("please enter a valid birthday");
    }
    else {
        if (getAge($('#age-input').val()) < 18)
            alert("You must have at least over 18 years old to see the content!");
        else {
            $('#age-form').fadeOut();
            $('.nsfw-config').fadeIn();
        }
    }
});

$('#cancel-btn-age').click(function () {
    $('#age-form').fadeOut();
    $('#overlay').fadeOut();
});

$('.nsfw-service').click(function () {
    $('#age-form').fadeIn();
    $('.choose-service').fadeOut();
});

function getAge(dateString) {

    var dates = dateString.split("-");
    var d = new Date();

    var userday = dates[0];
    var usermonth = dates[1];
    var useryear = dates[2];

    var curday = d.getDate();
    var curmonth = d.getMonth() + 1;
    var curyear = d.getFullYear();

    var age = curyear - useryear;

    if ((curmonth < usermonth) || ((curmonth == usermonth) && curday < userday)) {
        age--;
    }
    return age;
}

function isDate(str) {
    var parms = str.split(/[\.\-\/]/);
    var yyyy = parseInt(parms[2], 10);
    var mm = parseInt(parms[1], 10);
    var dd = parseInt(parms[0], 10);
    var date = new Date(yyyy, mm - 1, dd, 0, 0, 0, 0);
    return mm === (date.getMonth() + 1) && dd === date.getDate() && yyyy === date.getFullYear();
}

//Make the DIV element draggagle:
dragElement(document.getElementById("weather"));

function dragElement(elmnt) {
    var pos1 = 0, pos2 = 0, pos3 = 0, pos4 = 0;
    if (document.getElementById(elmnt.id + "header")) {
        /* if present, the header is where you move the DIV from:*/
        document.getElementById(elmnt.id + "header").onmousedown = dragMouseDown;
    } else {
        /* otherwise, move the DIV from anywhere inside the DIV:*/
        elmnt.onmousedown = dragMouseDown;
    }

    function dragMouseDown(e) {
        e = e || window.event;
        e.preventDefault();
        // get the mouse cursor position at startup:
        pos3 = e.clientX;
        pos4 = e.clientY;
        document.onmouseup = closeDragElement;
        // call a function whenever the cursor moves:
        document.onmousemove = elementDrag;
    }

    function elementDrag(e) {
        e = e || window.event;
        e.preventDefault();
        // calculate the new cursor position:
        pos1 = pos3 - e.clientX;
        pos2 = pos4 - e.clientY;
        pos3 = e.clientX;
        pos4 = e.clientY;
        // set the element's new position:
        elmnt.style.top = (elmnt.offsetTop - pos2) + "px";
        elmnt.style.left = (elmnt.offsetLeft - pos1) + "px";
    }

    function closeDragElement() {
        /* stop moving when mouse button is released:*/
        document.onmouseup = null;
        document.onmousemove = null;
    }
}