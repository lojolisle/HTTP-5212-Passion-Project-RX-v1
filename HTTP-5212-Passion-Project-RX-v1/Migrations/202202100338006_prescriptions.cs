namespace HTTP_5212_Passion_Project_RX_v1.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class prescriptions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Prescriptions",
                c => new
                    {
                        PrescriptionID = c.Int(nullable: false, identity: true),
                        DoctorName = c.String(),
                        PatientName = c.String(),
                    })
                .PrimaryKey(t => t.PrescriptionID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Prescriptions");
        }
    }
}
