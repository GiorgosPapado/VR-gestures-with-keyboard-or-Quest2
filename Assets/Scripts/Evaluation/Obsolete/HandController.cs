//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnityEngine;
//using UniRx;
//using Zenject;
//using Assets.Scripts.Evaluation.Gestures.Interface;
//using Assets.Scripts.Evaluation.Gestures;

//namespace Assets.Scripts.Evaluation.Input
//{
//    public class HandController : MonoBehaviour, IInitializable, IDisposable
//    {
//        public Camera m_Camera;
//        public GameObject m_Marker;
//        private IDisposable m_Sub;
//        private IHandLandmarkProvider m_HandLandmarkProvider;

//        private float m_EffectiveWebcamAreaFactor = 0.7f;

//        [Inject]
//        public void Init(IHandLandmarkProvider handLandmarkProvider)
//        {
//            m_HandLandmarkProvider = handLandmarkProvider;
//        }

//        private Vector3 GetEffectivePos(Vector3 pos)
//        {
//            float offset = (1.0f - m_EffectiveWebcamAreaFactor) / 2.0f;
//            float effectiveX = (pos.x - offset) / m_EffectiveWebcamAreaFactor;
//            float effectiveY = (pos.y - offset) / m_EffectiveWebcamAreaFactor;

//            effectiveX = Mathf.Clamp(effectiveX, 0.0f, 1.0f);
//            effectiveY = Mathf.Clamp(effectiveY, 0.0f, 1.0f);
//            return new Vector3(effectiveX, effectiveY, pos.z);
//        }

//        public void Initialize()
//        {
//            m_Sub = m_HandLandmarkProvider.HandLandmarkStream.Subscribe(hd =>
//            {
//                if (hd == null) { 
//                    m_Marker.SetActive(false);
//                    return;
//                }
//                var rightHand = hd.Hands.FirstOrDefault(); //hd.Hands.Where(x => x.Handedness == Handedness.RIGHT).FirstOrDefault();
//                if(rightHand != null)
//                {
//                    var pos = rightHand.Landmarks[(int)HandLandmark.INDEX_FINGET_TIP];
//                    pos = GetEffectivePos(pos);
//                    pos.y = 1.0f - pos.y;
//                    var spos = pos * new Vector2(Screen.width, Screen.height);
//                    Ray r = m_Camera.ScreenPointToRay(new Vector3(spos.x, spos.y, 1.0f));
//                    Vector3 wpos = r.origin + r.direction * 0.1f;
//                    m_Marker.transform.position = wpos;
//                    m_Marker.SetActive(true);
//                }
//                else
//                {
//                    m_Marker.SetActive(false);
//                }
//            });
//        }

//        public void Dispose()
//        {
//            m_Sub.Dispose();
//        }
//    }
//}
