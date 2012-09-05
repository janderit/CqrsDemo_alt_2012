using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Events;
using CqrsModel.Model;
using FluentAssertions;
using NUnit.Framework;

namespace CqrsModel.Tests.Warenwirtschaft
{
    [TestFixture]
    public class Warenbestellung : TestBase
    {
/*
        [Test]
        public void Keine_Automatische_Bestellung_Ohne_Verkaufspreis()
        {

            var sutId = Guid.NewGuid();

            Given(
                new ProduktWurdeDefiniert {ProduktId = sutId, Bezeichnung = "Testobst", ZielLagerbestand = 1000}
                );

            When<Produkt>(sutId, sut => sut.PruefeNachbestellungen(20));

            Events.Should().NotBeNull();
            Events.Should().BeEmpty();

        }

        [Test]
        public void Automatische_Bestellung()
        {

            var sutId = Guid.NewGuid();

            Given(
                new ProduktWurdeDefiniert {ProduktId = sutId, Bezeichnung = "Testobst", ZielLagerbestand = 1000},
                new VerkaufspreisWurdeFestgesetzt {ProduktId = sutId, Verkaufspreis = 50}
                );

            When<Produkt>(sutId, sut => sut.PruefeNachbestellungen(20));

            Events.Should().NotBeNull();
            Events.Count().ShouldBeEquivalentTo(1);
            Events.Should().OnlyContain(e => e is BestellungBeiLieferantGetaetigt);
            Events.OfType<BestellungBeiLieferantGetaetigt>().Single().ShouldBeEquivalentTo(new BestellungBeiLieferantGetaetigt {ProduktId = sutId, Menge = 1000, Einkaufspreis = 20});

        }
*/
    }
}
