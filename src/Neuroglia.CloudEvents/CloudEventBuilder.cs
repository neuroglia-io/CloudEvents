using CloudNative.CloudEvents;
using Neuroglia.CloudEvents.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;

namespace Neuroglia.CloudEvents
{

    /// <summary>
    /// Represents the default implementation of the <see cref="ICloudEventBuilder"/> interface
    /// </summary>
    public class CloudEventBuilder
        : ICloudEventBuilder
    {

        /// <summary>
        /// Initializes a new <see cref="CloudEventBuilder"/>
        /// </summary>
        public CloudEventBuilder()
        {
            this.Extensions = new List<ICloudEventExtension>();
        }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s <see cref="CloudEventsSpecVersion"/>
        /// </summary>
        protected CloudEventsSpecVersion SpecVersion { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s type
        /// </summary>
        protected string Type { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s subject
        /// </summary>
        protected string Subject { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s source <see cref="Uri"/>
        /// </summary>
        protected Uri Source { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s id
        /// </summary>
        protected string Id { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s timestamp
        /// </summary>
        protected DateTime? Time { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s data
        /// </summary>
        protected object Data { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s data content type
        /// </summary>
        protected ContentType DataContentType { get; set; }

        /// <summary>
        /// Gets/sets the <see cref="CloudEvent"/>'s data schema <see cref="Uri"/>
        /// </summary>
        protected Uri DataSchema { get; set; }

        /// <summary>
        /// Gets/sets a <see cref="List{T}"/> containing the <see cref="CloudEvent"/>'s extensions
        /// </summary>
        protected List<ICloudEventExtension> Extensions { get; set; }

        /// <summary>
        /// Gets/sets an <see cref="IDictionary{TKey, TValue}"/> containing the <see cref="CloudEvent"/>'s attributes
        /// </summary>
        protected IDictionary<string, object> Attributes { get; set; }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithSpecVersion(CloudEventsSpecVersion specVersion)
        {
            this.SpecVersion = specVersion;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                throw new ArgumentNullException(nameof(type));
            this.Type = type;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithSubject(string subject)
        {
            this.Subject = subject;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithSource(Uri source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));
            this.Source = source;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithId(string id)
        {
            this.Id = id;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithTime(DateTime? time)
        {
            this.Time = time;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithData(object data, ContentType contentType)
        {
            this.Data = data;
            this.DataContentType = contentType;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithDataSchema(Uri dataSchema)
        {
            this.DataSchema = dataSchema;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder HasBeenRedelivered(bool redelivered)
        {
            RedeliveredExtension extension = this.Extensions.OfType<RedeliveredExtension>().FirstOrDefault();
            if (extension == null)
                this.Extensions.Add(new RedeliveredExtension(redelivered));
            else
                extension.Redelivered = redelivered;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithSequence(ulong sequence)
        {
            ULongSequenceExtension extension = this.Extensions.OfType<ULongSequenceExtension>().FirstOrDefault();
            if (extension == null)
                this.Extensions.Add(new ULongSequenceExtension(sequence));
            else
                extension.Sequence = sequence;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithSubscriptionId(string subscriptionId)
        {
            SubscriptionIdExtension extension = this.Extensions.OfType<SubscriptionIdExtension>().FirstOrDefault();
            if (extension == null)
                this.Extensions.Add(new SubscriptionIdExtension(subscriptionId));
            else
                extension.SubscriptionId = subscriptionId;
            return this;
        }

        /// <inheritdoc/>
        public virtual ICloudEventBuilder WithAttributes(IDictionary<string, object> attributes)
        {
            this.Attributes = attributes;
            return this;
        }

        /// <inheritdoc/>
        public virtual CloudEvent Build()
        {
            CloudEvent e = new CloudEvent(this.SpecVersion, this.Type, this.Source, this.Subject, this.Id, this.Time, this.Extensions.ToArray())
            {
                Data = this.Data,
                DataContentType = this.DataContentType
            };
            if(this.Attributes != null)
            {
                IDictionary<string, object> eventAttributes = e.GetAttributes();
                foreach(KeyValuePair<string, object> kvp in this.Attributes)
                {
                    if (!eventAttributes.ContainsKey(kvp.Key))
                        eventAttributes[kvp.Key] = kvp.Value;
                }
            }
            return e;
        }

        /// <summary>
        /// Creates a new <see cref="ICloudEventBuilder"/> from the specified <see cref="CloudEvent"/>
        /// </summary>
        /// <param name="e">The <see cref="CloudEvent"/> to create a new <see cref="ICloudEventBuilder"/> for</param>
        /// <returns>A new <see cref="ICloudEventBuilder"/> based on the specified <see cref="CloudEvent"/></returns>
        public static CloudEventBuilder FromEvent(CloudEvent e)
        {
            CloudEventBuilder builder = new CloudEventBuilder();
            builder.WithSpecVersion(e.SpecVersion)
                .WithType(e.Type)
                .WithSubject(e.Subject)
                .WithSource(e.Source)
                .WithId(e.Id)
                .WithTime(e.Time)
                .WithData(e.Data, e.DataContentType)
                .WithDataSchema(e.DataSchema)
                .WithAttributes(e.GetAttributes());
            return builder;
        }

    }

}
