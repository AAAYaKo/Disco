using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Disco
{
    public class UiLoader : MonoBehaviour
    {
        public event Action OnLoadComplete;
        public UiRepository Repository => _repository;

        private UiRepository _repository;


        private void Awake()
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync("Ui", LoadSceneMode.Additive);
            operation.completed += _ =>
            {
                _repository = FindObjectOfType<UiRepository>();
                OnLoadComplete?.Invoke();
            };
        }
    }
}
