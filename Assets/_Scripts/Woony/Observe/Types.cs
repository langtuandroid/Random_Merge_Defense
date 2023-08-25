namespace Observe.Types
{
    public enum ObserveEventType
    {
        None,
        TestData,
    }

    public interface IObserver
    {
        public void Initialize();
        public void OnNoticed();
    }

    public interface IObserveEventData { }
    public interface IObserveEventContainer
    {
        public void Invoke(IObserveEventData eventData);
        public void ClearAllEvent();
    }
}
