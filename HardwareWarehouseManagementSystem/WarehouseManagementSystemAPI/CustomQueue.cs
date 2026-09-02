namespace WarehouseManagementSystemAPI
{
    //Part 15 - Generics - Generic Delegates and Events

    public delegate void QueueEventHandler<T, U>(T sender, U eventArgs);

    public class CustomQueue<T> where T : IEntityPrimaryProperties, IEntityAdditionalProperties //note: eg <T, U,...> for multiples
    {
        //Queue<T> _queue = null;

        //public CustomQueue()
        //{
        //    _queue = new Queue<T>();
        //}

        Queue<T> _queue = new Queue<T>();

        public event QueueEventHandler<CustomQueue<T>, QueueEventArgs> CustomQueueEvent;

        public int QueueLength
        {
            get { return _queue.Count; }
        }

        public void AddItem(T item)
        {
            _queue.Enqueue(item);
            string msg = $"DateTime: {DateTime.Now.ToString(Constants.DateTimeFormat)}, Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Quantity: {item.Quantity} has been added to the queue.";
            QueueEventArgs queueEventArgs = new QueueEventArgs { Message = msg };
            OnQueueChanged(queueEventArgs);
        }

        public T GetItem()
        {
            T item = _queue.Dequeue();
            string msg = $"DateTime: {DateTime.Now.ToString(Constants.DateTimeFormat)}, Id: {item.Id}, Name: {item.Name}, Type: {item.Type}, Quantity: {item.Quantity} has been processed.";
            QueueEventArgs queueEventArgs = new QueueEventArgs { Message = msg };
            OnQueueChanged(queueEventArgs);

            return item;
        }

        protected virtual void OnQueueChanged(QueueEventArgs a)
        {
            CustomQueueEvent(this, a);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }
    }

    public class QueueEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}