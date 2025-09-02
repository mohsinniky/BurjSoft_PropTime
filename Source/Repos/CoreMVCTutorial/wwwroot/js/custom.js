$(document).ready(function () {
    // Show and hide modal
    $('#modalButton').click(function () {
        $('#staticBackdrop').modal('show');
    });
    $('.closeModalButton').click(function () {
        $('#staticBackdrop').modal('hide');
    });

    // Validation functions
    function validateInput(inputElementId, errorElementId, errorMessage = 'This field is required') {
        const value = document.getElementById(inputElementId).value;
        if (!value) {
            document.getElementById(errorElementId).innerHTML = errorMessage;
            return false;
        } else {
            document.getElementById(errorElementId).innerHTML = '';
            switch (inputElementId){
                case "txtFullName":
                    {
                        // Validating length of full name
                        if (value.length < 3 || value.length > 50) {
                            document.getElementById(errorElementId).innerHTML = 'Full Name must be at least 3 characters long and no more than 50 characters long';
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                case "txtFatherName":
                    {
                        // Validating length of full name
                        if (value.length < 3 || value.length > 50) {
                            document.getElementById(errorElementId).innerHTML = 'Full Name must be at least 3 characters long and no more than 50 characters long';
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                case "txtPhone":
                    {
                        // Validating phone number format
                        const phoneRegex = /^\d{11}$/; // Assuming a 11-digit phone number
                        if (!phoneRegex.test(value)) {
                            document.getElementById(errorElementId).innerHTML = 'Phone number must be 11 digits long and contain only numbers';
                            return false;
                        } else {
                            return true;
                        }
                    }
                case "txtPassword":
                    {
                        // Validating password regex
                        const passwordRegex = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)[A-Za-z\d]{8,100}$/; // At least one uppercase, one lowercase, one digit, 8-100 characters
                        if (!passwordRegex.test(value)) {
                            document.getElementById(errorElementId).innerHTML = 'Password must be at least 8 characters long, contain at least one uppercase letter, one lowercase letter, and one number';
                            return false;
                        } else {
                            return true;
                        }
                    }
                case "txtAddress":
                    {
                        // Validating length of address
                        if (value.length < 10 || value.length > 200) {
                            document.getElementById(errorElementId).innerHTML = 'Address must be at least 10 characters long and no more than 200 characters long';
                            return false;
                        }
                        else {
                            return true;
                        }
                    }
                
            }
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


    // Main validation
    $('#addTeacher').click(function () {
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
            $('#myForm').submit();
        }
    });
});
