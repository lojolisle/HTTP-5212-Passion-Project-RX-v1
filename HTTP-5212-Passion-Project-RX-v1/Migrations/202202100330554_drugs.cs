namespace HTTP_5212_Passion_Project_RX_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class drugs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Drugs",
                c => new
                    {
                        DrugID = c.Int(nullable: false, identity: true),
                        DrugName = c.String(),
                        Dosage = c.String(),
                        Formulation = c.String(),
                    })
                .PrimaryKey(t => t.DrugID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Drugs");
        }
    }
}
