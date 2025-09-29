$(document).ready(function () {
    loadStudents();

    // Show modal for add
    $('#addStudentBtn').on('click', function () {
        clearStudentForm();
        $('#studentModal').modal('show');
    });

    // Save student (add or update)
    $('#studentForm').on('submit', function (e) {
        e.preventDefault();
        var student = {
            Id: $('#studentId').val() ? parseInt($('#studentId').val()) : 0,
            Name: $('#studentName').val(),
            Email: $('#studentEmail').val(),
            Age: parseInt($('#studentAge').val())
        };
        $.ajax({
            url: '/Students/SaveStudent',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(student),
            success: function (res) {
                $('#studentModal').modal('hide');
                loadStudents();
            }
        });
    });
});

// Load students table
function loadStudents() {
    $.get('/Students/GetAllStudents', function (students) {
        var html = `<table class="table table-bordered">
            <thead><tr><th>Name</th><th>Email</th><th>Age</th><th>Actions</th></tr></thead><tbody>`;
        $.each(students, function (i, s) {
            html += `<tr>
                <td>${s.name}</td>
                <td>${s.email}</td>
                <td>${s.age}</td>
                <td>
                    <button class="btn btn-sm btn-warning" onclick="editStudent(${s.id})">Edit</button>
                    <button class="btn btn-sm btn-danger" onclick="deleteStudent(${s.id})">Delete</button>
                </td>
            </tr>`;
        });
        html += '</tbody></table>';
        $('#studentsTableContainer').html(html);
    });
}

// Edit student
function editStudent(id) {
    $.get('/Students/GetStudent?id=' + id, function (s) {
        $('#studentId').val(s.id);
        $('#studentName').val(s.name);
        $('#studentEmail').val(s.email);
        $('#studentAge').val(s.age);
        $('#studentModal').modal('show');
    });
}

// Delete student
function deleteStudent(id) {
    if (confirm('Delete this student?')) {
        $.post('/Students/DeleteStudent?id=' + id, function (res) {
            loadStudents();
        });
    }
}

// Clear form
function clearStudentForm() {
    $('#studentId').val('');
    $('#studentName').val('');
    $('#studentEmail').val('');
    $('#studentAge').val('');
}