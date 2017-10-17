/// <reference path="../jquery-1.6.2-vsdoc.js" />


var xhr = new XMLHttpRequest();

function ShowResult() {
    if (xhr.readyState === 4 && xhr.status === 200) {
        var timeDiv = document.getElementById("timeDisplayRaw");
        timeDiv.innerHTML = xhr.responseText;
    }
}


function getServerTime() {
    if (xhr) {
        xhr.open("GET", "/SimpleAjax/ServerTimeAsString");
        xhr.onreadystatechange = ShowResult;        
        xhr.send();
    }
}
