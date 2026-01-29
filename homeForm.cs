using CrystalDecisions.CrystalReports.Engine; // Classes principales pour charger et manipuler les fichiers Crystal Reports
using CrystalDecisions.Shared;                // Classes partagées (paramètres, connexion, etc.)
using System.Configuration;                   // Permet de lire les valeurs du fichier app.config
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ReportCrystal
{
    public partial class homeForm : Form
    {
        public homeForm()
        {
            InitializeComponent(); // Initialise les composants du formulaire
        }

        /// <summary>
        /// Événement déclenché lors du clic sur le bouton "Charger un rapport".
        /// Permet de sélectionner un fichier .rpt, de le charger et d’appliquer les paramètres de connexion SQL.
        /// </summary>

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            // Ouvre une boîte de dialogue permettant de sélectionner un fichier Crystal Report (.rpt)
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Crystal Report Files (*.rpt)|*.rpt";

            // Si l'utilisateur choisit un fichier...
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                // Chargement du fichier Crystal Report choisi
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(openFileDialog.FileName);

                // Récupération des paramètres de connexion depuis app.config
                string serverName = ConfigurationManager.AppSettings.Get("serverName");
                string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
                string userId = ConfigurationManager.AppSettings["UserID"];
                string password = ConfigurationManager.AppSettings["Password"];
                bool trustedConnection = bool.Parse(ConfigurationManager.AppSettings["TrustedConnection"]);

                // Parcours de toutes les tables utilisées dans le rapport Crystal
                foreach (Table table in reportDocument.Database.Tables)
                {
                    // Récupération des informations de connexion actuelles de la table
                    TableLogOnInfo logOnInfo = table.LogOnInfo;

                    // Application du serveur et de la base de données définis dans app.config
                    logOnInfo.ConnectionInfo.ServerName = serverName;
                    logOnInfo.ConnectionInfo.DatabaseName = databaseName;

                    // Gestion du mode d’authentification
                    if (trustedConnection)
                    {
                        // Connexion Windows (SSPI)
                        logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    }
                    else
                    {
                        // Connexion SQL Server classique
                        logOnInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["UserID"];
                        logOnInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["Password"];
                    }
                    table.ApplyLogOnInfo(logOnInfo);
                }

                // Affectation du rapport au CrystalReportViewer pour affichage
                crystalReportViewer.ReportSource = reportDocument;
            }
        }
    }
}
