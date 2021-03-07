namespace Crosscutting.Sequencing.Contract
{
    public interface ISimpleSequenceBuilder
    {
        /// <summary>
        /// builds the sequence ( called last )
        /// </summary>
        public ISequence Build();
    }
}