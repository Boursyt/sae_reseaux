using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CLassLibrairieBDD
{
    public class BDD
    {

        public static MySqlConnection macnx;


        public static bool initConnexion()
        {
            string serveur = "10.1.139.236";//On stock l'ip du serveur
            string login = "c1";//On stock le login
            string passwd = "mdp";//On stock le mot de passe
            string BD = "basec1";//On stock le nom de la base
            string chaineDeConnexion = $"server={serveur};uid={login};pwd={passwd};database={BD}";//On stock la commande permettant de se connecter à la base

            macnx = new MySqlConnection(chaineDeConnexion);//On crée un variable SQL

            try
            {
                macnx.Open();//On ouvre la connexion
                return true;//On retourne vrai
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());//Si il y a une erreur on affiche l'erreur
                return false;//On retourne faux
            }
        }

        public static bool fermerConnexion()
        {
            try
            {
                if (macnx.State == System.Data.ConnectionState.Open)//Si la connexion est ouverte
                {
                    macnx.Close();//On ferme la connexion
                    macnx.Dispose();//On libére la mémoire
                }

                return true;//On retrourne vrai
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());//On affiche l'erreur
                return false;//On retourne faux
            }

        }



        public static string imageBDD(int numimage)//Fonctions qui permet de retourner l'url de l'image voulu
        {
            string sql;//Varaible qui va contenir la commande SQL
            string url = " ";//Variable qui va contenir l'url de l'image
            sql = $"select * from Image"; //commande SQL qui selectionne tout ce que contient la table Image
            MySqlCommand cmd = new MySqlCommand(sql, macnx); //exectution de la commande SQL
            MySqlDataReader rdr = cmd.ExecuteReader(); //lecture du resultat de la commande
            int i = 1; //variable qui sert de compteur de ligne dans la table 
            while (rdr.Read())//lis les lignes de la table SQL une par une
            {
                if (i == numimage)//si on tombe sur l'image choisie on fait
                {
                    url = rdr.GetString(2);//renvoi de l'url de l'image choisie
                    break;//sorti de boucle
                }
                i++;//passage à la ligne suivante
            }
            rdr.Close();//fermeture de la lecture
            cmd.Dispose();//fermeture de l'execution
            return url;//renvoi l'url de l'image 
        }

        public static bool executeSQL(string sql)//Fonction qui permet d'éxecuter une commande SQL
        {
            try // gestion d'erreur de l'execution de la commande SQL
            {
                MySqlCommand cmd = new MySqlCommand(sql, macnx); //execute la commande SQL en creant une connection
                cmd.Dispose(); //fermeture la connection
                return true; //retourne la valeur faux
            }
            catch (Exception ex)//si il y'a une erreur dans l'execution
            {
                Console.WriteLine(ex.ToString());//affichage de l'erreur dans la console
                return false; // retourne la valeur faux 
            }
        }
       
    }
}
