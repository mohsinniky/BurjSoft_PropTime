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
    const submitBtn = $('#submitStudentBtn');
    submitBtn.prop('disabled', true).html('<span class="spinner-border spinner-border-sm" role="status"></span> Saving...');
    $.ajax({
        url: '/Students/Create',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),

        success: function (response) {
            if (response.success) {
                showMessage('Student created successfully!', 'success');
                $('#addStudentModal').modal('hide');
                // Reload page after short delay to see the message
                setTimeout(function () {
                    window.location.reload();
                }, 1500);
            } else {
                // Show server validation errors
                showServerErrors(response.errors);
            }
        },
        error: function (xhr, status, error) {
            showMessage('Error saving student: ' + error, 'error');
        },
        complete: function () {
            // Reset button
            submitBtn.prop('disabled', false).text('Save Student');
        }
    });
}

function showServerErrors(errors) {
    // Clear previous errors
    $('.text-danger').text('');
    $('.is-invalid').removeClass('is-invalid');

    if (errors) {
        $.each(errors, function (field, errorMessages) {
            if (errorMessages && errorMessages.length > 0) {
                const fieldId = field.replace(/\./g, '_');
                showError(fieldId, errorMessages[0]);
            }
        });
        showMessage('Please correct the errors below.', 'error');
    }
}

function showError(fieldId, message) {
    $('#' + fieldId).addClass('is-invalid');
    $('#' + fieldId).siblings('.text-danger').text(message);
}


function showMessage(message, type) {
    $('.alert-dismissable').remove();
    const alertClass = type == 'success' ? 'alert-success' : 'alert-danger';
    const icon = type === 'success' ? '✓' : '!';

    const alertHtml = `
        <div class="alert ${alertClass} alert-dismissible fade show" role="alert" style="position: fixed; top: 20px; right: 20px; z-index: 9999; min-width: 300px;">
            <strong>${icon}</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;
    $('body').append(alertHtml);
    setTimeout(function () {
        $('.alert-dismissible').alert('close');
    })
}

function resetForm() {
    $('#createStudentForm')[0].reset();
    $('.text-danger').text('');
    $('.is-invalid').removeClass('is-invalid');
    $('input[name="SelectedCourseIds"]').prop('checked', false);
}

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}