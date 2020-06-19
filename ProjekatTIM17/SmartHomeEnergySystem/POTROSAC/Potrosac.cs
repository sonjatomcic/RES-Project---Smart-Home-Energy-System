using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeEnergySystem.POTROSAC
{
   public class Potrosac
    {
       [Key]
       public string Ime { get; set; } 
       public double Potrosnja { get; set; }
       public bool Upaljeno { get; set; }

       public Potrosac() { }

       public Potrosac(string ime,double? potrosnja,bool? upaljeno)
        {
            if (ime == null || potrosnja == null || upaljeno == null)
            {
                throw new ArgumentNullException("Parameti ne mogu da budu NULL!");
            }


            if(ime.Trim()=="")
            {
                throw new ArgumentException("Ime ne sme biti prazno polje.");
            }

            if (potrosnja < 0)
            {
                throw new ArgumentException("Potrosnja ne sme imati negativnu vrednost!");
            }

            Ime = ime;
            Potrosnja = (double)potrosnja;
            Upaljeno = (bool)upaljeno;
        }
       
    }
}
