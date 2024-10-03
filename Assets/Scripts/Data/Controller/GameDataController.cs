using UnityEngine;
using VContainer;

namespace Data
{
    public class GameDataController : MonoBehaviour
    {
        [Inject]
        private GameData gameData;

        public void Init()
        {
            LoadData();
        }

        public void SaveData()
        {
            ES3.Save("GameData", gameData);
        }

        public void LoadData()
        {
            if (ES3.FileExists() && ES3.KeyExists("GameData"))
            {
                ES3.LoadInto("GameData", gameData);
            }
        }

        public void DeleteData()
        {
            ES3.DeleteFile();
        }

        private void OnApplicationPause(bool pause)
        {
            if (pause == true)
            {
                SaveData();
            }
        }

        private void OnApplicationQuit()
        {
            SaveData();
        }
    }
}
