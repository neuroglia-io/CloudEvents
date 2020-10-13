using CloudNative.CloudEvents;
using System;
using System.Collections.Generic;

namespace Neuroglia.CloudEvents.Extensions
{

    /// <summary>
    /// Represents an <see cref="ICloudEventExtension"/> used to set the id of the subscription a <see cref="CloudEvent"/> is bound to
    /// </summary>
    public class SubscriptionIdExtension
        : ICloudEventExtension
    {

        /// <summary>
        /// Gets the <see cref="SubscriptionIdExtension"/> attribute name
        /// </summary>
        public const string SubscriptionIdAttributeName = "subscriptionid";

        private IDictionary<string, object> _Attributes = new Dictionary<string, object>();

        /// <summary>
        /// Initializes a new <see cref="SubscriptionIdExtension"/>
        /// </summary>
        /// <param name="subscriptionId">The id of the subscription the <see cref="CloudEvent"/> is bound to</param>
        public SubscriptionIdExtension(string subscriptionId)
        {
            this.SubscriptionId = subscriptionId;
        }

        /// <summary>
        /// Gets/sets the id of the subscription the <see cref="CloudEvent"/> is bound to
        /// </summary>
        public string SubscriptionId
        {
            get
            {
                return this._Attributes[SubscriptionIdAttributeName] as string;
            }
            set
            {
                this._Attributes[SubscriptionIdAttributeName] = value;
            }
        }

        /// <inheritdoc/>
        public void Attach(CloudEvent cloudEvent)
        {
            IDictionary<string, object> eventAttributes = cloudEvent.GetAttributes();
            if(this._Attributes == eventAttributes)
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
            return typeof(string);
        }

        /// <inheritdoc/>
        public bool ValidateAndNormalize(string key, ref object value)
        {
            if(key == SubscriptionIdAttributeName)
            {
                if (value == null || value is string)
                    return true;
                throw new InvalidOperationException();
            }
            return false;
        }

    }

}
