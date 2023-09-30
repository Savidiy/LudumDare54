using Sirenix.OdinInspector;
using UnityEngine;

namespace LudumDare54
{
    public sealed class LevelEditor : MonoBehaviour
    {
        private const string DEPENDENCIES = "Dependencies";
        [SerializeField, FoldoutGroup(DEPENDENCIES)] private LevelLibrary LevelLibrary;
        [SerializeField, FoldoutGroup(DEPENDENCIES)] private SpawnPointEditor SpawnPointEditorPrefab;

        [ValueDropdown(nameof(LevelIds))] public string LevelId;
        private ValueDropdownList<string> LevelIds => OdinLevelIdsProvider.LevelIds;
        private string _currentLevelId;

        private bool _isShowedNewLevelNameTextField;
        [ShowIf(nameof(_isShowedNewLevelNameTextField))] public string NewLevelId;

        [Button]
        private void CreateNewLevelButton()
        {
            if (_isShowedNewLevelNameTextField)
            {
                CreateNewLevel(NewLevelId);
                SaveLevel(NewLevelId);
                _isShowedNewLevelNameTextField = false;
            }
            else
            {
                _isShowedNewLevelNameTextField = true;
            }
        }

        [Button]
        private void AddSpawnPoint()
        {
            SpawnPointEditor spawnPointEditor = Instantiate(SpawnPointEditorPrefab, transform);
            spawnPointEditor.OnValidate();
        }

        [Button]
        private void SaveLevel()
        {
            SaveLevel(LevelId);
        }

        private void CreateNewLevel(string newLevelId)
        {
            if (LevelLibrary.TryGetLevelStaticData(newLevelId, out _))
            {
                Debug.LogError($"Level with this id '{newLevelId}' already exists");
            }
            else
            {
                LevelLibrary.CreateLevel(newLevelId);
                _currentLevelId = newLevelId;
                LevelId = newLevelId;
            }
        }

        private void OnValidate()
        {
            if (_currentLevelId != LevelId)
                LoadLevel(LevelId);
            else
                SaveLevel(LevelId);
        }

        private void LoadLevel(string levelId)
        {
            _currentLevelId = levelId;
            if (!LevelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
                return;

            foreach (SpawnPointStaticData spawnPointStaticData in levelStaticData.SpawnPoints)
            {
                SpawnPointEditor spawnPointEditor = Instantiate(SpawnPointEditorPrefab, spawnPointStaticData.Position,
                    Quaternion.Euler(spawnPointStaticData.Rotation), transform);

                spawnPointEditor.EnemyId = spawnPointStaticData.EnemyId;
            }
        }

        private void SaveLevel(string levelId)
        {
            if (!LevelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
                return;

            levelStaticData.SpawnPoints.Clear();
            foreach (SpawnPointEditor spawnPointEditor in GetComponentsInChildren<SpawnPointEditor>())
            {
                Transform spawnPointTransform = spawnPointEditor.transform;
                var spawnPointStaticData = new SpawnPointStaticData
                {
                    Position = spawnPointTransform.position,
                    Rotation = spawnPointTransform.rotation.eulerAngles,
                    EnemyId = spawnPointEditor.EnemyId
                };

                levelStaticData.SpawnPoints.Add(spawnPointStaticData);
            }
            
            LevelLibrary.ValidateAndSave();
        }
    }
}