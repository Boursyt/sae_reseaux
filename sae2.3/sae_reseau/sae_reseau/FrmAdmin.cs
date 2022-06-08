using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CLassLibrairieBDD;
using MySql.Data.MySqlClient;
using MySql.Data;

namespace CLassLibrairieBDD
{
    public partial class FrmAdmin : Form
    {
        public static class Global
        {
            public static string sql;
        }

        public FrmAdmin()
        {
            InitializeComponent();
        }

        private void cmdValider_Click(object sender, EventArgs e)//Evénement qui se déclenche lors d'un clic sur le bouton valider
        {
            if (cbxTable.SelectedIndex == -1)//Si rien n'est sélectionné dans la combobox
            {
                MessageBox.Show("Veuillez sélectionner une table");//On affiche un message d'erreur
            }
            else
            {
                lbl_nomtable.Text = "Table : " + cbxTable.Text;//On affiche le nom de la table au dessus de la listbox
                lbl_nomtable.Visible = true;//On rend visible le label

                string table, sql;
                table = cbxTable.Text;//La variable table prend comme valeur la table sélectionner dans la combobox

                sql = $"select * from {table}"; //On met la commande SQL à éxecuter dans la variable sql
                MySqlCommand cmd = new MySqlCommand(sql, BDD.macnx);//On execute la commande sql
                listBox_contenutable.Items.Clear();//On vide la listbox

                MySqlDataReader rdr = cmd.ExecuteReader();//On récupére le résultat de la commande SQL

                if (rdr.HasRows)//Si le résultat de la commande SQL a des colonnes donc si la commande retourne quelque chose
                {
                    while (rdr.Read())//Tant que rdr lit des choses dans la base
                    {
                        string chaine;//On déclare la variable qui va contenir ce que l'on va afficher dans la listbox
                        if (table == "Horaire")//Si la table sélectionner est Horaire
                        {
                            chaine = rdr[0].ToString() + "  " + rdr[1].ToString() + "  " + rdr[2].ToString() + "  " + rdr[3].ToString() + "  " + rdr[4].ToString() + "  " + rdr[5].ToString();//On affiche la colonne 1,2,3,4 et 5 de la table
                            listBox_contenutable.Items.Add(chaine);//On ajoute à la listbox le contenu de chaîne
                        }
                        if (table == "Arret" || table == "Image" || table == "Ligne")//Si la table sélectionner est Arrêt, Image ou Ligne
                        {
                            chaine = rdr[0].ToString() + "  " + rdr[1].ToString() + "  " + rdr[2].ToString();//On affiche la colonne 1,2,3,4 et 5 de la table
                            listBox_contenutable.Items.Add(chaine);//On ajoute à la listbow le contenu de chaîne
                        }
                        if (table == "Passage")//Si la table sélectionner est Passage
                        {
                            chaine = rdr[0].ToString() + "  " + rdr[1].ToString() + "  " + rdr[2].ToString() + "  " + rdr[3].ToString() + "  " + rdr[4].ToString();//On affiche la colonne 1,2,3,4 et 5 de la table
                            listBox_contenutable.Items.Add(chaine);//On ajoute à la listbow le contenu de chaîne
                        }
                    }
                }
                rdr.Close();//On libére la mémoire
                cmd.Dispose();//On libère la mémoire

                cmdAjouter.Enabled = true;//On active le bouton ajouter
                cmdModifier.Enabled = true;//On active le bouton modifier
                cmdSupr.Enabled = true;//On active le bouton supprimer
            }



        }


        private void masquerEntrer()
        {
            txt_admin1.Location = new Point(145, txt_admin1.Location.Y);//On change l'emplacement de la textbox

            lbl_admin1.Visible = false;//On rend le label visible
            lbl_admin2.Visible = false;
            lbl_admin3.Visible = false;
            lbl_admin4.Visible = false;
            lbl_admin5.Visible = false;

            txt_admin1.Visible = false;//On rend la textbox visible
            txt_admin2.Visible = false;


        }


