// @ts-check
import { defineConfig, devices } from '@playwright/test';

export default defineConfig({
  testDir: './tests',
  /* Run tests in files in parallel */
  fullyParallel: true,

  /* CI safety & stability */
  forbidOnly: !!process.env.CI,
  retries: process.env.CI ? 2 : 0,
  workers: process.env.CI ? 1 : undefined,

  /* Timeouts */
  timeout: 60_000,               // per-test timeout
  expect: { timeout: 10_000 },   // default expect timeout

  /* Reporters */
  reporter: [
    ['list'],
    ['html', { open: 'never' }],
  ],

  /* Shared settings for all projects */
  use: {
    // Use env-provided BASE_URL (set in your GitHub workflow), fallback for local runs.
    baseURL: process.env.BASE_URL || 'http://localhost:5000',

    // Helpful artifacts for debugging CI flakes
    trace: 'on-first-retry',
    screenshot: 'only-on-failure',
    video: 'on-first-retry',
  },

  /* Configure projects (keep it simple on CI: Chromium only) */
  projects: [
    {
      name: 'chromium',
      use: { ...devices['Desktop Chrome'] },
    },

    // Re-enable these once staging runs are stable:
    // {
    //   name: 'firefox',
    //   use: { ...devices['Desktop Firefox'] },
    // },
    // {
    //   name: 'webkit',
    //   use: { ...devices['Desktop Safari'] },
    // },
  ],

  /* If later want to run against a local dev server:
  // webServer: {
  //   command: 'npm run start',
  //   url: process.env.BASE_URL || 'http://127.0.0.1:3000',
  //   reuseExistingServer: !process.env.CI,
  // },
  */
});