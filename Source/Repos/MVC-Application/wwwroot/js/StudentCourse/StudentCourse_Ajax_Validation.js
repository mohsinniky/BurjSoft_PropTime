$(document).ready(function () {
    // Form submission
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
    const studentId = $('#createStudentForm').data('student-id');

    formData.StudentId = studentId || 0;

    submitViaAjax(formData);
}

function validateForm() {
    let isValid = true;
    // Clear previous errors
    $('.text-danger').text('');
    $('.is-invalid').removeClass('is-invalid');

    // Validate required fields
    const firstName = $('#FirstName').val().trim();
    const lastName = $('#LastName').val().trim();
    const email = $('#Email').val().trim();
    const phoneNumber = $('#PhoneNumber').val().trim();

    if (!firstName) {
        showError('FirstName', 'First Name is required');
        isValid = false;
    }

    if (!lastName) {
        showError('LastName', 'Last Name is required');
        isValid = false;
    }

    if (!phoneNumber) {
        showError('PhoneNumber', 'Phone Number is required');
        isValid = false;
    }

    if (!email) {
        showError('Email', 'Email is required');
        isValid = false;
    }
    else if (!isValidEmail(email)) {
        showError('Email', 'Please enter a valid email');
        isValid = false;
    }

    return isValid;
}

function isValidEmail(email) {
    const emailRegex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return emailRegex.test(email);
}

function getFormData() {
    const selectedCourseIds = [];
    $('input[name="SelectedCourseIds"]:checked').each(function () {
        selectedCourseIds.push(parseInt($(this).val()));
    });

    return {
        FirstName: $('#FirstName').val().trim(),
        LastName: $('#LastName').val().trim(),
        Email: $('#Email').val().trim(),
        PhoneNumber: $('#PhoneNumber').val().trim(),
        SelectedCourseIds: selectedCourseIds
    };
}

function submitViaAjax(formData) {
    $.ajax({
        url: '/Students/Upsert',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response.success) {
                $('#addStudentModal').modal('hide');

                if (formData.StudentId === 0) {
                    // Create operation
                    addNewStudentToTable(response.student);
                } else {
                    // Update operation
                    updateStudentInTable(formData.StudentId, response.student);
                }

                resetForm();
            }
        },
        error: function (xhr, status, error) {
            console.log('Error saving student: ' + error);
            showMessage('Error saving student. Please try again.', 'error');
        }
    });
}


function addNewStudentToTable(student) {
    // Remove "no students" message if exists
    $('.alert-info').remove();
    $('table').show();

    const newRow = `
        <tr data-student-id="${student.studentId}">
            <td>${student.studentId}</td>
            <td>${student.firstName} ${student.lastName}</td>
            <td>${student.email}</td>
            <td>${student.phoneNumber}</td>
            <td>
                <button type="button" class="btn btn-info btn-sm"
                        onclick="openDetailsModal(${student.studentId})">
                    Details
                </button>
                <button type="button" class="btn btn-warning btn-sm"
                        onclick="openEditModal(${student.studentId})">
                    Edit
                </button>
                <button type="button" class="btn btn-danger btn-sm"
                        onclick="deleteStudent(${student.studentId}, '${student.firstName} ${student.lastName}')"
                        data-student-id="${student.studentId}">
                    Delete
                </button>
            </td>
        </tr>
    `;

    $('table tbody').append(newRow);
}

function updateStudentInTable(studentId, student) {
    // Find the row by data attribute and update it
    const row = $(`tr[data-student-id="${studentId}"]`);

    if (row.length) {
        row.find('td:eq(0)').text(student.studentId);
        row.find('td:eq(1)').text(student.firstName + ' ' + student.lastName);
        row.find('td:eq(2)').text(student.email);
        row.find('td:eq(3)').text(student.phoneNumber);
        const coursesCell = row.find('td:eq(4)');

        if (student.coursesDisplay && student.coursesDisplay.trim() !== '') {
            coursesCell.html(`
                <div class="courses-container" style="max-width: 200px;">
                    ${student.coursesDisplay.split(', ').map(course =>
                `<span class="badge bg-secondary me-1 mb-1">${course}</span>`
            ).join('')}
                </div>
            `);
        } else {
            coursesCell.html('<span class="text-muted">No courses</span>');
        }
        // Update delete button with new name
        const deleteButton = row.find('.btn-danger');
        deleteButton.attr('onclick', `deleteStudent(${studentId}, '${student.firstName} ${student.lastName}')`);
    }
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

    // Re-enable all form fields
    $('#FirstName').prop('readonly', false);
    $('#LastName').prop('readonly', false);
    $('#Email').prop('readonly', false);
    $('#PhoneNumber').prop('readonly', false);

    // Re-enable all course checkboxes
    $('input[name="SelectedCourseIds"]').prop('disabled', false);

    // Clear edit mode and show submit button
    $('#createStudentForm').removeData('student-id');
    $('#modalTitle').text('Add New Student');
    $('#submitStudentBtn').show().text('Save Student');
}

