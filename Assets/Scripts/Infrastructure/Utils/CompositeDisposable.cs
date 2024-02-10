using System;
using System.Collections.Generic;

namespace CodeBase.Utils
{
    public class CompositeDisposable : IDisposable
    {
        private readonly List<IDisposable> _list = new();
        private bool _disposed;

        public void Add(IDisposable disposable)
        {
            if (_disposed) throw new Exception("Already disposed");
            _list.Add(disposable);
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                _disposed = true;
                _list.ForEach(d => d.Dispose());
                _list.Clear();
            }
        }
    }
}