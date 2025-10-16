$(document).ready(function () {
    // Load files on page load
    loadFiles();

    // Refresh button
    $('#refreshBtn').click(function () {
        loadFiles();
    });

    // Add new file button - reset modal
    $('#addFileBtn').click(function () {
        resetModal();
        $('#modalTitle').text('Add New File');
        //$('#fileModal').show();
        $('#fileUploadGroup').show();
    });

    // Save file (Upload/Update)
    $('#saveFileBtn').click(function () {
        const fileId = $('#fileId').val();
        
        if (fileId) {
            updateFile();
        } else {
            uploadFile();
        }
    });

    // Load all files
    function loadFiles() {
        $.ajax({
            url: '/Files/GetAllFiles',
            type: 'GET',
            success: function (response) {
                if (response.success) {
                    populateFilesTable(response.files);
                    showMessage('Files loaded successfully!', 'success');
                } else {
                    showMessage('Error loading files: ' + response.message, 'danger');
                }
            },
            error: function () {
                showMessage('Error loading files.', 'danger');
            }
        });
    }

    // Upload new file
    function uploadFile() {
        const formData = new FormData();
        const fileInput = $('#fileInput')[0];
        const fileType = $('#fileExtension').val();

        if (!fileInput.files[0]) {
            showMessage('Please select a file.', 'warning');
            return;
        }

        if (!fileType) {
            showMessage('Please select file type.', 'warning');
            return;
        }

        formData.append('file', fileInput.files[0]);
        formData.append('fileType', fileType);

        $.ajax({
            url: '/Files/UploadFile',
            type: 'POST',
            data: formData,
            processData: false,
            contentType: false,
            success: function (response) {
                if (response.success) {
                    $('#fileModal').modal('hide');
                    loadFiles();
                    showMessage(response.message, 'success');
                } else {
                    showMessage(response.message, 'danger');
                }
            },
            error: function () {
                showMessage('Error uploading file.', 'danger');
            }
        });
    }

    // Update file metadata
    function updateFile() {
        const request = {
            Id: $('#fileId').val(),
            Name: $('#fileName').val(),
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
            },
            error: function () {
                showMessage('Error updating file.', 'danger');
            }
        });
    }

    // Delete file with confirmation
    function deleteFile(fileId, fileName) {
        if (!confirm(`Are you sure you want to delete "${fileName}"?`)) {
            return;
        }

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
            },
            error: function () {
                showMessage('Error deleting file.', 'danger');
            }
        });
    }

    // Download file
    function downloadFile(fileId) {
        window.location.href = `/Files/DownloadFile?id=${fileId}`;
    }

    // Edit file - open modal
    function editFile(file) {
        resetModal();
        $('#modalTitle').text('Edit File');
        $('#fileId').val(file.id);
        $('#fileName').val(file.originalName);
        $('#fileExtension').val(file.extension);
        $('#fileUploadGroup').hide(); // Hide file input for edit
        $('#fileModal').modal('show');
    }

    // Populate files table
    function populateFilesTable(files) {
        const tbody = $('#filesTableBody');
        tbody.empty();

        if (files.length === 0) {
            tbody.append('<tr><td colspan="5" class="text-center">No files found</td></tr>');
            return;
        }

        files.forEach(file => {
            const row = `
                <tr>
                    <td>${file.originalName}</td>
                    <td>.${file.extension}</td>
                    <td>${new Date(file.createdDate).toLocaleDateString()}</td>
                    <td>${formatFileSize(file.size)}</td>
                    <td>
                        <button class="btn btn-sm btn-info" onclick="downloadFile('${file.id}')">
                            <i class="fas fa-download"></i> Download
                        </button>
                        <button class="btn btn-sm btn-warning" onclick="editFile(${JSON.stringify(file).replace(/"/g, '&quot;')})">
                            <i class="fas fa-edit"></i> Edit
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

    // Utility functions
    function resetModal() {
        $('#fileForm')[0].reset();
        $('#fileId').val('');
        $('#fileUploadGroup').show();
    }

    function showMessage(message, type) {
        const messageDiv = $('#message');
        messageDiv.removeClass('alert-success alert-danger alert-warning')
                 .addClass(`alert-${type}`)
                 .text(message)
                 .show();
        
        setTimeout(() => messageDiv.fadeOut(), 5000);
    }

    function formatFileSize(bytes) {
        if (bytes === 0) return '0 Bytes';
        const k = 1024;
        const sizes = ['Bytes', 'KB', 'MB', 'GB'];
        const i = Math.floor(Math.log(bytes) / Math.log(k));
        return parseFloat((bytes / Math.pow(k, i)).toFixed(2)) + ' ' + sizes[i];
    }

    // Make functions global for onclick handlers
    window.downloadFile = downloadFile;
    window.editFile = editFile;
    window.deleteFile = deleteFile;
});