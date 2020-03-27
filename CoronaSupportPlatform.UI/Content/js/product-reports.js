
/*--------------  bar chart 12 amchart start ------------*/
if ($('#divTenderProductQuantityChart').length) {
    var chart = AmCharts.makeChart("divTenderProductQuantityChart", {
        "type": "serial",
        "theme": "light",
        "dataProvider": [{
            "name": "John",
            "points": 35654,
            "color": "#7F8DA9"
        }, {
            "name": "Damon",
            "points": 65456,
            "color": "#FEC514"
        }, {
            "name": "Patrick",
            "points": 45724,
            "color": "#952FFE"
        }, {
            "name": "Mark",
            "points": 23654,
            "color": "#8282F1"
        }, {
            "name": "Natasha",
            "points": 35654,
            "color": "#2599D4"
        }, {
            "name": "Adell",
            "points": 55456,
            "color": "#2563D6"
        }, {
            "name": "Alessandro",
            "points": 13654,
            "color": "#9524D4"
        }],
        "valueAxes": [{
            "maximum": 80000,
            "minimum": 0,
            "axisAlpha": 0,
            "dashLength": 4,
            "position": "left"
        }],
        "startDuration": 1,
        "graphs": [{
            "balloonText": "<span style='font-size:13px;'>[[category]]: <b>[[value]]</b></span>",
            "bulletOffset": 10,
            "bulletSize": 52,
            "colorField": "color",
            "cornerRadiusTop": 8,
            "customBulletField": "bullet",
            "fillAlphas": 0.8,
            "lineAlpha": 0,
            "type": "column",
            "valueField": "points"
        }],
        "marginTop": 0,
        "marginRight": 0,
        "marginLeft": 0,
        "marginBottom": 0,
        "autoMargins": false,
        "categoryField": "name",
        "categoryAxis": {
            "axisAlpha": 0,
            "gridAlpha": 0,
            "inside": true,
            "tickLength": 10,
            "color": "#fff"
        },
        "export": {
            "enabled": false
        }
    });
}
/*--------------  bar chart 12 amchart END ------------*/