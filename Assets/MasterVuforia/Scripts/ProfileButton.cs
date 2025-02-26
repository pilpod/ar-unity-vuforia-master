using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ProfileButton : MonoBehaviour
{
    // El uso de SerializeField es una buena práctica para que la propiedad sea privada pero que sea visible y editable en el Inspector de Unity sin necesidad de hacerlos públicos.
    [Header("Profile")]
    [SerializeField] private ProfileDataSO _profileData;

    [Header("References")]
    [SerializeField] private TextMeshProUGUI _profileTxt;
    [SerializeField] private Image _profileImg;

    // Start is called before the first frame update
    private void Start()
    {
        _profileImg.sprite = _profileData.profileSprite;

        if (_profileData.useProfileTxt && _profileTxt != null)
        {
            _profileTxt.text = _profileData.profileTxt;
        }
    }

    public void Execute()
    {
        Application.OpenURL(_profileData.GetUrl());
    }

}
