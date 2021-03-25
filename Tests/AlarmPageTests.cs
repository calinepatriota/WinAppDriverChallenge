using NUnit.Framework;
using WinAppDriverChallenge.Pages;
using WinAppDriverChallenge.Bases;

namespace WinAppDriverChallenge.Tests
{
    [TestFixture]
    class AlarmPageTests : Session
    {
        #region Setups
        //   [SetUp]
        //public void SetUp()
        //{
        //    SessionSetUp(10);
        //}

        //  [TearDown]
        //public void TearDown()
        //{
        //    SessionTearDown();
        //}
        #endregion

        #region Test Cases
        [Test, Order(0)]
        public void DeleteAlarmByRightClick()
        {
            var mainPage = new AlarmPageObject(driver);

            mainPage.RightClickAlarm(0);
            mainPage.ClickContextDeleteAlarm();
            mainPage.AssertEmptyAlarmList();
        }

        [Test, Order(1)]
        public void CheckEmptyPage()
        {
            var mainPage = new AlarmPageObject(driver);

            mainPage.
                AssertEmptyAlarmList();

        }
        #endregion
    }
}
