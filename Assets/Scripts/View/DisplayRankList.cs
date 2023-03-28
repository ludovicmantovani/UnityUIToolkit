using System.Collections.Generic;
using UnityEngine.UIElements;
using UnityEngine;

namespace View
{
    public class DisplayRankList : MonoBehaviour
    {
        [Header("Visual Element")]
        [SerializeField] private string listViewName = "List";
        [SerializeField] private string listItemTemplateName = "RankItem";
        
        private UIDocument uiDocument = null;
        private ListView listView = null;
        private VisualTreeAsset listItemAsset = null;
        
        private void OnEnable()
        {
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                listView = uiDocument.rootVisualElement.Q<ListView>(listViewName);
                listItemAsset = Resources.Load<VisualTreeAsset>(listItemTemplateName);
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
            if (listView == null || listItemAsset == null) return;
            listView.itemsSource = ranklist;
            listView.makeItem = () => listItemAsset.Instantiate();
            listView.bindItem = (elem, index) =>
            {
                Label indexLabel = elem.Q<Label>("RankIndex");
                Label nameLabel = elem.Q<Label>("RankName");

                if (indexLabel != null) indexLabel.text = (index + 1).ToString();
                if (nameLabel != null) nameLabel.text = ranklist[index];
            };
            listView.fixedItemHeight = 40;
        }
    }
}
