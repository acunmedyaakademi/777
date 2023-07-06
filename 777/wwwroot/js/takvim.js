const dateList = document.querySelector(".dates");
const prevBtn = document.querySelector(".prevBtn");
const nextBtn = document.querySelector(".nextBtn");
const currentMonthYear = document.querySelector(".currentMonthYear");
const TOTAL_DAYS_VISIBLE = 42;
let malik = [];
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

$(document).ready(function () {

    let date = new Date();

    createCalendar(date);

});
function createCalendar(date) {
    const prev = getLastDate(date.getFullYear(), date.getMonth(), true);
    const curr = getLastDate(date.getFullYear(), date.getMonth() + 1);
    const next = TOTAL_DAYS_VISIBLE - (prev.days + curr);
    const currentMonth = MONTHS[date.getMonth()];
    const currentYear = date.getFullYear();

    // Firstly, clear the date list
    dateList.innerHTML = "";

    let Titles = [];
    $.post('/user/calendar/').done(function (data) {
        for (var i = 0; i < data.length; i++) {
            Titles.push(data[i]);
        }

        // Fill previous days on the calendar
        for (let i = prev.date - prev.days + 1; i <= prev.date; i++) {
            dateList.innerHTML += `
            <li class="date">${i}</li>
            `;
        }

        // Fill current days on the calendar
        for (let i = 1; i <= curr; i++) {
            malik.push(i);
            if (date.getDate() === i) {
                dateList.innerHTML += `
                <li class="date current today" data-id=${i} data-title=${i}-${currentMonth.toString()}-${currentYear.toString()}>${i.toString()}</li>
            `;
            }
            else {
                if (Titles.includes(`${i}-${currentMonth.toString()}`)) {
                    dateList.innerHTML += `
                <li class="date current complated" data-id=${i} data-title=${i}-${currentMonth.toString()}-${currentYear.toString()}>${i.toString()}</li>
            `;
                }
                else {
                    dateList.innerHTML += `
                <li class="date current" data-id=${i} data-title=${i}-${currentMonth.toString()}-${currentYear.toString()}>${i.toString()}</li>
            `;
                }
            }
        }

        // Fill next days on the calendar
        for (let i = 1; i <= next; i++) {
            dateList.innerHTML += `
            <li class="date">${i}</li>
             `;
        }

        // Update current month & year
        currentMonthYear.innerText = `${currentMonth
            }, ${currentYear}`;

        const liElements = dateList.querySelectorAll("li");


        liElements.forEach((li) => {
            if (li.classList.contains("complated")) {
                li.textContent = "✔";
                li.addEventListener("mouseover", (event) => {
                    li.textContent = handleMouseOver(event);
                })

                li.addEventListener("mouseout", () => {
                    li.textContent = "✔";
                })
            }
        });
    });


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
        if (withDay) {
            return {
                date: new Date(year, month, 0).getDate(),
                days: new Date(year, month, 0).getDay()
            }
        }

        return new Date(year, month, 0).getDate();
    };

    function buttonDeneme(event) {
        var li = event.target;
        var dataTitle = li.dataset.title;
        var dataArray = dataTitle.split("-");
        if (Titles.includes(`${dataArray[0]}-${dataArray[1]}`)) {

            var aElementi = document.getElementById("gizli");
            aElementi.href = "/user/textdetails?day=" + dataArray[0] + "&month=" + dataArray[1] + "&year=" + dataArray[2];
            aElementi.click();
            console.log(dataArray);
        }
    }

    // Event Listeners
    document.addEventListener("DOMContentLoaded", () => createCalendar(date));
    dateList.addEventListener("click", buttonDeneme);
}
    //// Confetti

    //const btn = document.querySelector('#sumbitBtn');
    //const popup = document.querySelector('.popup');
    //const close = document.querySelector('.close');
    //const canvasConfetti = document.querySelector('#my-canvas');

    //btn.addEventListener('click', (e) => {
    //    e.preventDefault();
    //    popup.classList.add('active');
    //    canvasConfetti.classList.add('active');
    //});

    //close.addEventListener('click', () => {
    //    popup.classList.remove('active');
    //    canvasConfetti.classList.remove('active');
    //});

    //let confettiSettings = { target: 'my-canvas' };
    //let confetti = new ConfettiGenerator(confettiSettings);
    //confetti.render();