namespace BLL.Common.Contract
{
    public interface ISequenceBuilder
    {
        ISequenceBuilder QueueTask(IRunnable task);

        ISequence Build();
    }
}