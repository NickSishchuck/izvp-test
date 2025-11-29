// This file handles the results page functionality

/**
 * TODO: Load and display test results when page loads
 *
 * Steps:
 * 1. Get test result from sessionStorage
 * 2. Get user name from sessionStorage
 * 3. If no result found, redirect back to index.html
 * 4. Display personalized greeting with user's name
 * 5. Display score (e.g., "15/30")
 * 6. Display additional statistics (correct answers, percentage, etc.)
 *
 * Result object format (from API):
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
    // TODO: Get result from sessionStorage
    // const resultData = sessionStorage.getItem('testResult');

    // TODO: Check if result exists
    // if (!resultData) {
    //     // No result found, redirect to start
    //     window.location.href = 'index.html';
    //     return;
    // }

    // TODO: Parse result JSON
    // const result = JSON.parse(resultData);

    // TODO: Display personalized greeting
    // displayGreeting(result.name, result.score, result.totalScore);

    // TODO: Display score
    // displayScore(result.score, result.totalScore);

    // TODO: Display statistics
    // displayStats(result);
  } catch (error) {
    // TODO: Handle error
    // console.error('Error loading results:', error);
    // showError('Помилка завантаження результатів');
  }
}

/**
 * TODO: Display personalized greeting message
 *
 * @param {string} name - User's name
 * @param {number} score - Points scored
 * @param {number} totalScore - Total possible points
 *
 * Examples:
 * - If score is perfect: "MARIA YOU ARE THE BEST!"
 * - If score is good: "GREAT JOB, MARIA!"
 * - If score is okay: "GOOD TRY, MARIA!"
 */
function displayGreeting(name, score, totalScore) {
  // TODO: Get greeting element
  // const greetingElement = document.getElementById('results-greeting');

  // TODO: Calculate performance
  // const percentage = (score / totalScore) * 100;

  // TODO: Create personalized message based on performance
  // let message = '';
  // if (percentage === 100) {
  //     message = `${name.toUpperCase()} YOU ARE THE BEST!`;
  // } else if (percentage >= 80) {
  //     message = `GREAT JOB, ${name.toUpperCase()}!`;
  // } else if (percentage >= 60) {
  //     message = `GOOD TRY, ${name.toUpperCase()}!`;
  // } else {
  //     message = `KEEP LEARNING, ${name.toUpperCase()}!`;
  // }

  // TODO: Update greeting element
  // greetingElement.textContent = message;
}

/**
 * TODO: Display the score
 *
 * @param {number} score - Points scored
 * @param {number} totalScore - Total possible points
 */
function displayScore(score, totalScore) {
  // TODO: Get score element
  // const scoreElement = document.getElementById('score-value');

  // TODO: Format and display score
  // scoreElement.textContent = `${score}/${totalScore}`;
}

/**
 * TODO: Display additional statistics
 *
 * @param {Object} result - Full result object
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
 * 1. Clear sessionStorage (remove old test data and results)
 * 2. Navigate back to index.html to start fresh
 */
function handleRetry() {
  // TODO: Clear stored data
  // sessionStorage.removeItem('testResult');
  // sessionStorage.removeItem('testData');
  // sessionStorage.removeItem('userName');

  // TODO: Navigate to start page
  // window.location.href = 'index.html';
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

// Initialize when DOM is fully loaded
document.addEventListener("DOMContentLoaded", init);

// ============================================================================
// CODE SNIPPETS FOR REFERENCE
// ============================================================================

/*
// Example: Get data from sessionStorage
const data = sessionStorage.getItem('key');
const object = JSON.parse(sessionStorage.getItem('objectKey'));

// Example: Remove data from sessionStorage
sessionStorage.removeItem('key');
sessionStorage.clear(); // Clear all

// Example: Check if data exists
if (!sessionStorage.getItem('key')) {
    // Data doesn't exist
    window.location.href = 'index.html';
}

// Example: Calculate percentage
const percentage = (score / total * 100).toFixed(1); // One decimal place

// Example: String manipulation
const name = "maria";
const upperName = name.toUpperCase(); // "MARIA"

// Example: Conditional messages
let message;
if (percentage === 100) {
    message = "Perfect!";
} else if (percentage >= 80) {
    message = "Great!";
} else {
    message = "Good try!";
}

// Example: Update element content
element.textContent = "Text only";
element.innerHTML = "<p>HTML content</p>";
*/
