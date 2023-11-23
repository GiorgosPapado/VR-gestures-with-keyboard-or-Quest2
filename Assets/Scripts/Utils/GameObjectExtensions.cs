using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class GameObjectExtensions
    {
        public static bool HasTagInHierarchy(this GameObject go, string tag)
        {
            Transform trans = go.transform;
            while(trans != null)
            {
                if (trans.gameObject.CompareTag(tag))
                {
                    return true;
                }
                trans = trans.parent;
            }
            return false;
        }

        public static GameObject GetParentWithTag(this GameObject go, string tag)
        {
            Transform trans = go.transform;
            while (trans != null)
            {
                if (trans.gameObject.CompareTag(tag))
                {
                    return trans.gameObject;
                }
                trans = trans.parent;
            }
            return null;
        }

        public static GameObject GetChildWithName(this GameObject obj, string name)
        {
            Transform trans = obj.transform;
            Transform childTrans = trans.Find(name);
            if (childTrans != null)
            {
                return childTrans.gameObject;
            }
            else
            {
                return null;
            }
        }
    }
}
