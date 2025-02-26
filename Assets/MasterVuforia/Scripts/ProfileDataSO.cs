using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Profile Data", menuName = "ScriptableObjects/ProfileData", order = 0)]
public class ProfileDataSO : ScriptableObject
{
    public enum URLType
    {
        Normal,
        Phone,
        Email
    }

    [Header("General")]
    public string URL;
    public URLType urlType = URLType.Normal;
    public Sprite profileSprite;

    [Space]
    public bool useProfileTxt;
    public string profileTxt;

    [Header("Type - Email")]
    public string emailDir;
    public string emailSubject;
    public string emailBody;

    public string GetUrl()
    {
        switch (urlType)
        {
            case URLType.Normal:
                return URL;

            case URLType.Phone:
                return string.Format("tel://{0}", URL);

            case URLType.Email:
                return string.Format("mailto:{0}?subject={1}&body={2}", emailDir, emailSubject, emailBody);
        }

        return "";
    }
}
