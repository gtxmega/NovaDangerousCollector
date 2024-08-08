using UnityEngine;
using UnityEngine.EventSystems;

namespace Logics.Displaying.MainMenu
{
    public class CloseAllPanels : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Canvas[] _allPanels;

        public void OnPointerClick(PointerEventData eventData)
        {
            for (int i = 0; i < _allPanels.Length; ++i)
            {
                _allPanels[i].enabled = false;
            }
        }
    }
}