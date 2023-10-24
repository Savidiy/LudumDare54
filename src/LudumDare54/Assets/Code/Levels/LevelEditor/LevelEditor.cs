#if UNITY_EDITOR
using Sirenix.OdinInspector;
using UnityEditor;
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
        [Min(1)] public float LevelWidth = 2;

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
            if (EditorApplication.isPlaying)
                return;

            if (_currentLevelId != LevelId)
                LoadLevel(LevelId);
        }

        private void LoadLevel(string levelId)
        {
            _currentLevelId = levelId;
            if (!LevelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
                return;

            LevelWidth = levelStaticData.Width;

            SpawnPointEditor[] spawnPointEditors = GetComponentsInChildren<SpawnPointEditor>(includeInactive: true);
            var index = 0;

            foreach (SpawnPointStaticData spawnPointStaticData in levelStaticData.SpawnPoints)
            {
                SpawnPointEditor spawnPointEditor = index < spawnPointEditors.Length
                    ? spawnPointEditors[index]
                    : Instantiate(SpawnPointEditorPrefab, transform);

                spawnPointEditor.transform.position = spawnPointStaticData.Position;
                spawnPointEditor.transform.rotation = Quaternion.Euler(spawnPointStaticData.Rotation);

                spawnPointEditor.ShipType = spawnPointStaticData.ShipType;
                spawnPointEditor.OnValidate();
                spawnPointEditor.gameObject.SetActive(true);
                index++;
            }

            for (int i = index; i < spawnPointEditors.Length; i++)
                spawnPointEditors[i].gameObject.SetActive(false);
        }

        private void SaveLevel(string levelId)
        {
            if (!LevelLibrary.TryGetLevelStaticData(levelId, out LevelStaticData levelStaticData))
                return;

            levelStaticData.Width = LevelWidth;
            levelStaticData.SpawnPoints.Clear();
            foreach (SpawnPointEditor spawnPointEditor in GetComponentsInChildren<SpawnPointEditor>())
            {
                Transform spawnPointTransform = spawnPointEditor.transform;
                var spawnPointStaticData = new SpawnPointStaticData
                {
                    Position = spawnPointTransform.position,
                    Rotation = spawnPointTransform.rotation.eulerAngles,
                    ShipType = spawnPointEditor.ShipType,
                };

                levelStaticData.SpawnPoints.Add(spawnPointStaticData);
            }

            LevelLibrary.ValidateAndSave();
        }
    }
}
#endif