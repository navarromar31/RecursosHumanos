
document.querySelectorAll('a').forEach(anchor => {
    anchor.addEventListener('click', function (e) {
        if (this.hash) {
            e.preventDefault();
            document.querySelector(this.hash).scrollIntoView({
                behavior: 'smooth'
            });
        }
    });
});

document.addEventListener('DOMContentLoaded', function () {
    const cards = document.querySelectorAll('.glass-card');

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                entry.target.classList.add('show');
            }
        });
    });

    cards.forEach(card => observer.observe(card));
});



document.addEventListener('DOMContentLoaded', function () {
    const heroText = document.querySelector('.hero-contentInstitucion');
    heroText.style.opacity = '0';
    heroText.style.transform = 'translateY(30px)';
    setTimeout(() => {
        heroText.style.transition = 'all 1s ease';
        heroText.style.opacity = '1';
        heroText.style.transform = 'translateY(0)';
    }, 500);
});

document.querySelectorAll('.btn').forEach(button => {
    button.addEventListener('mousedown', function () {
        this.style.transform = 'scale(0.95)';
    });
    button.addEventListener('mouseup', function () {
        this.style.transform = 'scale(1)';
    });
});

let currentDepartment = null;

function showDepartmentDetails(departmentName) {
    currentDepartment = departments.find(d => d.name === departmentName);
    document.getElementById('departmentModalLabel').innerText = currentDepartment.name;
    document.getElementById('imagenPuesto').src = currentDepartment.image;
    document.getElementById('departmentName').innerText = currentDepartment.name;
    document.getElementById('departmentDescription').innerText = currentDepartment.description;
}

// Editar 
document.getElementById('editDepartmentForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const name = document.getElementById('editName').value;
    const description = document.getElementById('editDescription').value;
    const image = document.getElementById('editImage').value;

    currentDepartment.name = name;
    currentDepartment.description = description;
    currentDepartment.image = image;

    renderlistaPuesto(departments);
    const modal = bootstrap.Modal.getInstance(document.getElementById('editDepartmentModal'));
    modal.hide();
});

// Eliminar 
document.getElementById('deleteBtn').addEventListener('click', function () {
    departments = departments.filter(department => department.name !== currentDepartment.name);
    renderlistaPuesto(departments);
    const modal = bootstrap.Modal.getInstance(document.getElementById('departmentModal'));
    modal.hide();
});

// Agregar
document.getElementById('addDepartmentForm').addEventListener('submit', function (event) {
    event.preventDefault();

    const name = document.getElementById('newName').value;
    const description = document.getElementById('newDescription').value;
    const image = document.getElementById('newImage').value;

    departments.push({ name, description, image });
    renderlistaPuesto(departments);

    const modal = bootstrap.Modal.getInstance(document.getElementById('addDepartmentModal'));
    modal.hide();




});



        // JavaScript para llenar los datos del modal de edición con los valores del puesto
    var editDepartmentModal = document.getElementById('editDepartmentModal')
    editDepartmentModal.addEventListener('show.bs.modal', function (event) {
            var button = event.relatedTarget; // Botón que abrió el modal
    var id = button.getAttribute('data-id');
    var nombre = button.getAttribute('data-nombre');
    var descripcion = button.getAttribute('data-descripcion');

    // Llenamos los campos del modal con los valores correspondientes
    document.getElementById('editName').value = nombre;
    document.getElementById('editDescription').value = descripcion;
            // Si es necesario, puedes pasar otros datos como el id
        })
