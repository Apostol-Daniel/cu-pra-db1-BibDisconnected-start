using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.IO;
using System.Runtime.InteropServices;

namespace DbBibliotheek.wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DataSet dsBoekenLijst = new DataSet("Bibliotheek");
        private void MakeTables() 
        {
            //create Auteur DataTable
            DataTable dtAuteur;
            dtAuteur = new DataTable();
            dsBoekenLijst.Tables.Add(dtAuteur);
            dtAuteur.TableName = "Auteur";

            DataColumn dcAuteurID = new DataColumn();
            dcAuteurID.ColumnName = "auteurID";
            dcAuteurID.DataType = typeof(int);
            dcAuteurID.AutoIncrement = true;
            dcAuteurID.AutoIncrementSeed = 1;
            dcAuteurID.AutoIncrementStep = 1;

            dcAuteurID.Unique = true;
            dtAuteur.Columns.Add(dcAuteurID);
            dtAuteur.PrimaryKey = new DataColumn[] { dcAuteurID };


            DataColumn dcAuteurNaam = new DataColumn();
            dcAuteurNaam.ColumnName = "auteurNaam";
            dcAuteurNaam.DataType = typeof(string);
            dcAuteurNaam.MaxLength = 50;
            dtAuteur.Columns.Add(dcAuteurNaam);

            //dtAuteur.Columns.Add(dcAuteurNaam) has to be written befote the 3 lines under
            // otherwise dcDisplayAuteur cannot find dcAuteurNaam

            DataColumn dcDisplayAuteur = new DataColumn("DisplayAuteur");
            dcDisplayAuteur.Expression = "auteurNaam + ' (' + auteurID + ')'";
            dtAuteur.Columns.Add(dcDisplayAuteur);

            //create Uitgever DataTable

            DataTable dtUitgever = new DataTable();
            dtUitgever.TableName = "Uitgever";
            dsBoekenLijst.Tables.Add(dtUitgever);

            DataColumn dcUitgeverID = new DataColumn();
            dcUitgeverID.ColumnName = "uitgeverID";
            dcUitgeverID.DataType = typeof(int);
            dcUitgeverID.AutoIncrement = true;
            dcUitgeverID.AutoIncrementSeed = 1;
            dcUitgeverID.AutoIncrementStep = 1;
            dcUitgeverID.Unique = true;

            DataColumn dcUitgeverNaam = new DataColumn();
            dcUitgeverID.ColumnName = "uitgeverNaam";
            dcUitgeverID.DataType = typeof(string);
            dcUitgeverID.MaxLength = 50;

            dtUitgever.Columns.Add(dcUitgeverID);
            dtUitgever.Columns.Add(dcUitgeverNaam);
            dtUitgever.PrimaryKey = new DataColumn[] { dcUitgeverID };

            //create books

            DataTable dtBoeken = new DataTable();
            dtBoeken.TableName = "Boeken";
            dsBoekenLijst.Tables.Add(dtBoeken);

            DataColumn dcBoekID = new DataColumn();
            dcBoekID.ColumnName = "boekID";
            dcBoekID.DataType = typeof(int);
            dcBoekID.AutoIncrement = true;
            dcBoekID.AutoIncrementSeed = 1;
            dcBoekID.AutoIncrementStep = 1;
            dcBoekID.Unique = true;
            dtBoeken.Columns.Add(dcBoekID);
            dtBoeken.PrimaryKey = new DataColumn[] { dcBoekID };

            // add the rest of the book fields
            dtBoeken.Columns.Add("Titel", typeof(string));
            dtBoeken.Columns.Add("AuteurID", typeof(int));
            dtBoeken.Columns.Add("UitgeverID", typeof(int));
            dtBoeken.Columns.Add("Jaartal", typeof(int));


        }

        private void FillTables() 
        {
            AddAuteurData("Boon Louis");
            AddAuteurData("Tuchman Barbara");
            AddAuteurData("Cook Robin");
            AddPublisherData("AW Bruna");
            AddPublisherData("Luttingh");
        }

        private void AddAuteurData(string naam) 
        {
            DataRow nieuweAuteur = dsBoekenLijst.Tables["Auteur"].NewRow();
            nieuweAuteur["auteurNaam"] = naam;
            dsBoekenLijst.Tables["Auteur"].Rows.Add(nieuweAuteur);
        }

        private void AddPublisherData(string naam) 
        {
            DataRow newPublisher = dsBoekenLijst.Tables["Uitgever"].NewRow();
            newPublisher["UitgeverNaam"] = naam;
            dsBoekenLijst.Tables["Uitgever"].Rows.Add(newPublisher);
        }

        private void EditSourceData() 
        {
            cmbAuteur.Items.Clear();
            cmbUitgever.Items.Clear();
            ComboBoxItem itm;
            for (int counter = 0; counter < dsBoekenLijst.Tables[0].Rows.Count; counter++) 
            {
                itm = new ComboBoxItem();
                itm.Content = (dsBoekenLijst.Tables[0].Rows[counter][1]);
                itm.Tag = (dsBoekenLijst.Tables[0].Rows[counter][0]);
                cmbAuteur.Items.Add(itm);
            }
            cmbAuteur.SelectedIndex = 0;

            for(int counter = 0; counter < dsBoekenLijst.Tables[1].Rows.Count;counter++) 
            {
                itm = new ComboBoxItem();
                itm.Content = dsBoekenLijst.Tables[1].Rows[counter][1];
                itm.Tag = dsBoekenLijst.Tables[1].Rows[counter][0];
                cmbUitgever.Items.Add(itm);
            }
            cmbUitgever.SelectedIndex = 0;
        }

        private void AddBook(string titel, int auteurID, int uitgverID, int jaartal) 
        {
            DataRow newBook = dsBoekenLijst.Tables[2].NewRow();
            newBook["Titel"] = titel;
            newBook["AuteurID"] = auteurID;
            newBook["UitgeverID"] = uitgverID;
            newBook["Jaartal"] = jaartal;
            dsBoekenLijst.Tables[2].Rows.Add(newBook);
        }


        public MainWindow()
        {
            InitializeComponent();
        }

        
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MakeTables();
            FillTables();
            dgAuteur.ItemsSource = dsBoekenLijst.Tables["Auteur"].DefaultView;
            EditSourceData();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
        }
        private void btnSorteer_Click(object sender, RoutedEventArgs e)
        {
            DataView sortedTable = new DataView(dsBoekenLijst.Tables["Auteur"]);
            sortedTable.Sort = "AuteurNaam desc , AuteurID desc";
            dgAuteur.ItemsSource = sortedTable;
        }
        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            DataView sortedTable = new DataView(dsBoekenLijst.Tables["Auteur"]);
            sortedTable.RowFilter = "AuteurNaam like 'T%'";
            dgAuteur.ItemsSource = sortedTable;
        }
        private void btnToevoegen_Click(object sender, RoutedEventArgs e)
        {
            string auteur = txtAuteur.Text.Trim();
            AddAuteurData(auteur);
            dgAuteur.ItemsSource = dsBoekenLijst.Tables["Auteur"].DefaultView;
        }
        private void btnBoekToevoegen_Click(object sender, RoutedEventArgs e)
        {
            try 
            {
                string titel = txtTitel.Text;
                int jaartal = int.Parse(txtJaartal.Text);
                ComboBoxItem itm;
                itm = (ComboBoxItem)cmbAuteur.SelectedItem;
                int IDauteur = int.Parse(itm.Tag.ToString());
                itm = (ComboBoxItem)cmbUitgever.SelectedItem;
                int IDuitgever = int.Parse(itm.Tag.ToString());
                AddBook(titel, IDauteur, IDuitgever, jaartal);
                dgBoek.ItemsSource = dsBoekenLijst.Tables[2].DefaultView;
            }
            catch (Exception ex) 
            {
                MessageBox.Show("Foute ingave\nReden:" + ex.Message);
            }
        }
        private void btnVerwijderAuteur_Click(object sender, RoutedEventArgs e)
        {
            if (dgAuteur.SelectedIndex > -1) 
            {
            }
        }





    }
}
