using CloudNative.CloudEvents;
using System;
using System.Collections.Generic;

namespace Neuroglia.CloudEvents.Extensions
{

    /// <summary>
    /// Represents an <see cref="ICloudEventExtension"/> used to set a boolean indicating whether or not a <see cref="CloudEvent"/> has been redelivered
    /// </summary>
    public class RedeliveredExtension
        : ICloudEventExtension
    {

        /// <summary>
        /// Gets the <see cref="RedeliveredExtension"/> attribute name
        /// </summary>
        public const string RedeliveredAttributeName = "redelivered";

        private IDictionary<string, object> _Attributes = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new <see cref="RedeliveredExtension"/>
        /// </summary>
        /// <param name="redelivered">A boolean indicating whether or not the <see cref="CloudEvent"/> has been redelivered</param>
        public RedeliveredExtension(bool redelivered)
        {
            this.Redelivered = redelivered;
        }

        /// <summary>
        /// Gets/sets a boolean indicating whether or not the <see cref="CloudEvent"/> has been redelivered
        /// </summary>
        public bool Redelivered
        {
            get
            {
                return (bool)this._Attributes[RedeliveredAttributeName];
            }
            set
            {
                this._Attributes[RedeliveredAttributeName] = value;
            }
        }

        /// <inheritdoc/>
        public void Attach(CloudEvent cloudEvent)
        {
            IDictionary<string, object> eventAttributes = cloudEvent.GetAttributes();
            if (this._Attributes == eventAttributes)
                return;
            foreach (KeyValuePair<string, object> attribute in this._Attributes)
            {
                eventAttributes[attribute.Key] = attribute.Value;
            }
            this._Attributes = eventAttributes;
        }

        /// <inheritdoc/>
        public Type GetAttributeType(string name)
        {
            return typeof(bool);
        }

        /// <inheritdoc/>
        public bool ValidateAndNormalize(string key, ref object value)
        {
            if (key == RedeliveredAttributeName)
            {
                if (value == null || value is bool)
                    return true;
                throw new InvalidOperationException();
            }
            return false;
        }

    }

}
