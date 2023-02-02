namespace SoftwareDesignPatterns
{
    public sealed class ParentEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public int ParentId { get; set; }

        public string ParentName { get; set; }
    }
}
