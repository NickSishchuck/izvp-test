// This file handles the greeting/landing page functionality
// The API object is available globally from api.js

/**
 * TODO: Load test information from the API when page loads
 *
 * Steps:
 * 1. Use API.getTest() to fetch test data (already implemented)
 * 2. Extract the test title from the response
 * 3. Update the page to display the title (element: #test-title-dynamic)
 * 4. Use API.storeTestData() to save the full test data for later
 * 5. Handle any errors with try/catch
 *
 * HINT: The API object provides:
 * - API.getTest() - Returns promise with test data
 * - API.storeTestData(data) - Saves test data to sessionStorage
 *
 * Expected test data format:
 * {
 *   "title": "Тест з C#",
 *   "questions": [...]
 * }
 */
async function loadTestInfo() {
  const loading = document.getElementById("loading");
  loading.style.display = "block";

  try {
    // TODO: Call API.getTest() to fetch test data
    // const testData = await API.getTest();

    // TODO: Extract title from testData

    // TODO: Store the complete test data using API helper
    // API.storeTestData(testData);

    console.log("Test loaded successfully!");
  } catch (error) {
    // Show error message to user
    console.error("Error loading test:", error);
    showError("Не вдалося завантажити тест. Спробуйте пізніше.");
  } finally {
    loading.style.display = "none";
  }
}

/**
 * TODO: Handle the start button click
 *
 * Steps:
 * 1. Get the user's name from input field (#user-name)
 * 2. Validate that name is not empty (use .trim() to remove spaces)
 * 3. If invalid, show error and focus on input field
 * 4. If valid, use API.storeUserName() to save the name
 * 5. Navigate to test.html
 *
 * HINT: The API object provides:
 * - API.storeUserName(name) - Saves name to sessionStorage
 */
function handleStartTest() {
  // TODO: Get the name input element
  // const nameInput = document.getElementById('user-name');
  // const name = nameInput.value.trim();

  // TODO: Validate that name is not empty
  // if (!name) {
  //     showError('Будь ласка, введіть ваше ім\'я');
  //     nameInput.focus();
  //     return;
  // }

  // TODO: Store name using API helper
  // API.storeUserName(name);

  // TODO: Navigate to test page
  // window.location.href = 'test.html';
}

/**
 * TODO: Show error message to user
 *
 * @param {string} message - The error message to display
 *
 * Steps:
 * 1. Get the error message element (#error-message)
 * 2. Set the message text
 * 3. Show the element (display: block)
 */
function showError(message) {
  // TODO: Get error element
  // const errorElement = document.getElementById('error-message');

  // TODO: Set and show error message
  // errorElement.textContent = message;
  // errorElement.style.display = 'block';
}

/**
 * TODO: Initialize the page when DOM is ready
 *
 * Steps:
 * 1. Call loadTestInfo() to fetch and display test data
 * 2. Add click event listener to start button
 */
function init() {
  // TODO: Load test information
  // loadTestInfo();

  // TODO: Add event listener to start button
  // const startButton = document.getElementById('start-button');
  // startButton.addEventListener('click', handleStartTest);
}

// Initialize when DOM is fully loaded
document.addEventListener("DOMContentLoaded", init);

// QUICK REFERENCE - API FUNCTIONS AVAILABLE

/*
The API object (from api.js) provides these functions:

// Fetch test data from backend
await API.getTest()
// Returns: { title: "...", questions: [...] }

// Storage helpers
API.storeUserName(name)   // Save user's name
API.getUserName()   // Get user's name
API.storeTestData(testData)   // Save complete test
API.getTestData()   // Get saved test
API.storeTestResult(result)   // Save test result
API.getTestResult()   // Get test result
API.clearAllData()    // Clear everything
*/
