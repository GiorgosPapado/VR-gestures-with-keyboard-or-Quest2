using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zenject;

namespace Assets.Scripts.Utils
{
    public class Repository<T> : IEnumerable<T> where T : IRepositoryItem
    {
        private Dictionary<int, T> m_Entities = new Dictionary<int, T>();
        private int m_NextID = 0;
        public Repository()
        {

        }
        [Inject]
        public Repository(IEnumerable<T> entities)
        {
            entities.ForEach(x => {
                x.ID = m_NextID;
                m_Entities.Add(m_NextID++, x);
                });
        }

        public T this[int id]
        {
            get
            {
                return m_Entities[id];
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return m_Entities.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return m_Entities.Values.GetEnumerator();
        }
    }
}
