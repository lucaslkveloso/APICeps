using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using ASPNET.Aula02Manha.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace ASPNET.Aula02Manha.Controllers
{
    public class CepController : Controller
    {
        private readonly Context _context;
        public CepController(Context context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var emps = _context.Ceps.ToList();
            return View(emps);
        }

        [HttpPost]
        public IActionResult PesquisarCep(string cep)
        {
            string url = $@"https://viacep.com.br/ws/{cep}/json/";

            WebClient client = new WebClient();
            var test =  client.DownloadString(url);

            _Cep cepx = JsonConvert.DeserializeObject<_Cep>(test);

            var aux = from a in _context.Ceps
                      where a.Cep == cepx.Cep
                      select a;
            var cadastro = aux.FirstOrDefault();
            if (cadastro == null)
            {

                _context.Add(cepx);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewData["Message"] = "Cep já cadastrado";
                return RedirectToAction("Index");
            }
        }
    }
}