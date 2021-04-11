using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTest
{
    public class NamesForParrotTests
    {
        public ChromeDriver driver;

        [SetUp]
        public void SetUp()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            driver = new ChromeDriver(options);

        }

        private By emailInputLocator = (By.Name("email"));
        private By buttonLocator = (By.Id("sendMe"));
        private By emailResultLocator = (By.ClassName("your-email"));
        private By anotherEmailLinkLocator = (By.Id("anotherEmail"));
        private By chooseGirlLocator = (By.Id("girl"));
        private By resultTextLocator = (By.ClassName("result-text"));
        private By emailIncorrectTextLocator = (By.ClassName("form-error"));

        [Test]
        public void NameForBoy_Email_Correct()
        {
            //перейти на страницу

            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            //заполнить форму и кликнуть
            var expectedEmail = "test@mail.ru";

            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            var expectedTextForBoy = "Хорошо, мы пришлём имя для вашего мальчика на e-mail:";
            //проверять текст 
            Assert.AreEqual(expectedTextForBoy, driver.FindElement(resultTextLocator).Text, "Неверный текст");

            //проверять e-mail
            Assert.AreEqual(expectedEmail, driver.FindElement(emailResultLocator).Text, "Отправили письмо не на тот e-mail");

        }



        [Test]

        public void NameForGirl_Email_Correct()
        {
            //перейти на страницу
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            //выбрать девочку
            driver.FindElement(chooseGirlLocator).Click();


            //заполнить форму и кликнуть
            var expectedEmail = "test@mail.ru";
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            var expecredTextForGirl = "Хорошо, мы пришлём имя для вашей девочки на e-mail:";
            //проверять текст 
            Assert.AreEqual(expecredTextForGirl, driver.FindElement(resultTextLocator).Text, "Неверный текст");

            //проверять e-mail
            Assert.AreEqual(expectedEmail, driver.FindElement(emailResultLocator).Text, "Отправили письмо не на тот e-mail");

        }


        [Test]

        public void NameForParrot_ClickToFillAnotherEmail_EmailInputIsEmpty()
        {
            //перейти на страницу
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");


            //заполнить форму и кликнуть
            var expectedEmail = "test@mail.ru";
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();
            driver.FindElement(anotherEmailLinkLocator).Click();

            //проверка очистки поля e-mail
            Assert.AreEqual(string.Empty, driver.FindElement(emailInputLocator).Text, "После клика по ссылке поле не очистилось");
            Assert.IsFalse(driver.FindElements(anotherEmailLinkLocator).Count == 0, "Не исчезла ссылка для ввода другого имейла");
        }

        [Test]
        public void NameForParrot_Email_Incorrect()
        {
            //перейти на страницу
            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            //заполнить форму и кликнуть
            var expectedEmail = "123";
            driver.FindElement(emailInputLocator).SendKeys(expectedEmail);
            driver.FindElement(buttonLocator).Click();

            var expectedTextForIncorrectEmail = "Некорректный email";
            
            //проверять текст ошибки
            Assert.AreEqual(expectedTextForIncorrectEmail, driver.FindElement(emailIncorrectTextLocator).Text, "Неверный текст ошибки");

        }

        [Test]
        public void NameForParrot_Title_Correct()
        {
            //перейти на страницу

            driver.Navigate().GoToUrl("https://qa-course.kontur.host/selenium-practice/");

            //заполнить форму и кликнуть
            var expectedTitle = "Тестирование программного обеспечения";

            //проверять текст заголовка 
            Assert.IsTrue(driver.Title.Contains(expectedTitle), "Неверный заголовок");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}