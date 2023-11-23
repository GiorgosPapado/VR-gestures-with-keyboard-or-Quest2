//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using Zenject;
//using UniRx;
//using Assets.Scripts.Evaluation.Actions;

//namespace Assets.Scripts.Evaluation.Utils
//{
//    public class WidgetStackInfo
//    {
//        public WidgetStackInfo(GameObject widget, bool closeOnCancel, bool closeOnOK, Action onCloseAction)
//        {
//            Widget = widget;
//            CloseOnCancel = closeOnCancel;
//            CloseOnOK = closeOnOK;
//            OnCloseAction = onCloseAction;
//        }

//        public GameObject Widget { get; set; }
//        public bool CloseOnCancel { get; set; }
//        public bool CloseOnOK { get; set; }
//        public Action OnCloseAction { get; set; }
//    }

//    public class WidgetStack : IInitializable, IDisposable
//    {
//        private Stack<WidgetStackInfo> m_Widgets = new Stack<WidgetStackInfo>();
//        private CompositeDisposable m_Sub = new CompositeDisposable();

//        public void Dispose()
//        {
//            m_Sub.Dispose();
//        }

//        public void Initialize()
//        {
//            m_Sub.Add(MessageBroker.Default.Receive<OKAction>().Subscribe(_=>
//            {
//                WidgetStackInfo si = m_Widgets.Peek();
//                if(si.CloseOnOK)
//                {
//                    si.Widget.SetActive(false);
//                    si.OnCloseAction();
//                }
//            }));
//            m_Sub.Add(MessageBroker.Default.Receive<CancelAction>().Subscribe(_ =>
//            {
//                WidgetStackInfo si = m_Widgets.Peek();
//                if (si.CloseOnCancel)
//                {
//                    si.Widget.SetActive(false);
//                    si.OnCloseAction();
//                }
//            }));
//        }

//        public void PushWidget(WidgetStackInfo widgetStackInfo)
//        {
//            m_Widgets.Push(widgetStackInfo);
//        }
//    }
//}
