﻿using FoodVaultApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FoodVaultApi
{
    public class FoodVaultDbContext : DbContext
    {
        public FoodVaultDbContext(DbContextOptions<FoodVaultDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySQL("USE YOUR CONNECTION STRING FROM appsettings.json here");
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Recipe> Recipes { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<GroupRecipe> GroupRecipes { get; set; }
        public DbSet<RecipeInstruction> RecipeInstructions { get; set; }
        public DbSet<RecipeIngredient> RecipeIngredients { get; set; }
        public DbSet<Invitation> Invitations { get; set; }
        public DbSet<RecipeView> RecipeViews { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Many-to-many relationships
            modelBuilder.Entity<UserGroup>()
                .HasKey(ug => ug.Id);
            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.User)
                .WithMany(u => u.UserGroups)
                .HasForeignKey(ug => ug.UserId);
            modelBuilder.Entity<UserGroup>()
                .HasOne(ug => ug.Group)
                .WithMany(g => g.UserGroups)
                .HasForeignKey(ug => ug.GroupId);

            modelBuilder.Entity<GroupRecipe>()
                .HasKey(gr => gr.Id);
            modelBuilder.Entity<GroupRecipe>()
                .HasOne(gr => gr.Group)
                .WithMany(g => g.GroupRecipes)
                .HasForeignKey(gr => gr.GroupId);
            modelBuilder.Entity<GroupRecipe>()
                .HasOne(gr => gr.Recipe)
                .WithMany(r => r.GroupRecipes)
                .HasForeignKey(gr => gr.RecipeId);

            // One-to-many relationships
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Instructions)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);
            modelBuilder.Entity<Recipe>()
                .HasMany(r => r.Ingredients)
                .WithOne(ri => ri.Recipe)
                .HasForeignKey(ri => ri.RecipeId);

            modelBuilder.Entity<User>()
                .HasKey(u => u.Id);
            // Group owner relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Groups)
                .WithOne(g => g.User)
                .HasForeignKey(g => g.UserId);
            // Recipe owner relationship
            modelBuilder.Entity<User>()
                .HasMany(u => u.Recipes)
                .WithOne(r => r.User)
                .HasForeignKey(r => r.UserId);

            // Invitation sent to user relationship
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.SentToUser)
                .WithMany(u => u.SentInvitations)
                .HasForeignKey(i => i.SentTo)
                .OnDelete(DeleteBehavior.NoAction);
            // Inviation sent from user relationship
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.SentFromUser)
                .WithMany(u => u.ReceivedInvitations)
                .HasForeignKey(i => i.SentFrom)
                .OnDelete(DeleteBehavior.NoAction);
            // Many relationships to one group relationship
            modelBuilder.Entity<Invitation>()
                .HasOne(i => i.Group)
                .WithMany(g => g.Invitations)
                .HasForeignKey(i => i.GroupId);
            modelBuilder.Entity<RecipeView>()
                .HasKey(rv => rv.Id);
            modelBuilder.Entity<RecipeView>()
                .HasOne(rv => rv.User)
                .WithMany(u => u.RecipeViews)
                .HasForeignKey(rv => rv.UserId);
            modelBuilder.Entity<RecipeView>()
                .HasOne(rv => rv.Recipe)
                .WithMany(r => r.RecipeViews)
                .HasForeignKey(rv => rv.RecipeId);
        }
    }
}
