// In your main view's script section or a linked JS file
$(document).ready(function () {
    $('#modalButton').on('click', function () {
        // Clear previous modal HTML to avoid duplicate elements
        $('#modalPlaceholder').html('');
        $.ajax({
            url: '/Teachers/ShowTeacherModal',
            type: 'GET',
            success: function (data) {
                $('#modalPlaceholder').html(data);
                $('#addTeacherModal').modal('show');

                $('#addTeacher').on('click', function (e) {
                    e.preventDefault();
                    saveTeacher();
                });

                $('.closeModalButton').off('click').on('click', function () {
                    $('#addTeacherModal').modal('hide');
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
    // Add other validations as needed

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
                    refreshTeachersTable();
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
        data: { name: "Mohsin" },
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
        data: { num1: 10, num2: 20 },
        success: function (response) {
            $("#ajaxResponseSection").text(response.product);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    })
})

//Send Teacher Data Object
var teacherObj = {
    TeacherId: null,
    FullName: "Mohsin",
    FatherName: "Raza",
    Email: "test@example.com",
    DateOfBirth: "1990-05-15",
    Phone: "03001234567",
    Password: "StrongPass123",
    Course: "B.Tech",
    Gender: 1,  // should match your enum
    Address: "123 Street, Lahore",
    TermsAndConditions: true,
    Hobbies: ["Reading", "Drawing"],
    Skills: ["C#", "ASP.NET Core", "SQL"]
};
// Send List of Teacher Data Objects
var teacherList = [
    {
        TeacherId: null,
        FullName: "Mohsin",
        FatherName: "Raza",
        Email: "test@example.com",
        DateOfBirth: "1990-05-15",
        Phone: "03001234567",
        Password: "StrongPass123",
        Course: "B.Tech",
        Gender: 1,  // enum (1 = Female if your enum is Male=0, Female=1, Other=2)
        Address: "123 Street, Lahore",
        TermsAndConditions: true,
        Hobbies: ["Reading", "Drawing"],
        Skills: ["C#", "ASP.NET Core", "SQL"]
    },
    {
        TeacherId: null,
        FullName: "Ali",
        FatherName: "Ahmed",
        Email: "test2@example.com",
        DateOfBirth: "1988-10-20",
        Phone: "03111234567",
        Password: "Pass@123",
        Course: "Mechanical Engg",
        Gender: 0,  // enum (0 = Male)
        Address: "456 Street, Karachi",
        TermsAndConditions: true,
        Hobbies: ["Cricket", "Music"],
        Skills: ["Java", "Spring Boot", "MySQL"]
    }
];



$("#postTeacherObject").click(function (e) {
    $.ajax({
        //Now here will come the parameters that are to used
        url: "/Teachers/SaveTeacher",
        type: "POST",
        data: JSON.stringify(teacherObj),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#ajaxResponseSection").text(response.message);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    })
})

$("#postMultipleTeacherData").click(function (e) {
    $.ajax({
        //Now here will come the parameters that are to used
        url: "/Teachers/SaveMultipleTeacher",
        type: "POST",
        data: JSON.stringify(teacherList),
        contentType: "application/json; charset=utf-8",
        success: function (response) {
            $("#ajaxResponseSection").text(response.message);
        },
        error: function (xhr, status, error) {
            alert('Error: ' + xhr.responseText);
        }
    })
})

function refreshTeachersTable() {
    $.ajax({
        url: '/Teachers/GetTeachersTable',
        type: 'GET',
        success: function (data) {
            $('#teachersTablePlaceholder').html(data);
        },
        error: function () {
            $("#ajaxResponseSection").text("Error loading teachers table.");
        }
    });
}

// Delegate event handlers after table refresh
$(document).on('click', '.edit-teacher-btn', function () {
    var teacherId = $(this).data('id');
    $.ajax({
        url: '/Teachers/GetTeacherById',
        type: 'GET',
        data: { id: teacherId },
        success: function (response) {
            if (response.status === "Success") {
                $('#modalPlaceholder').html('');
                $.ajax({
                    url: '/Teachers/ShowTeacherModal',
                    type: 'GET',
                    success: function (modalHtml) {
                        $('#modalPlaceholder').html(modalHtml);
                        $('#addTeacherModal').modal('show');

                        // Attach the event only once
                        $('#addTeacherModal').one('shown.bs.modal', function () {
                            fillTeacherForm(response.data);
                        });

                        // Change Add button to Update
                        $('#addTeacher').hide();
                        if ($('#updateTeacher').length === 0) {
                            $('#myForm').append('<button type="button" class="btn btn-success" id="updateTeacher">Update</button>');
                        } else {
                            $('#updateTeacher').show();
                        }

                        $('#updateTeacher').off('click').on('click', function (e) {
                            e.preventDefault();
                            updateTeacher(teacherId);
                        });

                        $('.closeModalButton').off('click').on('click', function () {
                            $('#addTeacherModal').modal('hide');
                        });
                    }
                });
            } else {
                $("#ajaxResponseSection").text(response.message);
            }
        }
    });
});

function fillTeacherForm(data) {
    $("#txtFullName").val(data.fullName || "N/A");
    $("#txtFatherName").val(data.fatherName || "N/A");
    $("#txtEmail").val(data.email || "N/A");
    $("#txtDateOfBirth").val(data.dateOfBirth ? data.dateOfBirth.substring(0, 10) : "N/A");
    $("#txtPhone").val(data.phone || "N/A");
    $("#txtPassword").val(data.password || "N/A");
    $("#course").val(data.course || "N/A");
    $("input[name='Gender'][value='" + data.gender + "']").prop('checked', true);
    $("#txtAddress").val(data.address || "N/A");
    $("#terms").prop('checked', !!data.termsAndConditions);

    $("input[name='Hobbies']").prop('checked', false);
    if (Array.isArray(data.hobbies)) {
        data.hobbies.forEach(function (hobby) {
            $("input[name='Hobbies'][value='" + hobby + "']").prop('checked', true);
        });
    }

    $("#skills option").prop('selected', false);
    if (Array.isArray(data.skills)) {
        data.skills.forEach(function (skill) {
            $("#skills option[value='" + skill + "']").prop('selected', true);
        });
    }
}

function updateTeacher(teacherId) {
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
    // Add other validations as needed

    if (valid) {
        var teacherObj = getTeacherFormData();
        teacherObj.TeacherId = teacherId;
        $.ajax({
            url: "/Teachers/UpdateTeacher",
            type: "POST",
            data: JSON.stringify(teacherObj),
            contentType: "application/json; charset=utf-8",
            success: function (response) {
                if (response.status === "Success") {
                    $("#ajaxResponseSection").text(response.message);
                    $('#addTeacherModal').modal('hide');
                    refreshTeachersTable();
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
                    refreshTeachersTable();
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


