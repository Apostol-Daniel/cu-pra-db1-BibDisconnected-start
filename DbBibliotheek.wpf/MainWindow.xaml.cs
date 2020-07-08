﻿using System;
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


        }

        private void FillTables() 
        {
            AddAuteurData("Boon Louis");
            AddAuteurData("Tuchman Barbara");
            AddAuteurData("Cook Robin");
        }

        private void AddAuteurData(string naam) 
        {
            DataRow nieuweAuteur = dsBoekenLijst.Tables["Auteur"].NewRow();
            nieuweAuteur["auteurNaam"] = naam;
            dsBoekenLijst.Tables["Auteur"].Rows.Add(nieuweAuteur);
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
        }
        private void btnVerwijderAuteur_Click(object sender, RoutedEventArgs e)
        {
            if (dgAuteur.SelectedIndex > -1) 
            {
            }
        }





    }
}
