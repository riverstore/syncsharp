using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncSharp.Business
{
    // Written by Guo Jiayuan

    /// <summary>
    /// Observer interface to be implemented by the UIs
    /// </summary>
    public interface IObserver
    {
        void Update(string status);
    }

    /// <summary>
    /// Observable class to be inherited by logic to update UIs
    /// </summary>
    public abstract class Observable
    {
        List<IObserver> _observerList;

        public Observable()
        {
            _observerList = new List<IObserver>();
        }

        public void AddUI(IObserver ui)
        {
            _observerList.Add(ui);
        }

        public void RemoveAllUIs()
        {
            _observerList.Clear();
        }

        public void NotifyUIs(string status)
        {
            foreach (IObserver o in _observerList)
                o.Update(status);
        }
    }
}
