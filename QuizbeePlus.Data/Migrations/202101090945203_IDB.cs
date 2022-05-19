namespace QuizbeePlus.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IDB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Amenities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Photo = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.BusinessAmenities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BusinessId = c.Int(nullable: false),
                        AmenityId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Amenities", t => t.AmenityId, cascadeDelete: true)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .Index(t => t.BusinessId)
                .Index(t => t.AmenityId);
            
            CreateTable(
                "dbo.Businesses",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Summary = c.String(),
                        Description = c.String(),
                        CategoryId = c.Int(nullable: false),
                        AreaId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Areas", t => t.AreaId, cascadeDelete: true)
                .ForeignKey("dbo.Categories", t => t.CategoryId, cascadeDelete: true)
                .Index(t => t.CategoryId)
                .Index(t => t.AreaId);
            
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CityId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Cities", t => t.CityId, cascadeDelete: true)
                .Index(t => t.CityId);
            
            CreateTable(
                "dbo.Cities",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        CountryId = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Countries", t => t.CountryId, cascadeDelete: true)
                .Index(t => t.CountryId);
            
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Photo = c.String(),
                        RegisteredOn = c.DateTime(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Area_ID = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Areas", t => t.Area_ID)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex")
                .Index(t => t.Area_ID);
            
            CreateTable(
                "dbo.UserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.Users", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.Roles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Categories",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Summary = c.String(),
                        Description = c.String(),
                        DisplaySeqNo = c.Int(nullable: false),
                        Photo = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BusinessId = c.Int(nullable: false),
                        UserId = c.String(nullable: false),
                        Rate = c.Int(nullable: false),
                        Text = c.String(),
                        ModifiedOn = c.DateTime(),
                        QuizbeeUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.QuizbeeUser_Id)
                .Index(t => t.BusinessId)
                .Index(t => t.QuizbeeUser_Id);
            
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                        Business_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Businesses", t => t.Business_ID)
                .Index(t => t.Business_ID);
            
            CreateTable(
                "dbo.OpeningHours",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DayId = c.Int(nullable: false),
                        BusinessId = c.Int(nullable: false),
                        From = c.Int(nullable: false),
                        To = c.Int(nullable: false),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Businesses", t => t.BusinessId, cascadeDelete: true)
                .ForeignKey("dbo.Days", t => t.DayId, cascadeDelete: true)
                .Index(t => t.DayId)
                .Index(t => t.BusinessId);
            
            CreateTable(
                "dbo.Days",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ModifiedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UserRoles", "RoleId", "dbo.Roles");
            DropForeignKey("dbo.BusinessAmenities", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.OpeningHours", "DayId", "dbo.Days");
            DropForeignKey("dbo.OpeningHours", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Images", "Business_ID", "dbo.Businesses");
            DropForeignKey("dbo.Comments", "QuizbeeUser_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "BusinessId", "dbo.Businesses");
            DropForeignKey("dbo.Businesses", "CategoryId", "dbo.Categories");
            DropForeignKey("dbo.Users", "Area_ID", "dbo.Areas");
            DropForeignKey("dbo.UserRoles", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserLogins", "UserId", "dbo.Users");
            DropForeignKey("dbo.UserClaims", "UserId", "dbo.Users");
            DropForeignKey("dbo.Cities", "CountryId", "dbo.Countries");
            DropForeignKey("dbo.Areas", "CityId", "dbo.Cities");
            DropForeignKey("dbo.Businesses", "AreaId", "dbo.Areas");
            DropForeignKey("dbo.BusinessAmenities", "AmenityId", "dbo.Amenities");
            DropIndex("dbo.Roles", "RoleNameIndex");
            DropIndex("dbo.OpeningHours", new[] { "BusinessId" });
            DropIndex("dbo.OpeningHours", new[] { "DayId" });
            DropIndex("dbo.Images", new[] { "Business_ID" });
            DropIndex("dbo.Comments", new[] { "QuizbeeUser_Id" });
            DropIndex("dbo.Comments", new[] { "BusinessId" });
            DropIndex("dbo.UserRoles", new[] { "RoleId" });
            DropIndex("dbo.UserRoles", new[] { "UserId" });
            DropIndex("dbo.UserLogins", new[] { "UserId" });
            DropIndex("dbo.UserClaims", new[] { "UserId" });
            DropIndex("dbo.Users", new[] { "Area_ID" });
            DropIndex("dbo.Users", "UserNameIndex");
            DropIndex("dbo.Cities", new[] { "CountryId" });
            DropIndex("dbo.Areas", new[] { "CityId" });
            DropIndex("dbo.Businesses", new[] { "AreaId" });
            DropIndex("dbo.Businesses", new[] { "CategoryId" });
            DropIndex("dbo.BusinessAmenities", new[] { "AmenityId" });
            DropIndex("dbo.BusinessAmenities", new[] { "BusinessId" });
            DropTable("dbo.Roles");
            DropTable("dbo.Days");
            DropTable("dbo.OpeningHours");
            DropTable("dbo.Images");
            DropTable("dbo.Comments");
            DropTable("dbo.Categories");
            DropTable("dbo.UserRoles");
            DropTable("dbo.UserLogins");
            DropTable("dbo.UserClaims");
            DropTable("dbo.Users");
            DropTable("dbo.Countries");
            DropTable("dbo.Cities");
            DropTable("dbo.Areas");
            DropTable("dbo.Businesses");
            DropTable("dbo.BusinessAmenities");
            DropTable("dbo.Amenities");
        }
    }
}
