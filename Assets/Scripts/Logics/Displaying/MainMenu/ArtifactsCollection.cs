using Game.Types;
using Logics.Artifacts;
using Services.locator;
using System.Collections.Generic;
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

        private List<ArtifactCollectionSlotWidget> _widgets = new();

        private int _currentArtifactDragID = -1;
        private int _currentArtifactWidgetID = -1;

        public void Inject(IServicesLocator locator)
        {
            FillArtifactsCollection();
        }

        public void OnEnterSelectWidget(int widgetID) => _currentArtifactWidgetID = widgetID;
        public void OnExitSelectWidget(int widgetID) => _currentArtifactWidgetID = -1;
        public void OnBeginDragWidget(int artifactID) => _currentArtifactDragID = artifactID;

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
                    _selectedWidgets[_currentArtifactWidgetID].SetSlotRare((int)artifactRare);
                    _selectedWidgets[_currentArtifactWidgetID].SetDisplayImage(_artifactConfigs[artifactID].DisplayImage);
                    status = true;
                }
            }

            _currentArtifactDragID = -1;
            _currentArtifactWidgetID = -1;
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

                widget.SetArtifactsCollection(this);
                widget.SetArtifactID(i);
                widget.SetNameText(_artifactConfigs[i].ArtifactName);
                widget.SetDisplayImage(_artifactConfigs[i].DisplayImage);
                widget.SetRareWidget((int)_artifactConfigs[i].Rare);
                widget.Unlock();

                _widgets.Add(widget);
            }
        }
    }
}