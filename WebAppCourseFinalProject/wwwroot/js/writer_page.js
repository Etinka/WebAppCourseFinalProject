
var drawBarChart = true;

function DrawBarChart(categories) {
    // init chart
    var svgWidth = 500;
    var svgHeight = 300;
    var svg = d3.select('svg')
        .attr("width", svgWidth)
        .attr("height", svgHeight)
        .attr("class", "bar-chart");
    //draw bars
    var dataset = Object.values(categories);
    var categoryNames = Object.keys(categories);


    var barPadding = 5;
    var textSize = 16;
    var barWidth = (svgWidth / dataset.length);

    svg.selectAll("rect")
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

    svg.selectAll("text").data(categoryNames).enter()
        .append("text")
        .text(function (d) {
            return d;
        })
        .attr('class', 'cat')
        .attr("text-anchor", "middle")
        .attr('x', function (d, i) {
            return i * (svgWidth / dataset.length) + (svgWidth / dataset.length) / 2;
        })
        .attr('y', function (d, i) {
            return svgHeight - barPadding;
        })
        .attr('font-size', 16);

    svg.selectAll('text.post').data(dataset).enter()
        .append("text")
        .text(function (d) {
            return d;
        })
        .attr('class', 'post')
        .attr("text-anchor", "middle")
        .attr('y', function (d, i) {
            return svgHeight - d + textSize + barPadding;
        })
        .attr('x', function (d, i) {
            return i * (svgWidth / dataset.length) + (svgWidth / dataset.length) / 2;
        })
        .attr('font-size', textSize);
}

function DrawPieChart(categories) {
    var width = 400,
        height = 400,
        radius = 200,
        innerRadius = 100,
        colors = d3.scaleOrdinal(d3.schemeCategory20);

    var pieData = [
        {
            label: "part one",
            value: 50
        },
        {
            label: "part two",
            value: 25
        },
        {
            label: "part three",
            value: 10
        }
    ]

    var pie = d3.pie()
        .value(function (d) { return d.count; })
        .sort(null);

    var arc = svg.arc()
        .outerRadius(radius);


    var myChart = d3.select('svg').append('svg')
        .attr('width', width)
        .attr('height', height)
        .append('g')
        .attr('transform', 'translate(' + (width - radius) + ', ' + (height - radius) + ')')
        .selectAll('path').data(pie(pieData))
        .enter().append('g')
        .attr('class', 'slice');


    var slices = d3.selectAll('g.slice')
        .append('path')
        .attr('fill', function (d, i) {
            return colors(i);
        })
        .attr('d', arc)
        .transition()
        .ease("elastic")
        .duration(2000)
        .attrTween("d", tweenPie);

    function tweenPie(b) {
        var i = d3.interpolate({ startAngle: 1.1 * Math.PI, endAngle: 1.1 * Math.PI }, b);
        return function (t) { return arc(i(t)); };
    }
}

function DrawGraph(categories) {
    if (drawBarChart) {
        DrawBarChart(categories);
    }
    else {
        DrawPieChart(categories);
    }   
}

function GetCategoryPostsCount() {
    $.get('/api/post-count').done(DrawGraph);
}

GetCategoryPostsCount();