﻿using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Data;

public class BookStoreContext : DbContext
{

    public BookStoreContext(DbContextOptions<BookStoreContext> options) : base(options){}

    public DbSet<Book>? Books { get; set; }
    public DbSet<Press>? Presses { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Book>().OwnsOne(c => c.Location);
    }
}
