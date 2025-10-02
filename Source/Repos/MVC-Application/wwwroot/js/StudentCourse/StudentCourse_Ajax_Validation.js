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
                addNewStudentToTable(response.student,); // Use the returned student
                resetForm();
            }
        },
        error: function (xhr, status, error) {
            console.log('Error saving student: ' + error, 'error');
        }
    });
}

function addNewStudentToTable(student) {

    const newRow = `
        <tr>
            <td>${student.studentId}</td>
            <td>${student.firstName} ${student.lastName}</td>
            <td>${student.email}</td>
            <td>${student.phoneNumber}</td>
            <td>
                <a href="/Students/Details/${student.studentId}" class="btn btn-info btn-sm">Details</a>
                <button type="button" class="btn btn-warning btn-sm" 
                        onclick="openEditModal(${student.studentId})">Edit</button>
                <button type="button" class="btn btn-danger btn-sm" 
                        onclick="deleteStudent(${student.studentId}, '${student.firstName} ${student.lastName}')"
                        data-student-id="${student.studentId}">Delete</button>
            </td>
        </tr>
    `;

    $('table tbody').append(newRow);
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

    // Clear edit mode
    $('#createStudentForm').removeData('student-id');
    $('#addStudentModal .modal-title').text('Add New Student');
    $('#submitStudentBtn').text('Save Student');
}


function openEditModal(studentId) {
    $.ajax({
        url: '/Students/GetStudent/' + studentId,
        type: 'GET',
        success: function (studentData) {
            populateEditForm(studentData);
            $('#addStudentModal').modal('show');
        },
        error: function (xhr, status, error) {
            console.log('Error loading student data: ' + error);
        }
    });
}

function populateEditForm(studentData) {
    // Fill form fields
    $('#StudentForm_Student_FirstName').val(studentData.firstName);
    $('#StudentForm_Student_LastName').val(studentData.lastName);
    $('#StudentForm_Student_Email').val(studentData.email);
    $('#StudentForm_Student_PhoneNumber').val(studentData.phoneNumber);

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
    $('#addStudentModal .modal-title').text('Edit Student');
    $('#submitStudentBtn').text('Update Student');
}

// Update form submission to handle both create and edit
function submitStudentForm() {
    if (!validateForm()) {
        return;
    }

    const formData = getFormData();
    const studentId = $('#createStudentForm').data('student-id');

    if (studentId) {
        // Editing existing student
        updateStudent(studentId, formData);
    } else {
        // Creating new student
        submitViaAjax(formData);
    }
}

// Function to update student
function updateStudent(studentId, formData) {
    formData.Student.StudentId = studentId; // Add ID for update

    $.ajax({
        url: '/Students/Update',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function (response) {
            if (response.success) {
                $('#addStudentModal').modal('hide');
                updateStudentInTable(studentId, formData.Student);
                resetForm();
            }
        },
        error: function (xhr, status, error) {
            console.log('Error updating student: ' + error);
        }
    });
}
function updateStudentInTable(studentId, student) {
    // Find the row and update it
    $('table tbody tr').each(function () {
        const row = $(this);
        const detailsLink = row.find('.btn-info').attr('href');

        if (detailsLink && detailsLink.includes(studentId)) {
            row.find('td:eq(0)').text(student.StudentId);
            row.find('td:eq(1)').text(student.FirstName + ' ' + student.LastName);
            row.find('td:eq(2)').text(student.Email);
            row.find('td:eq(3)').text(student.PhoneNumber);
        }
    });
}


// Delete functionality
function deleteStudent(studentId, studentName) {
    if (confirm(`Are you sure you want to delete student: ${studentName}?`)) {
        proceedWithDelete(studentId);
    }
}

// Function to actually delete the student
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
                showDeleteMessage('Student deleted successfully!', 'success');
            }
        },
        error: function (xhr, status, error) {
            console.log('Error deleting student: ' + error);
            showDeleteMessage('Error deleting student!', 'error');
        }
    });
}

function removeStudentFromTable(studentId) {
    // Find and remove the row using data attribute
    $(`button[data-student-id="${studentId}"]`).closest('tr').remove();
}

// Function to show delete confirmation/result message
function showDeleteMessage(message, type) {
    // Remove existing messages
    $('.delete-alert').remove();

    const alertClass = type === 'success' ? 'alert-success' : 'alert-danger';

    const alertHtml = `
        <div class="alert ${alertClass} alert-dismissible fade show delete-alert" role="alert" 
             style="position: fixed; top: 20px; right: 20px; z-index: 9999; min-width: 300px;">
            <strong>${type === 'success' ? '✓' : '!'}</strong> ${message}
            <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
        </div>
    `;

    $('body').append(alertHtml);

    // Auto remove after 3 seconds
    setTimeout(function () {
        $('.delete-alert').alert('close');
    }, 3000);
}