function DrawBarChart(categories) {
    // init chart
    var svgWidth = 500;
    var svgHeight = 300;
    var svg = d3.select('svg#bar')
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
   
    var posts = [];
    for (var cat in categories) {
        posts.push({ category: cat, posts: categories[cat] }); 
    }    

    var pie = d3.pie()
        .value(function (d) { return d.posts })
        
    var slices = pie(posts);

    var arc = d3.arc()
        .innerRadius(0)
        .outerRadius(60);

    // helper that returns a color based on an ID
    var color = d3.scaleOrdinal(d3.schemeCategory10);

    var svg = d3.select('svg#pie')
        .append('svg')
        .attr("class", "pie");

    var g = svg.append('g')
        .attr('transform', 'translate(200, 75)');

    var arcGraph = g.selectAll('path.slice')
        .data(slices)
        .enter();
    arcGraph.append('path')
        .attr('class', 'slice')
        .attr('d', arc)
        .attr('fill', function (d) {
            return color(d.data.category);
        });

    arcGraph.append("text")
        .attr("transform", function (d) { return "translate(" + arc.centroid(d) + ")"; })
        .attr("dy", "0.35em")
        .text(function (d) { return d.data.posts });
    // building a legend is as simple as binding
    // more elements to the same data. in this case,
    // <text> tags
    svg.append('g')
        .attr('class', 'legend')
        .selectAll('text')
        .data(slices)
        .enter()
        .append('text')
        .text(function (d) { return '• ' + d.data.category; })
        .attr('fill', function (d) { return color(d.data.category); })
        .attr('y', function (d, i) { return 20 * (i + 1); })
}

function DrawGraph(categories) {
    DrawBarChart(categories);
    DrawPieChart(categories);
}

function GetCategoryPostsCount() {
    $.get('/api/post-count').done(DrawGraph);
}

function OnChartOptionChange(event) {
    console.log(event.currentTarget.value);
    var drawBarChart = event.currentTarget.value === 'Bar Chart';
    if (drawBarChart) {
        $('#pie').addClass('hidden');
        $('#bar').removeClass('hidden');
    } else {
        $('#bar').addClass('hidden');
        $('#pie').removeClass('hidden');
    }
}

GetCategoryPostsCount();
$('select').change(OnChartOptionChange);