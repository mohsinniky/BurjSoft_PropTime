// Validation functions

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

function validateInput(inputElementId, errorElementId, errorMessage = 'This is required') {
    const value = document.getElementById(inputElementId).value;
    if (!value) {
        document.getElementById(errorElementId).innerHTML = errorMessage;
        return false;
    } else {
        document.getElementById(errorElementId).innerHTML = "";
        return true;
    }
}
function validateEmail(inputElementId, errorElementId, errorMessage = 'Email is invalid') {
    const email = document.getElementById(inputElementId).value;
    const emailRegex = /^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,}$/;
    if (!emailRegex.test(email)) {
        document.getElementById(errorElementId).innerHTML = errorMessage;
        return false;
    } else {
        document.getElementById(errorElementId).innerHTML = '';
        return true;
    }
}

function validateMultiSelect(selectId, errorElementId, min = 1, max = 10, errorMessage = 'Select at least one option') {
    const selected = $(`#${selectId}`).val();
    if (!selected || selected.length < min) {
        document.getElementById(errorElementId).innerHTML = `Select at least ${min} skill`;
        return false;
    } else if (selected.length > max) {
        document.getElementById(errorElementId).innerHTML = `You can select up to ${max} skills`;
        return false;
    } else {
        document.getElementById(errorElementId).innerHTML = '';
        return true;
    }
}