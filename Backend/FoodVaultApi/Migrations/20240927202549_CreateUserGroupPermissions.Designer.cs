﻿// <auto-generated />
using System;
using FoodVaultApi;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FoodVaultApi.Migrations
{
    [DbContext(typeof(FoodVaultDbContext))]
    [Migration("20240927202549_CreateUserGroupPermissions")]
    partial class CreateUserGroupPermissions
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FoodVaultApi.Models.Group", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("FoodVaultApi.Models.GroupRecipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("RecipeId");

                    b.ToTable("GroupRecipes");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Invitation", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("Accepted")
                        .HasColumnType("tinyint(1)");

                    b.Property<DateTime?>("AcceptedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("SentDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("SentFrom")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("SentTo")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("SentFrom");

                    b.HasIndex("SentTo");

                    b.ToTable("Invitations");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Recipe", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("UpdatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Recipes");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeIngredient", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<double>("Quantity")
                        .HasColumnType("double");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("UnitOfMeasurement")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeIngredients");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeInstruction", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<int>("SortOrder")
                        .HasColumnType("int");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.ToTable("RecipeInstructions");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeView", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("RecipeId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("RecipeId");

                    b.HasIndex("UserId");

                    b.ToTable("RecipeViews");
                });

            modelBuilder.Entity("FoodVaultApi.Models.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FoodVaultApi.Models.UserGroup", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("varchar(255)");

                    b.Property<bool>("CanAddRecipes")
                        .HasColumnType("tinyint(1)");

                    b.Property<bool>("CanAddUsers")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("GroupId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Group", b =>
                {
                    b.HasOne("FoodVaultApi.Models.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodVaultApi.Models.GroupRecipe", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Group", "Group")
                        .WithMany("GroupRecipes")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodVaultApi.Models.Recipe", "Recipe")
                        .WithMany("GroupRecipes")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Invitation", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Group", "Group")
                        .WithMany("Invitations")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodVaultApi.Models.User", "SentFromUser")
                        .WithMany("ReceivedInvitations")
                        .HasForeignKey("SentFrom")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("FoodVaultApi.Models.User", "SentToUser")
                        .WithMany("SentInvitations")
                        .HasForeignKey("SentTo")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("SentFromUser");

                    b.Navigation("SentToUser");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Recipe", b =>
                {
                    b.HasOne("FoodVaultApi.Models.User", "User")
                        .WithMany("Recipes")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeIngredient", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Recipe", "Recipe")
                        .WithMany("Ingredients")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeInstruction", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Recipe", "Recipe")
                        .WithMany("Instructions")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");
                });

            modelBuilder.Entity("FoodVaultApi.Models.RecipeView", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Recipe", "Recipe")
                        .WithMany("RecipeViews")
                        .HasForeignKey("RecipeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodVaultApi.Models.User", "User")
                        .WithMany("RecipeViews")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Recipe");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodVaultApi.Models.UserGroup", b =>
                {
                    b.HasOne("FoodVaultApi.Models.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FoodVaultApi.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Group", b =>
                {
                    b.Navigation("GroupRecipes");

                    b.Navigation("Invitations");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("FoodVaultApi.Models.Recipe", b =>
                {
                    b.Navigation("GroupRecipes");

                    b.Navigation("Ingredients");

                    b.Navigation("Instructions");

                    b.Navigation("RecipeViews");
                });

            modelBuilder.Entity("FoodVaultApi.Models.User", b =>
                {
                    b.Navigation("Groups");

                    b.Navigation("ReceivedInvitations");

                    b.Navigation("RecipeViews");

                    b.Navigation("Recipes");

                    b.Navigation("SentInvitations");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
