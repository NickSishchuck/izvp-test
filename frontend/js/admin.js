// Admin state
const AdminState = {
  token: null,
  testData: null,
  nextQuestionId: 1,
  nextOptionIds: {}, // Track next option ID per question
};

// Question types mapping
const QuestionTypes = {
  SingleChoice: 0,
  MultipleChoice: 1,
  Text: 2,
};

const QuestionTypeNames = {
  0: "Single Choice",
  1: "Multiple Choice",
  2: "Text Answer",
};

/**
 * Generate next test ID by incrementing the last segment
 */
function generateNextTestId(currentId) {
  if (!currentId || currentId === "00000000-0000-0000-0000-000000000000") {
    return "00000000-0000-0000-0000-000000000001";
  }

  // Split GUID into parts
  const parts = currentId.split("-");

  // Get last part and increment it as a hex number
  const lastPart = parts[parts.length - 1];
  const incremented = (parseInt(lastPart, 16) + 1).toString(16).padStart(
    12,
    "0",
  );

  // Replace last part with incremented value
  parts[parts.length - 1] = incremented;

  return parts.join("-");
}

/**
 * Initialize the admin panel
 */
function init() {
  // Check if already logged in (token in sessionStorage)
  const savedToken = sessionStorage.getItem("adminToken");
  if (savedToken) {
    AdminState.token = savedToken;
    showEditor();
    loadTest();
  } else {
    showLogin();
  }

  setupEventListeners();
}

/**
 * Setup event listeners
 */
function setupEventListeners() {
  // Login form
  const loginForm = document.getElementById("login-form");
  if (loginForm) {
    loginForm.addEventListener("submit", handleLogin);
  }

  // Logout button
  const logoutButton = document.getElementById("logout-button");
  if (logoutButton) {
    logoutButton.addEventListener("click", handleLogout);
  }

  // Add question button
  const addQuestionButton = document.getElementById("add-question-button");
  if (addQuestionButton) {
    addQuestionButton.addEventListener("click", handleAddQuestion);
  }

  // Save test button
  const saveTestButton = document.getElementById("save-test-button");
  if (saveTestButton) {
    saveTestButton.addEventListener("click", handleSaveTest);
  }
}

/**
 * Show login section, hide editor
 */
function showLogin() {
  document.getElementById("login-section").style.display = "flex";
  document.getElementById("editor-section").style.display = "none";
}

/**
 * Show editor section, hide login
 */
function showEditor() {
  document.getElementById("login-section").style.display = "none";
  document.getElementById("editor-section").style.display = "block";
}

/**
 * Handle login form submission
 */
async function handleLogin(e) {
  e.preventDefault();

  const username = document.getElementById("username").value.trim();
  const password = document.getElementById("password").value;

  if (!username || !password) {
    showLoginError("Please enter username and password");
    return;
  }

  showLoading(true);

  try {
    const response = await fetch("/api/admin/login", {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ username, password }),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.title || "Login failed");
    }

    const data = await response.json();
    AdminState.token = data.token;

    // Save token to sessionStorage
    sessionStorage.setItem("adminToken", data.token);

    // Show editor and load test
    showEditor();
    await loadTest();
  } catch (error) {
    console.error("Login error:", error);
    showLoginError(error.message || "Login failed. Please check credentials.");
  } finally {
    showLoading(false);
  }
}

/**
 * Handle logout
 */
function handleLogout() {
  AdminState.token = null;
  AdminState.testData = null;
  sessionStorage.removeItem("adminToken");
  showLogin();

  // Clear form
  document.getElementById("username").value = "";
  document.getElementById("password").value = "";
}

/**
 * Load test data from backend
 */
async function loadTest() {
  showLoading(true);

  try {
    const test = await API.getTest();
    AdminState.testData = test;

    // Calculate next IDs
    if (test.questions && test.questions.length > 0) {
      AdminState.nextQuestionId = Math.max(...test.questions.map((q) => q.id)) +
        1;

      test.questions.forEach((question) => {
        if (question.options && question.options.length > 0) {
          AdminState.nextOptionIds[question.id] =
            Math.max(...question.options.map((o) => o.id)) + 1;
        } else {
          AdminState.nextOptionIds[question.id] = 1;
        }
      });
    }

    renderTestEditor(test);
  } catch (error) {
    console.error("Error loading test:", error);
    showSaveError("Failed to load test data");
  } finally {
    showLoading(false);
  }
}

