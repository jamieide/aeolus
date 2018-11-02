var aeoleus = aeoleus || {};

(function($, axios, uri) {

    function getStationsList() {
        var state = $("#state-list").val();
        axios.get(uri + "home/getstationlist?state=" + state)
            .then(function(response) {
                $("#station-list-container").html(response.data);
                $("#analysis-options").removeClass("invisible");
                $("#analysis-results-container").html("");
            });
    }

    function stationSelect(el) {
        $("#station-list-container").find(".list-group-item").removeClass("active");
        $(el.target).addClass("active");
    }

    function getAnalysis() {
        var data = {
            "StationIdentifier": $("#station-list-container").find(".list-group-item.active").data("stationid"),
            "Start": $("#start-date").val(),
            "End": $("#end-date").val(),
            "AirDensity": $("#air-density").val(),
            "RotorRadius": $("#rotor-radius").val(),
            "PerformanceCoefficient": $("#performance-coefficient").val(),
            "Strategy": $("[name=strategy]:checked").val()
        };
        axios.post(uri + "home/getanalysis", data)
            .then(function(response) {
                $("#analysis-results-container").html(response.data);
            });
    }

    $(document).ready(function() {
        $("#get-station-list").click(getStationsList);
        $("#state-list").change(function() {
            $("#analysis-options").addClass("invisible");
            $("#station-list-container").html("");
            $("#analysis-results-container").html("");
        });
        $("#station-list-container").on("click", stationSelect);
        $("#get-analysis").click(getAnalysis);
        $("#analysis-options").on("change", function() {
                $("#analysis-results-container").html("");
            });
    });

}(jQuery, axios, uri));