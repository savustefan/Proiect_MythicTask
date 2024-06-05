using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.ContentModel;
using Proiect_MythicTask.Controllers;
using Proiect_MythicTask.Data;
using Proiect_MythicTask.Models;
using Xunit;

namespace Proiect_MythicTask.Tests
{
    public class TODOListsControllerTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _contextOptions;

        public TODOListsControllerTests()
        {
            _contextOptions = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase("TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(_contextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.TODOList.AddRange(
                    new TODOList { NrCrit = 1, Obiectiv = "Task 1", Descriere = "Description 1", Locație = "Location 1", Deadline = DateTime.Now.AddDays(1) },
                    new TODOList { NrCrit = 2, Obiectiv = "Task 2", Descriere = "Description 2", Locație = "Location 2", Deadline = DateTime.Now.AddDays(2) }
                );
                context.SaveChanges();
            }
        }

        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfTODOLists()
        {
            using (var context = new ApplicationDbContext(_contextOptions))
            {
                var controller = new TODOListsController(context);

                var result = await controller.Index();

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<IEnumerable<TODOList>>(viewResult.ViewData.Model);
                Assert.Equal(2, model.Count());
            }
        }

        [Fact]
        public async Task Details_ReturnsAViewResult_WithTODOList()
        {
            using (var context = new ApplicationDbContext(_contextOptions))
            {
                var controller = new TODOListsController(context);

                var result = await controller.Details(1);

                var viewResult = Assert.IsType<ViewResult>(result);
                var model = Assert.IsAssignableFrom<TODOList>(viewResult.ViewData.Model);
                Assert.Equal(1, model.NrCrit);
                Assert.Equal("Task 1", model.Obiectiv);
            }
        }
    }
}
