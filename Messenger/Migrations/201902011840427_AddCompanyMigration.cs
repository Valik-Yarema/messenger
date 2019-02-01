namespace Messenger.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCompanyMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Messeges",
                c => new
                    {
                        MessegesId = c.Int(nullable: false, identity: true),
                        UserId = c.Int(),
                        RecepientId = c.Int(),
                        DataMess = c.DateTime(nullable: false),
                        TimeMess = c.Time(nullable: false, precision: 7),
                        MessegeText = c.String(),
                    })
                .PrimaryKey(t => t.MessegesId)
                .ForeignKey("dbo.Recepients", t => t.RecepientId)
                .ForeignKey("dbo.Users", t => t.UserId)
                .Index(t => t.UserId)
                .Index(t => t.RecepientId);
            
            CreateTable(
                "dbo.Recepients",
                c => new
                    {
                        RecepientId = c.Int(nullable: false, identity: true),
                        RecepientNumber = c.String(),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.RecepientId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Password = c.String(),
                        Name = c.String(),
                        Adress = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Messeges", "UserId", "dbo.Users");
            DropForeignKey("dbo.Messeges", "RecepientId", "dbo.Recepients");
            DropIndex("dbo.Messeges", new[] { "RecepientId" });
            DropIndex("dbo.Messeges", new[] { "UserId" });
            DropTable("dbo.Users");
            DropTable("dbo.Recepients");
            DropTable("dbo.Messeges");
        }
    }
}
