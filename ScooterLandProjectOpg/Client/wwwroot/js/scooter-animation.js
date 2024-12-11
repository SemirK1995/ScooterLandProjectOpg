document.addEventListener("DOMContentLoaded", () => {
    const scooter = document.querySelector('.scooter');
    scooter.style.animation = 'none';
    setTimeout(() => {
        scooter.style.animation = '';
    }, 10);
});
