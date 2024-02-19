using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace AeC
{
    public class Utils
    {
        public Utils() { }
        public void WaitForElementByID(WebDriver driver, string id, int timeoutInSeconds)
        {   //Metodo para espera de carregamento até que o elemento esteja validado e pronto para uso.
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.ElementExists(By.Id(id)));
            }
            catch (WebDriverTimeoutException)
            {
                throw new TimeoutException($"O elemento com ID '{id}' não foi encontrado após {timeoutInSeconds} segundos.");
            }
        }
        public void WaitForElementByXPath(WebDriver driver, string XPath, int timeoutInSeconds)
        {   //Metodo para espera de carregamento até que o elemento esteja validado e pronto para uso.
            try
            {
                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutInSeconds));
                wait.Until(ExpectedConditions.ElementExists(By.XPath(XPath)));
            }
            catch (WebDriverTimeoutException)
            {
                throw new TimeoutException($"O elemento com XPath '{XPath}' não foi encontrado após {timeoutInSeconds} segundos.");
            }
        }
        public void CaptureScreenshot(WebDriver driver, string filename)
        {
            // Sempre crio este metodo pois já tive problemas com FireWall de sites onde, quando acessando "manualmente" a pagina e
            // o conteudo carregava, entretanto quando o robo executava, o mesmo não carregava os elementos.
            // Desta forma consegui evidencia que quando o robo executava o mesmo não estava carregando os elementos.
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;

            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string dataAtual = DateTime.Now.ToString("yyyyMMdd_HHmmss");

            // Construir o nome completo do arquivo. Desta forma gera uma PK e não ocrre de arquivo com nome igual.
            filename = filename + dataAtual;

            // Em condições apropriadas eu salvaria o arquivo em um FileServer e faria a persistencia do endereço no banco.
            string directory = @"C:\PrintsErroExecucao";
            string fullPath = Path.Combine(directory, filename);
            screenshot.SaveAsFile(fullPath);
        }
        public void refreshPagina(WebDriver driver)
        {
           driver.Navigate().Refresh();
           driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromMilliseconds(10000);
        }
    }
}