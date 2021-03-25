using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;
using System.Threading;

namespace WinAppDriverChallenge.Bases
{
    class Session
    {
        #region Properties
        public static WindowsDriver<WindowsElement> driver;
        private TimeSpan implictWait { get; set; }
        private string ip { get; set; }
        private string port { get; set; }
        private string appId { get; set; }
        private string root { get; set; }

        private static TimeSpan implictWaitDesktop { get; set; }
        private static string ipDesktop { get; set; }
        private static string portDesktop { get; set; }
        private static string appIdDesktop { get; set; }
        private static string rootDesktop { get; set; }
        public static WindowsDriver<WindowsElement> driverDesktop;
        #endregion

        #region Public Methods
        [SetUp]
        public void SessionSetUp(/*int defaultimplicitwait*/)
        {
            ip = TestContext.Parameters["ip"];
            port = TestContext.Parameters["port"];
            implictWait = TimeSpan.FromSeconds(10);
            appId = @"Microsoft.WindowsAlarms_8wekyb3d8bbwe!App";

            if (driver == null)
            {
                AppiumOptions appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("platformName", "Windows");
                appCapabilities.AddAdditionalCapability("app", appId);

                driver = new WindowsDriver<WindowsElement>(new Uri("http://" + ip + ":" + port), appCapabilities);
                Assert.IsNotNull(driver);

                driver.Manage().Window.Maximize();
                driver.Manage().Timeouts().ImplicitWait = implictWait;
            }
        }
        public static WindowsDriver<WindowsElement> SessionDesktopId(/*int defaultimplicitwait*/)
        {
            ipDesktop = TestContext.Parameters["ip"];
            portDesktop = TestContext.Parameters["port"];
            implictWaitDesktop = TimeSpan.FromSeconds(10);
            rootDesktop = "Root";
            //appId = @"Microsoft.WindowsAlarms_8wekyb3d8bbwe!App";

            if (driverDesktop == null)
            {
                AppiumOptions appCapabilities = new AppiumOptions();
                appCapabilities.AddAdditionalCapability("platformName", "Windows");
                appCapabilities.AddAdditionalCapability("app", rootDesktop);

                driverDesktop = new WindowsDriver<WindowsElement>(new Uri("http://" + ipDesktop + ":" + portDesktop), appCapabilities);
                Assert.IsNotNull(driverDesktop);

                // driverDesktop.Manage().Window.Maximize();
                driverDesktop.Manage().Timeouts().ImplicitWait = implictWaitDesktop;
            }
            return driverDesktop;
        }

        [TearDown]
        public void SessionTearDown()
        {
            if (driver != null)
            {
                Thread.Sleep(8000);
                driver.Quit();
                driver = null;
            }
        }
        #endregion
    }
}
