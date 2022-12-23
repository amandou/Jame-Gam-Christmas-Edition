using Assets.Scripts.Cards;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Tables;
using MyBox;
using UnityEngine.Localization.Settings;
using Assets.Scripts.Localization;

namespace Assets.Scripts.Letters
{
    public class LetterGenerator : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TextUI { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedIntroTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedDeedsTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedWishTable { get; private set; }
        [SerializeField] private PlayerValue player;
        private StringTable _introTable;
        private StringTable _deedsTable;
        private StringTable _wishTable;

        private void Awake()
        {
            _introTable = LocalizedIntroTable.GetTable();
            _deedsTable = LocalizedDeedsTable.GetTable();
            _wishTable = LocalizedWishTable.GetTable();
        }

        [ButtonMethod]
        public void CreateLetter()
        {
            Debug.Log($"Player Name = {player.Name}, Gender = {player.Gender}");
            TextUI.text = _introTable.GetRandom().Value.GetLocalizedString(player);
            TextUI.text += _deedsTable.GetRandom().Value.GetLocalizedString(player);
            TextUI.text += _wishTable.GetRandom().Value.GetLocalizedString(player);
        }
    }
}