        private void CmdAjouter_Click(object sender, EventArgs e)
        {

            masquerEntrer();//On masque tout les labels

            lbl_admin1.Text = $"INSERT INTO {cbxTable.Text} (";//On change le texte de tout les labels
            lbl_admin2.Text = ")";
            lbl_admin3.Text = " VALUES (";
            lbl_admin5.Text = ")";

            txt_admin2.Size = new Size(500, txt_admin2.Size.Height);//On change la position de la textbox2

            txt_admin1.ForeColor = Color.Blue;//On change la couleur de la saisie en gris
            txt_admin2.ForeColor = Color.Blue;
            txt_admin1.Text = "Entrer le nom des champs ";//On change le texte des textbox
            txt_admin2.Text = "Entrer la valeur des champs";

            lbl_admin1.Visible = true;//On rend visible tout les labels
            lbl_admin2.Visible = true;
            lbl_admin3.Visible = true;
            lbl_admin5.Visible = true;
            txt_admin1.Visible = true;
            txt_admin2.Visible = true;
        }

        private void CmdModifier_Click(object sender, EventArgs e)
        {

            masquerEntrer();//On masque tout les labels

            lbl_admin1.Text = $"UPDATE {cbxTable.Text} SET ";// On change le texte de tout les labels
            lbl_admin3.Text = "WHERE ";

            txt_admin2.Size = new Size(500, txt_admin2.Size.Height);//On change la position de la textbox2

            txt_admin1.ForeColor = Color.Gray;//On change la couleur de la saisie en gris
            txt_admin2.ForeColor = Color.Gray;
            txt_admin1.Text = "Entrer le nom des champs et sa nouvelle valeur";//On change le texte des textbox
            txt_admin2.Text = "Entrer la condition de selection";

            lbl_admin1.Visible = true;//On rend visible tout les labels
            lbl_admin3.Visible = true;
            txt_admin1.Visible = true;
            txt_admin2.Visible = true;

        }

        private void CmdSupr_Click(object sender, EventArgs e)
        {

            masquerEntrer();//On masque tout les labels

            txt_admin1.Location = new Point(180, txt_admin1.Location.Y);//On change la position de la textbox2

            lbl_admin1.Text = $"DELETE FROM {cbxTable.Text} WHERE";//On change le texte du label1


            txt_admin1.ForeColor = Color.Red;//On change la couleur de la saisie en gris
            txt_admin1.Text = "Entrer la condition de selection";//On change le texte da la textbox1

            lbl_admin1.Visible = true;//On rend le label visible
            txt_admin1.Visible = true;//On rend la textbox visible

        }

        private void cmdButtonSQLValide_Click(object sender, EventArgs e)
        {
            if (txt_admin1.ForeColor == Color.Blue)//Si le mode sélectionner est "ajouter"
            {
                Global.sql = lbl_admin1.Text + txt_admin1.Text + lbl_admin2.Text + lbl_admin3.Text + txt_admin2.Text + lbl_admin5.Text + ";";//On prend la saisie utilisateur pour la contenir dans une variable
            }
            if (txt_admin1.ForeColor == Color.Gray)//Si le mode sélectionner est "modifier"
            {
                Global.sql = lbl_admin1.Text + txt_admin1.Text + " " + lbl_admin3.Text + txt_admin2.Text + ';';//On prend la saisie utilisateur pour la contenir dans une variable
            }
            if (txt_admin1.ForeColor == Color.Red)//Si le mode sélectionner est "supprimer"
            {
                Global.sql = lbl_admin1.Text + " " + txt_admin1.Text + ';';//On prend la saisie utilisateur pour la contenir dans une variable
            }
            lbl_admin4.Text = Global.sql;//on affiche la commande SQL final à l'utilisateur
            lbl_admin4.Visible = true;//On rend visible le label 4

            MySqlCommand cmd = new MySqlCommand(Global.sql, BDD.macnx);//On execute la commande sql
            MySqlDataReader rdr = cmd.ExecuteReader();//On récupére le résultat de la commande SQL

            rdr.Close();//On libére la mémoire
            cmd.Dispose();//On libère la mémoire

        }

        private void textBox1_Click(object sender, EventArgs e)
        {
            txt_admin1.Text = "";//Cache le texte présent dans la textbox1 lors de la saisie
        }

        private void textBox2_Click(object sender, EventArgs e)
        {
            txt_admin2.Text = "";//Cache le texte présent dans la textbox1 lors de la saisie
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {
               bool ConnexionOuverte;

            ConnexionOuverte = BDD.initConnexion();//On ouvre la connection avec la base de données

            if (ConnexionOuverte)//Si le bool ConnexionOuverte est vrai donc que la connexion est un succés
            {
                //La connexion est un succés
            }
            else
            {
                MessageBox.Show("La connexion à la base de donnée a échoué.");//On affiche un message d'erreur si la connexion a échoué
            }
        }
    }

}
