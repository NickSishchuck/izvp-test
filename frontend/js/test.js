/**
 * Test state management
 */
const TestState = {
  testData: null, // Full test object from API
  userName: null, // User's name from sessionStorage
  currentQuestionIndex: 0, // Current question (0-based)
  userAnswers: {}, // Store user answers: { questionId: answer }
};

/**
 * Initialize the test page
 */
function init() {
  // Load test data and user name from sessionStorage
  TestState.testData = API.getTestData();
  TestState.userName = API.getUserName();

  // Validate that we have the required data
  if (!TestState.testData || !TestState.userName) {
    // Missing data, redirect back to start
    console.error("Missing test data or user name");
    window.location.href = "index.html";
    return;
  }

  // Initialize user answers storage
  TestState.testData.questions.forEach((question) => {
    TestState.userAnswers[question.id] = null;
  });

  // Render the first question
  renderQuestion(TestState.currentQuestionIndex);

  // Setup event listeners
  setupEventListeners();
}

/**
 * Setup event listeners for navigation buttons
 */
function setupEventListeners() {
  const prevButton = document.getElementById("prev-button");
  const nextButton = document.getElementById("next-button");
  const submitButton = document.getElementById("submit-button");

  prevButton.addEventListener("click", handlePrevious);
  nextButton.addEventListener("click", handleNext);
  submitButton.addEventListener("click", handleSubmit);
}

/**
 * Render a question by index
 * @param {number} index - Question index (0-based)
 */
function renderQuestion(index) {
  const questions = TestState.testData.questions;

  // Validate index
  if (index < 0 || index >= questions.length) {
    console.error("Invalid question index:", index);
    return;
  }

  const question = questions[index];

  // Update progress
  updateProgress(index + 1, questions.length);

  // Update question number and text
  document.getElementById("question-number").textContent = `${index + 1}.`;
  document.getElementById("question-text").textContent = question.text;

  // Render options based on question type
  const optionsContainer = document.getElementById("question-options");

  switch (question.type) {
    case "SingleChoice":
      renderSingleChoice(question, optionsContainer);
      break;
    case "MultipleChoice":
      renderMultipleChoice(question, optionsContainer);
      break;
    case "Text":
      renderTextInput(question, optionsContainer);
      break;
    default:
      console.error("Unknown question type:", question.type);
  }

  // Update navigation buttons visibility
  updateNavigationButtons(index, questions.length);

  // Restore previous answer if exists
  restorePreviousAnswer(question);
}

/**
 * Render single choice question (radio buttons)
 */
function renderSingleChoice(question, container) {
  container.innerHTML = "";
  container.className = "question-options options-single";

  question.options.forEach((option) => {
    const optionDiv = document.createElement("div");
    optionDiv.className = "option-item";

    const radio = document.createElement("input");
    radio.type = "radio";
    radio.name = `question-${question.id}`;
    radio.value = option.id;
    radio.id = `option-${option.id}`;
    radio.className = "option-input";

    const label = document.createElement("label");
    label.htmlFor = `option-${option.id}`;
    label.className = "option-label";
    label.textContent = option.text;

    optionDiv.appendChild(radio);
    optionDiv.appendChild(label);
    container.appendChild(optionDiv);
  });
}

/**
 * Render multiple choice question (checkboxes)
 */
function renderMultipleChoice(question, container) {
  container.innerHTML = "";
  container.className = "question-options options-multiple";

  question.options.forEach((option) => {
    const optionDiv = document.createElement("div");
    optionDiv.className = "option-item";

    const checkbox = document.createElement("input");
    checkbox.type = "checkbox";
    checkbox.name = `question-${question.id}`;
    checkbox.value = option.id;
    checkbox.id = `option-${option.id}`;
    checkbox.className = "option-input";

    const label = document.createElement("label");
    label.htmlFor = `option-${option.id}`;
    label.className = "option-label";
    label.textContent = option.text;

    optionDiv.appendChild(checkbox);
    optionDiv.appendChild(label);
    container.appendChild(optionDiv);
  });
}

/**
 * Render text input question
 */
function renderTextInput(question, container) {
  container.innerHTML = "";
  container.className = "question-options options-text";

  const textInput = document.createElement("textarea");
  textInput.id = `text-answer-${question.id}`;
  textInput.className = "text-answer-input";
  textInput.placeholder = "Ваша відповідь...";
  textInput.rows = 4;

  container.appendChild(textInput);
}

/**
 * Update progress bar and text
 */
function updateProgress(current, total) {
  const progressFill = document.getElementById("progress-fill");
  const progressText = document.getElementById("progress-text");

  const percentage = (current / total) * 100;
  progressFill.style.width = `${percentage}%`;
  progressText.textContent = `Питання ${current} з ${total}`;
}

/**
 * Update navigation buttons visibility
 */
function updateNavigationButtons(index, totalQuestions) {
  const prevButton = document.getElementById("prev-button");
  const nextButton = document.getElementById("next-button");
  const submitButton = document.getElementById("submit-button");

  // Show/hide previous button
  prevButton.style.display = index > 0 ? "block" : "none";

  // Show/hide next vs submit button
  if (index < totalQuestions - 1) {
    nextButton.style.display = "block";
    submitButton.style.display = "none";
  } else {
    nextButton.style.display = "none";
    submitButton.style.display = "block";
  }
}

