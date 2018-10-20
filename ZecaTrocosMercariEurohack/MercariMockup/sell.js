function triggerFileInput() {
    $("#fileInput").click();
}

$("[data-control-type=checkboxes]").click(function () {
    var stateBeforeReset = $(this).is(':checked');

    $('[data-control-type=checkboxes]').prop('checked', false);
    $(this).prop('checked', stateBeforeReset);
});

function getBase64(file) {
    var reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = function () {
        $.ajax({
            type: "POST",
            url: "/api/Categorize",
            data: { "basicString": reader.result },
            success: function (category) {
                $.ajax({
                    type: "GET",
                    url: "/api/GetHintsByCategoryAndModifiers/" + category,
                    success: function (result) {
                        for (var i = 0; i < result.length; i++) {
                            var options = {
                                "closeButton": true,
                                "newestOnTop": true,
                                "progressBar": true,
                                "timeOut": "0",
                                "extendedTimeOut": "0",
                            }
                            toastr.info(result[i].HintText, 'Selling ' + category + "?", options)
                        }
                    }
                });
            }
        });
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
}

$('#fileInput').on("change", function () {
    getBase64($('#fileInput')[0].files[0]);
});