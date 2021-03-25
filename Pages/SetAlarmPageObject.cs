
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;

namespace WinAppDriverChallenge.Pages
{

    class SetAlarmPageObject
    {
        #region Private Variables
        private WindowsDriver<WindowsElement> driver;
        #endregion

        #region Automation IDs
        const string LIST_HOUR_ALARM = "HourLoopingSelector";
        const string REPEAT_ALARM_BUTTON = "AlarmRepeatsToggleButton";
        const string SOUND_TYPE = "AlarmSoundButton";
        const string ALARM_BUTTON_SAVE = "AlarmSaveButton";
        const string ALARM_CHECKBOX = "SelectAlarmsButton";
        const string TRASH_ICON = "DeleteAlarmsButton";
        const string TRASH_ICON_EDIT_PAGE = "AlarmDeleteButton";
        const string POP_UP_TRASH_ALARM = "PrimaryButton";
        #endregion

        #region ClassName IDs
        const string ALARM_NAME = "TextBox";
        const string SNOOZE_TIME = "ComboBoxItem";
        const string VALIDATE_ALARM_NAME = "ListViewItem";
        #endregion


        #region TagName IDs
        const string LIST_HOUR = "ListItem";
        const string LIST_ALARM = "ListItem";
        #endregion

        #region Constructors
        public SetAlarmPageObject(WindowsDriver<WindowsElement> driver)
        {
            this.driver = driver;
        }
        #endregion

        #region Asserts
        public void ValidateNewAlarm(string alarmName)
        {
            Assert.IsTrue(driver.FindElementByClassName(VALIDATE_ALARM_NAME).Text.Contains(alarmName));

        }

        public void ValidateUpdateSoundAlarm(string soundName)
        {
            string nameSound = GetSoundName().TrimStart();
            //"Sound, Tap, "
            //Jingle
            Assert.AreEqual(soundName, nameSound);


        }
        #endregion

        #region Public Methods
        public void SetTime(string hour, string minutes, string period)
        {
            var list = driver.FindElementsByTagName(LIST_HOUR);
            ReadList(list, hour);
            ReadList(list, minutes);
            driver.FindElementByName(period).Click();
            // ReadList(list, time);
            Debug.WriteLine(list.Count);
        }

        internal void ReadList(ReadOnlyCollection<AppiumWebElement> seconds, string v)
        {
            throw new NotImplementedException();
        }

        public void SetAlarmName(string alarmName)
        {
            driver.FindElementByClassName(ALARM_NAME).SendKeys(alarmName);
        }

        public void RepeatAlarm(string day)
        {
            driver.FindElementByAccessibilityId(REPEAT_ALARM_BUTTON).Click();
            driver.FindElementByName(day).Click();
            driver.FindElementByName(day).SendKeys(Keys.Enter);
        }

        public void Sound(string soundType)
        {
            driver.FindElementByAccessibilityId(SOUND_TYPE).Click();
            driver.FindElementByName(soundType).Click();
        }

        public void SnoozeTime(string minutes)
        {
            driver.FindElementByClassName(SNOOZE_TIME).Click();
            driver.FindElementByName(minutes).Click();
        }

        public void SaveAlarm()
        {
            driver.FindElementByAccessibilityId(ALARM_BUTTON_SAVE).Click();
        }

        public void ClickAlarm()
        {
            driver.FindElementByClassName(VALIDATE_ALARM_NAME).Click();
        }

        public void DeleteAlarmCheckbox(string alarmName)
        {
            driver.FindElementByAccessibilityId(ALARM_CHECKBOX).Click();
            var list = driver.FindElementsByTagName(LIST_ALARM);
            foreach (var item in list)
            {
                var teste = item.Text;

                if (item.Text.Contains(alarmName))
                {
                    item.Click();
                    driver.FindElementByAccessibilityId(TRASH_ICON).Click();
                    break;
                }
            }

        }

        public void ClickDeleteAlarm()
        {
            driver.FindElementByAccessibilityId(TRASH_ICON_EDIT_PAGE).Click();
            driver.FindElementByAccessibilityId(POP_UP_TRASH_ALARM).Click();

        }


        #endregion

        #region Private Methods
        private void ReadList(ReadOnlyCollection<WindowsElement> list, string time)
        {
            foreach (var item in list)
            {
                var teste = item.Text;
                item.Click();
                //Thread.Sleep(TimeSpan.FromSeconds(2));
                if (item.Text.Equals(time))
                {
                    break;
                }
            }

        }
        private string GetSoundName()
        {
            string soundName = driver.FindElementByAccessibilityId(SOUND_TYPE).Text.Trim().Replace("Sound,", "").Replace(",", "");
            return soundName;
        }

        #endregion
    }
}
