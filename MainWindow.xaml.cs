using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    
    public partial class MainWindow : Window
    {
        public Piano MyPiano = new Piano(7) { Visibility = Visibility.Hidden,IsDisabled=true};
        public Piano PlayerPiano = new Piano(14) { Visibility = Visibility.Visible,IsDisabled=false};
        public Akord MyAkord = null;
        public Random b = new Random();
        public int akordIndex = 0;

        public MainWindow()
        {
            InitializeComponent();
            for (int i = 0; i < Akordy.AkordsList.Count; i++)
            {
                Button NewButton = new Button() { FontSize=16};
                NewButton.Click += OnClickOnButtonKnihovna;
                NewButton.Content = Akordy.AkordsList[i].Name;
                KnihovnaPanel.Children.Add(NewButton);
            }
            ShowKlavesyGrid.Children.Add(MyPiano);
            MainPage.Visibility = Visibility.Visible;
            GamePage.Children.Add(PlayerPiano);
            Grid.SetRow(PlayerPiano, 1);
            PlayerPiano.OnClickKlavesa = OnPianoCLicked;
            GenerateNew();
            Guitarname.Text = Akordy.AkordsList[0].Name;
            Switch(Akordy.AkordsList[akordIndex]);
        }

        //funkce je určena k zobrazení akordového rozkladu na klavírní oktávě v knihovně akordů
        void OnClickOnButtonKnihovna(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            for (int K = 0; K < Akordy.AkordsList.Count; K++)
            {
                if (button.Content.ToString() == Akordy.AkordsList[K].Name)
                {
                    MyPiano.Visibility = Visibility.Visible;
                    MyPiano.Hide();
                    MyPiano.Show(Akordy.AkordsList[K]);
                }
            }
        }

        // funkce KnihovnaAkordu, Play, GLibrary a Menu slouží k zobrazování a přiklikávání mezi jednotlivými hlavními Gridy
        
        //zobrazuje knihovnu s akordy
        private void KnihovnaAkordu(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            KnihovnaGrid.Visibility = Visibility.Visible;
        }

        //zobrazuje Grid s hrou a klavírní klávesnicí
        private void Play(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            GamePage.Visibility = Visibility.Visible;
            ProgressBarToNext.Value = 0;
        }

        //zobrazuje Grid s kytarovými akordy
        private void GLibrary(object sender, RoutedEventArgs e)
        {
            MainPage.Visibility = Visibility.Hidden;
            GuitarPage.Visibility = Visibility.Visible;
        }

        //zabrazuje úvodní Grid, vrací zpět do menu
        private void Menu(object sender, RoutedEventArgs e)
        {
            KnihovnaGrid.Visibility = Visibility.Hidden;
            GuitarPage.Visibility = Visibility.Hidden;
            GamePage.Visibility = Visibility.Hidden;
            MainPage.Visibility = Visibility.Visible;
            
        }

        // funkce Switchright a Switchleft jsou funkce tlačítek na stránce s kytarovými akordy, které načtou další akord v pořádí. Napravo nebo nalevo od zobrazeného
        private void Switchrigth(object sender, RoutedEventArgs e)
        {
            if (akordIndex >= Akordy.AkordsList.Count-1)
            {
                akordIndex = 0;
            }
            else
            {
                akordIndex++;
            }
            
            Switch(Akordy.AkordsList[akordIndex]);
        }

        private void Switchleft(object sender, RoutedEventArgs e)
        {
            if (akordIndex <= 0)
            {
                akordIndex = Akordy.AkordsList.Count - 1;
            }
            else
            {
                akordIndex--;
            }
            Switch(Akordy.AkordsList[akordIndex]);
        }

        //funkce Switch se stará o změnu akordu
        private void Switch(Akord akord)
        {
            Guitarname.Text = akord.Name;
            MyGuitar.ShowChords(akord);
        }

        //hlavni funkce hry, zpracovává klinutí uživatele na klávesy ve hře a následnou akci
        int Max = 0;
        void OnPianoCLicked(Klavesa a,RoutedEventArgs e,bool PreseNow)
        {
            if (!PreseNow)
            {
                if (Max < 3)
                {
                    Max++;
                }
                else
                {
                    PlayerPiano.Hide();
                    a.IsPressedKlavesa = true;
                    Max = 1;
                }
                if (Max>=3)
                {
                    if (Try())
                    {
                        PlayerPiano.Hide();
                        Max = 0;
                        IfKonec();
                        GenerateNew();
                    }
                }
            }
            else
            {
                Max--;
            }
        }

        //kontrola správnosti akordu zadaného uživatelem do klávesnice
        bool Try()
        {
            int NextGen=0;
            for (int i = 0; i < PlayerPiano.Klávesy.Count; i++)
            {
                if (PlayerPiano.Klávesy[i].IsPressedKlavesa)
                {
                    if (PlayerPiano.Klávesy[i].NotaName == MyAkord.Tony.Item1 | PlayerPiano.Klávesy[i].NotaName == MyAkord.Tony.Item2 | PlayerPiano.Klávesy[i].NotaName == MyAkord.Tony.Item3)
                    {
                        NextGen++;
                    }
                }
            }
            return NextGen >= 3;
        }

        //generování nového akordu
        void GenerateNew()
        {
            MyAkord = Akordy.AkordsList[b.Next(0, Akordy.AkordsList.Count - 1)];
            Kontent.Text = MyAkord.Name;
        }

        //funkce kontroluje, zda se hráč dostal na konec hry
        void IfKonec()
        {
            if (ProgressBarToNext.Value == ProgressBarToNext.Maximum - 1)
            {
                AsyncShow();
            }
            else
            {
                ProgressBarToNext.Value++;
            }
        }

        //zobrazuje konec hry- výherní napis , návrat do Úvodního Gridu
        async Task AsyncShow()
        {
            EndOfGame.Visibility = Visibility.Visible;
            GamePage.Visibility = Visibility.Hidden;
            Console.Beep(800, 300);
            Console.Beep(1200, 200);
            await Task.Delay(2500);
            

            MainPage.Visibility = Visibility.Visible;
            
            EndOfGame.Visibility = Visibility.Hidden;
        }      
    }
}
