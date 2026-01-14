namespace Less.Utils.EFCore.Test
{
    public class TestEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Type { get; set; }
        public DateTimeOffset CreateTime { get; set; } = DateTimeOffset.UtcNow;
    }
}
