/**
 * INTEGRATION CONTRACT
 *
 * REQUIRED OUTPUTS:
 * - Must call: API.storeUserName(name) before navigating
 * - Must call: API.storeTestData(testData) after fetching
 * - Must navigate to: 'test.html' when user starts test
 * - Must update DOM: #test-title-dynamic with test title from API
 *
 * REQUIRED INPUTS:
 * - API.getTest() returns: { title: string, questions: array }
 * - DOM element: #user-name (input field)
 * - DOM element: #start-button (button)
 */

// TODO: Fetch test from API, update #test-title-dynamic, store test data
async function loadTestInfo() {
  const loading = document.getElementById("loading");
  const titleElement = document.getElementById("test-title-dynamic");

  loading.style.display = "block";

  try {
    const test = await API.getTest(); // вызов из api.js
    titleElement.textContent = test.title || "Назва тесту"; // если заголовок не пришёл
  } catch (error) {
    console.error("Error loading test:", error);
    showError("Не вдалося завантажити тест. Спробуйте пізніше.");
  } finally {
    loading.style.display = "none";
  }
}

// TODO: Validate name input, store name, navigate to test.html
function handleStartTest() {
  // Your implementation here
}

// TODO: Display error message in #error-message element
function showError(message) {
  const errorElement = document.getElementById("error-message");
  errorElement.textContent = message;
  errorElement.style.display = "block";
}

function init() {
  loadTestInfo(); // загружаем инфо о тесте
  document.getElementById("start-button").addEventListener("click", handleStartTest);
}

document.addEventListener("DOMContentLoaded", init);
