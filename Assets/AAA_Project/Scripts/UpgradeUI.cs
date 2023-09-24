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

    [Header("Sprites")]
    [SerializeField] private List<Sprite> _lvlSprites;
    [SerializeField] private Sprite _maxLvl;

    public void UpgradeMelee()
    {
        if (_upgradeChest.UpgradeMelee())
        {
            _meleeImage.sprite = _lvlSprites[_upgradeChest.CurrentMeleeUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            _meleeImage.sprite = _maxLvl;
            _meleeButton.interactable = false;
        }
    }

    public void UpgradeRange()
    {
        if (_upgradeChest.UpgradeRange())
        {
            _rangeImage.sprite = _lvlSprites[_upgradeChest.CurrentRangeUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            _rangeImage.sprite = _maxLvl;
            _rangeButton.interactable = false;
        }
    }

    public void UpgradeHealth()
    {
        if (_upgradeChest.UpgradeHealth())
        {
            _healthImage.sprite = _lvlSprites[_upgradeChest.CurrentHealthUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            _healthImage.sprite = _maxLvl;
            _healthButton.interactable = false;
        }
    }

    public void UpgradeAmmo()
    {
        if (_upgradeChest.UpgradeAmmo())
        {
            _ammoImage.sprite = _lvlSprites[_upgradeChest.CurrentAmmoUpgrade];
            _upgradePanel.SetActive(false);
        }
        else
        {
            _ammoImage.sprite = _maxLvl;
            _ammoButton.interactable = false;
        }
    }
}
