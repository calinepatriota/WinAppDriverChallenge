using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WinAppDriverChallenge.Pages
{
    class TimerPageObjects
    {
        #region Private Variables
        private WindowsDriver<WindowsElement> driver;
        private WebDriverWait wait;
        #endregion

        #region Automation IDs
        const string TITLE_TIMER_DONE = "TitleText";
        const string DISMISS_BUTTON = "VerbButton";
        const string TIMER_BUTTON = "TimerButton";
        const string ALARM_BUTTON = "AlarmButton";
        const string ADD_TIMER_BUTTON = "AddTimerButton";
        const string LIST_HOUR = "HourLoopingSelector";
        const string LIST_MINUTE = "MinuteLoopingSelector";
        const string LIST_SECOND = "SecondLoopingSelector";
        const string PLAY_TIMER = "TimerStartButton";
        const string EMPTY_TIMER = "TimerListEmptyMessage";
        const string EXPAND_BUTTON_TIMER = "TimerExpandButton";
        const string RESTORE_BUTTON_TIMER = "TimerRestoreButton";
        const string SELECT_BUTTON_TIMER = "SelectTimersButton";
        const string TRASH_BUTTON_TIMER = "DeleteTimersButton";
        const string RESET_BUTTON_TIMER = "TimerResetButton";
        const string PLAY_PAUSE_BUTTON_TIMER = "TimerPlayPauseButton";
        #endregion

        #region TagName IDs
        const string LIST_TIME = "ListItem";
        const string LIST_EMPTY_TIMER = "ListItem";
        const string LIST_TIME_COLUMN = "List";
        #endregion

        public TimerPageObjects(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }


        #region Assserts
        public void ValidateNotification(/*WindowsDriver<WindowsElement> driverDesktop*/)
        {
            // Thread.Sleep(TimeSpan.FromSeconds(60));
            Assert.AreEqual(driver.FindElementByAccessibilityId(TITLE_TIMER_DONE).Text, "Timer done");
        }

        public void ValidateEmptyTimer()
        {
            Assert.IsTrue(driver.FindElementByAccessibilityId(EMPTY_TIMER).Text.StartsWith("You don’t have"));
        }

        public void ValidateEndTimer()
        {
            var list = driver.FindElementsByTagName(LIST_EMPTY_TIMER);
            Thread.Sleep(TimeSpan.FromSeconds(60));
            foreach (var item in list)
            {
                var teste = item.Text;
                if (item.Text.Contains("Completed"))
                {
                    Assert.IsTrue(item.Text.Contains("Completed"));
                    break;
                }

            }
        }

        public void ValidateExpandedTimerWindow()
        {
            Assert.IsNotNull(driver.FindElementByAccessibilityId(RESTORE_BUTTON_TIMER));
            driver.FindElementByAccessibilityId(RESTORE_BUTTON_TIMER).Click();

        }

        public void ValidateSixTimes()
        {
            var list = driver.FindElementsByTagName(LIST_TIME);
            Assert.IsTrue(list.Count.Equals(7));
        }

        #endregion

        public void ClickTimer()
        {
            driver.FindElementByAccessibilityId(TIMER_BUTTON).Click();
        }
        public void SetTime(string hourTime, string minutesTime, string secondsTime)
        {
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));
            wait.Until(waiting => driver.FindElementByAccessibilityId(ADD_TIMER_BUTTON).Displayed);
            driver.FindElementByAccessibilityId(ADD_TIMER_BUTTON).Click();
            //   var list = driver.FindElementsByTagName(LIST_TIME); 
            var hour = driver.FindElementByAccessibilityId(LIST_HOUR).FindElementsByTagName(LIST_TIME);
            var minutes = driver.FindElementByAccessibilityId(LIST_MINUTE).FindElementsByTagName(LIST_TIME);
            var seconds = driver.FindElementByAccessibilityId(LIST_SECOND).FindElementsByTagName(LIST_TIME);


            ReadList(hour, hourTime);
            ReadList(minutes, minutesTime);
            ReadList(seconds, secondsTime);
            driver.FindElementByAccessibilityId(PLAY_TIMER).Click();

            //  wait.Until(waiting => driver.FindElementByAccessibilityId(ADD_TIMER_BUTTON).Displayed);
            //   driver.FindElementByAccessibilityId(ADD_TIMER_BUTTON).Click();  


            Debug.WriteLine("Test");
        }
        public void ClickDimissNotification(/*WindowsDriver<WindowsElement> driverDesktop*/)
        {
            Thread.Sleep(TimeSpan.FromSeconds(60));
            driver.FindElementByAccessibilityId(DISMISS_BUTTON).Click();
        }

        public void ExpandTimerWindow()
        {
            var list = driver.FindElementsByTagName(LIST_EMPTY_TIMER);
            foreach (var item in list)
            {
                item.FindElementByAccessibilityId(EXPAND_BUTTON_TIMER).Click();

                break;
            }
            //   var list3 = driver.FindElementByXPath("/ Pane[@Name =\"Desktop 1\"][@ClassName=\"#32769\"]/Pane[@Name=\"Program Manager\"][@ClassName=\"Progman\"]/List[@Name=\"Desktop\"][@ClassName=\"SysListView32\"]/ListItem[starts-with(@AutomationId,\"ListViewItem-\")][@Name=\"Movies &amp; TV - Shortcut\"]");
            // list3.Click();
            // \"][@ClassName=\"ListViewItem\"]/

            //   driver.FindElementByAccessibilityId(EXPAND_BUTTON_TIMER).Click();
        }

        public void ClickSelectionModeTimer()
        {
            driver.FindElementByAccessibilityId(SELECT_BUTTON_TIMER).Click();
            var list = driver.FindElementsByTagName(LIST_TIME);
            foreach (var item in list)
            {
                if (item.Text.Contains("Timer (1)"))
                {
                    item.Click();
                    Assert.IsTrue(driver.FindElementByAccessibilityId(TRASH_BUTTON_TIMER).Enabled);
                    break;
                }
            }

        }

        public void ClickResetTimer()
        {
            var list = driver.FindElementsByTagName(LIST_TIME);
            foreach (var item in list)
            {
                string getTimerBeforeReset;
                string getTimerAfterReset;

                if (item.Text.Contains("Timer (1)"))
                {
                    item.FindElementByAccessibilityId(PLAY_PAUSE_BUTTON_TIMER).Click();
                    Thread.Sleep(TimeSpan.FromSeconds(5));
                    item.FindElementByAccessibilityId(PLAY_PAUSE_BUTTON_TIMER).Click();
                    getTimerBeforeReset = item.Text;
                    item.FindElementByAccessibilityId(PLAY_PAUSE_BUTTON_TIMER).Click();
                    item.FindElementByAccessibilityId(RESET_BUTTON_TIMER).Click();
                    getTimerAfterReset = item.Text;
                    Assert.AreNotEqual(getTimerBeforeReset, getTimerAfterReset);
                    break;
                }
            }

        }


        #region Private 
        private void ReadList(ReadOnlyCollection<AppiumWebElement> list, string time)
        {
            foreach (var item in list)
            {
                var teste = item.Text;
                item.Click();
                if (item.Text.Equals(time))
                {
                    break;
                }
            }

        }
        #endregion
    }
}
