using UnityEngine;

namespace Assets.Scripts.Localization
{
    public class PlayerValue : MonoBehaviour
    {
        public enum Genders
        {
            Male,
            Female
        }

        public string Name { get; private set; } = "Santa";
        public Genders Gender { get; private set; } = Genders.Male;

        private void Awake()
        {
            Name = "Santa";
            Gender = Genders.Female;
        }
    }
}