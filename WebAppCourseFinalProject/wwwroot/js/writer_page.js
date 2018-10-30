
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
GetCategoryPostsCount();