using UnityEngine;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace UI
{
    public class QuitApplication : MonoBehaviour
    {
        [Header("Visual Element")]
        [SerializeField] private string VisualButtonElementName = "Quit";

        private UIDocument uiDocument = null;

        private void OnEnable()
        {
            // Connect with UI
            if (TryGetComponent<UIDocument>(out uiDocument))
            {
                // Get Specific button
                Button quitButton = uiDocument.rootVisualElement.Q<Button>(VisualButtonElementName);
                // Set call function
                if (quitButton != null) quitButton.clicked += Exit;
            }
        }

        /// <summary>
        /// Quit the current execution, whether it's a build or in the editor
        /// </summary>
        private void Exit()
        {
            #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
            #else 
            Application.Quit();
            #endif
        }
    }
}
