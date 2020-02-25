using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{
    public abstract class BaseDocumentFrameworkObject
    {
        public override bool Equals(object obj)
        {
            if (obj != null && obj.GetType().Equals(GetType()))
            {
                foreach (System.Reflection.PropertyInfo property in GetType().GetProperties())
                {
                    object thisValue = property.GetValue(this);
                    object objValue = property.GetValue(obj);

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

            foreach (System.Reflection.PropertyInfo property in GetType().GetProperties())
            {
                hashCode *= 23 + property.GetValue(this).GetHashCode();
            }

            return hashCode;
        }
    }

    public abstract class DocumentFrameworkObject : BaseDocumentFrameworkObject, IDocumentFrameworkObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            throw new NotImplementedException();
        }
    }
}
