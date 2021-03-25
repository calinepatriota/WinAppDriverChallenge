using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAppDriverChallenge.Bases;
using WinAppDriverChallenge.Pages;

namespace WinAppDriverChallenge.Tests
{
    class SetAlarmTests : Session
    {
        //#region Setups
        //public void BeforeTest() {
        //public void BeforeTest() {
        //    SessionSetUp(5);

        //}


        #region Test Cases
        [Test, Order(0)]
        public void AddAlarm()
        {
            //  Debug.WriteLine("Teste");
            AlarmPageObject main = new AlarmPageObject(driver);
            main.ClickAddAlarm();

            SetAlarmPageObject alarm = new SetAlarmPageObject(driver);
            alarm.SetTime("11", "40", "PM");
            alarm.SetAlarmName("Team");
            alarm.RepeatAlarm("Sunday");
            alarm.Sound("Tap");
            alarm.SnoozeTime("5 minutes");
            alarm.SaveAlarm();
            alarm.ValidateNewAlarm("Team");
        }

        [Test, Order(1)]
        public void UpdateSoundAlarm()
        {
            SetAlarmPageObject alarm = new SetAlarmPageObject(driver);
            alarm.ClickAlarm();
            alarm.Sound("Jingle");
            alarm.ValidateUpdateSoundAlarm("Jingle");
            alarm.SaveAlarm();
        }

        [Test, Order(2)]
        public void DeleteAlarmCheckBox()
        {
            SetAlarmPageObject alarm = new SetAlarmPageObject(driver);
            alarm.DeleteAlarmCheckbox("Team");
            AlarmPageObject mainPage = new AlarmPageObject(driver);
            mainPage.AssertEmptyAlarmList();
        }

        [Test, Order(3)]
        public void AddAlarm2()
        {
            AlarmPageObject main = new AlarmPageObject(driver);
            main.ClickAddAlarm();

            SetAlarmPageObject alarm = new SetAlarmPageObject(driver);
            alarm.SetTime("10", "15", "AM");
            alarm.SetAlarmName("Team2");
            alarm.RepeatAlarm("Monday");
            alarm.Sound("Tap");
            alarm.SnoozeTime("20 minutes");
            alarm.SaveAlarm();
            alarm.ValidateNewAlarm("Team2");
        }

        [Test, Order(4)]
        public void DeleteAlarmEditWindow()
        {
            SetAlarmPageObject alarm = new SetAlarmPageObject(driver);
            alarm.ClickAlarm();
            alarm.ClickDeleteAlarm();
            AlarmPageObject mainPage = new AlarmPageObject(driver);
            mainPage.AssertEmptyAlarmList();
        }




        #endregion
    }
}
