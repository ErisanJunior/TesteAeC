using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AeC
{
    public class RoboAlura
    {
        //Model para salvar os dados extraidos.
        public int id { get; set; }
        public string Titulo { get; set; }
        public string Professor { get; set; }
        public string CargaHoraria { get; set; }
        public string Descricao { get; set; }
        public string Link { get; set; }
        public RoboAlura() { }

        //Persistencia no banco
        public void preencheDadosAlura(string titulo, string professor, string cargaHoraria, string descricao, string link)
        {
            using (var db = new MyContext())
            {
                db.RoboAlura.Add(new RoboAlura
                {
                    Titulo = titulo,
                    CargaHoraria = professor,
                    Professor = cargaHoraria,
                    Descricao = descricao,
                    Link = link
                });
                db.SaveChanges();
            }
        }
    }
}