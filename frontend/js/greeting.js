let currentTestData = null;

/**
 * Fetch test from API and update UI
 */
async function loadTestInfo() {
  const loading = document.getElementById("loading");
  const titleElement = document.getElementById("test-title-dynamic");

  loading.style.display = "block";

  try {
    const test = await API.getTest();
    currentTestData = test;

    titleElement.textContent = test.title || "Test";
    API.storeTestData(test);
  } catch (error) {
    console.error("Error loading test:", error);
    showError("Помилка при завантаженні тесту. Будь ласка спробуйте ще.");
  } finally {
    loading.style.display = "none";
  }
}

/**
 * Validate name input, check if user already passed, store data, and navigate
 */
async function handleStartTest() {
  const nameInput = document.getElementById("user-name");
  const name = nameInput.value.trim();

  // Validate name
  if (!name) {
    showError("Будь ласка введіть ім'я");
    nameInput.focus();
    return;
  }

  if (name.length < 2) {
    showError("Ім'я має бути довше ніж 2 літери");
    nameInput.focus();
    return;
  }

  if (name.length > 100) {
    showError("Ім'я повинно бути менше ніж 100 літер");
    nameInput.focus();
    return;
  }

  // Check if test data is loaded
  if (!currentTestData) {
    showError("Дані тесту не завантажені. Спробуйте оновити сторінку.");
    return;
  }

  // Show loading
  const loading = document.getElementById("loading");
  const startButton = document.getElementById("start-button");

  loading.style.display = "block";
  startButton.disabled = true;

  try {
    // Check if user already passed this test
    const hasPassed = await API.checkUserPassed(name, currentTestData.id);

    if (hasPassed) {
      showError("Ви вже складали цей тест.");
      loading.style.display = "none";
      startButton.disabled = false;
      return;
    }

    // Store user name and navigate to test
    API.storeUserName(name);
    window.location.href = "test.html";
  } catch (error) {
    console.error("Error checking user status:", error);
    showError(
      "Помилка при верифікації статусу користувача. Будь ласка спробуйте ще.",
    );
    loading.style.display = "none";
    startButton.disabled = false;
  }
}

/**
 * Display error message
 */
function showError(message) {
  const errorElement = document.getElementById("error-message");
  errorElement.textContent = message;
  errorElement.style.display = "block";

  // Auto-hide after 5 seconds
  setTimeout(() => {
    errorElement.style.display = "none";
  }, 5000);
}

/**
 * Initialize page
 */
function init() {
  loadTestInfo();

  const startButton = document.getElementById("start-button");
  const nameInput = document.getElementById("user-name");

  startButton.addEventListener("click", handleStartTest);

  // Allow Enter key to submit
  nameInput.addEventListener("keypress", (e) => {
    if (e.key === "Enter") {
      handleStartTest();
    }
  });
}

document.addEventListener("DOMContentLoaded", init);
