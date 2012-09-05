using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CqrsModel.Cqrs;
using CqrsModel.Konfiguration;
using CqrsModel.Projektionen;

namespace CqrsModel
{
    public struct DiContainer
    {
        public static DiContainer Current;

        public EventStore Store;
        public DocumentStore DocumentStore;
        public Projektor Projektor;
        public CommandDispatcher CommandBus;
    }
}
