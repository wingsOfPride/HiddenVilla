window.ShowToastr = (type, message) => {
    if (type === "success") {
        toastr.success(message, 'Operation Successful')
    }

    if (type === "error") {
        toastr.error(message, 'Operation Failed')
    }

}

window.ShowSweetAlert = (type, message) => {
    if (type === "success") {
        Swal.fire({
            icon: 'success',
            title: 'Success!',
            text: message
        })
    }

    if (type === "error") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: message
        })
    }

}


function ShowDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('show');
}

function HideDeleteConfirmationModal() {
    $('#deleteConfirmationModal').modal('hide');
}