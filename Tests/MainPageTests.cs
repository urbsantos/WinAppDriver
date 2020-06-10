using NUnit.Framework;
using WinAppDriverChallenge.Pages;
using WinAppDriverChallenge.Bases;

namespace WinAppDriverChallenge.Tests
{
    [TestFixture]
    class MainPageTests : Session
    {
        #region Setups
        [SetUp]
        public void SetUp()
        {
            SessionSetUp(10);
        }

        [TearDown]
        public void TearDown()
        {
            SessionTearDown();
        }
        #endregion

        #region Test Cases
        [Test, Order(0)]
        public void DeleteAlarmByRightClick()
        {
            var mainPage = new MainPageObject(driver);

            mainPage.
                RightClickAlarm(0).
                ClickContextDeleteAlarm().
                AssertEmptyAlarmList();
        }

        [Test, Order(1)]
        public void CheckEmptyPage()
        {
            var mainPage = new MainPageObject(driver);

            mainPage.
                AssertEmptyAlarmList();

        }

        [Test, Order(1)]
        public void AddAlarm()
        {
            var mainPage = new MainPageObject(driver);
            string name = "Novo alarme";
            string hour = "8";
            mainPage.ClickAddAlarm()
                .AddNewAlarm(name, hour)
                .AssertAlarm(name, hour);
        }

        [Test, Order(2)]
        public void DeleteAlarmByCheckbox() {
            var mainPage = new MainPageObject(driver);
            string name = "Novo alarme";
            mainPage.SelectAlarmListItem(name).DeleteAlarmByCheckbox();
        }

        #endregion
    }
}
