using CloudNative.CloudEvents;
using System;

namespace Neuroglia.CloudEvents
{

    /// <summary>
    /// Represents an <see cref="Attribute"/> used to specify the <see cref="CloudEvent"/>s attributes of the marked class
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CloudEventAttribute
        : Attribute
    {

        /// <summary>
        /// Initializes a new <see cref="CloudEventAttribute"/>
        /// </summary>
        /// <param name="type">The <see cref="CloudEvent"/>'s type <see cref="Uri"/></param>
        public CloudEventAttribute(string type)
        {
            this.Type = new Uri(type, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Initializes a new <see cref="CloudEventAttribute"/>
        /// </summary>
        /// <param name="type">The <see cref="CloudEvent"/>'s type <see cref="Uri"/></param>
        /// <param name="dataSchema">The <see cref="CloudEvent"/>'s data schema <see cref="Uri"/></param>
        public CloudEventAttribute(string type, string dataSchema)
            : this(type)
        {
            this.DataSchema = new Uri(dataSchema, UriKind.RelativeOrAbsolute);
        }

        /// <summary>
        /// Gets the <see cref="CloudEvent"/>'s type <see cref="Uri"/>
        /// </summary>
        public Uri Type { get; }

        /// <summary>
        /// Gets the <see cref="CloudEvent"/>'s data schema <see cref="Uri"/>
        /// </summary>
        public Uri DataSchema { get; }

    }

}
