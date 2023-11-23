using System;
using UniRx;
using TMPro;
using System.Collections.Generic;

namespace Assets.Scripts.Utils
{    
    public static class UniRxUIExtensions
    {
        public static IDisposable SubscribeToText(this IObservable<string> source, TextMeshProUGUI text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x);
        }

        public static IDisposable SubscribeToText<T>(this IObservable<T> source, TextMeshProUGUI text)
        {
            return source.SubscribeWithState(text, (x, t) => t.text = x.ToString());
        }

        //public static IObservable<IList<T>> RollingBuffer<T>(this IObservable<T> source, int count)
        //{
        //    return Observable.Create((IObserver<IList<T>> observer) =>
        //    {
        //        CircularQueue<T> queue = new CircularQueue<T>();
        //        Subject<T> subj = new Subject<T>();
        //        IObservable<IList<T>> obs = subj.AsObservable().Buffer(count);
        //        CompositeDisposable sub = new CompositeDisposable();
        //        sub.Add(obs.Subscribe(observer));
        //        sub.Add(source.Subscribe(v =>
        //        {
        //            queue.Enqueue(v);
        //            foreach (var val in queue)
        //            {
        //                subj.OnNext(val);
        //            }
        //        }));
        //        return sub;
        //    });
        //}
    }
   
}
