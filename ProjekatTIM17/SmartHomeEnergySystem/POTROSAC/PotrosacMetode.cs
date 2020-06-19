using SmartHomeEnergySystem.COMMON;
using SmartHomeEnergySystem.SHES;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.POTROSAC
{
   public class PotrosacMetode:IPotrosac
    {
        public void DodajPotrosacPrekoKonzole()
        {
            IShes ishes = new ShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(ishes);

            Console.WriteLine("Unesite naziv novog potrosaca\n");
            string naziv = Console.ReadLine();
            Console.WriteLine("Unesite potrosnju potrosaca\n");
            string potrosnjaString = Console.ReadLine();
            

            List<Potrosac> lista = shesMetode.VratiListuPotrosaca();
            bool validacija = ValidacijaDodatogPotrosaca(lista, naziv, potrosnjaString);
            if (validacija)
            {
                double potrosnja = Double.Parse(potrosnjaString);
                shesMetode.DodajPotrosac(naziv, potrosnja);
                Console.WriteLine("Uspesno ste dodali potrosac.");
            }
            else return;

        }

        public void UkljuciPotrosac()
        {
            IShes ishes = new ShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(ishes);
            List<Potrosac> lista = shesMetode.VratiListuPotrosaca();

            int brojac = 1;
            

            foreach (Potrosac p in lista)
            {

                if (!p.Upaljeno)
                {
                    Console.WriteLine(brojac.ToString() + ". " + p.Ime + ", potrosnja:" + p.Potrosnja + ", stanje: Iskljuceno");
                    brojac++;
                }

            }

            if (brojac == 1)
            {
                Console.WriteLine("Nema iskljucenih potrosaca.\n");
                Console.WriteLine("----------------------------------");
                return;
            }

            Console.WriteLine("\nUnesite naziv potrosaca koji zelite da ukljucite:");
            string naziv = Console.ReadLine();
            if (ValidacijaUkljuci(lista, naziv))
            {
                shesMetode.Ukljuci(naziv);
                Console.WriteLine("Uspesno ste ukljucili potrosaca.");
            }

            else return;


        }

        public void IskljuciPotrosac()
        {
            IShes ishes = new ShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(ishes);
            List<Potrosac> lista = shesMetode.VratiListuPotrosaca();

           

            int brojac = 1;
            foreach (Potrosac p in lista)
            {
                if (p.Upaljeno)
                {
                    Console.WriteLine(brojac.ToString() + ". " + p.Ime + ", potrosnja:" + p.Potrosnja + ", stanje: Ukljuceno");
                    brojac++;
                }

            }

            if (brojac == 1)
            {
                Console.WriteLine("Nema ukljucenih potrosaca.\n");
                Console.WriteLine("----------------------------------");
                return;
            }
            Console.WriteLine("\nUnesite naziv potrosaca koji zelite da iskljucite:");
            string naziv = Console.ReadLine();
            if (ValidacijaIskljuci(lista, naziv))
            {
                shesMetode.Iskljuci(naziv);
                Console.WriteLine("Uspesno ste iskljucili potrosaca.");
            }

            else return;


        }

        public void ObrisiPotrosacPrekoKonzole()
        {
            IShes ishes = new ShesRepozitorijum();
            ShesMetode shesMetode = new ShesMetode(ishes);
            List<Potrosac> lista = shesMetode.VratiListuPotrosaca();
            string upaljeno = "";
            int brojac = 1;
            foreach (Potrosac p in lista)
            {
                if (p.Upaljeno)
                    upaljeno = "Ukljucen";
                else
                    upaljeno = "Iskljucen";

               Console.WriteLine(brojac.ToString() + ". " + p.Ime + ", potrosnja:" + p.Potrosnja + ", stanje: "+upaljeno);
               brojac++;
            }
            Console.WriteLine("----------------------------------------------------");
            Console.WriteLine("Unesite naziv potrosaca koji zelite da obrisete\n");
            string naziv = Console.ReadLine();
            
            bool validacija = ValidacijaObrisiPotrosaca(lista, naziv);
            if (validacija)
            {
                shesMetode.ObrisiPotrosac(naziv);
                Console.WriteLine("Obrisali ste potrosac.\n");
            }
            else return;
        }

        public bool ValidacijaDodatogPotrosaca(List<Potrosac> lista,string naziv,string potrosnjaString)
        {
            bool validno = true;

            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    Console.WriteLine("Potrosac sa ovim nazivom vec postoji.");
                    validno = false;
                    return validno;
                }
            }

            if(naziv.Trim() == "")
            {
                Console.WriteLine("Potrosac mora imati naziv.");  
                validno = false;
                return validno;
            }
            double potrosnja;
            try
            {
                potrosnja = Double.Parse(potrosnjaString);
            }
            catch
            {
                Console.WriteLine("Potrosnja se izrazava u brojevima");
                return false;
            }

            if(potrosnja<=0)
            {
                Console.WriteLine("Potrosac ne moze imati negativnu potrosnju.");
                validno = false;
                return validno;
            }

          


            return validno;
        }

        public bool ValidacijaUkljuci(List<Potrosac> lista, string naziv)
         {
            bool validno = false;

            if (naziv.Trim() == "")
            {
                Console.WriteLine("Potrosac mora imati naziv.");
                validno = false;
                return validno;
            }

            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    validno = true;
                    break;  
                }
               
            }
            if(!validno)
            {
                Console.WriteLine("Potrosac sa tim imenom ne postoji.");
                return validno;
            }
            

            foreach(Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    if (p.Upaljeno)
                    {
                        validno = false;
                        Console.WriteLine("Uredjan je vec ukljucen");
                        return validno;
                    }
                    else break;
                }
            }
            return validno;
        }

        public bool ValidacijaIskljuci(List<Potrosac> lista, string naziv)
        {
            bool validno = false;

            if (naziv.Trim() == "")
            {
                Console.WriteLine("Potrosac mora imati naziv.");
                validno = false;
                return validno;
            }

            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    validno = true;
                    break;
                }

            }
            if (!validno)
            {
                Console.WriteLine("Potrosac sa tim imenom ne postoji.");
                return validno;
            }


            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    if (!p.Upaljeno)
                    {
                        validno = false;
                        Console.WriteLine("Uredjan je vec iskljucen");
                        return validno;
                    }
                    else break;
                }
            }
            return validno;
        }

        public bool ValidacijaObrisiPotrosaca(List<Potrosac> lista,string naziv)
        {
            bool validno = false;

            if (naziv.Trim() == "")
            {
                Console.WriteLine("Potrosac mora imati naziv.");
                validno = false;
                return validno;
            }

            foreach (Potrosac p in lista)
            {
                if (p.Ime.ToLower() == naziv.ToLower())
                {
                    validno = true;
                    return validno;
                }
            }

            Console.WriteLine("Potrosac sa ovim imenom ne postoji.\n");
            
            return validno;
        }

    }
}
