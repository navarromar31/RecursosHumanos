
let departments = [
    { name: 'Gerente Regional', description: 'Descripcion del gerente', image: 'marketing.jpg' },
    { name: 'Colaborador Interno', description: 'Descripcion del lider', image: 'rrhh.jpg' },
    { name: 'Analista de Datos', description: 'Descripcion del Analista', image: 'dev.jpg' }
];

const searchInputPuesto = document.getElementById('buscarPuesto');
const listaPuesto = document.getElementById('listaPuesto');
const toggleButton = document.getElementById('toggleButton-Puesto');

searchInputPuesto.addEventListener('input', function () {
    const query = searchInputPuesto.value.toLowerCase();
    const filteredDepartments = departments.filter(department =>
        department.name.toLowerCase().includes(query)
    );
    renderlistaPuesto(filteredDepartments);
});

toggleButton.addEventListener('click', function () {
    renderlistaPuesto(departments);
});

function renderlistaPuesto(departments) {
    listaPuesto.innerHTML = '';
    if (departments.length === 0) {
        listaPuesto.innerHTML = '<p class="text-center text-muted">No hay Puestos</p>';
    } else {
        departments.forEach(department => {
            const div = document.createElement('div');
            div.classList.add('itemPuesto');
            div.innerHTML = `
                <span>${department.name}</span>
                <button class="btn btn-info" data-bs-toggle="modal" data-bs-target="#departmentModal" onclick="showDepartmentDetails('${department.name}')" style="background-color: #505050; border-color: #505050; color: white;">Más Información</button>
            `;
            listaPuesto.appendChild(div);
        });
    }
}


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
