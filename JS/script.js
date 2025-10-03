// JS pour zoom fluide, fade du numéro, navigation aux flèches et Échap
const cards = Array.from(document.querySelectorAll('.card blue'));
let currentIndex = -1;

function expandCard(card, index) {
    const number = card.querySelector('.project-number');
    number.style.opacity = 0;

    // Dimensions et position initiales
    const rect = card.getBoundingClientRect();
    const scaleX = (window// JS pour zoom fluide, fade du numéro, navigation aux flèches et Échap
const cards = Array.from(document.querySelectorAll('.card'));
let currentIndex = -1;

function expandCard(card, index) {
    const number = card.querySelector('.project-number');
    number.style.opacity = 0;

    // Dimensions et position initiales
    const rect = card.getBoundingClientRect();
    const scaleX = (window.innerWidth * 0.9) / rect.width;
    const scaleY = (window.innerHeight * 0.9) / rect.height;
    const translateX = window.innerWidth / 2 - (rect.left + rect.width / 2);
    const translateY = window.innerHeight / 2 - (rect.top + rect.height / 2);

    card.style.transition = 'transform 0.5s ease';
    card.style.transform = `translate(${translateX}px, ${translateY}px) scale(${scaleX}, ${scaleY})`;
    card.classList.add('expanded');

    currentIndex = index;
}

function collapseCard(card) {
    const number = card.querySelector('.project-number');
    number.style.opacity = 1;

    card.style.transition = 'transform 0.5s ease';
    card.style.transform = 'translate(0px, 0px) scale(1, 1)';

    card.addEventListener('transitionend', function handler() {
        card.classList.remove('expanded');
        card.style.transition = '';
        card.style.transform = '';
        card.removeEventListener('transitionend', handler);
        currentIndex = -1;
    });
}

cards.forEach((card, index) => {
    const closeBtn = card.querySelector('.close-btn');

    // Clic sur la carte
    card.addEventListener('click', (e) => {
        if (!card.classList.contains('expanded') && e.target !== closeBtn) {
            // Fermer les autres cartes si ouvertes
            cards.forEach(c => {
                if (c.classList.contains('expanded')) collapseCard(c);
            });
            expandCard(card, index);
        }
    });

    // Clic sur le bouton fermer
    closeBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        collapseCard(card);
    });
});

// Navigation clavier
document.addEventListener('keydown', (e) => {
    if (currentIndex === -1) return;

    if (e.key === 'Escape') {
        collapseCard(cards[currentIndex]);
    } else if (e.key === 'ArrowRight') {
        let nextIndex = (currentIndex + 1) % cards.length;
        collapseCard(cards[currentIndex]);
        setTimeout(() => expandCard(cards[nextIndex], nextIndex), 500);
    } else if (e.key === 'ArrowLeft') {
        let prevIndex = (currentIndex - 1 + cards.length) % cards.length;
        collapseCard(cards[currentIndex]);
        setTimeout(() => expandCard(cards[prevIndex], prevIndex), 500);
    }
});
.innerWidth * 0.9) / rect.width;
    const scaleY = (window.innerHeight * 0.9) / rect.height;
    const translateX = window.innerWidth / 2 - (rect.left + rect.width / 2);
    const translateY = window.innerHeight / 2 - (rect.top + rect.height / 2);

    card.style.transition = 'transform 0.5s ease';
    card.style.transform = `translate(${translateX}px, ${translateY}px) scale(${scaleX}, ${scaleY})`;
    card.classList.add('expanded');

    currentIndex = index;
}

function collapseCard(card) {
    const number = card.querySelector('.project-number');
    number.style.opacity = 1;

    card.style.transition = 'transform 0.5s ease';
    card.style.transform = 'translate(0px, 0px) scale(1, 1)';

    card.addEventListener('transitionend', function handler() {
        card.classList.remove('expanded');
        card.style.transition = '';
        card.style.transform = '';
        card.removeEventListener('transitionend', handler);
        currentIndex = -1;
    });
}

cards.forEach((card, index) => {
    const closeBtn = card.querySelector('.close-btn');

    // Clic sur la carte
    card.addEventListener('click', (e) => {
        if (!card.classList.contains('expanded') && e.target !== closeBtn) {
            // Fermer les autres cartes si ouvertes
            cards.forEach(c => {
                if (c.classList.contains('expanded')) collapseCard(c);
            });
            expandCard(card, index);
        }
    });

    // Clic sur le bouton fermer
    closeBtn.addEventListener('click', (e) => {
        e.stopPropagation();
        collapseCard(card);
    });
});

// Navigation clavier
document.addEventListener('keydown', (e) => {
    if (currentIndex === -1) return;

    if (e.key === 'Escape') {
        collapseCard(cards[currentIndex]);
    } else if (e.key === 'ArrowRight') {
        let nextIndex = (currentIndex + 1) % cards.length;
        collapseCard(cards[currentIndex]);
        setTimeout(() => expandCard(cards[nextIndex], nextIndex), 300);
    } else if (e.key === 'ArrowLeft') {
        let prevIndex = (currentIndex - 1 + cards.length) % cards.length;
        collapseCard(cards[currentIndex]);
        setTimeout(() => expandCard(cards[prevIndex], prevIndex), 300);
    }
});
