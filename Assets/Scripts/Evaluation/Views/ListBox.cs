using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;
using TMPro;
using Assets.Scripts.Utils;

namespace Assets.Scripts.View
{
    public class ListBoxItem
    {
        public ReactiveProperty<string> ID { get; set; } = new ReactiveProperty<string>(string.Empty);
        public ReactiveProperty<string> Text { get; set; } = new ReactiveProperty<string>(string.Empty);
        public ReactiveProperty<bool> Checked { get; set; } = new ReactiveProperty<bool>(false);
        public ReactiveProperty<int> ImageIndex { get; set; } = new ReactiveProperty<int>();
    }

    public class ListBox : MonoBehaviour
    {
        public GameObject ListItemPrefab = null;
        public GameObject Content = null;
        public Sprite[] m_ImageRepository;
        public bool m_SingleSelection = false;
        private List<ListBoxItem> m_ListItems = new List<ListBoxItem>();
        private List<GameObject> m_ListBoxItemViews = new List<GameObject>();
        private List<IDisposable> m_ListItemSubs = new List<IDisposable>();

        private void UncheckAll()
        {
            m_ListItems.ForEach(x => x.Checked.Value = false);
        }

        public void AddListItem(ListBoxItem item)
        {
            var view = Instantiate(ListItemPrefab, Content.transform);
            CompositeDisposable subs = new CompositeDisposable();
            // model to view bindings
            subs.Add(item.Text.AsObservable().SubscribeToText(view.GetComponentInChildren<TextMeshProUGUI>()));
            subs.Add(item.Checked.AsObservable().Subscribe(v => view.GetComponentInChildren<Toggle>().SetIsOnWithoutNotify(v)));
            // view to model bindings
            subs.Add(view.GetComponentInChildren<Toggle>().OnValueChangedAsObservable().Subscribe(x => {
                if (m_SingleSelection) {
                    UncheckAll();
                }
                item.Checked.Value = x;
            }));
            view.GetComponentsInChildren<Image>().Where(x=>x.CompareTag("icon")).First().sprite = m_ImageRepository[item.ImageIndex.Value];   
            m_ListItems.Add(item);
            m_ListBoxItemViews.Add(view);
            m_ListItemSubs.Add(subs);
        }
        public void RemoveListItem(string itemID)
        {
            var itemIndex = m_ListItems.FindIndex(x => x.ID.Value == itemID);
            if (itemIndex >= 0)
            {
                m_ListItemSubs[itemIndex].Dispose();
                GameObject.Destroy(m_ListBoxItemViews[itemIndex]);
                m_ListItems.RemoveAt(itemIndex);
                m_ListBoxItemViews.RemoveAt(itemIndex);
                m_ListItemSubs.RemoveAt(itemIndex);
            }
        }
        public void RemoveListItems(IEnumerable<string> itemIDs)
        {
            var toRemove = new HashSet<string>(itemIDs);
            for (int i = m_ListItems.Count - 1; i >= 0; i--)
            {
                if (toRemove.Contains(m_ListItems[i].ID.Value))
                {
                    m_ListItemSubs[i].Dispose();
                    GameObject.Destroy(m_ListBoxItemViews[i]);
                    m_ListItems.RemoveAt(i);
                    m_ListBoxItemViews.RemoveAt(i);
                    m_ListItemSubs.RemoveAt(i);
                }
                //else
                //{
                //    i--;
                //}                
            }
        }
        public IList<ListBoxItem> GetSelectedListItems()
        {
            return m_ListItems.Where(x => x.Checked.Value).ToList();
        }
        public void Clear()
        {
            int count = m_ListItems.Count;
            for(int i=count-1;i>=0;i--)
            {
                m_ListItemSubs[i].Dispose();
                GameObject.Destroy(m_ListBoxItemViews[i]);
                m_ListItems.RemoveAt(i);
                m_ListBoxItemViews.RemoveAt(i);
                m_ListItemSubs.RemoveAt(i);
            }
        }

        public void ToggleSelections()
        {
            int totalChecked = m_ListItems.Where(x => x.Checked.Value).Sum(x => 1);
            int totalUnchecked = m_ListItems.Count - totalChecked;
            bool check = totalUnchecked > totalChecked;
            m_ListItems.ForEach(x => x.Checked.Value = check);
        }
        /*
        public void RemoveSelected()
        {
            for(int i=m_ListItems.Count-1; i>=0;)
            {
                if(m_ListItems[i].Checked.Value)
                {
                    m_ListItems.RemoveAt(i);
                    m_ListBoxItemViews.RemoveAt(i);
                    m_ListItemSubs[i].Dispose();
                    m_ListItemSubs.RemoveAt(i);
                }
                else
                {
                    i--;
                }
            }
        }*/
    }
}