/**
 * Render test editor with current data
 */
function renderTestEditor(test) {
  // Set test info
  document.getElementById("test-title").value = test.title || "";
  document.getElementById("test-id").value = test.id || "";

  // Render questions
  const questionsContainer = document.getElementById("questions-container");
  questionsContainer.innerHTML = "";

  if (test.questions && test.questions.length > 0) {
    test.questions.forEach((question) => {
      renderQuestion(question, questionsContainer);
    });
  } else {
    questionsContainer.innerHTML =
      '<p class="text-muted">No questions yet. Click "Add Question" to get started.</p>';
  }
}

/**
 * Render a single question
 */
function renderQuestion(question, container) {
  const questionDiv = document.createElement("div");
  questionDiv.className = "question-item";
  questionDiv.dataset.questionId = question.id;

  // Map type enum to integer
  const typeValue = typeof question.type === "string"
    ? QuestionTypes[question.type]
    : question.type;

  questionDiv.innerHTML = `
        <div class="question-header-controls">
            <span class="question-number-badge">Question ${question.id}</span>
            <div class="question-controls">
                <button type="button" class="btn btn-danger btn-sm" onclick="removeQuestion(${question.id})">
                    Remove
                </button>
            </div>
        </div>

        <div class="question-fields">
            <div class="form-group">
                <label class="form-label">Question Text</label>
                <textarea class="form-input question-text" data-question-id="${question.id}" rows="3">${
    question.text || ""
  }</textarea>
            </div>

            <div class="form-row">
                <div class="form-group">
                    <label class="form-label">Question Type</label>
                    <select class="form-input question-type" data-question-id="${question.id}">
                        <option value="0" ${
    typeValue === 0 ? "selected" : ""
  }>Single Choice</option>
                        <option value="1" ${
    typeValue === 1 ? "selected" : ""
  }>Multiple Choice</option>
                        <option value="2" ${
    typeValue === 2 ? "selected" : ""
  }>Text Answer</option>
                    </select>
                </div>

                <div class="form-group">
                    <label class="form-label">Score Points</label>
                    <input type="number" class="form-input question-score" data-question-id="${question.id}" value="${
    question.score || 1
  }" min="1" />
                </div>
            </div>

            <div class="options-section" data-question-id="${question.id}">
                ${renderOptionsSection(question)}
            </div>
        </div>
    `;

  container.appendChild(questionDiv);

  // Add event listener for type change
  const typeSelect = questionDiv.querySelector(".question-type");
  typeSelect.addEventListener("change", (e) => {
    handleQuestionTypeChange(question.id, parseInt(e.target.value));
  });
}

/**
 * Render options section based on question type
 */
function renderOptionsSection(question) {
  const typeValue = typeof question.type === "string"
    ? QuestionTypes[question.type]
    : question.type;

  if (typeValue === 2) {
    // Text question
    return `
            <div class="form-group">
                <label class="form-label">Correct Answer</label>
                <input type="text" class="form-input correct-text-answer" data-question-id="${question.id}" value="${
      question.correctTextAnswer || ""
    }" placeholder="Enter the correct answer" />
            </div>
            <div class="text-answer-info">
              Case sensitive!
            </div>
        `;
  } else {
    // Single or Multiple choice
    const isSingle = typeValue === 0;
    return `
            <div class="form-group">
                <label class="form-label">Answer Options</label>
                <div class="options-list" data-question-id="${question.id}">
                    ${
      question.options
        ? question.options.map((opt) =>
          renderOption(question.id, opt, isSingle)
        ).join("")
        : ""
    }
                </div>
                <button type="button" class="btn btn-secondary btn-sm" onclick="addOption(${question.id})" style="margin-top: 8px;">
                    + Add Option
                </button>
            </div>
        `;
  }
}

/**
 * Render a single option
 */
