$(document).ready(function () {
    // turning off Default form submission
    $('#createStudentForm').on('submit', function (e) {
        e.preventDefault();
        submitStudentForm();
    });

    // Reset form when modal is hidden
    $('#addStudentModal').on('hidden.bs.modal', function () {
        resetForm();
    });
});

function submitStudentForm() {
    if (!validateForm()) {
        return;
    }

    const formData = getFormData();
    submitViaAjax(formData);
}

function validateForm() {
    let isValid = true;
    // Clear previous errors
    $('.text-danger').text('');
    $('.is-invalid').removeClass('is-invalid');

    // Validate required fields
    const firstName = $('#StudentForm_Student_FirstName').val().trim();
    const lastName = $('#StudentForm_Student_LastName').val().trim();
    const email = $('#StudentForm_Student_Email').val().trim();
    const phoneNumber = $('#StudentForm_Student_PhoneNumber').val().trim();

    if (!firstName) {
        showError('StudentForm_Student_FirstName', 'First Name is required');
        isValid = false;
    }

    if (!lastName) {
        showError('StudentForm_Student_LastName', 'Last Name is required');
        isValid = false;
    }
    if (!phoneNumber) {
        showError('StudentForm_Student_PhoneNumber', 'Phone Number is required');
        isValid = false;
    }

    if (!email) {
        showError('StudentForm_Student_Email', 'Email is required');
        isValid = false;
    }
    else if (!isValidEmail(email)) {
        showError('StudentForm_Student_Email', 'Please enter a valid email');
        isValid = false;
    }
    return isValid;
}

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function getFormData() {
    // Get selected course IDs
    const selectedCourseIds = [];
    $('input[name="SelectedCourseIds"]:checked').each(function () {
        selectedCourseIds.push(parseInt($(this).val()));
    });

    return {
        Student: {
            FirstName: $('#StudentForm_Student_FirstName').val().trim(),
            LastName: $('#StudentForm_Student_LastName').val().trim(),
            Email: $('#StudentForm_Student_Email').val().trim(),
            PhoneNumber: $('#StudentForm_Student_PhoneNumber').val().trim()
        },
        SelectedCourseIds: selectedCourseIds
    };
}

function submitViaAjax(formData) {
    $.ajax({
        url: '/Students/Index',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
                $('#addStudentModal').modal('hide');
                loadStudentsTable(); // Reload table data
                resetForm();
            }
        },
        error: function (xhr, status, error) {
            console.log('Error saving student: ' + error, 'error');
        }
    });
}

function loadStudentsTable() {
    $.ajax({
        url: '/Students/GetStudentsTable', // Create this endpoint
        type: 'GET',
        success: function (data) {
            $('table tbody').html(data);
        }
    });
}


function showError(fieldId, message) {
    $('#' + fieldId).addClass('is-invalid');
    $('#' + fieldId).siblings('.text-danger').text(message);
}


function resetForm() {
    $('#createStudentForm')[0].reset();
    $('.text-danger').text('');
    $('.is-invalid').removeClass('is-invalid');
    $('input[name="SelectedCourseIds"]').prop('checked', false);
}
