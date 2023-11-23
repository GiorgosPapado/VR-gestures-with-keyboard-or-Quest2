using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniRx;
using Assets.Scripts.Utils;
using Assets.Scripts.Evaluation.Gestures.ONNX;
using Assets.Scripts.Evaluation.Gestures.Interface;

namespace Assets.Scripts.Evaluation.Gestures.Filters
{
    public enum MajorityFilterMode
    {
        ReturnDefaultIfThresholdNotExceeded,
        ReturnMajorVote
    }
    public class MajorityVotingFilter<T> 
    {
        // may require re-implementation with max-heap if this is slow

        private LinkedList<T> m_Values = new LinkedList<T>();

        private T m_LastMajorValue;

        public int CountThreshold { get; set; } = 8;
        public int WindowSize { get; set; } = 11;
        public T DefaultValueIfThresholdNotExceeded { get; set; }        
        
        private void UpdateMajorValue()
        {
            Dictionary<T, int> votes = new Dictionary<T, int>();

            for(LinkedListNode<T> node = m_Values.First; node != null; node=node.Next)
            {
                if (votes.ContainsKey(node.Value))
                    votes[node.Value] = votes[node.Value] + 1;
                else
                    votes.Add(node.Value, 1);
            }

            int max_votes = 0;
            T majorValue =default(T);
            foreach (KeyValuePair<T,int> kvp in votes)
            {
                if (kvp.Value > max_votes) 
                { 
                    max_votes = kvp.Value;
                    majorValue = kvp.Key;
                }
            }   
            
            if(FilterMode == MajorityFilterMode.ReturnDefaultIfThresholdNotExceeded) 
            { 
                m_LastMajorValue = max_votes > CountThreshold ? majorValue : DefaultValueIfThresholdNotExceeded;
            }
            else if(FilterMode == MajorityFilterMode.ReturnMajorVote)
            {
                m_LastMajorValue = majorValue;
            }
        }
        public T AddSample(T value)
        {
            if(m_Values.Count >= WindowSize)
            {
                m_Values.RemoveFirst();                
            }
            m_Values.AddLast(value);
            UpdateMajorValue();
            return m_LastMajorValue;
        }
        public T Peek()
        {
            return m_LastMajorValue;
        }

        public MajorityFilterMode FilterMode { get; set; } = MajorityFilterMode.ReturnDefaultIfThresholdNotExceeded;
    }
}