﻿// <auto-generated />
using System;
using AspNetCoreSpa.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AspNetCoreSpa.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.0");

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Category", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CategoryName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("Description")
                        .HasColumnType("ntext");

                    b.Property<byte[]>("Picture")
                        .HasColumnType("image");

                    b.HasKey("CategoryId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.ContactUs", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("Message")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(1024);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.HasKey("Id");

                    b.ToTable("ContactUs");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Customer", b =>
                {
                    b.Property<string>("CustomerId")
                        .HasColumnName("CustomerID")
                        .HasColumnType("TEXT")
                        .HasMaxLength(5);

                    b.Property<string>("Address")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<string>("City")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<string>("ContactName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("ContactTitle")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Country")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("Fax")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Region")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.HasKey("CustomerId");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Employee", b =>
                {
                    b.Property<int>("EmployeeId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<DateTime?>("BirthDate")
                        .HasColumnType("datetime");

                    b.Property<string>("City")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("Country")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("Extension")
                        .HasColumnType("TEXT")
                        .HasMaxLength(4);

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<DateTime?>("HireDate")
                        .HasColumnType("datetime");

                    b.Property<string>("HomePhone")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<string>("Notes")
                        .HasColumnType("ntext");

                    b.Property<byte[]>("Photo")
                        .HasColumnType("image");

                    b.Property<string>("PhotoPath")
                        .HasColumnType("TEXT")
                        .HasMaxLength(255);

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Region")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<int?>("ReportsTo")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("TitleOfCourtesy")
                        .HasColumnType("TEXT")
                        .HasMaxLength(25);

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("EmployeeId");

                    b.HasIndex("ReportsTo");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.EmployeeTerritory", b =>
                {
                    b.Property<int>("EmployeeId")
                        .HasColumnName("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TerritoryId")
                        .HasColumnName("TerritoryID")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.HasKey("EmployeeId", "TerritoryId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("TerritoryId");

                    b.ToTable("EmployeeTerritories");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Order", b =>
                {
                    b.Property<int>("OrderId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("CustomerId")
                        .HasColumnName("CustomerID")
                        .HasColumnType("TEXT")
                        .HasMaxLength(5);

                    b.Property<int?>("EmployeeId")
                        .HasColumnName("EmployeeID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("Freight")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0))");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("OrderDate")
                        .HasColumnType("datetime");

                    b.Property<DateTime?>("RequiredDate")
                        .HasColumnType("datetime");

                    b.Property<string>("ShipAddress")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<string>("ShipCity")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("ShipCountry")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("ShipName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<string>("ShipPostalCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("ShipRegion")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<int?>("ShipVia")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("ShippedDate")
                        .HasColumnType("datetime");

                    b.HasKey("OrderId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("EmployeeId");

                    b.HasIndex("ShipVia");

                    b.ToTable("Orders");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.OrderDetail", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnName("OrderID")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ProductId")
                        .HasColumnName("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<float>("Discount")
                        .HasColumnType("REAL");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<short>("Quantity")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("((1))");

                    b.Property<decimal>("UnitPrice")
                        .HasColumnType("money");

                    b.HasKey("OrderId", "ProductId");

                    b.HasIndex("ProductId");

                    b.ToTable("Order Details");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Product", b =>
                {
                    b.Property<int>("ProductId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ProductID")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("CategoryId")
                        .HasColumnName("CategoryID")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Created")
                        .HasColumnType("TEXT");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Discontinued")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("LastModified")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastModifiedBy")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProductName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<string>("QuantityPerUnit")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<short?>("ReorderLevel")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("((0))");

                    b.Property<int?>("SupplierId")
                        .HasColumnName("SupplierID")
                        .HasColumnType("INTEGER");

                    b.Property<decimal?>("UnitPrice")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("money")
                        .HasDefaultValueSql("((0))");

                    b.Property<short?>("UnitsInStock")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("((0))");

                    b.Property<short?>("UnitsOnOrder")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER")
                        .HasDefaultValueSql("((0))");

                    b.HasKey("ProductId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("SupplierId");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Region", b =>
                {
                    b.Property<int>("RegionId")
                        .HasColumnName("RegionID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("RegionDescription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("RegionId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.ToTable("Region");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Shipper", b =>
                {
                    b.Property<int>("ShipperId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ShipperID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.HasKey("ShipperId");

                    b.ToTable("Shippers");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Supplier", b =>
                {
                    b.Property<int>("SupplierId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("SupplierID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT")
                        .HasMaxLength(60);

                    b.Property<string>("City")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("CompanyName")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(40);

                    b.Property<string>("ContactName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("ContactTitle")
                        .HasColumnType("TEXT")
                        .HasMaxLength(30);

                    b.Property<string>("Country")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.Property<string>("Fax")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.Property<string>("HomePage")
                        .HasColumnType("ntext");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT")
                        .HasMaxLength(24);

                    b.Property<string>("PostalCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(10);

                    b.Property<string>("Region")
                        .HasColumnType("TEXT")
                        .HasMaxLength(15);

                    b.HasKey("SupplierId");

                    b.ToTable("Suppliers");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Territory", b =>
                {
                    b.Property<string>("TerritoryId")
                        .HasColumnName("TerritoryID")
                        .HasColumnType("TEXT")
                        .HasMaxLength(20);

                    b.Property<int>("RegionId")
                        .HasColumnName("RegionID")
                        .HasColumnType("INTEGER");

                    b.Property<string>("TerritoryDescription")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("TerritoryId")
                        .HasAnnotation("SqlServer:Clustered", false);

                    b.HasIndex("RegionId");

                    b.ToTable("Territories");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Employee", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Employee", "Manager")
                        .WithMany("DirectReports")
                        .HasForeignKey("ReportsTo")
                        .HasConstraintName("FK_Employees_Employees");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.EmployeeTerritory", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Employee", "Employee")
                        .WithMany("EmployeeTerritories")
                        .HasForeignKey("EmployeeId")
                        .HasConstraintName("FK_EmployeeTerritories_Employees")
                        .IsRequired();

                    b.HasOne("AspNetCoreSpa.Domain.Entities.Territory", "Territory")
                        .WithMany("EmployeeTerritories")
                        .HasForeignKey("TerritoryId")
                        .HasConstraintName("FK_EmployeeTerritories_Territories")
                        .IsRequired();
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Order", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Customer", "Customer")
                        .WithMany("Orders")
                        .HasForeignKey("CustomerId");

                    b.HasOne("AspNetCoreSpa.Domain.Entities.Employee", "Employee")
                        .WithMany("Orders")
                        .HasForeignKey("EmployeeId");

                    b.HasOne("AspNetCoreSpa.Domain.Entities.Shipper", "Shipper")
                        .WithMany("Orders")
                        .HasForeignKey("ShipVia")
                        .HasConstraintName("FK_Orders_Shippers");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.OrderDetail", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Order", "Order")
                        .WithMany("OrderDetails")
                        .HasForeignKey("OrderId")
                        .HasConstraintName("FK_Order_Details_Orders")
                        .IsRequired();

                    b.HasOne("AspNetCoreSpa.Domain.Entities.Product", "Product")
                        .WithMany("OrderDetails")
                        .HasForeignKey("ProductId")
                        .HasConstraintName("FK_Order_Details_Products")
                        .IsRequired();
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Product", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId");

                    b.HasOne("AspNetCoreSpa.Domain.Entities.Supplier", "Supplier")
                        .WithMany("Products")
                        .HasForeignKey("SupplierId");
                });

            modelBuilder.Entity("AspNetCoreSpa.Domain.Entities.Territory", b =>
                {
                    b.HasOne("AspNetCoreSpa.Domain.Entities.Region", "Region")
                        .WithMany("Territories")
                        .HasForeignKey("RegionId")
                        .HasConstraintName("FK_Territories_Region")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
