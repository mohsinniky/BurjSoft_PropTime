$(document).ready(function () {
    loadFiles();

    $('#refreshBtn').click(function () {
        loadFiles();
    });

    $('#addFileBtn').click(function () {
        resetModal();
        $('#modalTitle').text('New Text File');
        $('#fileExtension').val('.txt').prop('disabled', true);
        $('#fileContent').prop('disabled', false);
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
            Content: $('#fileContent').val(),
            Extension: $('#fileExtension').val()
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
            Content: $('#fileContent').val(),
            Extension: $('#fileExtension').val()
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
                    $('#fileExtension').val(response.file.extension).prop('disabled', true);

                    // Only allow editing for text files
                    if (response.canEdit) {
                        $('#fileContent').prop('disabled', false);
                        $('#modalTitle').text('Edit Text File');
                    } else {
                        $('#fileContent').prop('disabled', true);
                        $('#modalTitle').text('View File Details');
                        $('#saveFileBtn').hide();
                    }

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
            tbody.append('<tr><td colspan="5" class="text-center">No files found</td></tr>');
            return;
        }

        files.forEach(file => {
            const fileIcon = getFileIcon(file.extension);
            const fullFileName = `${file.originalName}${file.extension}`;

            const row = `
                <tr>
                    <td>
                        <i class="${fileIcon} me-2"></i>
                        ${fullFileName}
                    </td>
                    <td>${file.extension?.toUpperCase() || 'Unknown'}</td>
                    <td>${new Date(file.createdDate).toLocaleDateString()}</td>
                    <td>${formatFileSize(file.size)}</td>
                    <td>
                        <button class="btn btn-sm btn-info" onclick="readFile('${file.id}')">
                            <i class="fas fa-eye"></i> View
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

    function getFileIcon(extension) {
        switch (extension?.toLowerCase()) {
            case '.txt': return 'fas fa-file-alt text-secondary';
            case '.pdf': return 'fas fa-file-pdf text-danger';
            case '.doc': case '.docx': return 'fas fa-file-word text-primary';
            case '.xls': case '.xlsx': return 'fas fa-file-excel text-success';
            case '.jpg': case '.jpeg': case '.png': case '.gif': return 'fas fa-file-image text-warning';
            case '.zip': case '.rar': return 'fas fa-file-archive text-warning';
            default: return 'fas fa-file text-secondary';
        }
    }

    function resetModal() {
        $('#fileForm')[0].reset();
        $('#fileId').val('');
        $('#fileContent').prop('disabled', false);
        $('#saveFileBtn').show();
    }

    function showMessage(message, type) {
        $('#message').removeClass('alert-success alert-danger alert-warning')
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