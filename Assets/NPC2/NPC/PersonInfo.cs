
namespace NPC
{
    public class PersonInfo
    {
        public int Id { get; set; }

        public string Nickname { get; set; }

        public string Name { get; set; }

        public Location Address { get; set; }

        public Location PersonalRoom { get; set; }

        public int Relationship;

        public PersonInfo(int id)
        {
            Id = id;
            Nickname = null;
            Name = null;
            Address = null;
            PersonalRoom = null;
        }
    }
}