using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeUI : MonoBehaviour
{
    [SerializeField] private UpgradeChest _upgradeChest;
    [SerializeField] private GameObject _upgradePanel;

    [Header("Buttons")]
    [SerializeField] private Button _meleeButton;
    [SerializeField] private Button _rangeButton;
    [SerializeField] private Button _healthButton;
    [SerializeField] private Button _ammoButton;

    [Header("Images")]
    [SerializeField] private Image _meleeImage;
    [SerializeField] private Image _rangeImage;
    [SerializeField] private Image _healthImage;
    [SerializeField] private Image _ammoImage;
    [SerializeField] private Image _maxLvl;

    [Header("Image List")]
    [SerializeField] private List<Image> _meleeImages;
    [SerializeField] private List<Image> _rangeImages;
    [SerializeField] private List<Image> _healthImages;
    [SerializeField] private List<Image> _ammoImages;

    public void UpgradeMelee()
    {
        if (_upgradeChest.UpgradeMelee())
        {
            //_meleeImage = _meleeImages[_upgradeChest.CurrentMeleeUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            //_meleeImage = _maxLvl;
            _meleeButton.interactable = false;
        }
    }

    public void UpgradeRange()
    {
        if (_upgradeChest.UpgradeRange())
        {
            //_rangeImage = _rangeImages[_upgradeChest.CurrentRangeUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            //_rangeImage = _maxLvl;
            _rangeButton.interactable = false;
        }
    }

    public void UpgradeHealth()
    {
        if (_upgradeChest.UpgradeHealth())
        {
            //_healthImage = _healthImages[_upgradeChest.CurrentHealthUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            //_healthImage = _maxLvl;
            _healthButton.interactable = false;
        }
    }

    public void UpgradeAmmo()
    {
        if (_upgradeChest.UpgradeAmmo())
        {
            //_ammoImage = _ammoImages[_upgradeChest.CurrentAmmoUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            //_ammoImage = _maxLvl;
            _ammoButton.interactable = false;
        }
    }
}
