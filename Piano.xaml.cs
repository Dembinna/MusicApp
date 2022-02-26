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
    /// Interaction logic for Piano.xaml
    /// </summary>
    public partial class Piano : UserControl
    {
        static public Nota[] cely_tony = new Nota[] { Nota.c, Nota.d, Nota.e, Nota.f, Nota.g, Nota.a, Nota.h };
        static public Nota[] pul_tony = new Nota[] { Nota.cis, Nota.dis, Nota.fis, Nota.gis, Nota.b };
        public List<Klavesa> Klávesy = new List<Klavesa>();

        //funkce tvoří vlastnost pro klávesnici- manipulovatelnost
        //projde všechny klávesy a nastaví hodnotu
        public bool IsDisabled
        {
            set
            {              
                for (int i = 0; i < Klávesy.Count; i++)
                {
                    Klávesy[i].IsEnabled = !value;
                }
            }
        }

        public Action<Klavesa, RoutedEventArgs,bool> OnClickKlavesa=delegate { };

        //inicializace třídy
        public Piano(int Pocet/*počet kláves*/)
        {
            InitializeComponent();
            Init(Pocet);
            //šířka gridu se mění podle velikosti klávesnice kterou si stanovíme
            MainGrid.Width = (double)(50 * Pocet);
            //výška je konstatní
            MainGrid.Height = 300;
        }

        //funkce pro vytváření kláves
        //vytvoří řadu kláves a dosadí do hlavního Gridu
        public void Init(int pocetKlaves)
        {
            var colDef = new ColumnDefinition();
            BlackGrid.ColumnDefinitions.Add(colDef);
            colDef.Width = new GridLength(0.5, GridUnitType.Star);
            int b = 0;
            for (int i = 0; i < pocetKlaves; i++)
            {


                WhiteGrid.ColumnDefinitions.Add(new ColumnDefinition());
                var klavesa = new Klavesa(cely_tony[i % 7], Klavesa_Click) { Hover = new SolidColorBrush(Colors.LightSkyBlue) };
                klavesa.DefaultBackground = Klavesa.LinearBrushWhite;
                klavesa.Margin = new Thickness(1, 0, 1, 0);
                WhiteGrid.Children.Add(klavesa);
                Klávesy.Add(klavesa);
                Grid.SetColumn(klavesa, i);


                colDef = new ColumnDefinition();
                BlackGrid.ColumnDefinitions.Add(colDef);

                if (i == pocetKlaves - 1)
                {
                    colDef.Width = new GridLength(0.5, GridUnitType.Star);
                }
                
                //po splnění podmínky se vytváří také černé, ppkud je třeba
                if (addBlackKey(i))
                {
                    klavesa = new Klavesa(pul_tony[b % 5],Klavesa_Click,true) { Hover = new SolidColorBrush(Colors.YellowGreen),PressetColor = Klavesa.IsPressedDark };
                    klavesa.DefaultBackground = Klavesa.LinearBrushBlack;
                    klavesa.Margin = new Thickness(10,0,10,0);
                    BlackGrid.Children.Add(klavesa);
                    Klávesy.Add(klavesa);
                    Grid.SetColumn(klavesa, i);
                    b++;
                }
            }
        }

        //funkce vrací hodnotu true, pokud se má generovat další černá klávesa
        //funkce slouží k ověření podmínky
        public bool addBlackKey(int zeroBasedIndex)
        {
            var ind = (zeroBasedIndex) % 7;
            if (ind == 3 || ind == 0) return false;
            return true;
        }

        //funkce Show se stará o zobrazení akordu na klávesnici
        //(funkce Show zobrazí daný Akord => doporučuje se pří vlastnosti IsDisabled = true)
        public void Show(Akord Akord)
        {
            for (int i = 0; i < Klávesy.Count; i++)
            {
                if (Klávesy[i].NotaName == Akord.Tony.Item1| Klávesy[i].NotaName == Akord.Tony.Item2| Klávesy[i].NotaName == Akord.Tony.Item3)
                {
                    Klávesy[i].IsPressedKlavesa = true;
                }
            }
        }

        //funkce Hide skryje staré označení kláves, které jsou stisklé
        //schová předešlý akord
        public void Hide()
        {
            for (int i = 0; i < Klávesy.Count; i++)
            {
                Klávesy[i].IsPressedKlavesa = false;
            }
        }

        //funkce Klaves_Click se volá při zmáčknutí klávesy a mění její vlastnost IsPressedKlavesa na opačnou hodnotu
        //vlastnost slouží k tomu, aby hráč mohl měnit zda se má klávesa(tón) stisknout v danám akordu- je tu možnost opravit tak špatný výběr tónů
        public void Klavesa_Click(object sender, RoutedEventArgs e)
        {
            Klavesa klavesa = (Klavesa)sender;
            klavesa.IsPressedKlavesa = !klavesa.IsPressedKlavesa;
            OnClickKlavesa(klavesa, e, !klavesa.IsPressedKlavesa);
        }
    }
    public class Klavesa : Button
    {

        //statické štětce barev pro klávesy
        public static GradientStopCollection GradientStopsBlack = new GradientStopCollection() { 
            new GradientStop(Colors.White,0),
            new GradientStop(Colors.Gray,0.2),
            new GradientStop(Colors.Black,0.5)
        };

        public static GradientStopCollection GradientStopsWhite = new GradientStopCollection() {
            new GradientStop(Colors.LightGray,0),
            new GradientStop(Colors.White,0.7)
        };

        public static LinearGradientBrush LinearBrushBlack = new LinearGradientBrush() { EndPoint = new Point(0, 1), GradientStops = GradientStopsBlack };
        public static LinearGradientBrush LinearBrushWhite = new LinearGradientBrush() { EndPoint = new Point(0, 1), GradientStops = GradientStopsWhite };
        public static LinearGradientBrush IsPressedWhite = new LinearGradientBrush() { EndPoint = new Point(0, 1), GradientStops = new GradientStopCollection() { new GradientStop() { Color = Colors.LightGreen, Offset = 0 }, new GradientStop() { Color = Colors.Lime, Offset = 0.7 } } };
        public static LinearGradientBrush IsPressedDark = new LinearGradientBrush() { EndPoint = new Point(0, 1), GradientStops = new GradientStopCollection() { new GradientStop() { Color = Colors.LimeGreen, Offset = 0 }, new GradientStop() { Color = Colors.Green, Offset = 0.5 } } };

        //důležité pro dělení- černá
        public bool IsBlack = false;
        public Nota NotaName;
        //definice stisku
        public bool IsPressedKlavesa { get=> ispressed; set { ispressed = value;Background = CurrentBackground; } }
        bool ispressed = false;
        public Brush PressetColor = IsPressedWhite;
        public Brush DefaultBackground { get => defaultBackground; set { defaultBackground = value; Background = CurrentBackground; } }
        Brush defaultBackground = LinearBrushWhite;

        //funkce CurrentBackground je pro nastavení barvy(pozadí) klávesy, určené stiskem
        Brush CurrentBackground
        {
            get
            {
                if (IsPressedKlavesa)
                {
                    return PressetColor;
                }
                else
                {
                    return defaultBackground;
                }
            }
        }

        //Brush použitý jako hover- viditelnost pohybu myši
        public Brush Hover;

        //přiřazení jména tónu
        public Klavesa(Nota Name/*tato klávesa se bude hledat podle dané noty*/, RoutedEventHandler hedler/*delegát, kde dosadíme funkci cliknutí*/,bool isblack = false/*je-li klávesa černá*/) : base()
        {
            Hover = DefaultBackground;
            Background = DefaultBackground;
            IsBlack = isblack;
            Click += hedler;
            MouseEnter += Klavesa_MouseEnter;
            MouseLeave += Klavesa_MouseLeave;
            this.NotaName = Name;
        }

        //funkce vrací původní barvu, když myš opustí tlačítko (hover)
        private void Klavesa_MouseLeave(object sender, MouseEventArgs e)
        {
            Background = CurrentBackground;
        }

        //změna z původní barvy na určenou (hover)
        private void Klavesa_MouseEnter(object sender, MouseEventArgs e)
        {
            Background = Hover;
        }
    }
}
