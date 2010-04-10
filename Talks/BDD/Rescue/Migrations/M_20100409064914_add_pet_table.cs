using Domain.Model;
using Migrator.Framework;

namespace Migrations
{
    [Migration(20100409064914)]
    public class M_20100409064914_add_pet_table : Migration
    {
        public override void Up()
        {
            Database.AddTableFor<Pet>();
        }

        public override void Down()
        {
            Database.RemoveTableFor<Pet>();
        }
    }
}