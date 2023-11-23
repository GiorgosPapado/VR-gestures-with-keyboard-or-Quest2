using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Model.Entities;
using Assets.Scripts.Evaluation.Utils;
using Assets.Scripts.Evaluation.Input.Interface;
using Assets.Scripts.Evaluation.Input;

namespace Assets.Scripts.Evaluation.Presenters
{
    class AddEntityToMapPresenter : IInitializable, ITickable
    {
        private Raycaster m_Raycaster;
        private CompositeDisposable m_Sub = new CompositeDisposable();

        private IFactory<FbMapPresenter> m_FbMapFactory;
        private IFactory<TwitterMapPresenter> m_TwitterMapFactory;
        private IFactory<VideoMapPresenter> m_VideoMapFactory;

        private EntityInfo m_EntityToAdd;
        private Transform  m_MapEntityToAdd;
        
        private IAction m_Action;

        [Inject]
        public void Init(Raycaster raycaster, FbMapPresenter.Factory fbMapFactory,
            TwitterMapPresenter.Factory twitterMapFactory,
            VideoMapPresenter.Factory videoMapFactory,            
            IAction action)
        {
            m_Raycaster = raycaster;
            m_FbMapFactory = fbMapFactory;
            m_TwitterMapFactory = twitterMapFactory;
            m_VideoMapFactory = videoMapFactory;            
            m_Action = action;
        }

        public void Initialize()
        {
            m_Sub.Add(MessageBroker.Default.Receive<AddEntityOnMapAction>().Subscribe(act =>
            {
                m_EntityToAdd = act.EntityInfo;
            }));            
        }

        public void Tick()
        {
            if (m_EntityToAdd != null)
            {
                RaycastHit hit;
                bool hitOnMap = false;
                if (m_Raycaster.Raycast(out hit))
                {
                    if (hit.transform.gameObject.CompareTag("intersection"))
                    {
                        hitOnMap = true;
                        if (m_MapEntityToAdd == null)
                            m_MapEntityToAdd = CreateMapEntity(hit.point) ;
                        else
                        {
                            m_MapEntityToAdd.position = hit.point;
                        }
                    }
                }
                else
                {
                    Debug.Log("Not hit");
                }
                if (m_Action.ActionTriggered() || m_Action.OK())
                {
                    if(hitOnMap)
                    {
                        Observable.NextFrame().Subscribe(_ =>
                        {
                            m_MapEntityToAdd.gameObject.GetComponentInChildren<Button>().enabled = true;
                            m_MapEntityToAdd.gameObject.GetComponentInChildren<Collider>().enabled = true;
                            m_EntityToAdd = null;
                            m_MapEntityToAdd = null;
                            MessageBroker.Default.Publish(new EntityItemPinned());
                        });                                                
                    }
                }                
            }
            if(m_Action.Cancel())
            {
                if(m_MapEntityToAdd != null)
                {
                    GameObject.Destroy(m_MapEntityToAdd.gameObject);
                    m_EntityToAdd = null;
                    m_MapEntityToAdd = null;
                }
            }
        }

        private Transform CreateMapEntity(Vector3 pos)
        {
            Transform entityTransform = null;
            switch (m_EntityToAdd.EntityType)
            {
                case EntityType.FacebookPhoto:
                    var fb = m_FbMapFactory.Create();
                    fb.m_FbID = m_EntityToAdd.ReferenceEntityID;
                    fb.transform.position = pos;
                    entityTransform = fb.gameObject.transform; ;
                    break;
                case EntityType.TwitterTweet:
                    var tweet = m_TwitterMapFactory.Create();
                    tweet.m_TweetID = m_EntityToAdd.ReferenceEntityID;
                    tweet.transform.position = pos;
                    entityTransform = tweet.gameObject.transform;
                    break;
                case EntityType.Video:
                    var video = m_VideoMapFactory.Create();
                    video.m_VideoID = m_EntityToAdd.ReferenceEntityID;
                    video.transform.position = pos;
                    entityTransform = video.gameObject.transform;
                    break;
            }
            entityTransform.gameObject.GetComponentInChildren<Button>().enabled = false;
            entityTransform.gameObject.GetComponentInChildren<Collider>().enabled = false;
            return entityTransform;
        }
    }
}
