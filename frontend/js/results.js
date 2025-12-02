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
    const result = API.getTestResult(); 
    if (!result) {
      showError("Результат не знайдено");
      return;
    }

    const { name, score, totalQuestions, correctAnswers } = result;
    const percentage = Math.round((score / totalQuestions) * 100);

    displayGreeting(name, percentage);
    displayScore(score, totalQuestions);
    displayStats({ correctAnswers, percentage });

  } catch (error) {
    console.error("Error loading results:", error);
    showError("Помилка завантаження результатів");
  }
}

// TODO: Display personalized greeting based on score percentage
function displayGreeting(name, percentage) {
  const greetingEl = document.getElementById("results-greeting");
  let greeting;

  if (percentage >= 90) {
    greeting = `Вітаємо, ${name}! Ви блискуче пройшли тест!`;
  } else if (percentage >= 70) {
    greeting = `Добра робота, ${name}! Ви добре справились!`;
  } else {
    greeting = `Дякуємо, ${name}! Ви завершили тест.`;
  }

  greetingEl.textContent = greeting;
}

// TODO: Display score in format "score/totalScore"
function displayScore(score, totalScore) {
  const scoreEl = document.getElementById("score-value");
  scoreEl.textContent = `${score} / ${totalScore}`;
}

// TODO: Display statistics (correct answers count and percentage)
function displayStats({ correctAnswers, percentage }) {
  const statsEl = document.getElementById("stats-details");
  statsEl.innerHTML = `
    <p>Правильних відповідей: ${correctAnswers}</p>
    <p>Відсоток правильних: ${percentage}%</p>
  `;
}

// TODO: Display error in #error-message element
function showError(message) {
  const errorEl = document.getElementById("error-message");
  errorEl.style.display = "block";
  errorEl.textContent = message;
}

// TODO: Initialize page - load results and setup event listeners
function init() {
  loadResults();
}

document.addEventListener("DOMContentLoaded", init);
