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

    // Firstly, clear the date list
    dateList.innerHTML = "";

    // let malik = [];

    // Fill previous days on the calendar
    for(let i = prev.date - prev.days + 1; i <= prev.date; i++){
        dateList.innerHTML += `
            <li class="date">${i}</li>
        `;
    }

    // Fill current days on the calendar
    for(let i = 1; i <= curr; i++) {
        // malik.push(i);
        if(date.getDate() === i) {
            dateList.innerHTML += `
                <li class="date current today">${i}</li>
            `;
        }else {
            dateList.innerHTML += `
                <li class="date current">${i}</li>
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

    // const liElements = dateList.querySelectorAll("li");

    // liElements.forEach((li) => {
    //     if (li.classList.contains("complated")) {
    //         li.innerText = "✔";    
    //     }
    // });

    // let previousText = "";

    // liElements.forEach((li) => {
    //     if (li.classList.contains("complated")) {
    //       li.addEventListener("mouseover", () => {
    //         previousText = li.textContent;
    //         li.textContent = "✔";
    //       });
      
    //       li.addEventListener("mouseout", () => {
    //         li.textContent = previousText;
    //       });
    //     }
    //   });
};

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

