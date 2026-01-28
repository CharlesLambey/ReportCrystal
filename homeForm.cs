using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using CrystalDecisions.Windows.Forms;
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
            InitializeComponent();
        }

        private void btnLoadReport_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Crystal Report Files (*.rpt)|*.rpt";
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                ReportDocument reportDocument = new ReportDocument();
                reportDocument.Load(openFileDialog.FileName);

                string serverName = ConfigurationManager.AppSettings.Get("serverName");
                string databaseName = ConfigurationManager.AppSettings["DatabaseName"];
                string userId = ConfigurationManager.AppSettings["UserID"];
                string password = ConfigurationManager.AppSettings["Password"];

                bool trustedConnection = bool.Parse(ConfigurationManager.AppSettings["TrustedConnection"]);

                foreach (Table table in reportDocument.Database.Tables)
                {
                    TableLogOnInfo logOnInfo = table.LogOnInfo;
                    logOnInfo.ConnectionInfo.ServerName = serverName;
                    logOnInfo.ConnectionInfo.DatabaseName = databaseName;

                    if (trustedConnection)
                    {
                        logOnInfo.ConnectionInfo.IntegratedSecurity = true;
                    }
                    else
                    {
                        logOnInfo.ConnectionInfo.UserID = ConfigurationManager.AppSettings["UserID"];
                        logOnInfo.ConnectionInfo.Password = ConfigurationManager.AppSettings["Password"];
                    }
                    table.ApplyLogOnInfo(logOnInfo);
                }

                crystalReportViewer.ReportSource = reportDocument;
            }
        }
    }
}
