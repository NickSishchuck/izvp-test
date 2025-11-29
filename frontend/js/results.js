// This file handles the results page functionality
// The API object is available globally from api.js

/**
 * TODO: Load and display test results when page loads
 *
 * Steps:
 * 1. Use API.getTestResult() to get the saved result
 * 2. Use API.getUserName() to get the user's name
 * 3. If no result found, redirect back to index.html
 * 4. Display personalized greeting with user's name
 * 5. Display score (e.g., "15/30")
 * 6. Display additional statistics
 *
 * HINT: The API object provides:
 * - API.getTestResult() - Returns saved result object
 * - API.getUserName() - Returns user's name
 *
 * Expected result object format:
 * {
 *   "name": "Maria",
 *   "score": 5,
 *   "totalScore": 5,
 *   "correctAnswers": 3,
 *   "totalQuestions": 3
 * }
 */
function loadResults() {
  try {
    // TODO: Get result from sessionStorage using API helper
    // const result = API.getTestResult();

    // TODO: Check if result exists
    // if (!result) {
    //     // No result found, redirect to start
    //     window.location.href = 'index.html';
    //     return;
    // }

    // TODO: Display personalized greeting

    // TODO: Display score

    // TODO: Display statistics

    console.log("Results loaded successfully!");
  } catch (error) {
    console.error("Error loading results:", error);
    showError("Помилка завантаження результатів");
  }
}

/**
 * TODO: Display personalized greeting message
 *
 * @param {string} name - User's name
 * @param {number} score - Points scored
 * @param {number} totalScore - Total possible points
 *
 * Examples based on performance:
 * - 100%: "{NAME}, YOU ARE THE BEST!"
 * - 80-99%: "GREAT JOB, {NAME}"
 * - 60-79%: "GOOD TRY, {NAME}"
 * - <60%: "KEEP LEARNING, {NAME}"
 */
function displayGreeting(name, score, totalScore) {
  // TODO: Get greeting element
  // const greetingElement = document.getElementById('results-greeting');

  // TODO: Calculate performance percentage
  // const percentage = (score / totalScore) * 100;

  // TODO: Create personalized message based on performance

  // TODO: Update greeting element
  // greetingElement.textContent = message;
}

/**
 * TODO: Display the score
 *
 * @param {number} score - Points scored
 * @param {number} totalScore - Total possible points
 *
 * Should display as: "15/30" format
 */
function displayScore(score, totalScore) {
  // TODO: Get score element
  // const scoreElement = document.getElementById('score-value');

  // TODO: Format and display score as "score/totalScore"
  // scoreElement.textContent = `${score}/${totalScore}`;
}

/**
 * TODO: Display additional statistics
 *
 * @param {Object} result - Full result object
 *
 * Should show:
 * - Correct answers count (e.g., "3 з 3")
 * - Percentage (e.g., "100.0%")
 */
function displayStats(result) {
  // TODO: Get stats element
  // const statsElement = document.getElementById('stats-details');

  // TODO: Calculate percentage
  // const percentage = ((result.score / result.totalScore) * 100).toFixed(1);

  // TODO: Create stats HTML
  // const statsHTML = `
  //     <p>Правильних відповідей: ${result.correctAnswers} з ${result.totalQuestions}</p>
  //     <p>Відсоток: ${percentage}%</p>
  // `;

  // TODO: Update stats element
  // statsElement.innerHTML = statsHTML;
}

/**
 * TODO: Handle retry button click
 *
 * Steps:
 * 1. Use API.clearAllData() to remove all stored data
 * 2. Navigate back to index.html
 *
 * HINT: The API object provides:
 * - API.clearAllData() - Clears all sessionStorage data
 */
function handleRetry() {
  // TODO: Clear all stored data using API helper
  // API.clearAllData();

  // TODO: Navigate to start page
  // window.location.href = 'index.html';
}

/**
 * TODO: Show error message to user
 *
 * @param {string} message - The error message to display
 *
 * Steps:
 * 1. Get error element (#error-message)
 * 2. Set message text
 * 3. Show element (display: block)
 */
function showError(message) {
  // TODO: Get error message element
  // const errorElement = document.getElementById('error-message');

  // TODO: Set message text
  // errorElement.textContent = message;

  // TODO: Show error element
  // errorElement.style.display = 'block';
}

/**
 * TODO: Initialize the page when DOM is ready
 *
 * Steps:
 * 1. Load and display results
 * 2. Add event listener to retry button
 */
function init() {
  // TODO: Load results when page loads
  // loadResults();

  // TODO: Add click event listener to retry button
  // const retryButton = document.getElementById('retry-button');
  // retryButton.addEventListener('click', handleRetry);
}

document.addEventListener("DOMContentLoaded", init);
