using Assets.Scripts.Localization;
using MyBox;
using System;
using System.Collections;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
using UnityEngine.ResourceManagement.AsyncOperations;
using static Assets.Scripts.Gameplay.GameManager;

namespace Assets.Scripts.Letters
{
    public class LetterController : MonoBehaviour
    {
        private const int LETTER_AMOUNT = 10;

        [field: SerializeField] public TextMeshProUGUI TextUI { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedIntroTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedDeedsTable { get; private set; }
        [field: SerializeField] public LocalizedStringTable LocalizedWishTable { get; private set; }
        [field: SerializeField] public GameObject FinalInformation { get; private set; }
        [field: SerializeField] public GiftType WantedType { get; private set; }
        [field: SerializeField] public int ChildAlignment { get; private set; }

        private PlayerValue _player;
        private StringTable _introTable;
        private StringTable _deedsTable;
        private StringTable _wishTable;
        private int letterIndex;

        public static event Action ReachedEndingEventHandler;

        private void Awake()
        {
            letterIndex = 0;
            _player = new PlayerValue();
        }

        private void WriteLetter()
        {
            StartCoroutine(LoadTables());
        }

        private IEnumerator LoadTables()
        {
            yield return StartCoroutine(LoadTable("Intro"));
            yield return StartCoroutine(LoadTable("Deeds"));
            yield return StartCoroutine(LoadTable("Wish"));
            var gift = _wishTable.GetRandom().Value;
            GetGiftType(gift);
            var deed = _deedsTable.GetRandom().Value;
            GetDeedValue(deed);
            TextUI.text = _introTable.GetRandom().Value.GetLocalizedString(_player) + "\n\n";
            TextUI.text += deed.GetLocalizedString(_player) + "\n";
            TextUI.text += gift.GetLocalizedString(_player);
        }

        private void GetDeedValue(StringTableEntry deed)
        {
            var deedKey = deed.Key[0];
            switch (deedKey)
            {
                case 'W':
                    ChildAlignment = -2;
                    break;

                case 'B':
                    ChildAlignment = -1;
                    break;

                case 'N':
                    ChildAlignment = 0;
                    break;

                case 'G':
                    ChildAlignment = 1;
                    break;

                case 'E':
                    ChildAlignment = 2;
                    break;

                default:
                    Debug.LogError("Wrong Deed Type");
                    break;
            }
        }

        private void GetGiftType(StringTableEntry gift)
        {
            var giftKey = gift.Key[0];
            switch (giftKey)
            {
                case 'T':
                    WantedType = GiftType.Toy;
                    break;

                case 'V':
                    WantedType = GiftType.Videogame;
                    break;

                case 'S':
                    WantedType = GiftType.Sports;
                    break;

                default:
                    Debug.LogError("Wrong Gift Type");
                    break;
            }
        }

        private IEnumerator LoadTable(string tableName)
        {
            var loadingOperation = LocalizationSettings.StringDatabase.GetTableAsync(tableName);
            yield return loadingOperation;
            if (loadingOperation.Status == AsyncOperationStatus.Succeeded)
            {
                if (tableName.Equals("Intro"))
                {
                    _introTable = loadingOperation.Result;
                }
                else if (tableName.Equals("Deeds"))
                {
                    _deedsTable = loadingOperation.Result;
                }
                else if (tableName.Equals("Wish"))
                {
                    _wishTable = loadingOperation.Result;
                }
            }
            else
            {
                Debug.Log("Could not load String Table\n" + loadingOperation.OperationException.ToString());
            }
        }

        public void CreateLetter()
        {
            if (letterIndex < LETTER_AMOUNT)
            {
                WriteLetter();
                letterIndex++;
            }
            else
            {
                ReachedEndingEventHandler?.Invoke();
            }
        }
    }
}