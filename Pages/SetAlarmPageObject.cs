using System;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverChallenge.Pages
{
    class SetAlarmPageObject
    {
        #region Private Variables
        private WindowsDriver<WindowsElement> driver;
        #endregion

        #region Automation IDs
        const string HOUR_LIST = "HourLoopingSelector";
        const string MINUTE_LIST = "MinuteLoopingSelector";
        const string ALARM_NAME = "AlarmNameTextBox"; 
        const string ALARM_SAVE_BUTTON = "AlarmSaveButton";
        #endregion

        #region Constructors
        public SetAlarmPageObject(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }
        #endregion

        #region Asserts

        #endregion

        #region Public Methods

        public MainPageObject AddNewAlarm(string name, string hour)
        {
            WaitForElementById(10, HOUR_LIST);
            driver.FindElementByAccessibilityId(HOUR_LIST).FindElementByName(hour).Click();

            var alarmName = driver.FindElementByAccessibilityId(ALARM_NAME);
            alarmName.SendKeys(name);

            driver.FindElementByAccessibilityId(ALARM_SAVE_BUTTON).Click();

            return new MainPageObject(driver);
        }

        #endregion

        #region Private Methods
        public void WaitForElementById(int timeout, string elementId)
        {
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(timeout);

            try
            {
                driver.FindElementByAccessibilityId(elementId);
            }
            catch { }
            finally
            {
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            }
        }
        #endregion
    }
}
