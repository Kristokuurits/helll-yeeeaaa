using SQLite;

namespace MauiApp
{
    [Table("item")]
    public class Item
    {
        [PrimaryKey]
        [AutoIncrement]
        [Column("id")]
        public int Id { get; set; }

        [Column("item_name")]
        public string Name { get; set; }

        [Column("price")]
        public decimal Price { get; set; }
    }
}
