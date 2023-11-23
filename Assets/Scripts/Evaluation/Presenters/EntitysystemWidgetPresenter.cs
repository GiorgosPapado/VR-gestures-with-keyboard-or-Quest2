using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Zenject;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Model.Entities;
using Assets.Scripts.View;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    class EntitysystemWidgetPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        public GameObject m_EntitysystemWidget;
        public Button m_CloseButton;
        public Button m_PinButton;

        private Repository<EntityInfo> m_Repository;
        private CompositeDisposable m_Sub = new CompositeDisposable();
        private ListBox m_Listbox;
        private TopWidgetTracker m_TopWidgetTracker;
        public void Dispose()
        {
            m_Sub.Dispose();
        }

        public void Awake()
        {
            m_Listbox = m_EntitysystemWidget.GetComponent<ListBox>();
        }

        [Inject]
        public void Init(Repository<EntityInfo> repository, TopWidgetTracker topWidgetTracker)
        {
            m_Repository = repository;
            m_TopWidgetTracker = topWidgetTracker;
        }

        private void PopulateListBox()
        {
            m_Repository.ForEach(info =>
            {
                ListBoxItem item = new ListBoxItem();
                item.ID.Value = info.ID.ToString();
                item.ImageIndex.Value = (int)info.EntityType;
                item.Text.Value = info.Name;
                item.Checked.Value = false;
                m_Listbox.AddListItem(item);
            });
        }

        private void OnPin()
        {
            m_TopWidgetTracker.PopWidget(m_EntitysystemWidget);
            var items = m_Listbox.GetSelectedListItems();
            var entity = items.FirstOrDefault();
            if(entity!=null)
            {
                MessageBroker.Default.Publish(new AddEntityOnMapAction(m_Repository[int.Parse(entity.ID.Value)]));
            }            
        }

        public void Initialize()
        {
            m_Sub.Add(MessageBroker.Default.Receive<ShowEntitySystemAction>().Subscribe(_ =>
            {
                m_TopWidgetTracker.PushWidget(m_EntitysystemWidget);                
            }));
            m_Sub.Add(m_CloseButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<CancelAction>().Select(_ => Unit.Default)).Where(
                _=> m_EntitysystemWidget.activeInHierarchy && m_TopWidgetTracker.IsTop(m_EntitysystemWidget)).Subscribe(_ =>
            {
                m_TopWidgetTracker.PopWidget(m_EntitysystemWidget);
            }));
            m_Sub.Add(m_PinButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<OKAction>()
                .Where(_=> m_EntitysystemWidget.activeInHierarchy && m_TopWidgetTracker.IsTop(m_EntitysystemWidget)).Select(_=>Unit.Default)).Subscribe(_ =>
            {
                OnPin();
            }));
            PopulateListBox();
        }
    }
}
