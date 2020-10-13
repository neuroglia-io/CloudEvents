using CloudNative.CloudEvents.Extensions;

namespace Neuroglia.CloudEvents.Extensions
{

    /// <summary>
    /// Represents a <see cref="SequenceExtension"/> based on a <see cref="ulong"/> value
    /// </summary>
    public class ULongSequenceExtension
        : SequenceExtension
    {

        /// <summary>
        /// Initializes a new <see cref="ULongSequenceExtension"/>
        /// </summary>
        /// <param name="sequenceValue">The sequence value</param>
        public ULongSequenceExtension(ulong? sequenceValue = null) 
            : base()
        {
            base.SequenceType = "ulong";
            this.Sequence = sequenceValue;
        }

        /// <summary>
        /// Gets/sets the sequence value
        /// </summary>
        public new ulong? Sequence
        {
            get
            {
                string rawSequence = base.Sequence;
                if (rawSequence != null)
                    return ulong.Parse(rawSequence);
                return null;
            }
            set
            {
                base.Sequence = value?.ToString();
            }
        }

        /// Gets/sets the sequence type
        public new string SequenceType
        {
            get
            {
                return base.SequenceType;
            }
        }

    }

}
