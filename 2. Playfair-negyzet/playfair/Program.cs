using System;
//ezeket adtam hozzá
using System.Collections.Generic; //
using System.Linq;
using System.IO;

namespace playfair
{
    class PlayfairKodolo
    { 
        //masodik feladat: létre kellet hozni a playfairkodolo class-t illetve a függvényt azonos névvel(illetve hogy bekérje a nevét az állománynak)
        private List<string> Kodtabla;
        public PlayfairKodolo(string allomany) 
        {
            Kodtabla = new List<string>(); // egy új listát hozok létre (3.feladat mivel kérte hogy tároljuk el)
            foreach (var i in File.ReadAllLines(allomany)) //beolvas minden sort (foreach addig megy amig van valami)
            {
                Kodtabla.Add(i); //ez meg minden sorát hozzá adja a listához
            }
        }

        public int SorIndex(char betu)
        {
            for (int i = 0; i < Kodtabla.Count; i++) //pörgeti a kodtabla listát (4 sor)
            {
                if (Kodtabla[i].Contains(betu.ToString())) //ha az éppen vizsgált sor tartalmazza azt a nagy betut akkor vissza térik az indexével
                    return i;
            }
            return -1; //ha nem akkor -1 el tér vissza
        }
        public int OszlopIndex(char betu)
        {
            for (int sor = 0; sor < Kodtabla.Count; sor++) // ez nézi éppen hányadik sorban van
            {
                for (int oszlop = 0; oszlop < Kodtabla[sor].Length; oszlop++) //ez nézi hogy éppen hányadik oszlop van
                {
                    if (Kodtabla[sor][oszlop] == betu) return oszlop; //és ha az a betu amit éppen vizsgál az olyan betu akkor az egész oszlopot returnoli
                }
            }
            return -1; //ha nem akkor -1 et returnel
        }
        //innentől kezdődik az érdekes rész
        private string KodolAzonosSorban(int sor, int oszlop1, int oszlop2) //itt megnézi hogy ugyan abban a sorban van e
        {//mivel egy sorban van a két betü így bekérjük hogy hányadik sorról van szó és a két betü hol van
            int kodoltOszlopIndex1 = oszlop1 + 1; 
            if (kodoltOszlopIndex1 == 5) //ha a betü a sor végén van akkor az első betüt kell nézni
                kodoltOszlopIndex1 = 0;
            char kodoltKarakter1 = Kodtabla[sor][kodoltOszlopIndex1]; //itt bekárjük hogy pontosan melyik karakterről van szó
            int kodoltOszlopIndex2 = oszlop2 + 1;
            if (kodoltOszlopIndex2 == 5)  //itt ugyan ezt a másik karakterrel is 
                kodoltOszlopIndex2 = 0;
            //szabály szerint a mellete lévő karaktert kell használni 
            //pl ha 1 és 2 van akkor 2 és 3 lesz
            //ha a szélén van akkor meg a sor elején lévő lesz pl 4-5 = 5-1
            char kodoltKarakter2 = Kodtabla[sor][kodoltOszlopIndex2];
            return new string(new char[] { kodoltKarakter1, kodoltKarakter2 }); //és itt egy új listával vissza adjuk
        }

        private string KodolAzonosOszlopban(int oszlop, int sor1, int sor2)
        {//mivel nem egy sorban de egy oszlopban vannak így bekérjük az oszlop számát és a két sort
            int kodoltSorIndex1 = sor1 + 1; 
            if (kodoltSorIndex1 == 5)
                kodoltSorIndex1 = 0; //ha az oszlop alján van akkor a felsőt nézzük
            char kodoltKarakter1 = Kodtabla[kodoltSorIndex1][oszlop];
            int kodoltSorIndex2 = sor2 + 1;
            if (kodoltSorIndex2 == 5) 
                kodoltSorIndex2 = 0;
            char kodoltKarakter2 = Kodtabla[kodoltSorIndex2][oszlop];
            //szabály szerint az alatta lévőt kell nézni így találjuk meg a két karaktert
            return new string(new char[] { kodoltKarakter1, kodoltKarakter2 }); //ugyan úgy vissza adja a két karaktert
        }

        private string KodolTeglalapAlak(int sor1, int oszlop1, int sor2, int oszlop2)
        {//ha se egy sorban se egy oszlopban vannak akkor tömbösítve kell megnézni
            //a jobb felsőt és a bal alsót 
            //szóval itt megkapjuk a sorukat és az oszlopukat
            char kodoltKarakter1 = Kodtabla[sor1][oszlop2]; //itt megadjuk az elsőnek a sorát és a másiknak az oszlopát
            char kodoltKarakter2 = Kodtabla[sor2][oszlop1]; //így jön ki a két véglet (mivel a másiknak a helye kell)
            return new string(new char[] { kodoltKarakter1, kodoltKarakter2 });
        }

        public string KodolBetupar(string betupar) //itt kezdődik minden
        {
            char Betu1 = betupar[0];
            char Betu2 = betupar[1]; //szétszedi a betüket
            int sorBetu1 = SorIndex(Betu1); //majd megnézi hogy ezek a betük hol vannak (sor)
            int sorBetu2 = SorIndex(Betu2);
            int oszlopBetu1 = OszlopIndex(Betu1); //majd ugyan ezt hogy hol van oszlopnál
            int oszlopBetu2 = OszlopIndex(Betu2);
            if (sorBetu1 == sorBetu2)  //ha ugyan abban a sorban vannak akkor át megy a azonossorban részre
                return KodolAzonosSorban(sorBetu1, oszlopBetu1, oszlopBetu2);
            if (oszlopBetu1 == oszlopBetu2) //ha nem egy sorban de egy oszlopban vannak akkor meg az azonos oszlop részre
                return KodolAzonosOszlopban(oszlopBetu1, sorBetu1, sorBetu2);
            return KodolTeglalapAlak(sorBetu1, oszlopBetu1, sorBetu2, oszlopBetu2);//ha egyik se akkor a téglalap rész jön
        }




        //vége



    }
    class Program
    {
        static void Main(string[] args)
        {
            //4.feladat :
            //mivel ugyan az a neve a kattőnek így már az elején beveszi a nevét az állománynak
            PlayfairKodolo pfk = new PlayfairKodolo("kulcstabla.txt");

            //6.feladat : 
            Console.Write("6. feladat - Kérek egy nagybetűt: ");
            string inputKarakter = Console.ReadLine(); //itt bekértem a karaktert
            int sorszam = pfk.SorIndex(inputKarakter.First()); //itt meghívom a függvényt
            Console.WriteLine($"A karakter sorának az indexe: {pfk.SorIndex(inputKarakter.First())}"); //a megoldó kulcs ezt egyben teszi
            Console.WriteLine($"ez meg az én kiírásom :"+sorszam);//itt meg kiírom a végeredményt
            int oszlopszam = pfk.OszlopIndex(inputKarakter.First()); //same here
            Console.WriteLine($"A karakter oszlopának az indexe: {pfk.OszlopIndex(inputKarakter.First())}");
            Console.WriteLine($"ez meg az én kiírásom:"+oszlopszam);

            //8.feladat
            Console.Write("8. feladat - Kérek egy karakterpárt: ");
            string inputKarakterPar = Console.ReadLine();
            Console.WriteLine($"Kódolva: {pfk.KodolBetupar(inputKarakterPar)}");
            String visszajovo = pfk.KodolBetupar(inputKarakterPar);
            Console.WriteLine("Az enyém:" + visszajovo[0]); // egy string ként jön vissza


        }
    }
}
