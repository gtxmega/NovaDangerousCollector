using Game;
using Game.Types;
using Logics.Artifacts;
using Services.InputHandler;
using Services.locator;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Logics.Displaying.MainMenu
{
    public class ArtifactsCollection : MonoBehaviour, InjectDependency
    {
        [Header("Artifacts")]
        [SerializeField] private ArtifactConfig[] _artifactConfigs;

        [Header("UI")]
        [SerializeField] private ArtifactCollectionSlotWidget _artifactSlotWidget;
        [SerializeField] private RectTransform _collectionContainer;

        [Header("References")]
        [SerializeField] private ArtifactSelectWidget[] _selectedWidgets;
        [SerializeField] private ArtifactDescriptionWidget _descriptionWidget;
        [SerializeField] private DragWidget _dragWidget;

        private List<ArtifactCollectionSlotWidget> _widgets = new();

        private int _currentArtifactDragID = -1;
        private int _currentArtifactWidgetID = -1;

        private List<int> _selectedArtifactsIdx = new();

        private GameConstants _gameConstants;
        private IInputHandler _inputHandler;

        public void Inject(IServicesLocator locator)
        {
            _gameConstants = locator.GetServices<GameConstants>();
            _inputHandler = locator.GetServices<IInputHandler>();

            FillArtifactsCollection();
        }

        public ArtifactConfig[] GetSelectedArtifacts()
        {
            ArtifactConfig[] artifacts = new ArtifactConfig[_selectedArtifactsIdx.Count];
            for (int i = 0; i < _selectedArtifactsIdx.Count; ++i)
            {
                artifacts[i] = _artifactConfigs[_selectedArtifactsIdx[i]];
            }

            return artifacts;
        }

        public void OnClickCollectionWidget(int artifactID)
        {
            ArtifactConfig artifact = _artifactConfigs[artifactID];

            int rare = (int)artifact.Rare;

            _descriptionWidget.SetRareColor(_gameConstants.RareColors[rare]);
            _descriptionWidget.SetRareImage(_gameConstants.RareSprites[rare]);
            _descriptionWidget.SetDisplayImage(artifact.DisplayImage);
            _descriptionWidget.SetName(artifact.ArtifactName);
            _descriptionWidget.SetPlanetName(artifact.PlanetName);

            StringBuilder sbPassiveText = new StringBuilder();
            sbPassiveText.AppendLine("---------------------------------");
            sbPassiveText.AppendLine("  Passive bonus");
            sbPassiveText.AppendLine(string.Empty);
            sbPassiveText.AppendLine(artifact.GetPassiveBonusText());

            _descriptionWidget.SetPassiveBonusText(sbPassiveText.ToString());

            string activeBonusText = artifact.GetActiveBonusText();
            if (activeBonusText != string.Empty)
            {
                StringBuilder sbActiveText = new StringBuilder();
                sbActiveText.AppendLine("---------------------------------");
                sbActiveText.AppendLine("  Active bonus");
                sbActiveText.AppendLine(string.Empty);
                sbActiveText.AppendLine(activeBonusText);
                sbActiveText.AppendLine(string.Empty);

                _descriptionWidget.SetActiveBonusText(sbActiveText.ToString());
            }else
            {
                _descriptionWidget.SetActiveBonusText(string.Empty);
            }

            _descriptionWidget.ShowArtifact();
        }

        public void OnEnterSelectWidget(int widgetID) => _currentArtifactWidgetID = widgetID;
        public void OnExitSelectWidget(int widgetID) => _currentArtifactWidgetID = -1;
        public void OnBeginDragWidget(int artifactID)
        {
            _currentArtifactDragID = artifactID;
            _dragWidget.SetDisplayImage(_artifactConfigs[artifactID].DisplayImage);
            _dragWidget.SetRareImage(_gameConstants.RareSprites[(int)_artifactConfigs[artifactID].Rare]);
            _dragWidget.UpdatePosition(_inputHandler.GetMousePosition());
            _dragWidget.Show();
        }

        public void OnDragWidget(int weaponID)
        {
            _dragWidget.UpdatePosition(_inputHandler.GetMousePosition());
        }

        public bool OnEndDrag(int artifactID)
        {
            bool status = false;

            if (_currentArtifactWidgetID != -1 && _selectedWidgets[_currentArtifactWidgetID].IsEmpty)
            {
                string artifactName = _artifactConfigs[artifactID].ArtifactName;
                EArtifactRare artifactRare = _artifactConfigs[artifactID].Rare;

                int countExitArtifacts = 0;
                for (int i = 0; i < _selectedWidgets.Length; ++i)
                {
                    if (_selectedWidgets[i].IsEmpty) continue;

                    if (_artifactConfigs[_selectedWidgets[i].ArtifactID].ArtifactName == artifactName)
                        countExitArtifacts++;
                }

                if(countExitArtifacts == 0 && _selectedWidgets[_currentArtifactWidgetID].CheckSetCondition(artifactRare))
                {
                    _selectedWidgets[_currentArtifactWidgetID].SetArtifact(artifactID);
                    _selectedWidgets[_currentArtifactWidgetID].SetRareImage(_gameConstants.RareSprites[(int)artifactRare]);
                    _selectedWidgets[_currentArtifactWidgetID].SetDisplayImage(_artifactConfigs[artifactID].DisplayImage);
                    status = true;
                    _selectedArtifactsIdx.Add(artifactID);
                }
            }

            _currentArtifactDragID = -1;
            _currentArtifactWidgetID = -1;
            _dragWidget.Hide();
            return status;
        }

        private void FillArtifactsCollection()
        {
            for (int i = 0; i < _selectedWidgets.Length; ++i)
            {
                _selectedWidgets[i].SetArtifactCollection(this);
                _selectedWidgets[i].SetWidgetID(i);
            }

            for (int i = 0; i < _artifactConfigs.Length; ++i)
            {
                ArtifactCollectionSlotWidget widget = Instantiate(_artifactSlotWidget, _collectionContainer);

                int artifactRare = (int)_artifactConfigs[i].Rare;

                widget.SetArtifactsCollection(this);
                widget.SetArtifactID(i);
                widget.SetNameText(_artifactConfigs[i].ArtifactName);
                widget.SetDisplayImage(_artifactConfigs[i].DisplayImage);
                widget.SetRareImage(_gameConstants.RareSprites[artifactRare]);
                widget.SetRareColor(_gameConstants.RareColors[artifactRare]);
                widget.Unlock();

                _widgets.Add(widget);
            }
        }
    }
}