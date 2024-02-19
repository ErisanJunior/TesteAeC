using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AeC
{
    public class Robo
    {   
        //Controle de loop
        int tentativa = 0;
        bool continua = true;

        public void Executar()
        {
            while(tentativa<=3 && continua) 
            {
                tentativa++;
                //Montagem Objetos
                RoboAlura dadosalura = new RoboAlura();
                WebDriver driver = new ChromeDriver();
                roboNavegador roboNavegador = new roboNavegador();
                Utils utils = new Utils();

                //Inicio processamento
                acessaSite(driver, roboNavegador);
                procuraCurso(driver, roboNavegador, utils);
                preencheDadosAlura(driver, roboNavegador, dadosalura, utils);
                continua = false;
            };
            
            if (tentativa>=3)
            {
                Console.WriteLine("Ocorreram erros durante a execução e se excedeu o numero de tentativas, por gentileza checkar a pasta de Prints e checkar os Logs.");
            }
        }
        public void acessaSite(WebDriver driver, roboNavegador roboNavegador)
        {   //Acessa a URL do Alura
            try
            {
                roboNavegador.navigateTO("https://www.alura.com.br", driver);

            }catch (Exception ex)
            {
                Console.WriteLine("Erro ao acessar o site: "+ ex.Message);
                //Ao estourar alguma excessao reinicio o Loop;
                Executar();
            }
        }
        private void procuraCurso(WebDriver driver, roboNavegador roboNavegador, Utils utils)
        {
            try
            {
                roboNavegador.SendKeysToBox(driver, "header-barraBusca-form-campoBusca", "Curso RPA: automatize processos com ferramentos No/Low Code");

                roboNavegador.clickButtonByXpath(driver, "/html/body/main/section[1]/header/div/nav/div[2]/form/button");

                roboNavegador.clickButtonByXpath(driver, "/html/body/div[2]/div[2]/section/ul/li[1]");
            }catch(Exception ex) {
                //Ao estourar algum erro, reinicio o Loop, antes realizo um refresh na pagina atual.
                Console.WriteLine("Erro ao procurar curso: " + ex.Message);
                utils.refreshPagina(driver);
                Executar();
            }
            
        }
        public void preencheDadosAlura(WebDriver driver, roboNavegador roboNavegador, RoboAlura dadosalura, Utils utils)
        {
            try
            {
                //Busco os dados, e repasso os mesmos para variaveis para posteriormente persistir no banco.
                string titulo = roboNavegador.findElementByXpath("/html/body/section[1]/div/div[1]/h1", driver).Text;
                string CargaHoraria = roboNavegador.findElementByXpath("/html/body/section[1]/div/div[2]/div[1]/div/div[1]/div/p[1]", driver).Text;
                string Professor = roboNavegador.findElementByXpath("/html/body/section[2]/div[1]/section/div/div/div/h3", driver).Text;

                //Para a descrição do curso, como não está em uma unica DIV, tive de implementar um processamento
                string Descricao = roboNavegador.processaDetalhesCurso(driver, "/html/body/section[2]/div[1]/div/ul/li");
                string Link = driver.Url;
                dadosalura.preencheDadosAlura(titulo, Professor, CargaHoraria, Descricao, Link);
            }
            catch(Exception ex)
            {   //Ao estourar algum erro, reinicio o Loop, antes realizo um refresh na pagina atual.
                Console.WriteLine("Erro ao buscar dados do curso: " + ex.Message);
                utils.refreshPagina(driver);
                Executar();
            }
        }

    }
}