// Edit functionality
function openEditModal(studentId) {
    console.log('Opening edit modal for student:', studentId);
    $.ajax({
        url: '/Students/GetStudent/' + studentId,
        type: 'GET',
        success: function (studentData) {
            populateEditForm(studentData);
            $('#addStudentModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log('Error loading student data: ' + error);
            showMessage('Error loading student data', 'error');
        }
    });
}

function populateEditForm(studentData) {
    // Fill form fields
    $('#FirstName').val(studentData.firstName);
    $('#LastName').val(studentData.lastName);
    $('#Email').val(studentData.email);
    $('#PhoneNumber').val(studentData.phoneNumber);

    // Store student ID for update
    $('#createStudentForm').data('student-id', studentData.studentId);

    // Check enrolled courses
    $('input[name="SelectedCourseIds"]').prop('checked', false);
    if (studentData.courses) {
        studentData.courses.forEach(function (course) {
            $('#course_' + course.courseId).prop('checked', true);
        });
    }

    // Change modal title and button text
    $('#modalTitle').text('Edit Student');
    $('#submitStudentBtn').text('Update Student');
}

// Details functionality
function openDetailsModal(studentId) {
    console.log('Opening details modal for student:', studentId);
    $.ajax({
        url: '/Students/GetStudent/' + studentId,
        type: 'GET',
        success: function (studentData) {
            populateDetailsForm(studentData);
            $('#addStudentModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log('Error loading student details: ' + error);
            showMessage('Error loading student details', 'error');
        }
    });
}

function populateDetailsForm(studentData) {
    // Fill form fields with student data
    $('#FirstName').val(studentData.firstName);
    $('#LastName').val(studentData.lastName);
    $('#Email').val(studentData.email);
    $('#PhoneNumber').val(studentData.phoneNumber);

    // Check enrolled courses
    $('input[name="SelectedCourseIds"]').prop('checked', false);
    if (studentData.courses) {
        studentData.courses.forEach(function (course) {
            $('#course_' + course.courseId).prop('checked', true);
        });
    }

    // Disable all form fields for details view
    $('#FirstName').prop('readonly', true);
    $('#LastName').prop('readonly', true);
    $('#Email').prop('readonly', true);
    $('#PhoneNumber').prop('readonly', true);

    // Disable all course checkboxes
    $('input[name="SelectedCourseIds"]').prop('disabled', true);

    // Change modal title and hide submit button
    $('#modalTitle').text('Student Details');
    $('#submitStudentBtn').hide();
}

// Delete functionality
function deleteStudent(studentId, studentName) {
    if (confirm(`Are you sure you want to delete student: ${studentName}?`)) {
        proceedWithDelete(studentId);
    }
}
function proceedWithDelete(studentId) {
    $.ajax({
        url: '/Students/Delete/' + studentId,
        type: 'POST',
        headers: {
            'RequestVerificationToken': $('input[name="__RequestVerificationToken"]').val()
        },
        success: function (response) {
            if (response.success) {
                removeStudentFromTable(studentId);
                showMessage('Student deleted successfully!', 'success');
            } else {
                showMessage('Error deleting student!', 'error');
            }
        },
        error: function (xhr, status, error) {
            console.log('Error deleting student: ' + error);
            showMessage('Error deleting student!', 'error');
        }
    });
}
function removeStudentFromTable(studentId) {
    // Find and remove the row using data attribute
    $(`tr[data-student-id="${studentId}"]`).remove();

    // Show "no students" message if table is empty
    if ($('table tbody tr').length === 0) {
        $('table').before(
            '<div class="alert alert-info">No students found. Click "Add New Student" to create the first student.</div>'
        );
        $('table').hide();
    }
}
function showMessage(message, type) {
    // Remove existing messages
    $('.ajax-alert').remove();

    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';
    const icon = type === 'success' ? '✓' : '!';

    const alertHtml = `
        <div class="alert ${alertClass} alert-dismissible fade show ajax-alert" role="alert" 
             style="position: fixed; top: 20px; right: 20px; z-index: 9999; min-width: 300px;">
            <strong>${icon}</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;

    $('body').append(alertHtml);

    // Auto remove after 5 seconds
    setTimeout(function () {
        $('.ajax-alert').alert('close');
    }, 5000);
}