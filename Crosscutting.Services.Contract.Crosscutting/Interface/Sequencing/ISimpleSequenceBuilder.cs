namespace Crosscutting.Services.Contract.Crosscutting.Interface.Sequencing
{
    public interface ISimpleSequenceBuilder
    {
        /// <summary>
        /// builds the sequence ( called last )
        /// </summary>
        public ISequence Build();
    }
}