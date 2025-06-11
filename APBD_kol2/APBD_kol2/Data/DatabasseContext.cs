using APBD_kol2.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_kol2.Data;

public class DatabaseContext : DbContext
{
    public DbSet<Backpack> Backpacks { get; set; }
    public DbSet<Character_Title> Character_Titles { get; set; }
    public DbSet<Character> Characters { get; set; }
    public DbSet<Item> Items { get; set; }
    public DbSet<Title> Titles { get; set; }
    
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Item>(i =>
        {
            i.HasKey(k => k.ItemId);
            i.Property(k => k.Name).HasMaxLength(100).IsRequired();
            i.Property(k => k.Weight).IsRequired();
        });
        modelBuilder.Entity<Item>().HasData(new List<Item>()
        {
            new Item(){ ItemId = 1,Name = "Item1", Weight = 10},
            new Item(){ ItemId = 2,Name = "Item2", Weight = 15},
            new Item(){ ItemId = 3,Name = "Item3", Weight = 20},
        });


        modelBuilder.Entity<Backpack>(b =>
        {
            b.HasKey(k => new { k.CharacterId, k.ItemId });
            b.HasOne(k => k.Character)
                .WithMany(k => k.Backpacks)
                .HasForeignKey(k => k.CharacterId)
                .OnDelete(DeleteBehavior.ClientCascade);
            b.HasOne(k => k.Item)
                .WithMany(k => k.Backpacks)
                .HasForeignKey(k => k.ItemId)
                .OnDelete(DeleteBehavior.ClientCascade);
            b.Property(k => k.Amount).IsRequired();
        });
        modelBuilder.Entity<Backpack>().HasData(new List<Backpack>()
        {
            new Backpack() { CharacterId = 1, ItemId = 1, Amount = 3 },
            new Backpack() { CharacterId = 2, ItemId = 3, Amount = 1 },
        });


        modelBuilder.Entity<Character>(c =>
        {
            c.HasKey(k => k.CharacterID);
            c.Property(k => k.FirstName).HasMaxLength(50).IsRequired();
            c.Property(k => k.LastName).HasMaxLength(120).IsRequired();
            c.Property(k => k.CurrentWeight).IsRequired();
            c.Property(k => k.MaxWeight).IsRequired();
        });
        modelBuilder.Entity<Character>().HasData(new List<Character>()
        {
            new Character() { CharacterID = 1, FirstName = "John", LastName = "Doe", CurrentWeight = 50, MaxWeight = 100 },
            new Character() { CharacterID = 2, FirstName = "Jane", LastName = "Doe", CurrentWeight = 30, MaxWeight = 50 }
        });
        


        modelBuilder.Entity<Character_Title>(ct =>
        {
            ct.HasKey(k => new { k.CharacterId, k.TitleId });
            ct.HasOne(k => k.Character)
                .WithMany(k => k.Character_Titles)
                .HasForeignKey(k => k.CharacterId)
                .OnDelete(DeleteBehavior.ClientCascade);
            ct.HasOne(k => k.Title)
                .WithMany(k => k.Character_Titles)
                .HasForeignKey(k => k.TitleId)
                .OnDelete(DeleteBehavior.ClientCascade);
            ct.Property(k => k.AcquiredAt).IsRequired();
        });
        modelBuilder.Entity<Character_Title>().HasData(new List<Character_Title>()
        {
            new Character_Title() { CharacterId = 1, TitleId = 1, AcquiredAt = DateTime.Today },
            new Character_Title() { CharacterId = 2, TitleId = 2, AcquiredAt = DateTime.Today },
        });


        modelBuilder.Entity<Title>(t =>
        {
            t.HasKey(k => k.TitleId);
            t.Property(k => k.Name).HasMaxLength(100).IsRequired();
        });
        modelBuilder.Entity<Title>().HasData(new List<Title>()
        {
            new Title() { TitleId = 1, Name = "Title 1" },
            new Title() { TitleId = 2, Name = "Title 2" },
            new Title() { TitleId = 3, Name = "Title 3" },
        });

    }
}