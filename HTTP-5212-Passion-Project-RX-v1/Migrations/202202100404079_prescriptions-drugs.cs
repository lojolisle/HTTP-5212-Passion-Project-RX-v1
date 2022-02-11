namespace HTTP_5212_Passion_Project_RX_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prescriptionsdrugs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PrescriptionDrugs",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Quantity = c.Int(nullable: false),
                        Repeat = c.Int(nullable: false),
                        Sig = c.String(),
                        PrescriptionID = c.Int(nullable: false),
                        DrugId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Drugs", t => t.DrugId, cascadeDelete: true)
                .ForeignKey("dbo.Prescriptions", t => t.PrescriptionID, cascadeDelete: true)
                .Index(t => t.PrescriptionID)
                .Index(t => t.DrugId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PrescriptionDrugs", "PrescriptionID", "dbo.Prescriptions");
            DropForeignKey("dbo.PrescriptionDrugs", "DrugId", "dbo.Drugs");
            DropIndex("dbo.PrescriptionDrugs", new[] { "DrugId" });
            DropIndex("dbo.PrescriptionDrugs", new[] { "PrescriptionID" });
            DropTable("dbo.PrescriptionDrugs");
        }
    }
}
