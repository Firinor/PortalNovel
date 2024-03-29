using FirUnityEditor;
using FirSaveLoad;
using System.Collections.Generic;
using UnityEngine;
using static FirSaveLoad.GlobalSaveManager;
using Zenject;

public class SaveManager : MonoBehaviour
{
    [Inject]
    private OptionsOperator optionsOperator;

    public static SaveData Data;

    void Awake()
    {
        optionsOperator.LoadOptions();
    }

    public static int PlayerAccount()
    {
        if (Data == null)
            return -1;

        return Data.Account;
    }

    public static void CreateNewSave(int account)
    {
        GlobalSaveManager.Save(GlobalSaveManager.GetPath(account), new SaveData(account, null));
    }

    public static void Save(int account)
    {
        Data.Account = account;
        Data.Progress = PlayerManager.GetProgress();

        GlobalSaveManager.Save(GlobalSaveManager.GetPath(account), Data);
    }


    public void SaveOptions(int ScreenResolution = -1)
    {
        Save<OptionsParameters>(GlobalSaveManager.GetOptionPath(),
            optionsOperator.GetParameters(ScreenResolutoin: ScreenResolution));
    }

    [System.Serializable]
    public class SaveData : AbstractSaveData
    {
        public int Account;
        public Dictionary<int, bool> Progress;

        public SaveData(int Account = -1, Dictionary<int, bool> Progress = null)
        {
            this.Account = Account;
            this.Progress = Progress;
        }
    }
}