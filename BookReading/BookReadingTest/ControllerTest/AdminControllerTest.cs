﻿using BookReading.Controllers;
using BookReading.Data;
using BookReading.Models;
using BookReading.Repository;
using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;
namespace BookReadingTest.ControllerTest
{
    public class AdminControllerTest
    {
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AdminControllerTest()
        {
            _signInManager = A.Fake<SignInManager<ApplicationUser>>();
        }

        private async Task<BookReadingContext> GetDbContext()
        {
            var options = new DbContextOptionsBuilder<BookReadingContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var databaseContext = new BookReadingContext(options);
            databaseContext.Database.EnsureCreated();
            if (await databaseContext.BookEvents.CountAsync() < 0)
            {
                for (int i = 0; i < 10; i++)
                {
                    databaseContext.BookEvents.Add(

                        new BookEventModel()
                        {
                            Email = "sumit.sahu@nagarro.com",
                            Title = "potter",
                            Date = DateTime.Parse("12-09-2022"),
                            StartTime = DateTime.Parse("00:00:00"),
                            Location = "Dabra",
                            Type = EType.Public
                        });
                    await databaseContext.SaveChangesAsync();
                }
            }
            return databaseContext;

        }

        [Fact]
  
        public async void IndexTest()
        {
            //Arrange
            var dbContext = await GetDbContext();
            var admin = new AdminPanelController(dbContext,_signInManager);
            //Act
            var result = admin.Index();
            //Assets
            result.Should().BeOfType<Task<IActionResult>>();
        }

    }
}
