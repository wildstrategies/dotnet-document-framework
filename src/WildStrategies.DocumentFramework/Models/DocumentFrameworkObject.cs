using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace WildStrategies.DocumentFramework
{

    public abstract class DocumentFrameworkObject : IDocumentFrameworkObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            foreach (var prop in GetType().GetProperties())
            {
                var value = prop.GetValue(this);
                if (value != null)
                {
                    List<ValidationResult> results = new List<ValidationResult>();
                    if (prop.PropertyType == typeof(string))
                    {

                    }
                    else if (prop.PropertyType.GetInterface(nameof(IEnumerable)) != null)
                    {
                        foreach (var item in (value as IEnumerable))
                        {
                            Validator.TryValidateObject(item, new ValidationContext(item, validationContext.Items), results);
                            foreach (var result in results)
                            {
                                yield return result;
                            }
                        }
                    }
                    else
                    {
                        Validator.TryValidateObject(value, new ValidationContext(value, validationContext.Items), results);
                        foreach (var result in results)
                        {
                            yield return result;
                        }
                    }
                }
            }
        }
    }
}
