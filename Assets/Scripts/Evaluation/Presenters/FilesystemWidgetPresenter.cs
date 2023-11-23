using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using UniRx;
using Assets.Scripts.Evaluation.Model.Filesystem;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Actions;
using Assets.Scripts.View;
using Assets.Scripts.Evaluation.Utils;

namespace Assets.Scripts.Evaluation.Presenters
{
    public class FilesystemWidgetPresenter : MonoBehaviour, IInitializable, IDisposable
    {
        public GameObject m_FilesystemWidget;
        public Button m_CloseButton;
        public Button m_ImportButton;

        private Repository<FSInfo> m_Repository;
        private CompositeDisposable m_Sub = new CompositeDisposable();
        private ListBox m_Listbox;
        private TopWidgetTracker m_TopWidgetTracker;
        public void Dispose()
        {
            m_Sub.Dispose();
        }

        public void Awake()
        {
            m_Listbox = m_FilesystemWidget.GetComponent<ListBox>();
        }

        [Inject]
        public void Init(Repository<FSInfo> repository, TopWidgetTracker topWidgetTracker)
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
                item.ImageIndex.Value = (int)info.Filetype;
                item.Text.Value = info.Filename;
                item.Checked.Value = false;
                m_Listbox.AddListItem(item);
            });
        }

        private void OnImport()
        {
            m_TopWidgetTracker.PopWidget(m_FilesystemWidget);
            var items = m_Listbox.GetSelectedListItems();
            var filenames = items.Select(x => int.Parse(x.ID.Value)).Select(id => m_Repository[id].Filename).Aggregate((s1, s2) => { return s1 + ", " + s2; });
            if(filenames != null)
            {
                string text = "Files: " + filenames + " were imported";
                MessageBroker.Default.Publish(new ShowInfoOnHUDAction(text, 3.0f));
                MessageBroker.Default.Publish(new FileUploadedTaskAction("FileUploadedTaskTag"));
            }
        }

        public void Initialize()
        {
            m_Sub.Add(MessageBroker.Default.Receive<ShowFileSystemAction>().Subscribe(_ =>
            {
                m_TopWidgetTracker.PushWidget(m_FilesystemWidget);
            }));
            m_Sub.Add(m_CloseButton.OnClickAsObservable().Merge(MessageBroker.Default.Receive<CancelAction>().Select(_=>Unit.Default)).
                Where(_ => m_FilesystemWidget.activeInHierarchy && m_TopWidgetTracker.IsTop(m_FilesystemWidget))
                .Subscribe(_ =>
            {                
                m_TopWidgetTracker.PopWidget(m_FilesystemWidget);
            }));
            m_Sub.Add(m_ImportButton.OnClickAsObservable().Subscribe(_ =>
            {
                OnImport();
            }));
            m_Sub.Add(MessageBroker.Default.Receive<OKAction>().Where(_=> m_FilesystemWidget.activeInHierarchy
            && m_TopWidgetTracker.IsTop(m_FilesystemWidget)).Subscribe(_ =>
            {
                OnImport();
            }));
            PopulateListBox();
        }
    }
}
