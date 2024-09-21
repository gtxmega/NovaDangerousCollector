using Game;
using Logics.Actors;
using Logics.Artifacts;
using Services.Events;
using Services.Factory.Builders;
using Services.locator;
using UnityEngine;

namespace Logics.Displaying.MainMenu
{
    public class WorldsCollection : MonoBehaviour, InjectDependency
    {
        [SerializeField] private WorldConfig[] _worldConfig;

        [Header("UI")]
        [SerializeField] private WorldCollectionSlotWidget _slotWidgetPrefab;
        [SerializeField] private RectTransform _collectionContainer;

        [Header("References")]
        [SerializeField] private ArtifactDescriptionWidget _descriptionWidget;

        private GameConstants _gameConstants;
        private GameInstance _gameInstance;
        private IMenuEventsExec _menuEventsExec;

        private int _currentSelectedWorld = -1;

        public void Inject(IServicesLocator locator)
        {
            _gameConstants = locator.GetServices<GameConstants>();
            _gameInstance = locator.GetServices<GameInstance>();
            _menuEventsExec = locator.GetServices<IMenuEventsExec>();

            FillCollection();
        }

        public void OnClickStartWorld()
        {
            if (_currentSelectedWorld != -1)
            {
                _menuEventsExec.OnPlayerStartWorld(_worldConfig[_currentSelectedWorld].SceneID);
            }
        }

        public void OnClickToWorld(int worldID)
        {
            _currentSelectedWorld = worldID;
            int worldRare = (int)_worldConfig[worldID].WorldRare;

            _descriptionWidget.SetName(_worldConfig[worldID].WorldName);
            _descriptionWidget.SetRareImage(_gameConstants.RareSprites[worldRare]);
            _descriptionWidget.SetRareColor(_gameConstants.RareColors[worldRare]);
            _descriptionWidget.SetDisplayImage(_worldConfig[worldID].DisplayImage);

            ArtifactConfig[] artifacts = _worldConfig[worldID].ArtifactConfigs;
            if (artifacts.Length > 0)
            {
                DropLootWidget[] artifactDropWidgets = _descriptionWidget.GetArtifactDropWidgets();
                for (int i = 0; i < artifacts.Length; ++i)
                {
                    int artifactRare = (int)artifacts[i].Rare;
                    Sprite artifactRareSprite = _gameConstants.RareSprites[artifactRare];
                    Sprite artifactDisplayImage = artifacts[i].DisplayImage;

                    artifactDropWidgets[i].SetRareImage(artifactRareSprite);
                    artifactDropWidgets[i].SetDisplayImage(artifactDisplayImage);
                    artifactDropWidgets[i].Show();
                }

                _descriptionWidget.ShowArtifactsDropsPanel(artifacts.Length);
            }else
            {
                _descriptionWidget.HideArtifactsDropsPanel();
            }

            WeaponConfig[] weapons = _worldConfig[worldID].WeaponConfigs;
            if (weapons.Length > 0)
            {
                DropLootWidget[] weaponDropWidgets = _descriptionWidget.GetWeaponDropWidgets();
                for (int i = 0; i < weapons.Length; ++i)
                {
                    int weaponRare = (int)weapons[i].WeaponRare;
                    Sprite weaponRareSprite = _gameConstants.RareSprites[weaponRare];
                    Sprite weaponDisplayImage = weapons[i].DisplayImage;

                    weaponDropWidgets[i].SetRareImage(weaponRareSprite);
                    weaponDropWidgets[i].SetDisplayImage(weaponDisplayImage);
                    weaponDropWidgets[i].Show();
                }

                _descriptionWidget.ShowWeaponDropsPanel(weapons.Length);
            }else
            {
                _descriptionWidget.HideWeaponsDropsPanel();
            }

            NpcActorConfig[] populations = _worldConfig[worldID].NpcActorConfigs;
            if (populations.Length > 0)
            {
                DropLootWidget[] populationWidgets = _descriptionWidget.GetPopulationDropWidgets();
                for (int i = 0; i < populations.Length; ++i)
                {
                    Sprite populationDisplayImage = populations[i].DisplayImage;

                    populationWidgets[i].SetDisplayImage(populationDisplayImage);
                    populationWidgets[i].Show();
                }

                _descriptionWidget.ShowPopulationPanel(populations.Length);
            }else
            {
                _descriptionWidget.HidePopulationPanel();
            }

            _descriptionWidget.ShowWorldButton();
            _descriptionWidget.ShowWorld();
        }

        private void FillCollection()
        {
            for (int i = 0; i < _worldConfig.Length; ++i)
            {
                WorldCollectionSlotWidget widget = Instantiate(_slotWidgetPrefab, _collectionContainer);

                widget.SetWorldIndex(i);
                widget.SetName(_worldConfig[i].WorldName);
                widget.SetDisplayImage(_worldConfig[i].DisplayImage);
                widget.SetRareImage(_gameConstants.RareSprites[(int)_worldConfig[i].WorldRare]);
                widget.SetRareColor(_gameConstants.RareColors[(int)_worldConfig[i].WorldRare]);
                widget.SetWorldCollection(this);
            }
        }
    }
}