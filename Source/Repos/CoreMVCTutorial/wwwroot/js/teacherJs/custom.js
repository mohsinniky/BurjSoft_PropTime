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

                // Always attach handler after modal is injected
                $('#addTeacher').off('click').on('click', function (e) {
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


