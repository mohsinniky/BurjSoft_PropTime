// Validation functions
function validateInput(inputElementId, errorElementId, errorMessage = 'This field is required') {
    const value = document.getElementById(inputElementId).value;
    if (!value) {
        document.getElementById(errorElementId).innerHTML = errorMessage;
        return false;
    } else {
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