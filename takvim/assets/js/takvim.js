// DOM Elements
const dateList = document.querySelector(".dates");
const prevBtn = document.querySelector(".prevBtn");
const nextBtn = document.querySelector(".nextBtn");
const currentMonthYear = document.querySelector(".currentMonthYear");

let date = new Date();
const TOTAL_DAYS_VISIBLE = 42;
const MONTHS = [
    'Ocak',
    'Şubat',
    'Mart',
    'Nisan',
    'Mayıs',
    'Haziran',
    'Temmuz',
    'Ağustos',
    'Eylül',
    'Ekim',
    'Kasım',
    'Aralık'
];


function createCalendar(date) {
    const prev = getLastDate(date.getFullYear(), date.getMonth(), true);
    const curr = getLastDate(date.getFullYear(), date.getMonth() + 1);
    const next = TOTAL_DAYS_VISIBLE - (prev.days + curr);
    const currentMonth = MONTHS[date.getMonth()];
    const currentYear = date.getFullYear();

    // Tarih listesini temizle
    dateList.innerHTML = "";


    // Takvimde onceki gunleri doldurun
    for(let i = prev.date - prev.days + 1; i <= prev.date; i++){
        dateList.innerHTML += `
            <li class="date">${i}</li>
        `;
    }

    // Takvimdeki mevcut gunleri doldurun
    for(let i = 1; i <= curr; i++) {
        if(date.getDate() === i) {
            dateList.innerHTML += `
                <li class="date current today" data-id=${i} data-title=${i}->${i}</li>
            `;
        }else {
            dateList.innerHTML += `
                <li class="date current" data-id=${i} data-title=${i}-${currentMonth}-${currentYear}>${i}</li>
            `;
        }
    }
    
    // Takvimde sonraki gunleri doldur
    for(let i = 1; i <= next; i++) {
        dateList.innerHTML += `
            <li class="date">${i}</li>
        `;
    }

    // Mevcut ay ve gunu guncelle
    currentMonthYear.innerText = `${
        currentMonth
    }, ${currentYear}`;

    const liElements = dateList.querySelectorAll("li");

    liElements.forEach((li) => {
        if (li.classList.contains("complated")) {
            li.innerText = "✔";
            li.addEventListener("mouseover", (event) => {
                li.textContent = handleMouseOver(event);
            })

            li.addEventListener("mouseout", () => {
                li.textContent = "✔";
            })
        }
    });
};

// Her bir buton uzerine geldiginde calisacak olay dinleyicisi fonksiyonunu tanimlayin
function handleMouseOver(event) {
    const liElements = dateList.querySelectorAll(".current");
    // Hangi dugmeye tikladigimizi almak icin "target" ozelligini kullanin
    let buttonIndex = Array.from(liElements).indexOf(event.target) + 1;
    console.log("Button Index:", buttonIndex);
    return buttonIndex;
}

// Previous month
function prevMonth() {
    date = new Date(date.getFullYear(), date.getMonth() - 1, date.getDate());

    createCalendar(date);
}

// Next month
function nextMonth() {
    date = new Date(date.getFullYear(), date.getMonth() + 1, date.getDate());
    
    createCalendar(date);
}
    

// Onceki ayin son tarihini al
function getLastDate(year, month, withDay = false) {
    if(withDay) {
        return {
            date: new Date(year, month, 0).getDate(),
            days: new Date(year, month, 0).getDay()
        }
    }

    return new Date(year, month, 0).getDate();
};

function buttonDeneme() {
    console.log("tıklandı");
}

// Event Listeners
document.addEventListener("DOMContentLoaded", () => createCalendar(date));
dateList.addEventListener("click", buttonDeneme);


// Confetti

const btn = document.querySelector('#button');
const popup = document.querySelector('.popup');
const close = document.querySelector('.close');
const canvasConfetti = document.querySelector('#my-canvas');

btn.addEventListener('click', (e) => {
    e.preventDefault();
    popup.classList.add('active');
    canvasConfetti.classList.add('active');
});

close.addEventListener('click', () => {
    popup.classList.remove('active');
    canvasConfetti.classList.remove('active');
});

let confettiSettings = { target: 'my-canvas' };
let confetti = new ConfettiGenerator(confettiSettings);
confetti.render();