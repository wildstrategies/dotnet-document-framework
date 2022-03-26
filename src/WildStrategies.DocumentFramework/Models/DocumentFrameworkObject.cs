﻿using System;
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
    }
}
