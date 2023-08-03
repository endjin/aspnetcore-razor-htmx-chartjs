/*
 We use HTMX extensibility to bridge the world of HTMX Hypermedia and Chart.js requirement for RESTful JSON APIs.
 We leverage HAL-isms to provide stateful links to self, previous, and next pages of data, which can also be used
 to enable/disable pagination buttons.
*/

htmx.defineExtension('update_chartjs', {
    transformResponse: function (text, xhr, elt) {
        var response = JSON.parse(text);
        var chart = Chart.getChart(response.chartId);

        chart.config.data.datasets[0].data = response.data;
        chart.options.plugins.title.text = response.title;

        chart.update();

        // Find the previous button and update its hx-get attributes
        var previous = htmx.find("#" + response.previousButtonId)
        previous.setAttribute("hx-get", response.previous);

        // We have to notify HTMX about the updates to the DOM
        htmx.process(previous); 

        // Find the next button and update its hx-get attributes
        var next = htmx.find("#" + response.nextButtonId)
        next.setAttribute("hx-get", response.next);

        // We have to notify HTMX about the updates to the DOM
        htmx.process(next); 

        // Disable the previous and next buttons if we are on the first or last page
        if (response.self == response.previous) {
            previous.setAttribute("disabled", "disabled");
        } else {
            previous.removeAttribute("disabled");
        }

        if (response.self == response.next) {
            next.setAttribute("disabled", "disabled");
        } else { 
            next.removeAttribute("disabled");
        }

        // Return an empty string to prevent HTMX from performing string operations on a null.
        return "";
    }
});