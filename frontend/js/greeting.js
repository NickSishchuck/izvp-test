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
  loading.style.display = "block";

  try {
    // Your implementation here
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
  // Your implementation here
}

// TODO: Initialize page - load test info and setup event listeners
function init() {
  // Your implementation here
}

document.addEventListener("DOMContentLoaded", init);
