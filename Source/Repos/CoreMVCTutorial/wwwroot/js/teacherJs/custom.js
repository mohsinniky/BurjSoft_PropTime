// In your main view's script section or a linked JS file
$(document).ready(function () {
    getTeachersList();
    $('#modalButton').on('click', function () {
        // Clear previous modal HTML to avoid duplicate elements
        $('#modalPlaceholder').html('');
        $.ajax({
            url: '/Teachers/ShowTeacherModal',
            type: 'GET',
            success: function (data) {
                $('#modalPlaceholder').html(data);
                $('#addTeacherModal').modal('show');


            },
            error: function () {
                alert('Error loading modal.');
            }
        });
    });

    // Delete handler
    $(document).on('click', '.delete-teacher-btn', function () {
        var teacherId = $(this).data('id');
        if (confirm("Are you sure you want to delete this teacher?")) {
            $.ajax({
                url: "/Teachers/DeleteTeacher",
                type: "POST",
                data: { id: teacherId },
                success: function (response) {
                    if (response.status === "Success") {
                        $("#ajaxResponseSection").text(response.message);
                        getTeachersList();
                    } else {
                        $("#ajaxResponseSection").text(response.message);
                    }
                },
                error: function (xhr, status, error) {
                    $("#ajaxResponseSection").text("Error: " + xhr.responseText);
                }
            });
        }
    });

    // Delegate event handlers after table refresh
    $(document).on('click', '.edit-teacher-btn', function () {
        var teacherId = $(this).data('id');
        $('#modalPlaceholder').html('');
        $.ajax({
            url: '/Teachers/ShowTeacherModal',
            type: 'GET',
            data: { id: teacherId },
            success: function (data) {
                $('#modalPlaceholder').html(data);
                $('#addTeacherModal').modal('show');

                $('#addTeacher').off('click').on('click', function () {
                    submitTeacher();
                });
            },
            error: function () {
                alert('Error loading modal.');
            }
        });
    });
});

// Main validation and AJAX

function getTeacherFormData() {
    return {
        TeacherId: $("#TeacherId").val() || 0,
        FullName: $("#txtFullName").val(),
        FatherName: $("#txtFatherName").val(),
        Email: $("#txtEmail").val(),
        DateOfBirth: $("#txtDateOfBirth").val(),
        Phone: $("#txtPhone").val(),
        Password: $("#txtPassword").val(),
        Course: $("#course").val(),
        Gender: parseInt($("input[name='Gender']:checked").val()),
        Address: $("#txtAddress").val(),
        TermsAndConditions: $("#terms").prop("checked"),
        Hobbies: $("input[name='Hobbies']:checked").map(function () { return $(this).val(); }).get(),
        Skills: $("#skills").val()
    };
}
function inputValidations(){
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
    return valid;
}
function getTeachersList() {
    $.ajax({
        url: "/Teachers/GetTeachers",
        type: "GET",
        success: function (data) {
            $("#teachersList").html(data);
        },
        error: function (error) {
            console.log(error);
        }
        
    })
}

//Main Submit function
function submitTeacher() {
    let valid = inputValidations();
    if (valid) {
        var teacherObj = getTeacherFormData();

        $.ajax({
            url: "/Teachers/UpdateTeachers",
            type: "POST",
            data: JSON.stringify(teacherObj),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.status === "Success") {
                    $("#ajaxResponseSection").text(response.message);
                    $('#addTeacherModal').modal('hide');
                    getTeachersList();
                } else {
                    $("#ajaxResponseSection").text(response.message);
                }
            },
            error: function (xhr, status, error) {
                $("#ajaxResponseSection").text("Error: " + xhr.responseText);
            }
        });
    }
}



// // Validation functions

// function validateInput(inputElementId, errorElementId, errorMessage = 'This is required') {
//     const value = document.getElementById(inputElementId).value;
//     if (!value) {
//         document.getElementById(errorElementId).innerHTML = errorMessage;
//         return false;
//     } else {
//         document.getElementById(errorElementId).innerHTML = "";
//         return true;
//     }
// }
// function validateEmail(inputElementId, errorElementId, errorMessage = 'Email is invalid') {
//     const email = document.getElementById(inputElementId).value;
//     const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
//     if (!emailRegex.test(email)) {
//         document.getElementById(errorElementId).innerHTML = errorMessage;
//         return false;
//     } else {
//         document.getElementById(errorElementId).innerHTML = '';
//         return true;
//     }
// }

// function validateMultiSelect(selectId, errorElementId, min = 1, max = 10, errorMessage = 'Select at least one option') {
//     const selected = $(`#${selectId}`).val();
//     if (!selected || selected.length < min) {
//         document.getElementById(errorElementId).innerHTML = `Select at least ${min} skill`;
//         return false;
//     } else if (selected.length > max) {
//         document.getElementById(errorElementId).innerHTML = `You can select up to ${max} skills`;
//         return false;
//     } else {
//         document.getElementById(errorElementId).innerHTML = '';
//         return true;
//     }
// }