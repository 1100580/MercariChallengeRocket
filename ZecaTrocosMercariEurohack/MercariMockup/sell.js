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
        console.log(reader.result);
    };
    reader.onerror = function (error) {
        console.log('Error: ', error);
    };
}

$('#fileInput').on("change", function () {
    //faking a category, can't get the api to work now
    var category = 'Digital camera';


});
