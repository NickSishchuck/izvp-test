// API abstraction layer - handles all API calls and data storage

const API_URL = "/api";

/**
 * Fetch test information from the API
 *
 * @returns {Promise<Object>} Test object with title and questions
 * @throws {Error} If the request fails
 *
 * @example
 * const test = await API.getTest();
 * console.log(test.title); // "Тест з C# для початківців"
 */
async function getTest() {
  try {
    const response = await fetch(`${API_URL}/test`);

    if (!response.ok) {
      throw new Error(
        `Failed to fetch test: ${response.status} ${response.statusText}`,
      );
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error in API.getTest():", error);
    throw error;
  }
}

/**
 * Store user's name in session storage
 *
 * @param {string} name - User's name
 *
 * @example
 * API.storeUserName("Maria");
 */
function storeUserName(name) {
  sessionStorage.setItem("userName", name);
}

/**
 * Get user's name from session storage
 *
 * @returns {string|null} User's name or null if not found
 *
 * @example
 * const name = API.getUserName();
 */
function getUserName() {
  return sessionStorage.getItem("userName");
}

/**
 * Store test data in session storage
 * This is used to pass test data between pages
 *
 * @param {Object} testData - The full test object from API
 *
 * @example
 * const test = await API.getTest();
 * API.storeTestData(test);
 */
function storeTestData(testData) {
  sessionStorage.setItem("testData", JSON.stringify(testData));
}

/**
 * Get test data from session storage
 *
 * @returns {Object|null} Test data object or null if not found
 *
 * @example
 * const test = API.getTestData();
 * if (test) {
 *     console.log(test.title);
 * }
 */
function getTestData() {
  const data = sessionStorage.getItem("testData");
  return data ? JSON.parse(data) : null;
}

/**
 * Store test result in session storage
 * This is called by the backend after test submission
 *
 * @param {Object} result - Result object from API
 *
 * @example
 * API.storeTestResult({
 *     name: "Maria",
 *     score: 5,
 *     totalScore: 5,
 *     correctAnswers: 3,
 *     totalQuestions: 3
 * });
 */
function storeTestResult(result) {
  sessionStorage.setItem("testResult", JSON.stringify(result));
}

/**
 * Get test result from session storage
 *
 * @returns {Object|null} Result object or null if not found
 *
 * @example
 * const result = API.getTestResult();
 * if (result) {
 *     console.log(`Score: ${result.score}/${result.totalScore}`);
 * }
 */
function getTestResult() {
  const data = sessionStorage.getItem("testResult");
  return data ? JSON.parse(data) : null;
}

/**
 * Check API health status
 * Useful for debugging connection issues
 *
 * @returns {Promise<Object>} Health status object
 *
 * @example
 * const health = await API.checkHealth();
 * console.log(health.status); // "Healthy"
 */
async function checkHealth() {
  try {
    const response = await fetch(`${API_URL}/health`);

    if (!response.ok) {
      throw new Error(`Health check failed: ${response.status}`);
    }

    const data = await response.json();
    return data;
  } catch (error) {
    console.error("Error in API.checkHealth():", error);
    throw error;
  }
}

// Export all functions as API object
const API = {
  // API calls
  getTest,
  checkHealth,

  // Storage helpers
  storeUserName,
  getUserName,
  storeTestData,
  getTestData,
  storeTestResult,
  getTestResult,
};

// Make available globally
window.API = API;

// ============================================================================
// USAGE EXAMPLES FOR REFERENCE
// ============================================================================

/*

// Example 1: Load test in greeting page
async function loadTestInfo() {
    try {
        const test = await API.getTest();
        console.log(test.title);
        API.storeTestData(test);
    } catch (error) {
        showError('Failed to load test');
    }
}

// Example 2: Store user name and navigate
function handleStartTest() {
    const name = document.getElementById('user-name').value.trim();
    if (!name) {
        showError('Please enter your name');
        return;
    }
    API.storeUserName(name);
    window.location.href = 'test.html';
}

// Example 3: Load results in results page
function loadResults() {
    const result = API.getTestResult();
    if (!result) {
        window.location.href = 'index.html';
        return;
    }
    displayScore(result.score, result.totalScore);
}

// Example 4: Retry test
function handleRetry() {
    API.clearAllData();
    window.location.href = 'index.html';
}

// Example 5: Debug API connection
async function testConnection() {
    try {
        const health = await API.checkHealth();
        console.log('API is healthy:', health);
    } catch (error) {
        console.error('API is not responding');
    }
}

*/
