using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace Sinama
{
    abstract class Olustur
    {
        public string KartId { get; set; }
        public string KartAciklama { get; set; }
       
       
        public Olustur olustur = null;
        public abstract List<Olustur> kartOlustur(string numara,string Aciklama);
        public List<Olustur> Kart1 = new List<Olustur>();
        public List<Olustur> Kart2 = new List<Olustur>();
        public List<Olustur> Kart3 = new List<Olustur>();
        public List<Olustur> Kart4 = new List<Olustur>();
        public List<Olustur> Kart5 = new List<Olustur>();
    }
  

    
    class bolum1 : Olustur
    {
        
        
       
        public override List<Olustur> kartOlustur(string kID, string Aciklama)
        { olustur = new bolum1();


        olustur.KartId = kID;
            olustur.KartAciklama = Aciklama;
            Kart1.Add(olustur);
            return Kart1;
             
        }
    }
    class bolum2 : Olustur
    {
       
      
        public override List<Olustur> kartOlustur(string kID, string Aciklama)
        {
            olustur = new bolum2();
            olustur.KartId = kID;
            olustur.KartAciklama = Aciklama;
            Kart2.Add(olustur);

            return Kart2;

        }
    }
    class bolum3 : Olustur
    {
   
    
        public override List<Olustur> kartOlustur(string kID, string Aciklama)
        {
            olustur = new bolum3();
            olustur.KartId = kID;
            olustur.KartAciklama = Aciklama;
            Kart3.Add(olustur);

            return Kart3;

        }
    }
    class bolum4 : Olustur
    {
      
        
        public override List<Olustur> kartOlustur(string kID, string Aciklama)
         {
            olustur = new bolum4();
            olustur.KartId = kID;
            olustur.KartAciklama = Aciklama;
            Kart4.Add(olustur);
            return Kart4;

        }
    }
    class bolum5 : Olustur
    {

       
        public override List<Olustur> kartOlustur(string kID, string Aciklama)
        {
             

            olustur = new bolum5();
            olustur.KartId = kID;
            olustur.KartAciklama = Aciklama;
            Kart5.Add(olustur);

            return Kart5;

        }
    }
    enum Bolumler
    {
        bolum1,
        bolum2,
        bolum3,
        bolum4,
        bolum5
    }
    class Creater
    {
        public Olustur FactoryMethod(Bolumler bolumTip)
        {
            Olustur olustur = null;
            switch (bolumTip)
            {
                case Bolumler.bolum1:
                    olustur = new bolum1();
                    break;
                case Bolumler.bolum2:
                    olustur = new bolum2();
                    break;
                case Bolumler.bolum3:
                    olustur = new bolum3();
                    break;
                case Bolumler.bolum4:
                    olustur = new bolum4();
                    break;
                case Bolumler.bolum5:
                    
                    break;

            }
            return olustur;
        }
    }


}
