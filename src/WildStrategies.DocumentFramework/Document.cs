using NodaTime;
using System;

namespace WildStrategies.DocumentFramework
{
    public sealed class Document<TEntity> : DocumentFrameworkObject, IIDProvider, ICreatedTimeProvider, ILatsUpdateTimeProvider, IDeletedProvider
        where TEntity : Entity
    {
        internal Document(TEntity root)
        {
            Root = root;
        }

        public Guid Id => Root.Id;
        public TEntity Root { get; private set; }
        public Instant CreatedTime { get; private set; } = SystemClock.Instance.GetCurrentInstant();
        public Instant LatsUpdateTime { get; private set; } = SystemClock.Instance.GetCurrentInstant();
        public Instant DeletedTime { get; private set; }

        public bool Deleted => DeletedTime != null;
    }
}
