using Microsoft.AspNetCore.Mvc;
using System.Data;
using ProjetMedicament.Models.MesExceptions;
using ProjetMedicament.Models.Persistance;
using System;
using ProjetMedicament.Models.Metier;

namespace ProjetMedicament.Models.DAO
{
    public class ServiceMedicament : Controller
    {
        public static DataTable GetListeMedicament()
        {
            DataTable mesMedicaments;
            Serreurs er = new Serreurs("Erreur sur lecture des Medicaments.", "Medicament.getListeMedicament()");
            try
            {
                string mysql = "SELECT medicament.id_medicament, medicament.id_famille, medicament.nom_commercial, type_individu.lib_type_individu, dosage.qte_dosage, dosage.unite_dosage, prescrire.posologie ";
                mysql += "FROM dosage, prescrire, type_individu, medicament ";
                mysql += "WHERE dosage.id_dosage = prescrire.id_dosage AND prescrire.id_type_individu = type_individu.id_type_individu AND prescrire.id_medicament = medicament.id_medicament ";
                


                mesMedicaments = DBInterface.Lecture(mysql, er);
                
                return mesMedicaments;
            }
            catch (MonException e) {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static Tuple<Medicament, Dosage, TypeIndividu, Prescrire> GetunMedicament(string id)
        {
            DataTable dt = null;
            Medicament unMedicament = null;
            Dosage unDosage = null;
            Prescrire unPrescrire = null;
            TypeIndividu unTypeIndividu = null;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga-getUnManga()");
            try
            {
                // Récupérer les informations du médicament
                string mysql = "SELECT medicament.id_medicament, medicament.id_famille, medicament.nom_commercial, type_individu.lib_type_individu, dosage.qte_dosage, dosage.unite_dosage, prescrire.posologie ";
                mysql += "FROM dosage, prescrire, type_individu, medicament ";
                mysql += "WHERE dosage.id_dosage = prescrire.id_dosage AND prescrire.id_type_individu = type_individu.id_type_individu AND prescrire.id_medicament = medicament.id_medicament AND id_medicament = " + id;

                dt = DBInterface.Lecture(mysql, er);

                if (dt != null && dt.Rows.Count > 0)
                {
                    unMedicament = new Medicament();
                    DataRow dataRow = dt.Rows[0];
                    unMedicament.Id_medicament = int.Parse(dataRow[0].ToString());
                    unMedicament.Depot_legal = dataRow[1].ToString();
                    unMedicament.Id_famille = int.Parse(dataRow[2].ToString());
                    unMedicament.Nom_commercial = dataRow[3].ToString();
                    unMedicament.Effets = dataRow[4].ToString();
                    unMedicament.Contre_indication = dataRow[5].ToString();
                    unMedicament.Prix_echantillon = Double.Parse(dataRow[6].ToString());

                    // Récupérer les informations de dosage, type d'individu et prescrire
                    // Vous devez implémenter la logique pour récupérer ces informations à partir de la base de données

                    return new Tuple<Medicament, Dosage, TypeIndividu, Prescrire>(unMedicament, unDosage, unTypeIndividu, unPrescrire);
                }
                else
                {
                    return null;
                }
            }
            catch (MonException e)
            {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static void UpdateMedicament(Medicament unM,Dosage unD,TypeIndividu unT, Prescrire unP)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un Medicament.", "Medicament.update()");
            string requete = "UPDATE dosage " +
                             "SET qte_dosage = " + unD.Qte_dosage + ", " +
                                 "unite_dosage = " + unD.Unite_dosage + ", " +
                                 "posologie = " + unP.Posologie + " " +
                             "FROM dosage ";

            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }

    }
}
