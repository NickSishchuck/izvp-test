// This file handles the greeting/landing page functionality

const API_URL = "/api";

/**
 * TODO: Load test information from the API when page loads
 *
 * Steps:
 * 1. Fetch test data from GET /api/test
 * 2. Extract the test title from the response
 * 3. Display the title in the page (update #test-title-dynamic element)
 * 4. Store the full test data in sessionStorage for later use
 * 5. Handle any errors that occur during fetch
 *
 * API Response format:
 * {
 *   "title": "Тест з C# для початківців",
 *   "questions": [...]
 * }
 */
async function loadTestInfo() {
  // HINT: Show loading indicator
  // const loading = document.getElementById('loading');
  // loading.style.display = 'block';

  try {
    // TODO: Fetch the test from the API
    // const response = await fetch(`${API_URL}/test`);

    // TODO: Check if response is OK
    // if (!response.ok) {
    //     throw new Error('Failed to load test');
    // }

    // TODO: Parse the JSON response
    // const data = await response.json();

    // TODO: Update the page with test title
    // Example: Extract a keyword from title or use full title
    // const titleElement = document.getElementById('test-title-dynamic');
    // titleElement.textContent = data.title;

    // TODO: Store test data in sessionStorage for use in test.html
    // sessionStorage.setItem('testData', JSON.stringify(data));
  } catch (error) {
    // TODO: Handle error - show error message to user
    // console.error('Error loading test:', error);
    // showError('Не вдалося завантажити тест. Спробуйте пізніше.');
  } finally {
    // HINT: Hide loading indicator
    // loading.style.display = 'none';
  }
}

/**
 * TODO: Handle the start button click
 *
 * Steps:
 * 1. Get the user's name from the input field
 * 2. Validate that name is not empty (trim whitespace)
 * 3. If invalid, show error message and stop
 * 4. If valid, store name in sessionStorage
 * 5. Navigate to test.html
 */
function handleStartTest() {
  // TODO: Get name input element
  // const nameInput = document.getElementById('user-name');
  // const name = nameInput.value.trim();

  // TODO: Validate name
  // if (!name) {
  //     showError('Будь ласка, введіть ваше ім\'я');
  //     nameInput.focus();
  //     return;
  // }

  // TODO: Store name in sessionStorage
  // sessionStorage.setItem('userName', name);

  // TODO: Navigate to test page
  // window.location.href = 'test.html';
}

/**
 * TODO: Show error message to user
 *
 * @param {string} message - The error message to display
 */
function showError(message) {
  // TODO: Get error message element
  // const errorElement = document.getElementById('error-message');

  // TODO: Set message text
  // errorElement.textContent = message;

  // TODO: Show error element
  // errorElement.style.display = 'block';

  // OPTIONAL: Auto-hide after 5 seconds
  // setTimeout(() => {
  //     errorElement.style.display = 'none';
  // }, 5000);
}

/**
 * TODO: Initialize the page when DOM is ready
 *
 * Steps:
 * 1. Load test information from API
 * 2. Add event listener to start button
 * 3. Optional: Add event listener to name input for Enter key
 */
function init() {
  // TODO: Load test info when page loads
  // loadTestInfo();

  // TODO: Add click event listener to start button
  // const startButton = document.getElementById('start-button');
  // startButton.addEventListener('click', handleStartTest);

  // OPTIONAL: Allow Enter key in name input to start test
  // const nameInput = document.getElementById('user-name');
  // nameInput.addEventListener('keypress', (event) => {
  //     if (event.key === 'Enter') {
  //         handleStartTest();
  //     }
  // });
}

// Initialize when DOM is fully loaded
document.addEventListener("DOMContentLoaded", init);

// ============================================================================
// CODE SNIPPETS FOR REFERENCE
// ============================================================================

/*
// Example: Fetch data from API
async function fetchExample() {
    const response = await fetch(`${API_URL}/test`);
    if (!response.ok) {
        throw new Error('Network response was not ok');
    }
    const data = await response.json();
    return data;
}

// Example: Store data in sessionStorage
sessionStorage.setItem('key', 'value');
sessionStorage.setItem('objectKey', JSON.stringify({name: 'John'}));

// Example: Get data from sessionStorage
const value = sessionStorage.getItem('key');
const object = JSON.parse(sessionStorage.getItem('objectKey'));

// Example: Navigate to another page
window.location.href = 'test.html';

// Example: Show/hide elements
element.style.display = 'block';  // Show
element.style.display = 'none';   // Hide

// Example: Validate input
const input = document.getElementById('my-input');
const value = input.value.trim();
if (!value) {
    alert('Please enter a value');
    input.focus();
    return;
}
*/
