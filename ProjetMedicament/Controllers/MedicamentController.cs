using Microsoft.AspNetCore.Mvc;
using ProjetMedicament.Models.DAO;
using ProjetMedicament.Models.MesExceptions;
using ProjetMedicament.Models.Metier;
using System;

namespace ProjetMedicament.Controllers
{
    public class MedicamentController : Controller
    {
        public IActionResult Index()
        {
            System.Data.DataTable mesMedicament = null;

            try
            {
                mesMedicament = ServiceMedicament.GetListeMedicament();
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la récupération des medicaments : " + e.Message);
            }
            return View(mesMedicament);
        }

        public IActionResult Modifier(string id)
        {
            Tuple<Medicament, Dosage, TypeIndividu, Prescrire> infosMedicament = null;

            try
            {
                infosMedicament = ServiceMedicament.GetunMedicament(id);
                if (infosMedicament == null)
                {
                    return NotFound();
                }
                return View(infosMedicament);
            }
            catch (MonException e)
            {
                return NotFound();
            }
        }


        [HttpPost]
        public IActionResult Modifier(Medicament unM, Dosage unD, TypeIndividu unT, Prescrire unP)
        {
            try
            {
                ServiceMedicament.UpdateMedicament(unM, unD, unT, unP);
                return View();
            }
            catch (MonException e)
            {
                return NotFound();

            }
        }

    }

}
