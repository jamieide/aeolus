var aeoleus = aeoleus || {};

(function($, axios, uri) {

    function getStationsList() {
        var state = $("#state-list").val();
        axios.get(uri + "home/stationlist?state=" + state)
            .then(function(response) {
                $("#station-list-container").html(response.data);
                $("#analysis-options").toggleClass("invisible");
            });
    }

    $(document).ready(function() {
        $("#get-station-list").click(getStationsList);
        $("#state-list").change(function() {
            $("#station-list-container").html("");
            $("#analysis-options").toggleClass("invisible");
        });
    });
        
}(jQuery, axios, uri));