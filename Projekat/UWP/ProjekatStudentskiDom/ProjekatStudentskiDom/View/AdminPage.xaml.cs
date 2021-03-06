﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using ProjekatStudentskiDom.Klase;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace ProjekatStudentskiDom
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminPage : Page
    {
        private StudentskiDom sd;
        public AdminPage(StudentskiDom sd)
        {
            this.InitializeComponent();
            this.sd = sd;
            novostValidacija.Opacity = 0;
            if (sd.Admin().Pol == 'M')
            {
                dobrodosaoAdmin.Text = "Dobrodošao, " + sd.Admin().Ime + "e!";
            }
            else
            {
                dobrodosaoAdmin.Text = "Dobrodošla, " + sd.Admin().Ime + "!";
            }
            comboUposlenici.Items.Add("Blagajnik");
            comboUposlenici.Items.Add("Kuhar");
            comboUposlenici.Items.Add("Šef restorana");
            comboUposlenici.Items.Add("Konobar");
            comboUposlenici.Items.Add("Majstor");
            comboUposlenici.SelectedIndex = 0;
            List<string> novosti = sd.dajNovosti();
            foreach (string novost in novosti)
            {
                listaNovosti.Items.Add(novost);
            }
        }

        private void pregled_Click(object sender, RoutedEventArgs e)
        {
            Page adminPregled = new AdminPregledClanova(sd);
            this.Content = adminPregled;
        }

        private void registracija_Click(object sender, RoutedEventArgs e)
        {
            if (student.IsChecked==true)
            {
                Page studentRegistracija = new RegistracijaStudenta(sd);
                this.Content = studentRegistracija;
            }
            else
            {
                if((string)comboUposlenici.SelectedItem=="Blagajnik")
                {
                    Page blagajnikRegistracija = new RegistracijaBlagajnika(sd);
                    this.Content = blagajnikRegistracija;
                }

                if ((string)comboUposlenici.SelectedItem == "Kuhar")
                {
                    Page kuharRegistracija = new RegistracijaKuhara(sd);
                    this.Content = kuharRegistracija;
                }

                if ((string)comboUposlenici.SelectedItem == "Šef restorana")
                {
                    Page sefRestorana = new RegistracijaSefaRestorana(sd);
                    this.Content = sefRestorana;
                }

                if ((string)comboUposlenici.SelectedItem == "Konobar")
                {
                    Page konobarRegistracija = new RegistracijaKonobara(sd);
                    this.Content = konobarRegistracija;
                }

                if ((string)comboUposlenici.SelectedItem == "Majstor")
                {
                    Page majstorRegistracija = new RegistracijaMajstora(sd);
                    this.Content = majstorRegistracija;
                }
            }
        }

        private void dodajNovost_Click(object sender, RoutedEventArgs e)
        {
            if(novostText.Text.Length>0)
            {
                listaNovosti.Items.Add(novostText.Text);
                novostValidacija.Opacity = 0;
                sd.dodajNovost(novostText.Text);
                novostText.Text = "";
            }
            else
            {
                novostValidacija.Opacity = 100;
            }
        }

        private void odjava_Click(object sender, RoutedEventArgs e)
        {
            Page main = new MainPage(sd);
            this.Content = main;
        }

        private void blagajnik_Click(object sender, RoutedEventArgs e)
        {
            comboUposlenici.IsEnabled = true;
        }

        private void student_Click(object sender, RoutedEventArgs e)
        {
            comboUposlenici.IsEnabled = false;
        }

        private void listaNovosti_ItemClick(object sender, ItemClickEventArgs e)
        {
            for (int i = listaNovosti.Items.Count - 1; i >= 0; i--)
            {
                if (listaNovosti.Items[i] == listaNovosti.SelectedItem)
                {
                    listaNovosti.Items.RemoveAt(i);
                    sd.obrisiNovost(i);
                }
            }
        }
    }
}
