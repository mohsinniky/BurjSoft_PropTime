const popoverTriggerList = document.querySelectorAll('[data-bs-toggle="popover"]')
const popoverList = [...popoverTriggerList].map(popoverTriggerEl => new bootstrap.Popover(popoverTriggerEl))



// Initialize all toasts on the page
const toastTriggerList = document.querySelectorAll('[data-bs-toggle="toast"]');
const toastList = [...toastTriggerList].map(toastEl => new bootstrap.Toast(toastEl));