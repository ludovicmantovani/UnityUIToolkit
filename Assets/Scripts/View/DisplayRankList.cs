using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace View
{
    public class DisplayRankList : MonoBehaviour
    {
        [Header("Visual Element")]
        [SerializeField] private string listViewName = "List";
        
        private UIDocument uiDocument = null;
        private ListView listView = null;
        
        private void OnEnable()
        {
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                listView = uiDocument.rootVisualElement.Q<ListView>(listViewName);
            }
            Controller.RankManager.onUpdateRanking += RefreshListView;
        }
        private void OnDisable()
        {
            Controller.RankManager.onUpdateRanking -= RefreshListView;
        }

        /// <summary>
        /// Called after ranklist update.
        /// Display this new rank list.
        /// </summary>
        /// <param name="ranklist"></param>
        private void RefreshListView(List<string> ranklist)
        {
            if (listView == null) return;
            listView.itemsSource = ranklist;
            listView.makeItem = () => new Label();
            listView.bindItem = (elem, index) => (elem as Label).text = (index + 1).ToString() + " " + ranklist[index];
        }
    }
}
