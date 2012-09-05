using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Commands;
using NUnit.Framework;

namespace CqrsModel.Tests.Kunde
{
    [TestFixture]
    public class Kundenverwaltung : TestBase
    {

        [Test, ExpectedException(typeof (ApplicationException))]
        public void Neuer_Kunde_kann_nur_mit_Id_angelegt_werden()
        {
            When(new KundeErfassen {});
        }
    }
}