/**
 * Restore previously saved answer for a question
 */
function restorePreviousAnswer(question) {
  const savedAnswer = TestState.userAnswers[question.id];

  if (!savedAnswer) return;

  switch (question.type) {
    case "SingleChoice":
      const radio = document.querySelector(
        `input[name="question-${question.id}"][value="${savedAnswer}"]`,
      );
      if (radio) radio.checked = true;
      break;

    case "MultipleChoice":
      savedAnswer.forEach((optionId) => {
        const checkbox = document.querySelector(
          `input[name="question-${question.id}"][value="${optionId}"]`,
        );
        if (checkbox) checkbox.checked = true;
      });
      break;

    case "Text":
      const textInput = document.getElementById(`text-answer-${question.id}`);
      if (textInput) textInput.value = savedAnswer;
      break;
  }
}

/**
 * Save current question answer
 */
function saveCurrentAnswer() {
  const question = TestState.testData.questions[TestState.currentQuestionIndex];
  let answer = null;

  switch (question.type) {
    case "SingleChoice":
      const selectedRadio = document.querySelector(
        `input[name="question-${question.id}"]:checked`,
      );
      answer = selectedRadio ? parseInt(selectedRadio.value) : null;
      break;

    case "MultipleChoice":
      const selectedCheckboxes = document.querySelectorAll(
        `input[name="question-${question.id}"]:checked`,
      );
      answer = Array.from(selectedCheckboxes).map((cb) => parseInt(cb.value));
      if (answer.length === 0) answer = null;
      break;

    case "Text":
      const textInput = document.getElementById(`text-answer-${question.id}`);
      answer = textInput ? textInput.value.trim() : null;
      if (answer === "") answer = null;
      break;
  }

  TestState.userAnswers[question.id] = answer;
}

/**
 * Validate that current question is answered
 */
function validateCurrentAnswer() {
  const question = TestState.testData.questions[TestState.currentQuestionIndex];
  const answer = TestState.userAnswers[question.id];

  if (answer === null || (Array.isArray(answer) && answer.length === 0)) {
    showError("Це питання вимагає відповіді");
    return false;
  }

  return true;
}

/**
 * Handle previous button click
 */
function handlePrevious() {
  saveCurrentAnswer();
  TestState.currentQuestionIndex--;
  hideError();
  renderQuestion(TestState.currentQuestionIndex);
}

/**
 * Handle next button click
 */
function handleNext() {
  // Save current answer
  saveCurrentAnswer();

  // Validate answer
  if (!validateCurrentAnswer()) {
    return;
  }

  // Hide error if shown
  hideError();

  // Move to next question
  TestState.currentQuestionIndex++;

  // Render next question
  renderQuestion(TestState.currentQuestionIndex);
}

/**
 * Handle submit button click - submit to backend
 */
async function handleSubmit() {
  // Save current answer
  saveCurrentAnswer();

  // Validate answer
  if (!validateCurrentAnswer()) {
    return;
  }

  // Show loading
  const loading = document.getElementById("loading");
  const submitButton = document.getElementById("submit-button");

  loading.style.display = "block";
  submitButton.disabled = true;

  try {
    // Format answers for backend API
    const formattedAnswers = formatAnswersForBackend();

    // Submit to backend
    const testSubmission = {
      testId: TestState.testData.id,
      userName: TestState.userName,
      title: TestState.testData.title,
      answers: formattedAnswers,
    };

    const result = await API.submitTest(testSubmission);

    // Store result from backend
    API.storeTestResult(result);

    // Navigate to results page
    window.location.href = "results.html";
  } catch (error) {
    console.error("Error submitting test:", error);
    showError("Помилка при відправці тесту. Будь ласка спробуйте ще раз.");
    loading.style.display = "none";
    submitButton.disabled = false;
  }
}

/**
 * Format user answers to match backend DTO structure
 * @returns {Array} Formatted answers array
 */
function formatAnswersForBackend() {
  const answers = [];

  TestState.testData.questions.forEach((question) => {
    const userAnswer = TestState.userAnswers[question.id];

    let answerDto = {
      id: question.id,
      selectedOptionIds: [],
      textAnswer: null,
    };

    switch (question.type) {
      case "SingleChoice":
        if (userAnswer !== null) {
          answerDto.selectedOptionIds = [userAnswer];
        }
        break;

      case "MultipleChoice":
        if (userAnswer !== null && Array.isArray(userAnswer)) {
          answerDto.selectedOptionIds = userAnswer;
        }
        break;

      case "Text":
        answerDto.textAnswer = userAnswer || "";
        break;
    }

    answers.push(answerDto);
  });

  return answers;
}

/**
 * Show error message
 */
function showError(message) {
  const errorElement = document.getElementById("error-message");
  errorElement.textContent = message;
  errorElement.style.display = "block";

  setTimeout(() => {
    hideError();
  }, 5000);
}

/**
 * Hide error message
 */
function hideError() {
  const errorElement = document.getElementById("error-message");
  errorElement.style.display = "none";
}

document.addEventListener("DOMContentLoaded", init);
