using TechTalk.SpecFlow;
using FluentAssertions;
using BPCalculator.BPTests.Helpers;

namespace BPCalculator.BPTests.StepDefinitions
{
    [Binding]
    public class BloodPressureSteps
    {
        private int systolic;
        private int diastolic;
        private string category = string.Empty;

        [Given(@"the systolic pressure is (.*)")]
        public void GivenTheSystolicPressureIs(int value) => systolic = value;

        [Given(@"the diastolic pressure is (.*)")]
        public void GivenTheDiastolicPressureIs(int value) => diastolic = value;

        [When(@"the category is calculated")]
        public void WhenTheCategoryIsCalculated()
        {
            var bp = new BloodPressure { Systolic = systolic, Diastolic = diastolic };
            category = bp.Category.GetDisplayName();
        }

        [Then(@"the result should be ""(.*)""")]
        public void ThenTheResultShouldBe(string expected) =>
            category.Should().Be(expected);
    }
}