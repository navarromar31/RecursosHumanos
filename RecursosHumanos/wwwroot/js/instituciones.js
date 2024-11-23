
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



