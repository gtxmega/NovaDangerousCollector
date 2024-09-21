using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Logics.Displaying.MainMenu
{
    public class WorldCollectionSlotWidget : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _worldNameText;
        [SerializeField] private Image _worldDisplayImage;
        [SerializeField] private Image _worldRareImage;

        private int _worldIndex;
        private WorldsCollection _worldCollection;

        public void SetName(string name) => _worldNameText.text = name;
        public void SetDisplayImage(Sprite sprite) => _worldDisplayImage.sprite = sprite;
        public void SetRareImage(Sprite sprite) => _worldRareImage.sprite = sprite;
        public void SetRareColor(Color color) => _worldNameText.color = color;
        public void SetWorldIndex(int index) => _worldIndex = index;
        public void SetWorldCollection(WorldsCollection worldsCollection) => _worldCollection = worldsCollection;

        public void OnPointerClick(PointerEventData eventData)
        {
            _worldCollection.OnClickToWorld(_worldIndex);
        }
    }
}