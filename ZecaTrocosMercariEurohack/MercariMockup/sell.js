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
                            var cs = result[i].CsFields;
                            var options = {
                                "closeButton": true,
                                "newestOnTop": true,
                                "progressBar": true,
                                "timeOut": "0",
                                "extendedTimeOut": "0",
                                "onclick": function () { promptfields(cs); }
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

function promptfields(csfields) {
    if (csfields != null && csfields != undefined && csfields.length > 0) {
        var arrayOfFields = csfields.split(",");

        if (arrayOfFields != null && arrayOfFields.length > 0) {

            var addToDescription = $("#description").val();
            for (var i = 0; i < arrayOfFields.length; i++) {
                let stringToDisplay = arrayOfFields[i].trim();
                addToDescription += stringToDisplay.charAt(0).toUpperCase() + stringToDisplay.slice(1) + ': \n';
            }

            $("#description").val(addToDescription);
            $('html,body').animate({
                scrollTop: $("#description").offset().top
            }, 'slow');
        }
    }
}