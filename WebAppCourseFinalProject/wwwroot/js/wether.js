var API_KEY = '2126940242517023bf7f7eb3f25133fd';
var API_BASE_URL = 'http://api.openweathermap.org';

function GetWetherData() {
    // call to https://openweathermap.org/forecast5

    var url = API_BASE_URL + '/data/2.5/weather?id=293396&units=metric&appid=' + API_KEY;
    //var url = "http://localhost:37787/js/api_response.json";

    $.get(url).done(function (response) {
        console.log(response);
    })
}


GetWetherData();