// tests/bloodpressure.spec.js
import { test, expect } from '@playwright/test';

const BASE_URL = process.env.BASE_URL || 'http://localhost:5000';

/**
 * Waits until the app is actually served 
 * Also verifies that at least one stable bit of your page content is present.
 */
async function waitForAppReady(page) {
    await page.goto(BASE_URL, { waitUntil: 'domcontentloaded' });

    // 1) Wait for the splash title to disappear
    await page.waitForFunction(
        () => !/Render - Application loading/i.test(document.title || ''),
        { timeout: 60_000 }
    );

    const contentHints = [
        /systolic/i,
        /diastolic/i,
        /blood\s*pressure/i,
    ];

    await page.waitForFunction(
        (patterns) => {
            const bodyText = document.body?.innerText || '';
            return patterns.some((p) => new RegExp(p, 'i').test(bodyText));
        },
        contentHints.map(String),
        { timeout: 60_000 }
    );
}


function bpFieldLocators(page) {
    const systolic =
        page.getByLabel(/systolic/i).or(page.locator('input[name="BP.Systolic"]'));
    const diastolic =
        page.getByLabel(/diastolic/i).or(page.locator('input[name="BP.Diastolic"]'));
    return { systolic, diastolic };
}

test.describe('Blood Pressure Calculator', () => {
    test.setTimeout(90_000);

    test('User can calculate blood pressure category', async ({ page }) => {
        await waitForAppReady(page);

        const { systolic, diastolic } = bpFieldLocators(page);

        // Wait for input visibility (covers any late client-side render)
        await expect(systolic, 'Systolic input should be visible').toBeVisible({ timeout: 30_000 });
        await expect(diastolic, 'Diastolic input should be visible').toBeVisible({ timeout: 30_000 });

        // Enter values
        await systolic.fill('135');
        await diastolic.fill('85');

        // Click submit (support a few button label variants)
        const submitBtn = page.getByRole('button', { name: /submit|calculate|compute/i });
        await expect(submitBtn, 'Submit/Calculate button should be visible').toBeVisible({ timeout: 10_000 });
        await submitBtn.click();

        const result = page.getByText(/pre[-\s]?high|elevated|stage\s*1|normal|hypertension/i, { exact: false });
        await expect(result, 'Result text should become visible').toBeVisible({ timeout: 15_000 });
    });

    //a super fast smoke that just proves the form renders
    test('Form renders with both fields', async ({ page }) => {
        await waitForAppReady(page);
        const { systolic, diastolic } = bpFieldLocators(page);
        await expect(systolic).toBeVisible({ timeout: 20_000 });
        await expect(diastolic).toBeVisible({ timeout: 20_000 });
    });
});