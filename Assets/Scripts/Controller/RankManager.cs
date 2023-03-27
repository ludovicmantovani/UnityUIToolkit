using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class RankManager : MonoBehaviour
    {
        [SerializeField] private int maxSlot = 20; 

        private List<string> _rankList = null;

        public int MaxSlot { get => maxSlot;}

        public delegate void OnUpdateRanking(List<string> ranklist);
        public static event OnUpdateRanking onUpdateRanking;

        void Start()
        {
            _rankList = new List<string>();
        }

        /// <summary>
        /// Add name in rank list.
        /// Can choose if insert is in first or last position.
        /// No insert if max slot is reached.
        /// Make onUpdateRanking event after insert. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="lastPosition"></param>
        public void AddRank(string name = "", bool lastPosition = true)
        {
            if (name.Length > 0 && _rankList.Count < maxSlot)
            {
                int index = lastPosition ? _rankList.Count : 0;
                _rankList.Insert(index, name);
                if (onUpdateRanking != null)
                    onUpdateRanking(_rankList);
            }
        }
        /// <summary>
        /// Remove name in rank list.
        /// Make onUpdateRanking event after remove.
        /// </summary>
        /// <param name="index"></param>
        public void DeleteRank(int index)
        {
            if (index >= 0 && index < _rankList.Count)
            {
                _rankList.RemoveAt(index);
                if (onUpdateRanking != null)
                    onUpdateRanking(_rankList);
            }
        }
    }
}