function renderOption(questionId, option, isSingle) {
  const inputType = isSingle ? "radio" : "checkbox";
  const inputName = `correct-${questionId}`;

  return `
        <div class="option-item-editor" data-option-id="${option.id}">
            <input 
                type="${inputType}" 
                name="${inputName}" 
                class="option-checkbox" 
                data-question-id="${questionId}" 
                data-option-id="${option.id}"
                ${option.isCorrect ? "checked" : ""}
                title="Mark as correct answer"
            />
            <div class="option-input-wrapper">
                <input 
                    type="text" 
                    class="form-input option-input" 
                    data-question-id="${questionId}" 
                    data-option-id="${option.id}"
                    value="${option.text || ""}" 
                    placeholder="Enter option text"
                />
                <button type="button" class="btn btn-danger btn-sm" onclick="removeOption(${questionId}, ${option.id})">
                    ×
                </button>
            </div>
        </div>
    `;
}

/**
 * Handle question type change
 */
function handleQuestionTypeChange(questionId, newType) {
  const question = AdminState.testData.questions.find((q) =>
    q.id ===
      questionId
  );
  if (!question) return;

  question.type = newType;

  // Re-render options section
  const optionsSection = document.querySelector(
    `.options-section[data-question-id="${questionId}"]`,
  );
  if (optionsSection) {
    optionsSection.innerHTML = renderOptionsSection(question);
  }
}

/**
 * Add new question
 */
function handleAddQuestion() {
  const newQuestion = {
    id: AdminState.nextQuestionId++,
    text: "",
    type: 0, // Single choice by default
    score: 1,
    options: [
      { id: 1, text: "", isCorrect: true },
      { id: 2, text: "", isCorrect: false },
    ],
  };

  AdminState.nextOptionIds[newQuestion.id] = 3;

  if (!AdminState.testData.questions) {
    AdminState.testData.questions = [];
  }

  AdminState.testData.questions.push(newQuestion);

  // Re-render
  renderTestEditor(AdminState.testData);

  // Scroll to new question
  const questionsContainer = document.getElementById("questions-container");
  const newQuestionEl = questionsContainer.lastElementChild;
  if (newQuestionEl) {
    newQuestionEl.scrollIntoView({ behavior: "smooth", block: "center" });
  }
}

/**
 * Remove question
 */
function removeQuestion(questionId) {
  if (!confirm("Are you sure you want to remove this question?")) {
    return;
  }

  AdminState.testData.questions = AdminState.testData.questions.filter((q) =>
    q.id !== questionId
  );
  delete AdminState.nextOptionIds[questionId];

  renderTestEditor(AdminState.testData);
}

/**
 * Add option to question
 */
function addOption(questionId) {
  const question = AdminState.testData.questions.find((q) =>
    q.id ===
      questionId
  );
  if (!question) return;

  if (!question.options) {
    question.options = [];
  }

  if (!AdminState.nextOptionIds[questionId]) {
    AdminState.nextOptionIds[questionId] = 1;
  }

  const newOption = {
    id: AdminState.nextOptionIds[questionId]++,
    text: "",
    isCorrect: false,
  };

  question.options.push(newOption);

  // Re-render options section
  const optionsSection = document.querySelector(
    `.options-section[data-question-id="${questionId}"]`,
  );
  if (optionsSection) {
    optionsSection.innerHTML = renderOptionsSection(question);
  }
}

/**
 * Remove option from question
 */
function removeOption(questionId, optionId) {
  const question = AdminState.testData.questions.find((q) =>
    q.id ===
      questionId
  );
  if (!question) return;

  question.options = question.options.filter((o) => o.id !== optionId);

  // Re-render options section
  const optionsSection = document.querySelector(
    `.options-section[data-question-id="${questionId}"]`,
  );
  if (optionsSection) {
    optionsSection.innerHTML = renderOptionsSection(question);
  }
}

/**
 * Collect test data from form
 */
