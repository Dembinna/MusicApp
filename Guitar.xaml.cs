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

namespace MusicApp
{
    /// <summary>
    /// Interaction logic for Guitar.xaml
    /// </summary>
    public partial class Guitar : UserControl
    {
        //inicializace kytarového obrazu
        public Guitar()
        {
            InitializeComponent();
        }

        // funkce ShowChords se stará o zabrazení vybraného akordu uživatelem
        // dělá viditelné elipsy na souřadnicích uložených u každého akordu
        // -1 reprezentuje, že daná struna se na kytaře nehraje. Ve sloupci se nezobrazí žádný symbol
        // sloupců je šest, podle počtu strun. Tedy procházíme šest různých možných pozic
        public void ShowChords(Akord akord)
        { 
            SetForAllEllipse(VisibilityProperty, Visibility.Hidden);
            for (int i = 0; i < 6; i++)
            {
                if (akord.KytaraPozice[i] != -1)
                {
                    GetEllipse(i, akord.KytaraPozice[i]).Visibility = Visibility.Visible;
                }
            }
        }

        //nevyužité pomocné pole- zobrazuje mapu tónů na prvních pěti pražcích klasické kytary
        public string[,] guitar_notes = new string[,] { { "e", "a", "d", "g", "h", "e" } , { "f", "b", "dis", "gis", "c", "f" },
            { "fis", "h", "e", "a", "cis", "fis" }, { "g", "c", "f", "b", "d", "g" }, { "gis", "cis", "fis", "h", "dis", "gis" }, { "a", "d", "g", "c", "e", "a" }};

        //funkce GetElipse podle souřadnic hledá odpovídající elipsu nebo border, kterou pak ve funkci ShowChords nastavujeme na viditelné
        public UIElement GetEllipse(int X, int Y)
        {
            for (int i = 0; i < GuitarGrid.Children.Count; i++)
            {
                UIElement Object = GuitarGrid.Children[i];
                if (Object is Ellipse|Object is Border)
                {
                    if ((int)Object.GetValue(Grid.RowProperty) == Y &(int)Object.GetValue(Grid.ColumnProperty) == X)
                    {
                        return Object;
                    }
                }
            }
            return null;
        }

        //funkce SetForAllEllipse slouží k tomu, aby před zobrazením dalšího akordu byly všechny onjekty zase skryty
        //funkce projde celé pole objektů- a nastavuje objektům vybranou hodnotu dané vlastnosti. V našem případě ve funkci ShowChords nastavujeme vlastnost visibility(viditelnosti) na Hidden(skryto)
        public void SetForAllEllipse(DependencyProperty Property,object Value)
        {
            for (int i = 0; i < GuitarGrid.Children.Count; i++)
            {
                UIElement Object = GuitarGrid.Children[i];
                if (Object is Ellipse|Object is Border)
                {
                    Object.SetValue(Property, Value);
                }
            }
        }
    }
}
