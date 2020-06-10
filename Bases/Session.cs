using NUnit.Framework;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Windows;
using System;

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
        #endregion

        #region Public Methods
        public void SessionSetUp(int defaultImplicitWait)
        {
            ip = TestContext.Parameters["ip"];
            port = TestContext.Parameters["port"];
            implictWait = TimeSpan.FromSeconds(defaultImplicitWait);
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

        public void SessionTearDown()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }
        #endregion
    }
}
