using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public abstract class DocumentFrameworkObject : IDocumentFrameworkObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType().Equals(this.GetType()))
            {
                foreach (var property in GetType().GetProperties())
                {
                    var thisValue = property.GetValue(this);
                    var objValue = property.GetValue(this);

                    if (thisValue != objValue && (thisValue == null || !thisValue.Equals(objValue)))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hashCode = 17;

            foreach (var property in GetType().GetProperties())
            {
                hashCode *= 23 + property.GetValue(this).GetHashCode();
            }

            return hashCode;
        }
    }
}
