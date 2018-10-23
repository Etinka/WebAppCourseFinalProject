var API_KEY = '2126940242517023bf7f7eb3f25133fd';
var API_BASE_URL = 'http://api.openweathermap.org';

function GetWetherData() {
    // call to https://openweathermap.org/forecast5

    var url = API_BASE_URL + '/data/2.5/weather?id=293396&units=metric&appid=' + API_KEY;
    //var url = "http://localhost:37787/js/api_response.json";

    $.get(url).done(function (wether) {
        $('#max_temp').text(wether.main.temp_max);
        $('#min_temp').text(wether.main.temp_min);
        $('#temp').text(wether.main.temp);
    })
}

//TODO: Add Labels
function GetCategoryPostsCount() {
    $.get('/api/post-count').done(function (categories) {
        // init chart
        var svgWidth = 500;
        var svgHeight = 300;
        var svg = d3.select('svg')
            .attr("width", svgWidth)
            .attr("height", svgHeight)
            .attr("class", "bar-chart");
        //draw bars
        var dataset = Object.values(categories);
        var barPadding = 5;
        var barWidth = (svgWidth / dataset.length);
        var barChart = svg.selectAll("rect")
            .data(dataset)
            .enter()
            .append("rect")
            //Not Proportional
            .attr("y", function (d) {
                return svgHeight - d
            })
            .attr("height", function (d) {
                return d;
            })
            .attr("width", barWidth - barPadding)
            .attr("transform", function (d, i) {
                var translate = [barWidth * i, 0];
                return "translate(" + translate + ")";
            });
    })
}

GetWetherData();
GetCategoryPostsCount();
