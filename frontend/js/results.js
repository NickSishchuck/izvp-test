/**
 * INTEGRATION CONTRACT
 *
 * REQUIRED INPUTS:
 * - API.getTestResult() returns: { name, score, totalScore, correctAnswers, totalQuestions }
 * - API.getUserName() returns: string
 *
 * REQUIRED OUTPUTS:
 * - Must update DOM: #results-greeting (personalized message)
 * - Must update DOM: #score-value (format: "15/30")
 * - Must update DOM: #stats-details (correct answers, percentage)
 * - Must call: API.clearAllData() before retry
 * - Must navigate to: 'index.html' on retry
 * - If no result data exists: redirect to 'index.html'
 */

// TODO: Load result data, display greeting, score, and stats
function loadResults() {
  try {
    // Your implementation here
  } catch (error) {
    console.error("Error loading results:", error);
    showError("Помилка завантаження результатів");
  }
}

// TODO: Display personalized greeting based on score percentage
function displayGreeting(name, score, totalScore) {
  // Your implementation here
}

// TODO: Display score in format "score/totalScore"
function displayScore(score, totalScore) {
  // Your implementation here
}

// TODO: Display statistics (correct answers count and percentage)
function displayStats(result) {
  // Your implementation here
}

// TODO: Clear all data and navigate to index.html
function handleRetry() {
  // Your implementation here
}

// TODO: Display error in #error-message element
function showError(message) {
  // Your implementation here
}

// TODO: Initialize page - load results and setup event listeners
function init() {
  // Your implementation here
}

document.addEventListener("DOMContentLoaded", init);
