// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    
    $("#companyList").on("click", "li", function (event) {
        console.log(this);
        var companyId = $(this).data("value");



        $.ajax({
            url: "/Admin/GetUser",
            type: "get", 
            data: {
                companyId: companyId,
            },
            success: function (response) {
                console.log(response);
                $('#displayUsers').html(response);

            },
            error: function (xhr) {
                
            }
        });

    });



    
});