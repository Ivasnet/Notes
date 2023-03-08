namespace DataBase.Models
{
    public class Note
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public override string ToString()
        {
            if (Name != null)
            {
                return Name;
            }
            return Id.ToString();
        }
    }
}
