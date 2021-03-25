using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WinAppDriverChallenge.Bases;
using WinAppDriverChallenge.Pages;

namespace WinAppDriverChallenge.Tests
{
    class SetTimerTest : Session
    {

        [Test, Order(0)]
        public void ValidateEmptyTimer()
        {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ClickTimer();
            timer.ValidateEmptyTimer();
        }

        [Test, Order(1)]
        public void AddTimer()
        {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ClickTimer();
            timer.SetTime("0", "01", "00");
        }

        [Test, Order(2)]
        public void ValidateEndTimer()
        {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ValidateEndTimer();
        }


        [Test, Order(3)]
        public void ValidateNotificationTimer()
        {


            var driverDesktop = Session.SessionDesktopId();
            TimerPageObjects timer = new TimerPageObjects(driverDesktop);
            timer.ValidateNotification(/*driverDesktop*/);
            timer.ClickDimissNotification(/*driverDesktop*/);


        }
        [Test, Order(4)]
        public void ValidateExpandTimer()
        {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ExpandTimerWindow();
            timer.ValidateExpandedTimerWindow();
        }

        [Test, Order(5)]
        public void AddTimer6Times()
        {

            for (int i = 1; i <= 6; i++)
            {

                TimerPageObjects timer = new TimerPageObjects(driver);
                timer.ClickTimer();
                timer.SetTime("0", "01", "00");
            }
            TimerPageObjects timerValidation = new TimerPageObjects(driver);
            timerValidation.ValidateSixTimes();
        }


        [Test,Order(6)]
    public void ValidateActivationSelection() {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ClickSelectionModeTimer();
        }

        [Test,Order(7)]
        public void ValidateResetTimer() {
            TimerPageObjects timer = new TimerPageObjects(driver);
            timer.ClickResetTimer();
        }
    }

}
