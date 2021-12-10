using System;
using System.Collections.Generic;
using System.Text;

namespace Ordering.Core.Entities.Base
{
    public abstract class EntityBase<TId> : IEntityBase<TId>
    {
        public TId Id { get; protected set; }

        int? _requestHashCode;

        public bool IsTransient() => Id.Equals(default(TId));       //is == operation for validate if entity is defined or not

        public override bool Equals(object obj)
        {
            if (obj is null || !(obj is EntityBase<TId>))
                return false;

            if (ReferenceEquals(this, obj))
                return true;

            if (this.GetType() != obj.GetType())
                return false;

            var item = (EntityBase<TId>)obj;

            if (item.IsTransient() || IsTransient())
                return false;
            else
                return item == this;
        }

        public override int GetHashCode()
        {
            if (!IsTransient())         //return hash code 
            {
                if (!_requestHashCode.HasValue)
                    _requestHashCode = Id.GetHashCode() ^ 31;       //using XOR to get object hashCode as readable number

                return _requestHashCode.Value;
            }
            else
                return base.GetHashCode();
        }

    }
}
