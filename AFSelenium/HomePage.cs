using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace PageObjectModel.Source.Pages
{
    public class HomePage
    {
        private IWebDriver _driver;

        [FindsBy(How = How.Name, Using = "q")]
        private IWebElement _searchtxtbox;

        [FindsBy(How = How.Name, Using = "btnK")]
        private IWebElement _searchbtn;

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void Search(string searchText)
        {
            _searchtxtbox.SendKeys(searchText);
            _searchtxtbox.SendKeys("\n");
        }
    }
}