using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CqrsModel.Cqrs
{
    public class UnitOfWork
    {
        private static UnitOfWork _current = new UnitOfWork();


        public static void Start()
        {
            _current = new UnitOfWork();
        }

        public static IEnumerable<Event> Commit()
        {
            var active = _current;
            Start();

            if (!active._check.All(_=>_())) throw new ApplicationException("Unable to commit.");
            active._commit.ForEach(_=>_());

            return active.Events;
        }

        private readonly List<Event> _events = new List<Event>();
        private readonly List<Func<bool>> _check = new List<Func<bool>>(); 
        private readonly List<Action> _commit = new List<Action>();
        private readonly Dictionary<Guid, object> _cache = new Dictionary<Guid, object>();

        protected IEnumerable<Event> Events
        {
            get { return _events.ToList(); }
        }

        public static void Publish(Guid source, Event @event)
        {
            _current._events.Add(@event);
        }


        public static void Register(Func<bool> canCommit, Action onCommit)
        {
            _current._check.Add(canCommit);
            _current._commit.Add(onCommit);
        }

        public static void Cache(Guid id, object o)
        {
            _current._cache.Add(id, o);
        }


        public static bool CacheHas(Guid id)
        {
            return _current._cache.ContainsKey(id);
        }

        public static object GetFromCache(Guid id)
        {
            return _current._cache[id];
        }

        public static void OnCommit(Action action)
        {
            Register(()=>true, action);
        }
    }


    
}
