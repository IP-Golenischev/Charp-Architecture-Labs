namespace Delegates.Observers
{
    public class StackEventData<T>
    {
        public bool IsPushed { get; set; }
        public T Value { get; set; }
        public override string ToString()
        {
            return (IsPushed ? "+" : "-") + Value.ToString();
        }
    }
}
