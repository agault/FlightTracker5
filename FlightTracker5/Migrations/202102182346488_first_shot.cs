namespace FlightTracker5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first_shot : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Flights",
                c => new
                    {
                        FlightID = c.Int(nullable: false, identity: true),
                        PilotID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Duty = c.String(),
                        Seat = c.String(),
                        Mission = c.String(),
                        Day = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Night = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightGoggles = c.Decimal(nullable: false, precision: 18, scale: 2),
                        NightSystems = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Weather = c.Decimal(nullable: false, precision: 18, scale: 2),
                        AircraftId = c.String(),
                    })
                .PrimaryKey(t => t.FlightID)
                .ForeignKey("dbo.Pilots", t => t.PilotID, cascadeDelete: true)
                .Index(t => t.PilotID);
            
            CreateTable(
                "dbo.Pilots",
                c => new
                    {
                        PilotID = c.Int(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        DateofBirth = c.DateTime(nullable: false),
                        Rank = c.String(),
                        PC = c.Boolean(nullable: false),
                        PI = c.Boolean(nullable: false),
                        SP = c.Boolean(nullable: false),
                        IP = c.Boolean(nullable: false),
                        FAC = c.String(),
                    })
                .PrimaryKey(t => t.PilotID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flights", "PilotID", "dbo.Pilots");
            DropIndex("dbo.Flights", new[] { "PilotID" });
            DropTable("dbo.Pilots");
            DropTable("dbo.Flights");
        }
    }
}
