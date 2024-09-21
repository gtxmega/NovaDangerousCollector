using Game;
using Services.Factory.Builders;
using Services.InputHandler;
using Services.locator;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Logics.Displaying.MainMenu
{
    public class WeaponCollection : MonoBehaviour, InjectDependency
    {
        [Header("Weapons")]
        [SerializeField] private WeaponConfig[] _weaponConfigs;

        [Header("UI")]
        [SerializeField] private WeaponCollectionSlotWidget _widgetPrefab;
        [SerializeField] private RectTransform _widgetContainer;

        [Header("References")]
        [SerializeField] private ArtifactDescriptionWidget _descriptionWidget;
        [SerializeField] private WeaponSelectWidget[] _weaponSelectWidgets;
        [SerializeField] private DragWidget _dragWidget;

        private int _weaponDragID = -1;
        private int _weaponSelectWidgetID = -1;

        private List<int> _selectedWeaponIdx = new();

        private GameConstants _gameConstants;
        private IInputHandler _inputHandler;

        public void Inject(IServicesLocator locator)
        {
            _gameConstants = locator.GetServices<GameConstants>();
            _inputHandler = locator.GetServices<IInputHandler>();

            FillWeaponCollection();
        }

        public WeaponConfig[] GetSelectedWeapons()
        {
            WeaponConfig[] weapons = new WeaponConfig[_selectedWeaponIdx.Count];
            for (int i = 0; i < _selectedWeaponIdx.Count; ++i)
            {
                weapons[i] = _weaponConfigs[_selectedWeaponIdx[i]];
            }

            return weapons;
        }

        public void OnClickWidget(int weaponID)
        {
            WeaponConfig weapon = _weaponConfigs[weaponID];

            int rare = (int)weapon.WeaponRare;

            _descriptionWidget.SetName(weapon.WeaponName);
            _descriptionWidget.SetPlanetName(weapon.PlanetName);
            _descriptionWidget.SetRareColor(_gameConstants.RareColors[rare]);
            _descriptionWidget.SetRareImage(_gameConstants.RareSprites[rare]);
            _descriptionWidget.SetDisplayImage(weapon.DisplayImage);

            StringBuilder sbWeaponAttributeText = new StringBuilder();
            sbWeaponAttributeText.AppendLine("---------------------------------");
            sbWeaponAttributeText.AppendLine("  Attributes");
            sbWeaponAttributeText.AppendLine(string.Empty);
            sbWeaponAttributeText.AppendLine(_weaponConfigs[weaponID].GetAttributeString());
            
            _descriptionWidget.SetPassiveBonusText(sbWeaponAttributeText.ToString());

            string uniqueAttributeString = _weaponConfigs[weaponID].GetUniqueAttributeString();
            if(uniqueAttributeString != string.Empty)
            {
                StringBuilder sbWeaponUniqueAttributeText = new StringBuilder();
                sbWeaponUniqueAttributeText.AppendLine("---------------------------------");
                sbWeaponUniqueAttributeText.AppendLine("  Unique attributes");
                sbWeaponUniqueAttributeText.AppendLine(string.Empty);
                sbWeaponUniqueAttributeText.AppendLine(uniqueAttributeString);
                sbWeaponUniqueAttributeText.AppendLine(string.Empty);

                _descriptionWidget.SetActiveBonusText(sbWeaponUniqueAttributeText.ToString());
            }else
            {
                _descriptionWidget.SetActiveBonusText(string.Empty);
            }

            _descriptionWidget.ShowArtifact();
        }

        public void OnEnterSelectedWidget(int widgetID)
        {
            _weaponSelectWidgetID = widgetID;
        }

        public void OnExitSelectedWidget(int widgetID)
        {
            _weaponSelectWidgetID = -1;
        }

        public void OnBeginDragWidget(int weaponID)
        {
            _weaponDragID = weaponID;
            _dragWidget.SetDisplayImage(_weaponConfigs[weaponID].DisplayImage);
            _dragWidget.SetRareImage(_gameConstants.RareSprites[(int)_weaponConfigs[weaponID].WeaponRare]);
            _dragWidget.UpdatePosition(_inputHandler.GetMousePosition());
            _dragWidget.Show();
        }

        public void OnDragWidget(int weaponID)
        {
            _dragWidget.UpdatePosition(_inputHandler.GetMousePosition());
        }

        public bool OnEndDragWidget(int weaponID)
        {
            bool status = false;

            if (_weaponSelectWidgetID != -1 && _weaponSelectWidgets[_weaponSelectWidgetID].IsEmpty)
            {
                int weaponRare = (int)_weaponConfigs[weaponID].WeaponRare;

                _weaponSelectWidgets[_weaponSelectWidgetID].SetWeapon(weaponID);
                _weaponSelectWidgets[_weaponSelectWidgetID].SetRareImage(_gameConstants.RareSprites[weaponRare]);
                _weaponSelectWidgets[_weaponSelectWidgetID].SetDisplayImage(_weaponConfigs[weaponID].DisplayImage);
                _selectedWeaponIdx.Add(weaponID);
                status = true;
            }

            _dragWidget.Hide();
            return status;
        }

        private void FillWeaponCollection()
        {
            for (int i = 0; i < _weaponSelectWidgets.Length; ++i)
            {
                _weaponSelectWidgets[i].SetWeaponCollection(this);
                _weaponSelectWidgets[i].SetWidgetID(i);
            }

            for (int i = 0; i < _weaponConfigs.Length; ++i)
            {
                WeaponCollectionSlotWidget widget = Instantiate(_widgetPrefab, _widgetContainer);

                int weaponRare = (int)_weaponConfigs[i].WeaponRare;

                widget.SetWeaponCollection(this);
                widget.SetWeaponID(i);
                widget.SetDisplayImage(_weaponConfigs[i].DisplayImage);
                widget.SetWeaponName(_weaponConfigs[i].WeaponName);
                widget.SetRareImage(_gameConstants.RareSprites[weaponRare]);
                widget.SetRareColor(_gameConstants.RareColors[weaponRare]);
                widget.Unlock();
            }
        }
    }
}