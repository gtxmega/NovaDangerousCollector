using UnityEngine;

namespace Logics.Displaying.MainMenu
{
    public class DropsLootPanel : MonoBehaviour
    {
        [SerializeField] private DropLootWidget[] _widgets;

        public DropLootWidget[] GetWidgets => _widgets;

        public void Show(int countWidgets)
        {
            for (int i = 0; i < _widgets.Length; ++i)
            {
                if(i < countWidgets)
                    _widgets[i].Show();
                else
                    _widgets[i].Hide();
            }

            gameObject.SetActive(true);
        }

        public void Hide() => gameObject.SetActive(false);
    }
}