using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Assets.Scripts.Evaluation.Actions;
using Zenject;

namespace Assets.Scripts.Evaluation.Presenters
{
    class FbMapPresenter : MonoBehaviour, IDisposable
    {
        public Button m_FbInfoButton;
        public int m_FbID = 0;
        private IDisposable m_Sub;
        void Awake()
        {
            m_Sub = m_FbInfoButton.OnClickAsObservable().Subscribe(_ =>
            {
                MessageBroker.Default.Publish(new ShowFacebookInfoAction(m_FbID));
            });
        }
        public void Dispose()
        {
            m_Sub.Dispose();
        }

        public class Factory : PlaceholderFactory<FbMapPresenter>
        {

        }
    }
}
