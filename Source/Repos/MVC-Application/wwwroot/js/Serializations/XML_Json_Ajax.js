$(document).ready(function () {
    // JSON Serialization
    $('#jsonSerializeForm').on('submit', function (e) {
        e.preventDefault();

        const formData = {
            name: $(this).find('input[name="name"]').val(),
            email: $(this).find('input[name="email"]').val(),
            birthDate: $(this).find('input[name="birthDate"]').val()
        };

        $.ajax({
            url: '/Serializations/SerializeToJson',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#jsonMessage').removeClass('alert-danger').addClass('alert-success').html(response.message).show();
                    $('#jsonContent').html('<pre><code>' + escapeHtml(response.jsonContent) + '</code></pre>').show();
                    $('#jsonFileName').val(response.fileName);
                } else {
                    $('#jsonMessage').removeClass('alert-success').addClass('alert-danger').html(response.message).show();
                }
            },
            error: function () {
                $('#jsonMessage').removeClass('alert-success').addClass('alert-danger').html('Error occurred during JSON serialization.').show();
            }
        });
    });

    // JSON Deserialization
    $('#jsonDeserializeForm').on('submit', function (e) {
        e.preventDefault();

        const formData = {
            fileName: $(this).find('input[name="jsonFileName"]').val()
        };

        $.ajax({
            url: '/Serializations/DeserializeFromJson',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#jsonMessage').removeClass('alert-danger').addClass('alert-success').html(response.message).show();
                    $('#jsonContent').html('<pre><code>' + escapeHtml(response.jsonContent) + '</code></pre>').show();

                    const person = response.person;
                    $('#jsonPersonDetails').html(`
                        <p><strong>ID:</strong> ${person.id}</p>
                        <p><strong>Name:</strong> ${person.name}</p>
                        <p><strong>Email:</strong> ${person.email}</p>
                        <p><strong>Birth Date:</strong> ${new Date(person.birthDate).toLocaleDateString()}</p>
                        <p><strong>Age:</strong> ${person.age}</p>
                    `).show();
                } else {
                    $('#jsonMessage').removeClass('alert-success').addClass('alert-danger').html(response.message).show();
                    $('#jsonPersonDetails').hide();
                }
            },
            error: function () {
                $('#jsonMessage').removeClass('alert-success').addClass('alert-danger').html('Error occurred during JSON deserialization.').show();
            }
        });
    });

    // XML Serialization
    $('#xmlSerializeForm').on('submit', function (e) {
        e.preventDefault();

        const formData = {
            name: $(this).find('input[name="name"]').val(),
            email: $(this).find('input[name="email"]').val(),
            birthDate: $(this).find('input[name="birthDate"]').val()
        };

        $.ajax({
            url: '/Serializations/SerializeToXml',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#xmlMessage').removeClass('alert-danger').addClass('alert-success').html(response.message).show();
                    $('#xmlContent').html('<pre><code>' + escapeHtml(response.xmlContent) + '</code></pre>').show();
                    $('#xmlFileName').val(response.fileName);
                } else {
                    $('#xmlMessage').removeClass('alert-success').addClass('alert-danger').html(response.message).show();
                }
            },
            error: function () {
                $('#xmlMessage').removeClass('alert-success').addClass('alert-danger').html('Error occurred during XML serialization.').show();
            }
        });
    });

    // XML Deserialization
    $('#xmlDeserializeForm').on('submit', function (e) {
        e.preventDefault();

        const formData = {
            fileName: $(this).find('input[name="xmlFileName"]').val()
        };

        $.ajax({
            url: '/Serializations/DeserializeFromXml',
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(formData),
            success: function (response) {
                if (response.success) {
                    $('#xmlMessage').removeClass('alert-danger').addClass('alert-success').html(response.message).show();
                    $('#xmlContent').html('<pre><code>' + escapeHtml(response.xmlContent) + '</code></pre>').show();

                    const person = response.person;
                    $('#xmlPersonDetails').html(`
                        <p><strong>ID:</strong> ${person.id}</p>
                        <p><strong>Name:</strong> ${person.name}</p>
                        <p><strong>Email:</strong> ${person.email}</p>
                        <p><strong>Birth Date:</strong> ${new Date(person.birthDate).toLocaleDateString()}</p>
                        <p><strong>Age:</strong> ${person.age}</p>
                    `).show();
                } else {
                    $('#xmlMessage').removeClass('alert-success').addClass('alert-danger').html(response.message).show();
                    $('#xmlPersonDetails').hide();
                }
            },
            error: function () {
                $('#xmlMessage').removeClass('alert-success').addClass('alert-danger').html('Error occurred during XML deserialization.').show();
            }
        });
    });

    // List Files
    $('#listFilesBtn').on('click', function () {
        $.ajax({
            url: '/Serializations/ListSerializedFiles',
            type: 'GET',
            success: function (response) {
                if (response.success) {
                    $('#fileMessage').removeClass('alert-danger').addClass('alert-success').html(response.message).show();

                    // Populate JSON files
                    let jsonFilesHtml = '';
                    response.jsonFiles.forEach(file => {
                        jsonFilesHtml += `<li class="list-group-item">${file}</li>`;
                    });
                    $('#jsonFilesList').html(jsonFilesHtml);

                    // Populate XML files
                    let xmlFilesHtml = '';
                    response.xmlFiles.forEach(file => {
                        xmlFilesHtml += `<li class="list-group-item">${file}</li>`;
                    });
                    $('#xmlFilesList').html(xmlFilesHtml);
                } else {
                    $('#fileMessage').removeClass('alert-success').addClass('alert-danger').html(response.message).show();
                }
            },
            error: function () {
                $('#fileMessage').removeClass('alert-success').addClass('alert-danger').html('Error occurred while listing files.').show();
            }
        });
    });

    // Utility function to escape HTML
    function escapeHtml(unsafe) {
        return unsafe
            .replace(/&/g, "&amp;")
            .replace(/</g, "&lt;")
            .replace(/>/g, "&gt;")
            .replace(/"/g, "&quot;")
            .replace(/'/g, "&#039;");
    }

});