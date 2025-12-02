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
 * Check if user has already passed the test
 *
 * @param {string} username - User's name
 * @param {string} testId - Test ID (GUID)
 * @returns {Promise<boolean>} True if user already passed, false otherwise
 *
 * @example
 * const hasPassed = await API.checkUserPassed("Maria", "00000000-0000-0000-0000-000000000000");
 */
async function checkUserPassed(username, testId) {
  try {
    const response = await fetch(
      `${API_URL}/test/check/${encodeURIComponent(username)}/${testId}`,
    );

    if (response.ok) {
      // Status 200 means user has NOT passed yet
      return false;
    } else if (response.status === 400) {
      // Status 400 with validation error means user already passed
      return true;
    } else {
      throw new Error(`Unexpected response: ${response.status}`);
    }
  } catch (error) {
    console.error("Error in API.checkUserPassed():", error);
    throw error;
  }
}

/**
 * Submit test answers to backend for evaluation
 *
 * @param {Object} testSubmission - Test submission data
 * @param {string} testSubmission.testId - Test ID
 * @param {string} testSubmission.userName - User's name
 * @param {string} testSubmission.title - Test title
 * @param {Array} testSubmission.answers - Array of answer objects
 * @returns {Promise<Object>} Test result from backend
 *
 * @example
 * const result = await API.submitTest({
 *     testId: "00000000-0000-0000-0000-000000000000",
 *     userName: "Maria",
 *     title: "Test Title",
 *     answers: [
 *         { id: 1, selectedOptionIds: [1], textAnswer: null },
 *         { id: 2, selectedOptionIds: [1, 3], textAnswer: null },
 *         { id: 3, selectedOptionIds: [], textAnswer: "Console.WriteLine" }
 *     ]
 * });
 */
async function submitTest(testSubmission) {
  try {
    const response = await fetch(`${API_URL}/test/submit`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(testSubmission),
    });

    if (!response.ok) {
      const errorData = await response.json();
      throw new Error(
        `Failed to submit test: ${response.status} - ${
          JSON.stringify(errorData)
        }`,
      );
    }

    const result = await response.json();
    return result;
  } catch (error) {
    console.error("Error in API.submitTest():", error);
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
 * This is called after receiving result from backend
 *
 * @param {Object} result - Result object from API
 *
 * @example
 * API.storeTestResult({
 *     correctAnswers: 3,
 *     totalQuestions: 3,
 *     score: 5,
 *     totalScore: 5
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
  checkUserPassed,
  submitTest,
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
