using lcmrecak__zadaca_3.Klase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lcmrecak__zadaca_3.Builder
{
    public class ZahtjevRezervacijeBuilder
    {
        private ZahtjevRezervacije zahtjev = new ZahtjevRezervacije();
        public ZahtjevRezervacijeBuilder(int idBrod, DateOnly datumOd, TimeOnly vrijemeOd, int trajanjePrivezaUH)
        {
            zahtjev.IdBrod = idBrod;
            zahtjev.DatumOd = datumOd;
            zahtjev.VrijemeOd = vrijemeOd;
            zahtjev.TrajanjePrivezaUH = trajanjePrivezaUH;
        }

        public ZahtjevRezervacijeBuilder setStatus(string status)
        {
            zahtjev.Status = status;
            return this;
        }

        public ZahtjevRezervacije Build()
        {
            return zahtjev;
        }
    }
}
