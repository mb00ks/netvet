namespace NetVet.Domain.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Appointments",
                c => new
                    {
                        AppointmentId = c.Guid(nullable: false, identity: true),
                        AppointmentDateTime = c.DateTime(nullable: false),
                        DateDeleted = c.DateTime(),
                        PetId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.AppointmentId)
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .Index(t => t.PetId);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        NoteId = c.Guid(nullable: false, identity: true),
                        Summary = c.String(),
                        Detail = c.String(),
                        DateCreated = c.DateTime(nullable: false),
                        DateModified = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.NoteId);
            
            CreateTable(
                "dbo.Pets",
                c => new
                    {
                        PetId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Age = c.Int(nullable: false),
                        Breed = c.String(),
                        ImageBase64 = c.String(),
                        AnimalId = c.Guid(nullable: false),
                        Owner_OwnerId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => t.PetId)
                .ForeignKey("dbo.Animals", t => t.AnimalId, cascadeDelete: true)
                .ForeignKey("dbo.Owners", t => t.Owner_OwnerId, cascadeDelete: true)
                .Index(t => t.AnimalId)
                .Index(t => t.Owner_OwnerId);
            
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        AnimalId = c.Guid(nullable: false, identity: true),
                        Name = c.String(),
                        Size = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AnimalId);
            
            CreateTable(
                "dbo.Owners",
                c => new
                    {
                        OwnerId = c.Guid(nullable: false, identity: true),
                        FirstName = c.String(),
                        LastName = c.String(),
                        PreferredName = c.String(),
                        IsOptInForNotifications = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.OwnerId);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        ContactId = c.Guid(nullable: false, identity: true),
                        ContactType = c.Int(nullable: false),
                        ContactData = c.String(),
                        IsPreferred = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.ContactId);
            
            CreateTable(
                "dbo.AppointmentNotes",
                c => new
                    {
                        AppointmentId = c.Guid(nullable: false),
                        NoteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.AppointmentId, t.NoteId })
                .ForeignKey("dbo.Appointments", t => t.AppointmentId, cascadeDelete: true)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.AppointmentId)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.PetNotes",
                c => new
                    {
                        PetId = c.Guid(nullable: false),
                        NoteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.PetId, t.NoteId })
                .ForeignKey("dbo.Pets", t => t.PetId, cascadeDelete: true)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.PetId)
                .Index(t => t.NoteId);
            
            CreateTable(
                "dbo.OwnerContacts",
                c => new
                    {
                        OwnerId = c.Guid(nullable: false),
                        ContactId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerId, t.ContactId })
                .ForeignKey("dbo.Owners", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Contacts", t => t.ContactId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.ContactId);
            
            CreateTable(
                "dbo.OwnerNotes",
                c => new
                    {
                        OwnerId = c.Guid(nullable: false),
                        NoteId = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.OwnerId, t.NoteId })
                .ForeignKey("dbo.Owners", t => t.OwnerId, cascadeDelete: true)
                .ForeignKey("dbo.Notes", t => t.NoteId, cascadeDelete: true)
                .Index(t => t.OwnerId)
                .Index(t => t.NoteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Appointments", "PetId", "dbo.Pets");
            DropForeignKey("dbo.Pets", "Owner_OwnerId", "dbo.Owners");
            DropForeignKey("dbo.OwnerNotes", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.OwnerNotes", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.OwnerContacts", "ContactId", "dbo.Contacts");
            DropForeignKey("dbo.OwnerContacts", "OwnerId", "dbo.Owners");
            DropForeignKey("dbo.PetNotes", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.PetNotes", "PetId", "dbo.Pets");
            DropForeignKey("dbo.Pets", "AnimalId", "dbo.Animals");
            DropForeignKey("dbo.AppointmentNotes", "NoteId", "dbo.Notes");
            DropForeignKey("dbo.AppointmentNotes", "AppointmentId", "dbo.Appointments");
            DropIndex("dbo.OwnerNotes", new[] { "NoteId" });
            DropIndex("dbo.OwnerNotes", new[] { "OwnerId" });
            DropIndex("dbo.OwnerContacts", new[] { "ContactId" });
            DropIndex("dbo.OwnerContacts", new[] { "OwnerId" });
            DropIndex("dbo.PetNotes", new[] { "NoteId" });
            DropIndex("dbo.PetNotes", new[] { "PetId" });
            DropIndex("dbo.AppointmentNotes", new[] { "NoteId" });
            DropIndex("dbo.AppointmentNotes", new[] { "AppointmentId" });
            DropIndex("dbo.Pets", new[] { "Owner_OwnerId" });
            DropIndex("dbo.Pets", new[] { "AnimalId" });
            DropIndex("dbo.Appointments", new[] { "PetId" });
            DropTable("dbo.OwnerNotes");
            DropTable("dbo.OwnerContacts");
            DropTable("dbo.PetNotes");
            DropTable("dbo.AppointmentNotes");
            DropTable("dbo.Contacts");
            DropTable("dbo.Owners");
            DropTable("dbo.Animals");
            DropTable("dbo.Pets");
            DropTable("dbo.Notes");
            DropTable("dbo.Appointments");
        }
    }
}
