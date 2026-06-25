// Starting flight data
const startingFlights = [
  { time: "15:05", flight: "NH 0175", dest: "TOKYO", gate: "D02", status: "DEPARTED" },
  { time: "15:15", flight: "WN 0612", dest: "LAS VEGAS", gate: "B09", status: "DEPARTED" },
  { time: "12:59", flight: "F9 1635", dest: "BOSTON", gate: "B05", status: "GATE CLOSED" },
  { time: "13:11", flight: "AS 3188", dest: "NEW YORK", gate: "D12", status: "DEPARTED" },
  { time: "13:37", flight: "BA 1760", dest: "SAN FRANCISCO", gate: "B20", status: "DELAYED" },
  { time: "12:40", flight: "AA 8826", dest: "CHICAGO", gate: "A11", status: "DEPARTED" },
  { time: "12:50", flight: "F9 0970", dest: "LONDON", gate: "C11", status: "BOARDING" }
];

let flights = [...startingFlights];

const board = document.getElementById("board");
const addBtn = document.getElementById("addBtn");
const resetBtn = document.getElementById("resetBtn");
const clock = document.getElementById("clock");
const counter = document.getElementById("counter");

const statusFlow = ["ON TIME", "BOARDING", "GATE CLOSED", "DEPARTED"];
const maxRows = 10;

// Creates one cell using createElement and textContent
function createCell(text, className) {
  const cell = document.createElement("div");
  cell.textContent = text;
  cell.className = `cell ${className}`;
  return cell;
}

// Converts status text into CSS class
function getStatusClass(status) {
  return status.toLowerCase().replaceAll(" ", "-");
}

// Creates one row fully using DOM methods
function createRow(flightObj, index) {
  const row = document.createElement("div");
  row.className = "row";
  row.dataset.index = index;

  row.appendChild(createCell(flightObj.time, "time"));
  row.appendChild(createCell(flightObj.flight, "flight"));
  row.appendChild(createCell(flightObj.dest, "dest"));
  row.appendChild(createCell(flightObj.gate, "gate"));

  const statusCell = createCell(flightObj.status, `status ${getStatusClass(flightObj.status)}`);
  row.appendChild(statusCell);

  return row;
}

// Creates header row
function createHeaderRow() {
  const header = document.createElement("div");
  header.className = "row header-row";

  header.appendChild(createCell("TIME", ""));
  header.appendChild(createCell("FLIGHT", ""));
  header.appendChild(createCell("DESTINATION", ""));
  header.appendChild(createCell("GATE", ""));
  header.appendChild(createCell("STATUS", ""));

  return header;
}

// Renders full board from data
function renderBoard() {
  board.textContent = "";

  board.appendChild(createHeaderRow());

  flights.sort((a, b) => a.time.localeCompare(b.time));

  flights.forEach((flight, index) => {
    const row = createRow(flight, index);
    board.appendChild(row);
  });

  updateCounter();
  saveToLocalStorage();
}

// Adds one new random departure
function addDeparture() {
  const destinations = ["DUBAI", "PARIS", "SINGAPORE", "ROME", "SEOUL", "MUMBAI"];
  const airlines = ["AI", "EK", "SQ", "DL", "BA", "QR"];
  const gates = ["A04", "B10", "C14", "D08", "E02"];

  const randomHour = String(Math.floor(Math.random() * 6) + 12).padStart(2, "0");
  const randomMinute = String(Math.floor(Math.random() * 60)).padStart(2, "0");

  const newFlight = {
    time: `${randomHour}:${randomMinute}`,
    flight: `${airlines[Math.floor(Math.random() * airlines.length)]} ${Math.floor(Math.random() * 9000) + 1000}`,
    dest: destinations[Math.floor(Math.random() * destinations.length)],
    gate: gates[Math.floor(Math.random() * gates.length)],
    status: "ON TIME"
  };

  flights.push(newFlight);

  if (flights.length > maxRows) {
    flights.shift();
  }

  renderBoard();
}

// Resets board to starting data
function resetBoard() {
  flights = [...startingFlights];
  renderBoard();
}

// Updates the live clock every second
function updateClock() {
  const now = new Date();

  const h = String(now.getHours()).padStart(2, "0");
  const m = String(now.getMinutes()).padStart(2, "0");
  const s = String(now.getSeconds()).padStart(2, "0");

  clock.textContent = `${h}:${m}:${s}`;
}

// Updates one status cell only
function updateRandomStatus() {
  if (flights.length === 0) return;

  const index = Math.floor(Math.random() * flights.length);
  const flight = flights[index];

  if (flight.status === "DELAYED" || flight.status === "DEPARTED") return;

  const currentIndex = statusFlow.indexOf(flight.status);
  const nextStatus = statusFlow[currentIndex + 1] || "DEPARTED";

  flight.status = nextStatus;

  const row = board.querySelector(`[data-index="${index}"]`);

  if (row) {
    const statusCell = row.querySelector(".status");

    statusCell.textContent = nextStatus;
    statusCell.className = `cell status ${getStatusClass(nextStatus)} flash`;

    setTimeout(() => {
      statusCell.classList.remove("flash");
    }, 500);
  }

  updateCounter();
  saveToLocalStorage();
}

// Updates live summary counter
function updateCounter() {
  const total = flights.length;
  const boarding = flights.filter(f => f.status === "BOARDING").length;
  const delayed = flights.filter(f => f.status === "DELAYED").length;

  counter.textContent = `${total} departures · ${boarding} boarding · ${delayed} delayed`;
}

// Saves board data
function saveToLocalStorage() {
  localStorage.setItem("departureBoard", JSON.stringify(flights));
}

// Loads saved data
function loadFromLocalStorage() {
  const saved = localStorage.getItem("departureBoard");

  if (saved) {
    flights = JSON.parse(saved);
  }
}

// Event listeners
addBtn.addEventListener("click", addDeparture);
resetBtn.addEventListener("click", resetBoard);

// Start app
loadFromLocalStorage();
renderBoard();

updateClock();
setInterval(updateClock, 1000);

setInterval(updateRandomStatus, 4000);