function collectTestData() {
  const testTitle = document.getElementById("test-title").value.trim();
  const testId = document.getElementById("test-id").value;

  if (!testTitle) {
    throw new Error("Test title is required");
  }

  const questions = [];

  // Collect each question
  const questionElements = document.querySelectorAll(".question-item");

  questionElements.forEach((questionEl) => {
    const questionId = parseInt(questionEl.dataset.questionId);
    const text = questionEl.querySelector(".question-text").value.trim();
    const type = parseInt(questionEl.querySelector(".question-type").value);
    const score = parseInt(questionEl.querySelector(".question-score").value);

    if (!text) {
      throw new Error(`Question ${questionId} text is required`);
    }

    const question = {
      id: questionId,
      text: text,
      type: type,
      score: score,
      options: [],
    };

    // Collect options based on type
    if (type === 2) {
      // Text question
      const correctAnswer = questionEl.querySelector(".correct-text-answer")
        .value.trim();
      if (!correctAnswer) {
        throw new Error(`Question ${questionId} must have a correct answer`);
      }
      question.correctTextAnswer = correctAnswer;
    } else {
      // Choice questions
      const optionElements = questionEl.querySelectorAll(".option-item-editor");

      if (optionElements.length === 0) {
        throw new Error(`Question ${questionId} must have at least one option`);
      }

      let hasCorrect = false;

      optionElements.forEach((optionEl) => {
        const optionId = parseInt(optionEl.dataset.optionId);
        const optionText = optionEl.querySelector(".option-input").value.trim();
        const isCorrect = optionEl.querySelector(".option-checkbox").checked;

        if (!optionText) {
          throw new Error(
            `Question ${questionId}, Option ${optionId} text is required`,
          );
        }

        if (isCorrect) {
          hasCorrect = true;
        }

        question.options.push({
          id: optionId,
          text: optionText,
          isCorrect: isCorrect,
        });
      });

      if (!hasCorrect) {
        throw new Error(
          `Question ${questionId} must have at least one correct answer`,
        );
      }

      // For single choice, ensure only one is correct
      if (type === 0) {
        const correctCount = question.options.filter((o) => o.isCorrect).length;
        if (correctCount > 1) {
          throw new Error(
            `Question ${questionId} is single choice but has multiple correct answers`,
          );
        }
      }
    }

    questions.push(question);
  });

  if (questions.length === 0) {
    throw new Error("Test must have at least one question");
  }

  return {
    id: testId,
    title: testTitle,
    questions: questions,
  };
}

/**
 * Handle save test
 */
async function handleSaveTest() {
  hideSaveMessages();

  try {
    // Collect and validate data
    const testData = collectTestData();

    // Generate new incremented test ID
    const newTestId = generateNextTestId(testData.id);
    testData.id = newTestId;

    showLoading(true);

    // Send to backend
    const response = await fetch("/api/admin/change", {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
        "X-Admin-Token": AdminState.token,
      },
      body: JSON.stringify(testData),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(errorData.title || "Failed to save test");
    }

    const result = await response.json();
    AdminState.testData = result;

    showSaveSuccess(`Test saved successfully with ID: ${newTestId}`);

    // Reload test to sync with backend
    setTimeout(() => {
      loadTest();
    }, 1000);
  } catch (error) {
    console.error("Save error:", error);
    showSaveError(error.message || "Failed to save test");
  } finally {
    showLoading(false);
  }
}

/**
 * Show/hide loading indicator
 */
function showLoading(show) {
  const loading = document.getElementById("loading");
  loading.style.display = show ? "block" : "none";
}

/**
 * Show login error
 */
function showLoginError(message) {
  const errorEl = document.getElementById("login-error");
  errorEl.textContent = message;
  errorEl.style.display = "block";

  setTimeout(() => {
    errorEl.style.display = "none";
  }, 5000);
}

/**
 * Show save success message
 */
function showSaveSuccess(message) {
  const successEl = document.getElementById("save-message");
  successEl.textContent = message;
  successEl.style.display = "block";

  setTimeout(() => {
    successEl.style.display = "none";
  }, 3000);
}

/**
 * Show save error message
 */
function showSaveError(message) {
  const errorEl = document.getElementById("save-error");
  errorEl.textContent = message;
  errorEl.style.display = "block";

  setTimeout(() => {
    errorEl.style.display = "none";
  }, 5000);
}

/**
 * Hide all save messages
 */
function hideSaveMessages() {
  document.getElementById("save-message").style.display = "none";
  document.getElementById("save-error").style.display = "none";
}

// Make functions globally accessible
window.removeQuestion = removeQuestion;
window.addOption = addOption;
window.removeOption = removeOption;

// Initialize on page load
document.addEventListener("DOMContentLoaded", init);
