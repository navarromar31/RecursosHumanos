
// Función para mostrar el perfil del colaborador
function viewProfile(name, id, email, department, position, institution) {
    document.getElementById('profile-name').innerText = name;
    document.getElementById('profile-id').innerText = id;
    document.getElementById('profile-email').innerText = email;
    document.getElementById('profile-department').innerText = department;
    document.getElementById('profile-position').innerText = position;
    document.getElementById('profile-institution').innerText = institution;
    document.getElementById('profile-section').classList.remove('d-none');
}

// Función para mostrar/ocultar la lista de colaboradores
document.getElementById('toggleCollaboratorList').addEventListener('click', function () {
    let list = document.getElementById('collaboratorList');
    if (list.style.display === 'none') {
        list.style.display = 'block';
        this.innerHTML = '<i class="fas fa-chevron-up"></i>';
    } else {
        list.style.display = 'none';
        this.innerHTML = '<i class="fas fa-chevron-down"></i>';
    }
});
