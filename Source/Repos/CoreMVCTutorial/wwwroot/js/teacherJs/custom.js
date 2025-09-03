$(document).ready(function () {

    // Show and hide modal
    $('#modalButton').click(function (e) {
        $('#staticBackdrop').modal('show');
    });

    $('.closeModalButton').click(function (e) {
        $('#staticBackdrop').modal('hide');
    });

    // Main validation
    $('#addTeacher').click(function (e) {
        saveTeacher();
    });
});

// save teacher info
function saveTeacher() {
    let valid = true;
    valid = validateInput('txtFullName', 'fullname-error', 'Full name is required') && valid;
    valid = validateInput('txtFatherName', 'fathername-error', 'Father name is required') && valid;
    valid = validateInput('txtDateOfBirth', 'dob-error', 'Date of birth is required') && valid;
    valid = validateInput('txtPhone', 'phone-error', 'Phone is required') && valid;
    valid = validateInput('txtPassword', 'password-error', 'Password is required') && valid;
    valid = validateInput('course', 'course-error', 'Course is required') && valid;
    valid = validateInput('txtAddress', 'address-error', 'Address is required') && valid;

    valid = validateEmail('txtEmail', 'email-error', 'Email is invalid') && valid;
    valid = validateMultiSelect('skills', 'skills-error', 1, 10) && valid;



    if (valid) {
        //  $('#myForm')[0].submit();
    }
}



$("#getTimeButton").click(function (e) {
    $.ajax({
        //Now here will come the parameters that are to used
        url: "/Teachers/GetServertime",
        type: "GET",
        success: function (response) {
            $("#ajaxResponseSection").text(response);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + error);
        }
    })
})

$("#getGreeting").click(function (e) {
    $.ajax({
        //Now here will come the parameters that are to used
        url: "/Teachers/GetGreeting",
        type: "GET",
        data: {name: "Mohsin"},
        success: function (response) {
            $("#ajaxResponseSection").text(response.greeting);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    })
})

$("#postMultiply").click(function (e) {
    $.ajax({
        //Now here will come the parameters that are to used
        url: "/Teachers/MultiplyTwoNums",
        type: "POST",
        data: { num1: 10,num2: 20},
        success: function (response) {
            $("#ajaxResponseSection").text(response.product);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    })
})