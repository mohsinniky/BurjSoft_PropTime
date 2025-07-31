const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))



// Initialize toasts with custom options
const toastElList = document.querySelectorAll('.toast');
const toastList = [...toastElList].map(toastEl => new bootstrap.Toast(toastEl, {
  autohide: false,       // Disable auto-hiding
  delay: 5000            // Hide after 5 seconds (if autohide is true)
}));

// Show toast on button click
document.getElementById('liveToastBtn').addEventListener('click', function () {
    const toastLive = document.getElementById('liveToast');
    const toast = bootstrap.Toast.getOrCreateInstance(toastLive);
    toast.show();
});

// Show toast on button click
document.getElementById('customToastBtn').addEventListener('click', function () {
    const toastElement = document.getElementById('customToast');
    const toast = bootstrap.Toast.getOrCreateInstance(toastElement);
    toast.show();
});