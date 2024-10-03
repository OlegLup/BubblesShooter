using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

public class LoadingScreen : MonoBehaviour
{
    [SerializeField]
    private AssetReference scene;
    
    private void Awake()
    {
        Addressables.LoadSceneAsync(scene, LoadSceneMode.Single);
    }
}
