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

                $('#updateTeacher').off('click').on('click', function () {
                    updateTeacher();
                });
            },
            error: function () {
                alert('Error loading modal.');
            }
        });
    });
});

// Main validation and AJAX

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
        var teacherObj = getTeacherFormData();
        $.ajax({
            url: "/Teachers/AddTeacher",
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

function updateTeacher() {
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
    console.log(valid)
    if (valid) {
        var teacherObj = getTeacherFormData();
        teacherObj.TeacherId = $("#TeacherId").val();
        $.ajax({
            url: "/Teachers/UpdateTeacher",
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

function getTeacherFormData() {
    return {
        TeacherId: null,
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
