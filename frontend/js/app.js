const API_URL = "/api";

let currentTest = null;
let answers = {};

document.addEventListener("DOMContentLoaded", () => {
  loadTest();
});

async function loadTest() {
  try {
    const response = await fetch(`${API_URL}/test`);

    if (!response.ok) {
      throw new Error("Failed to load test");
    }

    currentTest = await response.json();
    renderTest(currentTest);
  } catch (error) {
    console.error("Error loading test:", error);
    showError("Не вдалося завантажити тест. Перевірте підключення до API.");
  }
}

function renderTest(test) {
  const app = document.getElementById("app");

  let html = `
        <h2>${test.title}</h2>
        <form id="testForm">
            <div id="username-section">
                <label for="username">Ваше ім'я:</label>
                <input type="text" id="username" name="username" required>
            </div>
    `;

  test.questions.forEach((question) => {
    html += renderQuestion(question);
  });

  html += `
            <button type="submit">Надіслати відповіді</button>
        </form>
    `;

  app.innerHTML = html;

  // Add form submit handler
  document.getElementById("testForm").addEventListener("submit", handleSubmit);
}

// Render individual question
function renderQuestion(question) {
  let html = `
        <div class="question" data-question-id="${question.id}">
            <h3>Питання ${question.id}: ${question.text}</h3>
            <p><small>Бали: ${question.score}</small></p>
    `;

  if (question.type === "SingleChoice") {
    html += renderSingleChoice(question);
  } else if (question.type === "MultipleChoice") {
    html += renderMultipleChoice(question);
  } else if (question.type === "Text") {
    html += renderTextAnswer(question);
  }

  html += `</div>`;
  return html;
}

function renderSingleChoice(question) {
  let html = '<div class="options">';

  question.options.forEach((option) => {
    html += `
            <div class="option">
                <label>
                    <input type="radio" name="question-${question.id}" value="${option.id}">
                    ${option.text}
                </label>
            </div>
        `;
  });

  html += "</div>";
  return html;
}

function renderMultipleChoice(question) {
  let html = '<div class="options">';

  question.options.forEach((option) => {
    html += `
            <div class="option">
                <label>
                    <input type="checkbox" name="question-${question.id}" value="${option.id}">
                    ${option.text}
                </label>
            </div>
        `;
  });

  html += "</div>";
  return html;
}

function renderTextAnswer(question) {
  return `
        <div class="options">
            <input type="text" 
                   id="text-${question.id}" 
                   name="question-${question.id}" 
                   placeholder="Введіть вашу відповідь">
        </div>
    `;
}

async function handleSubmit(event) {
  event.preventDefault();

  const username = document.getElementById("username").value.trim();

  if (!username) {
    alert("Будь ласка, введіть ваше ім'я");
    return;
  }

  const answers = collectAnswers();

  const submitData = {
    title: currentTest.title,
    userName: username,
    answers: answers,
  };

  console.log("Submitting:", submitData);

  try {
    const response = await fetch(`${API_URL}/test/submit`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(submitData),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.title || "Failed to submit test");
    }

    const result = await response.json();
    showResult(result);
  } catch (error) {
    console.error("Error submitting test:", error);
    showError("Помилка при відправці тесту: " + error.message);
  }
}

function collectAnswers() {
  const answers = [];

  currentTest.questions.forEach((question) => {
    const answer = {
      id: question.id,
      selectedOptionIds: [],
      textAnswer: null,
    };

    if (question.type === "SingleChoice") {
      const selected = document.querySelector(
        `input[name="question-${question.id}"]:checked`,
      );
      if (selected) {
        answer.selectedOptionIds = [parseInt(selected.value)];
      }
    } else if (question.type === "MultipleChoice") {
      const selected = document.querySelectorAll(
        `input[name="question-${question.id}"]:checked`,
      );
      answer.selectedOptionIds = Array.from(selected).map((el) =>
        parseInt(el.value)
      );
    } else if (question.type === "Text") {
      const textInput = document.getElementById(`text-${question.id}`);
      answer.textAnswer = textInput.value.trim();
    }

    answers.push(answer);
  });

  return answers;
}

function showResult(result) {
  const app = document.getElementById("app");

  const percentage = (result.score / result.totalScore * 100).toFixed(1);

  app.innerHTML = `
        <div class="result">
            <h2>🎉 Тест завершено!</h2>
            <div class="score">${result.score} / ${result.totalScore}</div>
            <p>Правильних відповідей: ${result.correctAnswers} з ${result.totalQuestions}</p>
            <p>Відсоток: ${percentage}%</p>
            <button onclick="location.reload()">Пройти ще раз</button>
        </div>
    `;
}

function showError(message) {
  const app = document.getElementById("app");
  app.innerHTML = `
        <div class="error">
            <strong>Помилка:</strong> ${message}
        </div>
        <button onclick="location.reload()">Спробувати знову</button>
    `;
}
