using CloudNative.CloudEvents;
using System;
using System.Collections.Generic;
using System.Net.Mime;

namespace Neuroglia.CloudEvents
{

    /// <summary>
    /// Defines the fundamentals of a service used to build a <see cref="CloudEvent"/>
    /// </summary>
    public interface ICloudEventBuilder
    {

        /// <summary>
        /// Sets the <see cref="CloudEventsSpecVersion"/> of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="specVersion">The <see cref="CloudEventsSpecVersion"/> of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithSpecVersion(CloudEventsSpecVersion specVersion);

        /// <summary>
        /// Sets the type of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="type">The type of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithType(string type);

        /// <summary>
        /// Sets the subject of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="subject">The subject of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithSubject(string subject);

        /// <summary>
        /// Sets the source of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="source">The source of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithSource(Uri source);

        /// <summary>
        /// Sets the id of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="id">The id of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithId(string id);

        /// <summary>
        /// Sets the timestamp of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="time">The timestamp of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithTime(DateTime? time);

        /// <summary>
        /// Sets the data of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="data">The data of the <see cref="CloudEvent"/> to build</param>
        /// <param name="contentType">The <see cref="ContentType"/> of the <see cref="CloudEvent"/>'s data</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithData(object data, ContentType contentType);

        /// <summary>
        /// Sets the data schema <see cref="Uri"/> of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="dataSchema">The data schema <see cref="Uri"/> of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithDataSchema(Uri dataSchema);

        /// <summary>
        /// Sets the subscription id the <see cref="CloudEvent"/> to build is bound to
        /// </summary>
        /// <param name="subscriptionId">The subscription id the <see cref="CloudEvent"/> to build is bound to</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithSubscriptionId(string subscriptionId);

        /// <summary>
        /// Sets the sequence of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="sequence">The sequence of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithSequence(ulong sequence);

        /// <summary>
        /// Sets a boolean indicating whether or not the <see cref="CloudEvent"/> to build has been redelivered
        /// </summary>
        /// <param name="redelivered">A boolean indicating whether or not the <see cref="CloudEvent"/> to build has been redelivered</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder HasBeenRedelivered(bool redelivered);

        /// <summary>
        /// Sets an <see cref="IDictionary{TKey, TValue}"/> representing the attributes of the <see cref="CloudEvent"/> to build
        /// </summary>
        /// <param name="attributes">An <see cref="IDictionary{TKey, TValue}"/> representing the attributes of the <see cref="CloudEvent"/> to build</param>
        /// <returns>The configured <see cref="ICloudEventBuilder"/></returns>
        ICloudEventBuilder WithAttributes(IDictionary<string, object> attributes);

        /// <summary>
        /// Builds the <see cref="CloudEvent"/>
        /// </summary>
        /// <returns>The newly created <see cref="CloudEvent"/></returns>
        CloudEvent Build();

    }

}
