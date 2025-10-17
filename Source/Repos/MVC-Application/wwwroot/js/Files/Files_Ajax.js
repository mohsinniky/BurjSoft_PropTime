$(document).ready(function () {
    loadFiles();

    $('#refreshBtn').click(function () {
        loadFiles();
    });

    $('#addFileBtn').click(function () {
        resetModal();
        $('#modalTitle').text('New Text File');
    });

    $('#saveFileBtn').click(function () {
        const fileId = $('#fileId').val();
        fileId ? updateFile() : createFile();
    });

    $('#uploadBtn').click(function () {
        uploadFile();
    });

    function loadFiles() {
        $.ajax({
            url: '/Files/GetAllFiles',
            type: 'GET',
            success: function (response) {
                if (response.success) {
                    populateFilesTable(response.files);
                } else {
                    showMessage('Error: ' + response.message, 'danger');
                }
            }
        });
    }

    function createFile() {
        const request = {
            Name: $('#fileName').val(),
            Content: $('#fileContent').val()
        };

        $.ajax({
            url: '/Files/CreateFile',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(request),
            success: function (response) {
                if (response.success) {
                    $('#fileModal').modal('hide');
                    loadFiles();
                    showMessage(response.message, 'success');
                } else {
                    showMessage(response.message, 'danger');
                }
            }
        });
    }

    function uploadFile() {
        const fileInput = $('#uploadInput')[0];

        if (!fileInput.files[0]) {
            showMessage('Please select a file.', 'warning');
            return;
        }

        const formData = new FormData();
        formData.append('file', fileInput.files[0]);

        $.ajax({
            url: '/Files/UploadFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    $('#uploadModal').modal('hide');
                    $('#uploadForm')[0].reset();
                    loadFiles();
                    showMessage(response.message, 'success');
                } else {
                    showMessage(response.message, 'danger');
                }
            }
        });
    }

    function updateFile() {
        const request = {
            Id: $('#fileId').val(),
            Name: $('#fileName').val(),
            Content: $('#fileContent').val()
        };

        $.ajax({
            url: '/Files/UpdateFile',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(request),
            success: function (response) {
                if (response.success) {
                    $('#fileModal').modal('hide');
                    loadFiles();
                    showMessage(response.message, 'success');
                } else {
                    showMessage(response.message, 'danger');
                }
            }
        });
    }

    function readFile(fileId) {
        $.ajax({
            url: '/Files/ReadFile?id=' + fileId,
            type: 'GET',
            success: function (response) {
                if (response.success) {
                    $('#fileId').val(response.file.id);
                    $('#fileName').val(response.file.originalName);
                    $('#fileContent').val(response.content);
                    $('#modalTitle').text('Edit Text File');
                    $('#fileModal').modal('show');
                } else {
                    showMessage(response.message, 'danger');
                }
            }
        });
    }

    function deleteFile(fileId, fileName) {
        if (!confirm(`Delete "${fileName}"?`)) return;

        $.ajax({
            url: '/Files/DeleteFile',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify({ Id: fileId }),
            success: function (response) {
                if (response.success) {
                    loadFiles();
                    showMessage(response.message, 'success');
                } else {
                    showMessage(response.message, 'danger');
                }
            }
        });
    }

    function downloadFile(fileId) {
        window.location.href = `/Files/DownloadFile?id=${fileId}`;
    }

    function populateFilesTable(files) {
        const tbody = $('#filesTableBody');
        tbody.empty();

        if (files.length === 0) {
            tbody.append('<tr><td colspan="4" class="text-center">No files found</td></tr>');
            return;
        }

        files.forEach(file => {
            const row = `
                <tr>
                    <td>${file.originalName}.txt</td>
                    <td>${new Date(file.createdDate).toLocaleDateString()}</td>
                    <td>${formatFileSize(file.size)}</td>
                    <td>
                        <button class="btn btn-sm btn-info" onclick="readFile('${file.id}')">
                            <i class="fas fa-edit"></i> Edit
                        </button>
                        <button class="btn btn-sm btn-success" onclick="downloadFile('${file.id}')">
                            <i class="fas fa-download"></i> Download
                        </button>
                        <button class="btn btn-sm btn-danger" onclick="deleteFile('${file.id}', '${file.originalName}')">
                            <i class="fas fa-trash"></i> Delete
                        </button>
                    </td>
                </tr>
            `;
            tbody.append(row);
        });
    }

    function resetModal() {
        $('#fileForm')[0].reset();
        $('#fileId').val('');
    }

    function showMessage(message, type) {
        $('#message').removeClass('alert-success alert-danger')
            .addClass(`alert-${type} alert`)
            .text(message)
            .show();
        setTimeout(() => $('#message').fadeOut(), 3000);
    }

    function formatFileSize(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    }

    window.readFile = readFile;
    window.downloadFile = downloadFile;
    window.deleteFile = deleteFile;
});