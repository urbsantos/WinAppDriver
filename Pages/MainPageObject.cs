using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Windows;
using Newtonsoft.Json.Schema;

namespace WinAppDriverChallenge.Pages
{
    class MainPageObject
    {
        #region Private Variables
        private WindowsDriver<WindowsElement> driver;
        #endregion

        #region Automation IDs
        const string EMPTY_LIST_MESSAGE = "EmptyAlarmsListMessage";
        const string ALARM_LIST = "AlarmListView";
        const string CONTEXT_DELETE_BUTTON = "ContextMenuDelete";
        const string ADD_ALARM_BUTTON = "AddAlarmButton";
        const string ALARM_LIST_ITEM = "AlarmListView";
        const string ALARM_ITEM = "ListViewItem";
        const string ALARM_TITLE_TEXT = "AlarmTitleText";
        const string ALARM_NAME_TEXT = "AlarmEnabledNameText";
        const string ALARM_DELETE_BUTTON = "DeleteAlarmsButton";
        const string SELECT_ALARM_LIST = "SelectAlarmsButton";

        #endregion

        #region Automation ClassNames
        const string ALARM_ENTRY = "ListViewItem";
        #endregion

        #region Constructor
        public MainPageObject(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }
        #endregion

        #region Asserts
        public MainPageObject AssertEmptyAlarmList()
        {
            WaitForElementById(10, EMPTY_LIST_MESSAGE);
            var element = driver.FindElementByAccessibilityId(EMPTY_LIST_MESSAGE);
            Assert.IsNotNull(element);
            return this;
        }

        public MainPageObject AssertAlarm(string name, string hour)
        {
           
            Assert.IsNotNull(driver.FindElementByAccessibilityId(ALARM_LIST));
            var hourItems = driver.FindElementByAccessibilityId(ALARM_LIST)
                .FindElementsByClassName(ALARM_ITEM);
            Assert.IsTrue(hourItems.Count > 0);
                        
            Assert.True(hourItems[0].Text.Contains(hour));
            Assert.True(hourItems[0].Text.Contains(name));
            
            return this;
        }
        #endregion

        #region Public Methods
        public MainPageObject RightClickAlarm(int index)
        {
            WaitForElementById(10, ALARM_LIST);
            var pane = driver.FindElementByAccessibilityId(ALARM_LIST);
            var alarms = pane.FindElementsByClassName(ALARM_ENTRY);
            RightClickElementInArray(alarms, index);
            return this;
        }

        public SetAlarmPageObject ClickAddAlarm()
        {
            WaitForElementById(10, ADD_ALARM_BUTTON);
            driver.FindElementByAccessibilityId(ADD_ALARM_BUTTON).Click();
            return new SetAlarmPageObject(driver);
        }

        public MainPageObject ClickContextDeleteAlarm()
        {
            driver.FindElementByAccessibilityId(CONTEXT_DELETE_BUTTON).Click();
            return this;
        }

        public MainPageObject SelectAlarmListItem(string name) {
            WaitForElementById(10, SELECT_ALARM_LIST);
            driver.FindElementByAccessibilityId(SELECT_ALARM_LIST).Click();
            WaitForElementById(10, ALARM_ITEM);
            driver.FindElementByClassName(ALARM_ITEM).FindElementByName(name).Click();
            return this;
        }
        public MainPageObject DeleteAlarmByCheckbox() {
            driver.FindElementByAccessibilityId(ALARM_DELETE_BUTTON).Click();
            return this;
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

        private void RightClickElementInArray(ReadOnlyCollection<AppiumWebElement> elementList, int index)
        {
            Actions actions = new Actions(driver);
            int elementPosition = (elementList.Count + index) % elementList.Count;
            actions.ContextClick(elementList[elementPosition]).Perform();
        }
        #endregion
    }
}
