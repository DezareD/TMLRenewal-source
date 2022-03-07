using Blazorise;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using RenewalTML.Data;

namespace RenewalTML.Shared.Validation
{
    public class ContextValidationModule<T> where T: class
    {
        public Validations _validationContext { get; set; }
        public T _model { get; set; }
        public Dictionary<string, Blazorise.Validation> _fieldValidationStatus { get; set; }

        public ContextValidationModule(T model)
        {
            _model = model;

            var listOfFieldNames = typeof(T).GetProperties().Where(m => !m.GetCustomAttributes(typeof(DisableModuleValidationAttribute), true).Any()).Select(f => f.Name).ToList();
            _fieldValidationStatus = new Dictionary<string, Blazorise.Validation>();

            foreach(var k in listOfFieldNames)
                _fieldValidationStatus.Add(k, null);
        }

        public void SetFieldStatus(string FieldName, ValidationStatus status, IEnumerable<string> messages = null) => this._fieldValidationStatus[FieldName].NotifyValidationStatusChanged(status, messages);
        public void SetFielAllStatus(ValidationStatus status, IEnumerable<string> messages = null)
        {
            foreach (var key in this._fieldValidationStatus.Keys)
                this._fieldValidationStatus.GetValueOrDefault(key)?.NotifyValidationStatusChanged(status, messages);
        }
    }
}
