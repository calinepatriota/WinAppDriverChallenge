using System;
using System.Collections.ObjectModel;
using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Appium.Windows;

namespace WinAppDriverChallenge.Pages
{
    class AlarmPageObject
    {
        #region Private Variables
        private WindowsDriver<WindowsElement> driver;
        #endregion

        #region Automation IDs
        const string EMPTY_LIST_MESSAGE = "EmptyAlarmsListMessage";
        const string ALARM_LIST = "AlarmListView";
        const string CONTEXT_DELETE_BUTTON = "ContextMenuDelete";
        const string ADD_ALARM_BUTTON = "AddAlarmButton";
        #endregion

        #region Automation ClassNames
        const string ALARM_ENTRY = "ListViewItem";
        #endregion

        #region Constructor
        public AlarmPageObject(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }
        #endregion

        #region Asserts
        public void AssertEmptyAlarmList()
        {
            var element = driver.FindElementByAccessibilityId(EMPTY_LIST_MESSAGE);
            Assert.IsNotNull(element);
           // return this;
        }
        #endregion

        #region Public Methods
        public void RightClickAlarm(int index)
        {
            WaitForElementById(10, ALARM_LIST);
            var pane = driver.FindElementByAccessibilityId(ALARM_LIST);
            var alarms = pane.FindElementsByClassName(ALARM_ENTRY);
            RightClickElementInArray(alarms, index);
           // return this;
        }

        public SetAlarmPageObject ClickAddAlarm()
        {
            driver.FindElementByAccessibilityId(ADD_ALARM_BUTTON).Click();
            return new SetAlarmPageObject(driver);
        }

        public void ClickContextDeleteAlarm()
        {
            driver.FindElementByAccessibilityId(CONTEXT_DELETE_BUTTON).Click();
           // return this;
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
