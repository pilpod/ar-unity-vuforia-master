using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Vuforia;
using static UnityEngine.CullingGroup;
using static Vuforia.ObjectRecoBehaviour;

public class SimpleCloudRecoEventHandler : MonoBehaviour
{
    CloudRecoBehaviour mCloudRecoBehaviour;
    bool mIsScanning = false;
    string mTargetMetadata = "";

    [Header("Vuforia")]
    public ImageTargetBehaviour ImageTargetTemplate;

    [Header("MetaData")]
    [SerializeField] private int _contentIndex;
    [SerializeField] private string _contentTitle;
    [SerializeField] private string _contentDescription;
    [Space]
    [SerializeField] private Transform _contentParent;
    [SerializeField] private MetaDataSO[] _metaDataSO;

    [Header("UI")]
    [SerializeField] private UnityEngine.UI.Image _detectionImg;
    [SerializeField] private Color _detectionColorTrue;
    [SerializeField] private Color _detectionColorFalse;
    [Space]
    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [Space]
    [SerializeField] private Button _resetBtn;

    private GameObject _content;


    // Register cloud reco callbacks
    void Awake()
    {
        mCloudRecoBehaviour = GetComponent<CloudRecoBehaviour>();
        mCloudRecoBehaviour.RegisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.RegisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.RegisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.RegisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.RegisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }
    //Unregister cloud reco callbacks when the handler is destroyed
    void OnDestroy()
    {
        mCloudRecoBehaviour.UnregisterOnInitializedEventHandler(OnInitialized);
        mCloudRecoBehaviour.UnregisterOnInitErrorEventHandler(OnInitError);
        mCloudRecoBehaviour.UnregisterOnUpdateErrorEventHandler(OnUpdateError);
        mCloudRecoBehaviour.UnregisterOnStateChangedEventHandler(OnStateChanged);
        mCloudRecoBehaviour.UnregisterOnNewSearchResultEventHandler(OnNewSearchResult);
    }

    private void Start()
    {
        ResetMetaData();
    }

    public void OnInitialized(CloudRecoBehaviour cloudRecoBehaviour)
    {
        Debug.Log("Cloud Reco initialized");
    }

    public void OnInitError(CloudRecoBehaviour.InitError initError)
    {
        Debug.Log("Cloud Reco init error " + initError.ToString());
    }

    public void OnUpdateError(CloudRecoBehaviour.QueryError updateError)
    {
        Debug.Log("Cloud Reco update error " + updateError.ToString());

    }

    public void OnStateChanged(bool scanning)
    {
        mIsScanning = scanning;

        _detectionImg.color = mIsScanning ? _detectionColorTrue : _detectionColorFalse;

        if (scanning)
        {
            // Clear all known targets
        }
        else
        {
            // Show the reset button
            _resetBtn.gameObject.SetActive(true);
        }
    }

    // Here we handle a cloud target recognition event
    public void OnNewSearchResult(CloudRecoBehaviour.CloudRecoSearchResult cloudRecoSearchResult)
    {
        // Store the target metadata
        mTargetMetadata = cloudRecoSearchResult.MetaData;

        // Stop the scanning by disabling the behaviour
        mCloudRecoBehaviour.enabled = false;

        Debug.LogFormat("Cloud target recognized: {0}", mTargetMetadata);
        ParseMetaData(mTargetMetadata);
        ChangeContent(true);

        /* Enable the new result with the same ImageTargetBehaviour: */
        mCloudRecoBehaviour.EnableObservers(cloudRecoSearchResult, _content);
    }

    /*void OnGUI()
    {
        // Display current 'scanning' status
        GUI.Box(new Rect(100, 100, 200, 50), mIsScanning ? "Scanning" : "Not scanning");
        // Display metadata of latest detected cloud-target
        GUI.Box(new Rect(100, 200, 200, 50), "Metadata: " + mTargetMetadata);
        // If not scanning, show button
        // so that user can restart cloud scanning
        if (!mIsScanning)
        {
            if (GUI.Button(new Rect(100, 300, 200, 50), "Restart Scanning"))
            {
                // Reset Behaviour
                mCloudRecoBehaviour.enabled = true;
                mTargetMetadata = "";
            }
        }
    }*/

    public void ResetMetaData()
    {
        mCloudRecoBehaviour.enabled = true;
        mTargetMetadata = "";

        _detectionImg.color = _detectionColorTrue;
        _titleText.text = "-";
        _descriptionText.text = "-";
        _resetBtn.gameObject.SetActive(false);

        ChangeContent(false);
    }

    private void ParseMetaData(string metaData)
    {
        string[] tempMetaData = metaData.Split('_');

        _contentIndex = int.Parse(tempMetaData[0]);
        _contentTitle = tempMetaData[1];
        _contentDescription = tempMetaData[2];

        _titleText.text = _contentTitle;
        _descriptionText.text = _contentDescription;
    }

    private void ChangeContent(bool show)
    {
        if (show)
        {
            _content = Instantiate(_metaDataSO[_contentIndex].content, _contentParent);
        }
        else
        {
            Destroy(_content);
        }
    }
}