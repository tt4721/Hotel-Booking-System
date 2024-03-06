const allreservations = document.querySelector('.all-reservations');
const allreservationBtn = document.querySelector('.all-reservations-btn');


const addadmin = document.querySelector('.add-admin');
const addadminBtn = document.querySelector('.add-admin-btn');


const closeBtnAllReservations = document.querySelector('.all-reservations .icon-close');
const closeBtnAdmin = document.querySelector('.add-admin .icon-close');



allreservationBtn.addEventListener('click', () => {
    allreservations.classList.add('active');
    addadmin.classList.remove('active');
});

addadminBtn.addEventListener('click', () => {
    addadmin.classList.add('active');
    allreservations.classList.remove('active')
});

closeBtnAllReservations.addEventListener('click', () => {
    allreservations.classList.remove('active');
});

closeBtnAdmin.addEventListener('click', () => {
    addadmin.classList.remove('active');
});