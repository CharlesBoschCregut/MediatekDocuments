using TechTalk.SpecFlow;
using MediaTekDocuments.view;
using System.Windows.Forms;
using NUnit.Framework;

namespace SpecFlowMediatekDocuments.Steps
{
    [Binding]
    public class RechercheLivreTitreSteps
    {
        private FrmMediatek frmMediatek;
        
        [Given(@"je saisis la valeur ""(.*)""")]
        public void GivenJeSaisisLaValeur(string nom)
        {
            frmMediatek = new FrmMediatek("111");
            TextBox txbLivresNumRecherche = (TextBox)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["txbLivresNumRecherche"];
            TabControl tabonglet = (TabControl)frmMediatek.Controls["tabOngletsApplication"];
            frmMediatek.Visible = true;
            tabonglet.SelectedTab = (TabPage)tabonglet.Controls["tabLivres"];
            txbLivresNumRecherche.Text = nom;
        }
        
        [Then(@"le nombre de colonne est (.*)")]
        public void ThenLIdDuLivreObtenuEst(int cols)
        {
            DataGridView dgv = (DataGridView)frmMediatek.Controls["tabOngletsApplication"].Controls["tabLivres"].Controls["grpLivresRecherche"].Controls["dgvLivresListe"];
            Assert.AreEqual(dgv.Rows.Count, cols);
        }
    }
}
