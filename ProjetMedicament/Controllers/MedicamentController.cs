using Microsoft.AspNetCore.Mvc;
using ProjetMedicament.Models.DAO;
using ProjetMedicament.Models.MesExceptions;
using ProjetMedicament.Models.Metier;

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

        public IActionResult Ajouter()
        {
            var resultat = new Resultat(); // Initialisez un nouvel objet Resultat
            return View(resultat); // Passez le modèle à la vue
        }


        [HttpPost]
        public IActionResult Ajouter(Resultat unR)
        {
            try
            {
                ServiceMedicament.InsertMedicament(unR);
                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de l'ajout du médicament : " + e.Message);
                return View(unR);
            }
        }

        public IActionResult Modifier(string id)
        {
            Resultat infosMedicament = null;
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
        public IActionResult Modifier(Resultat unR)
        {
            try
            {
                ServiceMedicament.UpdateMedicament(unR);
                return RedirectToAction("Index");
            }
            catch (MonException e)
            {
                ModelState.AddModelError("Erreur", "Erreur lors de la modification du médicament : " + e.Message);
                return View(unR);
            }
        }
    }
}
