import { test, expect } from '@playwright/test';
const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

async function waitForAppReady(page) {
  await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });
  await page.waitForFunction(() => !/Render - Application loading/i.test(document.title || ''), { timeout: 60_000 });
  await page.waitForLoadState('networkidle', { timeout: 30_000 });
}

test('homepage loads and shows form content', async ({ page }) => {
  await waitForAppReady(page);

  await expect(page).toHaveTitle(/BPCalculator/i);

  // Avoid strict mode collisions by asserting specific elements
  await expect(page.getByLabel(/systolic/i)).toBeVisible({ timeout: 20_000 });
  await expect(page.getByLabel(/diastolic/i)).toBeVisible({ timeout: 20_000 });

  // If labels ever fail
  // await expect(page.locator('input#BP_Systolic, input[name="BP.Systolic"]')).toBeVisible();
  // await expect(page.locator('input#BP_Diastolic, input[name="BP.Diastolic"]')).toBeVisible();
});