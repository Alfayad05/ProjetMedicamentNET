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
                string mysql = "SELECT id_medicament, id_famille, nom_commercial, lib_type_individu, qte_dosage, unite_dosage, posologie ";
                mysql += "FROM resultat ";
                


                mesMedicaments = DBInterface.Lecture(mysql, er);
                
                return mesMedicaments;
            }
            catch (MonException e) {
                throw new MonException(er.MessageUtilisateur(), er.MessageApplication(), e.Message);
            }
        }

        public static Resultat GetunMedicament(string id)
        {
            DataTable dt = null;
            Resultat unResultat = null;
            Serreurs er = new Serreurs("Erreur sur lecture des Mangas", "ServiceManga-getUnManga()");
            try
            {
                // Récupérer les informations du médicament
                string mysql = "SELECT id_medicament, depot_legal, id_famille, nom_commercial, effets, contre_indication, prix_echantillon, id_type_individu, lib_type_individu, id_dosage, qte_dosage, unite_dosage, posologie ";
                mysql += "FROM resultat ";
                mysql += "WHERE id_type_individu = " + id;

                dt = DBInterface.Lecture(mysql, er);

                if (dt != null && dt.Rows.Count > 0)
                {
                    unResultat = new Resultat();
                    DataRow dataRow = dt.Rows[0];
                    unResultat.Id_medicament = int.Parse(dataRow[0].ToString());
                    unResultat.Depot_legal = dataRow[1].ToString();
                    unResultat.Id_famille = int.Parse(dataRow[2].ToString());
                    unResultat.Nom_commercial = dataRow[3].ToString();
                    unResultat.Effets = dataRow[4].ToString();
                    unResultat.Contre_indication = dataRow[5].ToString();
                    unResultat.Prix_echantillon = Double.Parse(dataRow[6].ToString());
                    unResultat.Id_type_individu = int.Parse(dataRow[7].ToString());
                    unResultat.Lib_type_individu = dataRow[8].ToString();
                    unResultat.Id_dosage = int.Parse(dataRow[9].ToString());
                    unResultat.Qte_dosage = int.Parse(dataRow[10].ToString());
                    unResultat.Unite_dosage = dataRow[11].ToString();
                    unResultat.Posologie = dataRow[12].ToString();

                    return unResultat;
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

        public static void UpdateMedicament(Resultat unR)
        {
            Serreurs er = new Serreurs("Erreur sur l'écriture d'un Medicament.", "Medicament.update()");
            string requete = "UPDATE resultat " +
                             "SET qte_dosage = " + unR.Qte_dosage + ", " +
                                 "unite_dosage = " + unR.Unite_dosage + ", " +
                                 "posologie = " + unR.Posologie + " " +
                             "FROM resultat ";

            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }

        public static void InsertMedicament(Resultat unR)
        {
            Serreurs er = new Serreurs("Erreur sur l'insertion d'un Medicament.", "Medicament.insert()");
            string requete = "INSERT INTO resultat (id_medicament, depot_legal, id_famille, nom_commercial, effets, contre_indication, prix_echantillon, id_type_individu, lib_type_individu, id_dosage, qte_dosage, unite_dosage, posologie) " +
                             "VALUES (" + unR.Id_medicament + ", '" +
                                 unR.Depot_legal + "', " +
                                 unR.Id_famille + ", '" +
                                 unR.Nom_commercial + "', '" +
                                 unR.Effets + "', '" +
                                 unR.Contre_indication + "', " +
                                 unR.Prix_echantillon + ", " +
                                 unR.Id_type_individu + ", '" +
                                 unR.Lib_type_individu + "', " +
                                 unR.Id_dosage + ", " +
                                 unR.Qte_dosage + ", '" +
                                 unR.Unite_dosage + "', '" +
                                 unR.Posologie + "')";

            try
            {
                DBInterface.Execute_Transaction(requete);
            }
            catch (MonException erreur)
            {
                throw erreur;
            }
        }


        public static void DeleteMedicament(Resultat unR)
        {
            Serreurs er = new Serreurs("Erreur sur la suppression d'un Medicament.", "Medicament.delete()");
            string requete = "DELETE FROM resultat " +
                             "WHERE id_medicament = " + unR.Id_medicament;

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
