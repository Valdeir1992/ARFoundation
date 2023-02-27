using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using TMPro;

public class PuzzleC : MonoBehaviour
{
    private GameObject _trackedObject;
    [SerializeField] private TextMeshProUGUI _status;
    [SerializeField] private GameObject _prefabObject;
    private void OnEnable()
    {
        try
        { 
            ARTrackedImageManager manager = FindObjectOfType<ARTrackedImageManager>();
            if (manager != null)
            {
                manager.trackedImagesChanged += OnChangeMission;
                Debug.LogError("Success find");
            }
            else
            {
                Debug.LogError("Erro manager");
            }
        }
        catch
        {
            Debug.LogError("Erro Find");
        }

        
    }

    private void OnDisable()
    {
        ARTrackedImageManager manager = FindObjectOfType<ARTrackedImageManager>();

        if (manager != null)
        {
            manager.trackedImagesChanged -= OnChangeMission;
        }
    }
    private void OnChangeMission(ARTrackedImagesChangedEventArgs eventArgs)
    {
        foreach (var AddedImage in eventArgs.added)
        {
            try
            {
                _trackedObject = Instantiate(_prefabObject, Vector3.zero, Quaternion.identity, AddedImage.transform);
                Debug.LogError("success instantiate");
            }
            catch
            {
                Debug.LogError("Fail instantiate");
            }
         }

        foreach (var updatedImage in eventArgs.updated)
        {
            _status.text = $"Status: {updatedImage.trackingState} {Time.time}";
        } 
    }
}
