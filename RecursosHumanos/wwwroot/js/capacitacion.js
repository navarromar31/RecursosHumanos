
document.addEventListener('DOMContentLoaded', function () {
        const searchInput = document.getElementById('searchInput');
const searchButton = document.getElementById('searchButton');
const noResultsMessage = document.getElementById('noResultsMessage');
const cards = document.querySelectorAll('.capacitacion-card');

// Función para filtrar tarjetas y mostrar mensaje si no hay resultados
function filterCards() {
            const searchTerm = searchInput.value.toLowerCase();
let foundResults = false;

            cards.forEach(card => {
                const name = card.querySelector('.card-title').textContent.toLowerCase();
const modalidad = card.querySelector('.card-text').textContent.toLowerCase();

if (name.includes(searchTerm) || modalidad.includes(searchTerm)) {
    card.style.display = 'block';
foundResults = true;
                } else {
    card.style.display = 'none';
                }
            });

// Mostrar el mensaje si no se encontraron resultados
if (!foundResults && searchTerm !== "") {
    noResultsMessage.style.display = 'block';
            } else {
    noResultsMessage.style.display = 'none';
            }
        }

// Buscar cuando se presiona el botón o se escribe en el campo
searchButton.addEventListener('click', filterCards);
searchInput.addEventListener('input', filterCards);
    });







document.getElementById('ImagenCapacitacion').addEventListener('change', function () {
    const fileName = this.files[0] ? this.files[0].name : 'Ningún archivo seleccionado';
    document.getElementById('imagenNombre').value = fileName;
});

document.getElementById('MinColabIncrement').onclick = () => {
    let input = document.getElementById('MinColab');
    input.value = parseInt(input.value || 0) + 1;
};

document.getElementById('MinColabDecrement').onclick = () => {
    let input = document.getElementById('MinColab');
    if (parseInt(input.value || 0) > 0) {
        input.value = parseInt(input.value || 0) - 1;
    }
};

document.getElementById('MaxColabIncrement').onclick = () => {
    let input = document.getElementById('MaxColab');
    input.value = parseInt(input.value || 0) + 1;
};

document.getElementById('MaxColabDecrement').onclick = () => {
    let input = document.getElementById('MaxColab');
    if (parseInt(input.value || 0) > 0) {
        input.value = parseInt(input.value || 0) - 1;
    }
};







// Previsualización de la imagen (en caso de que se necesite mostrar una imagen cargada desde el servidor)
document.addEventListener('DOMContentLoaded', () => {
    const preview = document.getElementById('previewImage');
    const imageUrl = '@Model.ImagenUrlCap'; // Asegúrate de que el modelo contenga la URL de la imagen
    if (imageUrl) {
        preview.src = imageUrl;
    }
});
