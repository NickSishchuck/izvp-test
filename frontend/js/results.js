/**
 * Load result data and display
 */
function loadResults() {
  try {
    const result = API.getTestResult();
    const userName = API.getUserName();

    if (!result) {
      console.error("No result found");
      window.location.href = "index.html";
      return;
    }

    const { correctAnswers, totalQuestions, score, totalScore } = result;
    const percentage = Math.round((correctAnswers / totalQuestions) * 100);

    displayGreeting(userName || "User", percentage);
    displayScore(score, totalScore);
    displayStats({ correctAnswers, totalQuestions, percentage });
  } catch (error) {
    console.error("Error loading results:", error);
    showError("Error loading results");
  }
}

/**
 * Display personalized greeting based on score percentage
 */
function displayGreeting(name, percentage) {
  const greetingEl = document.getElementById("results-greeting");
  let greeting;

  if (percentage >= 90) {
    greeting = `Congratulations, ${name}! You passed the test brilliantly!`;
  } else if (percentage >= 70) {
    greeting = `Great job, ${name}! You did well!`;
  } else if (percentage >= 50) {
    greeting = `Good effort, ${name}! Keep practicing!`;
  } else {
    greeting = `Thank you for participating, ${name}!`;
  }

  greetingEl.textContent = greeting;
}

/**
 * Display score in format "score / totalScore"
 */
function displayScore(score, totalScore) {
  const scoreEl = document.getElementById("score-value");
  scoreEl.textContent = `${score} / ${totalScore}`;
}

/**
 * Display statistics (correct answers count and percentage)
 */
function displayStats({ correctAnswers, totalQuestions, percentage }) {
  const statsEl = document.getElementById("stats-details");
  statsEl.innerHTML = `
    <p>Correct answers: ${correctAnswers} out of ${totalQuestions}</p>
    <p>Accuracy: ${percentage}%</p>
  `;
}

/**
 * Display error message
 */
function showError(message) {
  const errorEl = document.getElementById("error-message");
  errorEl.style.display = "block";
  errorEl.textContent = message;
}

/**
 * Initialize page
 */
function init() {
  loadResults();
}

document.addEventListener("DOMContentLoaded", init);
