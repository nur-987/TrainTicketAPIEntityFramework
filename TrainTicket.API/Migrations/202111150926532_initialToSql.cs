namespace TrainTicket.API.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialToSql : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tickets",
                c => new
                    {
                        TicketId = c.Int(nullable: false, identity: true),
                        SelectedClass = c.Int(nullable: false),
                        BookingTime = c.DateTime(nullable: false),
                        NumOfTickets = c.Int(nullable: false),
                        GrandTotal = c.Double(nullable: false),
                        SelectedTrain_TrainId = c.Int(),
                        User_UserId = c.Int(),
                    })
                .PrimaryKey(t => t.TicketId)
                .ForeignKey("dbo.Trains", t => t.SelectedTrain_TrainId)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.SelectedTrain_TrainId)
                .Index(t => t.User_UserId);
            
            CreateTable(
                "dbo.Trains",
                c => new
                    {
                        TrainId = c.Int(nullable: false, identity: true),
                        StartDestination = c.String(),
                        EndDestination = c.String(),
                        Distance = c.Int(nullable: false),
                        DepartureTime = c.DateTime(nullable: false),
                        ArrivalTime = c.DateTime(nullable: false),
                        FirstClassFare = c.Double(nullable: false),
                        BusinessClassFare = c.Double(nullable: false),
                        EconomyClassFare = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.TrainId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tickets", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Tickets", "SelectedTrain_TrainId", "dbo.Trains");
            DropIndex("dbo.Tickets", new[] { "User_UserId" });
            DropIndex("dbo.Tickets", new[] { "SelectedTrain_TrainId" });
            DropTable("dbo.Users");
            DropTable("dbo.Trains");
            DropTable("dbo.Tickets");
        }
    }
}
