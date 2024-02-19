using AeC;
using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace AeC
{
    public class roboNavegador
    {
        Utils Utils = new Utils();  
        public roboNavegador(){}
        public void navigateTO(String URL, WebDriver driver)
        {   //Navega até a URL, colocado Wait para tempo de carregamento do site.
            try
            {
                driver.Navigate().GoToUrl(URL);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(5000);

            }//Tratamento para Timeout
            catch (TimeoutException TE) 
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Acessar_Site");
                Console.WriteLine("Erro de Timeout ao tentar acessar o site: " + URL + ". " + TE.ToString());
                driverQuit(driver);
            }
            catch (Exception e)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Acessar_Site");
                Console.WriteLine("Erro ao acessar o site: " + URL + ". " + e.ToString());
                driverQuit(driver);
            }
        }
        public void driverQuit(WebDriver driver)
        {
            driver.Quit();
            driver = null;
        }
        public WebElement findElementByXpath(String xPath, WebDriver driver)
        {   //Metodo para achar os elementos pelo XPath com tratamento de erros e um Wait implicito.
            try
            {
                Utils.WaitForElementByXPath(driver, xPath, 5000);
                return (WebElement)driver.FindElement(By.XPath(xPath));

            }
            catch (NoSuchElementException e)
            {
                Utils.CaptureScreenshot(driver, "Elemento_nao_encontrado");
                Console.WriteLine("Elemento não encontrado. Screenshot salvo da tela no momento atual.");
                driverQuit(driver);
                return null;
            }
            catch (TimeoutException TE)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Acessar_Site");
                Console.WriteLine("Erro de Timeout ao tentar procurar procurar o element: " + TE.ToString());
                driverQuit(driver);
                return null;
            }
        }
        public WebElement findElementById(String id, WebDriver driver)
        {//Metodo para achar os elementos pelo iD da DIV com tratamento de erros e um Wait implicito.
            try
            {
                Utils.WaitForElementByID(driver, id, 5000);
                return (WebElement)driver.FindElement(By.Id(id));

            }
            catch (NoSuchElementException e)
            {
                Utils.CaptureScreenshot(driver, "Elemento_ID_nao_encontrado");
                Console.WriteLine("Elemento não encontrado. Screenshot salvo da tela no momento atual.");
                driverQuit(driver);
                return null;
            }
            catch (TimeoutException TE)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Acessar_Site");
                Console.WriteLine("Erro de Timeout ao tentar procurar procurar o element: " + TE.ToString());
                driverQuit(driver);
                return null;
            }
        }
        public void SendKeysToBox(WebDriver driver, String id, string text)
        {   //Metodo para inserir dados digitados, como tratamento de excessao NotInteractable, erro comum nesta operação.
            try
            {
                WebElement textBox = findElementById(id, driver);
                textBox.SendKeys(text);
            }
            catch (ElementNotInteractableException eie)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Inserir_Dados_TextBox");
                Console.WriteLine("Erro ao inserir dados, algum problema impedindo a digitação dos dados: " + eie.ToString());
                driverQuit(driver);
            }
            catch (Exception e)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Inserir_Dados_TextBox");
                Console.WriteLine("Erro ao inserir dados na TextBox: " + e.ToString());
                driverQuit(driver);
            }

        }
        public void clickButtonByXpath(WebDriver driver, String botao)
        {//Metodo para clicar em botões pelo xPath, como tratamento de excessao NotInteractable, erro comum nesta operação.
            try
            {
                WebElement button = findElementByXpath(botao, driver);
                button.Click();
            }
            catch (ElementNotInteractableException eie)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Clicar");
                Console.WriteLine("Erro ao clicar no botão, algum problema impedindo o click: " + eie.ToString());
                driverQuit(driver);
            }
            catch (Exception e)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Clicar");
                Console.WriteLine("Erro ao clicar no botão: " + e.ToString());
                driverQuit(driver);
            }
        }
        public void clickButtonById(WebDriver driver, String botao)
        {//Metodo para clicar em botões pelo ID, como tratamento de excessao NotInteractable, erro comum nesta operação.
            try
            {
                WebElement button = findElementById(botao, driver);
                button.Click();
            }
            catch (ElementNotInteractableException eie)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Clicar");
                Console.WriteLine("Erro ao clicar no botão, algum problema impedindo o click: " + eie.ToString());
                driverQuit(driver);
            }
            catch (Exception e)
            {
                Utils.CaptureScreenshot(driver, "Erro_ao_Clicar");
                Console.WriteLine("Erro ao clicar no botão: " + e.ToString());
                driverQuit(driver);
            }
        }
        public string processaDetalhesCurso(WebDriver driver, string XPath)
        {//No site escolhido pelo termo de pesquisa a descrição do curso estava em varias div's , desta ver utilizei o FindElements para achar todos e tratei o mesmo com um
        //StringBuilder visto que iria inserir o campo do banco. Utilizando este metodo somente para este exemplo.
            ReadOnlyCollection<IWebElement> elementos = driver.FindElements(By.XPath(XPath));

            // Variável para armazenar os textos
            StringBuilder textoAgregado = new StringBuilder();

            foreach (var elemento in elementos)
            {
                textoAgregado.Append(elemento.Text);
                textoAgregado.Append(" ");
            }
            return textoAgregado.ToString();
        }

    }
}