// tests/example.spec.js
import { test, expect } from '@playwright/test';

const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

// Small helper to make this test resilient
async function waitForAppReady(page) {
  await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });

  // Wait until the Render splash title is gone
  await page.waitForFunction(
    () => !/Render - Application loading/i.test(document.title || ''),
    { timeout: 60_000 }
  );

  //  wait until some real page text appears
  await expect(page.getByText(/Systolic|Diastolic|Blood\s*Pressure/i)).toBeVisible({ timeout: 60_000 });
}

test('homepage loads and shows form content', async ({ page }) => {
  await waitForAppReady(page);

  await expect(page.getByText(/Systolic/i)).toBeVisible({ timeout: 20_000 });
  await expect(page.getByText(/Diastolic/i)).toBeVisible({ timeout: 20_000 });

});
