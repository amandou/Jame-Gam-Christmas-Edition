namespace Assets.Scripts.Localization
{
    public class PlayerValue
    {
        public enum Genders
        {
            Male,
            Female
        }

        public string Name { get; private set; }
        public Genders Gender { get; private set; }

        public PlayerValue()
        {
            Name = "Santa";
            Gender = Genders.Female;
        }
    }
}