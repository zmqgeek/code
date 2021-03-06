    class DataConcierge<T>
    {
        // *************************
        // Simple save functionality
        // *************************
        public void Save(T dataObject)
        {
            // perform save logic
            this.OnSaved(dataObject);
        }
        public event DataObjectSaved<T> Saved;
        protected void OnSaved(T dataObject)
        {
            var saved = this.Saved;
            if (saved != null)
                saved(this, new DataObjectEventArgs<T>(dataObject));
        }
        // ************************
        // Batch save functionality
        // ************************
        Dictionary<BatchToken, List<T>> _BatchSavedDataObjects = new Dictionary<BatchToken, List<T>>();
        System.Threading.ReaderWriterLockSlim _BatchSavedDataObjectsLock = new System.Threading.ReaderWriterLockSlim();
        int _SavedObjectThreshold = 17; // if the number of objects being stored for a batch reaches this threshold, then those objects are to be cleared from the list.
        public BatchToken BeginSave()
        {
            // create a batch token to represent this batch
            BatchToken token = new BatchToken();
            _BatchSavedDataObjectsLock.EnterWriteLock();
            try
            {
                _BatchSavedDataObjects.Add(token, new List<T>());
            }
            finally
            {
                _BatchSavedDataObjectsLock.ExitWriteLock();
            }
            return token;
        }
        public void EndSave(BatchToken token)
        {
            List<T> batchSavedDataObjects;
            _BatchSavedDataObjectsLock.EnterWriteLock();
            try
            {
                if (!_BatchSavedDataObjects.TryGetValue(token, out batchSavedDataObjects))
                    throw new ArgumentException("The BatchToken is expired or invalid.", "token");
                this.OnBatchSaved(batchSavedDataObjects); // this causes a single BatchSaved event to be fired
                if (!_BatchSavedDataObjects.Remove(token))
                    throw new ArgumentException("The BatchToken is expired or invalid.", "token");
            }
            finally
            {
                _BatchSavedDataObjectsLock.ExitWriteLock();
            }
        }
        public void Save(BatchToken token, T dataObject)
        {
            List<T> batchSavedDataObjects;
            // the read lock prevents EndSave from executing before this Save method has a chance to finish executing
            _BatchSavedDataObjectsLock.EnterReadLock();
            try
            {
                if (!_BatchSavedDataObjects.TryGetValue(token, out batchSavedDataObjects))
                    throw new ArgumentException("The BatchToken is expired or invalid.", "token");
                // perform save logic
                this.OnBatchSaved(batchSavedDataObjects, dataObject);
            }
            finally
            {
                _BatchSavedDataObjectsLock.ExitReadLock();
            }
        }
        public event BatchDataObjectSaved<T> BatchSaved;
        protected void OnBatchSaved(List<T> batchSavedDataObjects)
        {
            lock (batchSavedDataObjects)
            {
                var batchSaved = this.BatchSaved;
                if (batchSaved != null)
                    batchSaved(this, new BatchDataObjectEventArgs<T>(batchSavedDataObjects));
            }
        }
        protected void OnBatchSaved(List<T> batchSavedDataObjects, T savedDataObject)
        {
            // add the data object to the list storing the data objects that have been saved for this batch
            lock (batchSavedDataObjects)
            {
                batchSavedDataObjects.Add(savedDataObject);
                // if the threshold has been reached
                if (_SavedObjectThreshold > 0 && batchSavedDataObjects.Count >= _SavedObjectThreshold)
                {
                    // then raise the BatchSaved event with the data objects that we currently have
                    var batchSaved = this.BatchSaved;
                    if (batchSaved != null)
                        batchSaved(this, new BatchDataObjectEventArgs<T>(batchSavedDataObjects.ToArray()));
                    // and clear the list to ensure that we are not holding on to the data objects unnecessarily
                    batchSavedDataObjects.Clear();
                }
            }
        }
    }
    class BatchToken
    {
        static int _LastId = 0;
        static object _IdLock = new object();
        static int GetNextId()
        {
            lock (_IdLock)
            {
                return ++_LastId;
            }
        }
        public BatchToken()
        {
            this.Id = GetNextId();
        }
        public int Id { get; private set; }
    }
    class DataObjectEventArgs<T> : EventArgs
    {
        public T DataObject { get; private set; }
        public DataObjectEventArgs(T dataObject)
        {
            this.DataObject = dataObject;
        }
    }
    delegate void DataObjectSaved<T>(object sender, DataObjectEventArgs<T> e);
    class BatchDataObjectEventArgs<T> : EventArgs
    {
        public IEnumerable<T> DataObjects { get; private set; }
        public BatchDataObjectEventArgs(IEnumerable<T> dataObjects)
        {
            this.DataObjects = dataObjects;
        }
    }
    delegate void BatchDataObjectSaved<T>(object sender, BatchDataObjectEventArgs<T> e);
