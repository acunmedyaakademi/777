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

let malik = [];


function createCalendar(date) {
    const prev = getLastDate(date.getFullYear(), date.getMonth(), true);
    const curr = getLastDate(date.getFullYear(), date.getMonth() + 1);
    const next = TOTAL_DAYS_VISIBLE - (prev.days + curr);


    // Firstly, clear the date list
    dateList.innerHTML = "";


    // Fill previous days on the calendar
    for(let i = prev.date - prev.days + 1; i <= prev.date; i++){
        dateList.innerHTML += `
            <li class="date">${i}</li>
        `;
    }

    // Fill current days on the calendar
    for(let i = 1; i <= curr; i++) {
        malik.push(i);
        if(date.getDate() === i) {
            dateList.innerHTML += `
                <li class="date current today" data-id=${i}>${i}</li>
            `;
        }else {
            dateList.innerHTML += `
                <li class="date current complated" data-id=${i}>${i}</li>
            `;
        }
    }
    
    // Fill next days on the calendar
    for(let i = 1; i <= next; i++) {
        dateList.innerHTML += `
            <li class="date">${i}</li>
        `;
    }
    
    // Update current month & year
    currentMonthYear.innerText = `${
        MONTHS[date.getMonth()]
    }, ${date.getFullYear()}`;

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

// Her bir buton üzerine geldiğinde çalışacak olay dinleyicisi fonksiyonunu tanımlayın
function handleMouseOver(event) {
    const liElements = dateList.querySelectorAll(".current");
    // Hangi düğmeye tıkladığımızı almak için "target" özelliğini kullanın
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
    

// Get last date of the previous month
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

