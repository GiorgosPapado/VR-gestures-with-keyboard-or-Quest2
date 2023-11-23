using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Assets.Scripts.Evaluation.Input;
using Zenject;
using UniRx;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class WebcamWidgetPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        public GameObject m_WebcamWidget;
        public KeyCode m_EnableKey = KeyCode.V;
        public Renderer m_WebcamTextureRenderer;

        private WebcamProvider m_WebcamProvider;
        private CompositeDisposable m_Subs = new CompositeDisposable();

        public void Initialize()
        {
            m_Subs.Add(m_WebcamProvider.OnWebcamStreamEnabled.Subscribe(_ =>
            {
                m_WebcamTextureRenderer.material.SetColor("_Color", Color.white);
                m_WebcamTextureRenderer.material.mainTexture = m_WebcamProvider.Core.Texture;
                if(m_WebcamProvider.Core.FlipUpsideDown)
                    m_WebcamTextureRenderer.material.mainTextureScale = new Vector2(1.0f, -1.0f);
                else
                    m_WebcamTextureRenderer.material.mainTextureScale = new Vector2(1.0f, 1.0f);
            })); 

            m_Subs.Add(m_WebcamProvider.OnWebcamStreamDisabled.Subscribe(_ => {
                m_WebcamTextureRenderer.material.mainTexture = null;
                m_WebcamTextureRenderer.material.SetColor("_Color", Color.black);
            }));
        }

        [Inject]
        void Init(WebcamProvider webcamProvider)
        {
            m_WebcamProvider = webcamProvider;
        }

        public void Update()
        {
            if(UnityEngine.Input.GetKeyDown(m_EnableKey))
            {
                m_WebcamWidget.SetActive(!m_WebcamWidget.activeSelf);
            }
        }

        public void Dispose()
        {
            m_Subs.Dispose();
        }
    }
}
