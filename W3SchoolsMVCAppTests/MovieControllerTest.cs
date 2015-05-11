using W3SchoolsMvcApp.Controllers;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting.Web;
using System.Web.Mvc;
using System.Data.Entity;
using W3SchoolsMvcApp.Models;
using Moq;
using W3SchoolsMVCAppTests.DatabaseHelpers;

namespace W3SchoolsMVCAppTests
{
    
    /// <summary>
    ///This is a test class for MovieController and is intended
    ///to contain all MovieController Unit Tests
    ///</summary>
    [TestClass()]
    public class MovieControllerTest
    {

        //Sample data used for mocking repository
        private readonly Movie testMovie = new Movie
        {
            ID = 10,
            Title = "Mahamovie",
            Director = "Shubbu",
            ReleaseDate = new DateTime(2020, 02, 20)
        };

        private Mock<MoviesDbEntities> mockDB;
        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}

        //Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {
            //Setup the mock DBContext
            mockDB = new Mock<MoviesDbEntities>();
            mockDB.Object.Movies = new FakeDbSet<Movie>() { testMovie };
        }
        
        //Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup()
        {
            mockDB = null;
        }
        
        #endregion


        /// <summary>
        ///A test for Index
        ///</summary>
        // TODO: Ensure that the UrlToTest attribute specifies a URL to an ASP.NET page (for example,
        // http://.../Default.aspx). This is necessary for the unit test to be executed on the web server,
        // whether you are testing a page, web service, or a WCF service.
        /*
        [TestMethod()]
        [HostType("ASP.NET")]
        [AspNetDevelopmentServerHost("D:\\StudyData\\Practice\\MyProjects\\W3SchoolsMvcApp", "/")]
        [UrlToTest("http://localhost:49150/")]
        public void IndexTest()
        {
            MovieController target = new MovieController(); // TODO: Initialize to an appropriate value
            ViewResult expected = null; // TODO: Initialize to an appropriate value
            ViewResult actual;
            actual = target.Index();
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
        */

        [TestMethod()]
        public void IndexTest()
        {
            IDbSet<Movie> expectedMovies = new FakeDbSet<Movie> { testMovie };
            Mock<MoviesDbEntities> mockDB = new Mock<MoviesDbEntities>();
            mockDB.Object.Movies = expectedMovies;
            //mockDB.SetupGet(db => db.Movies).Returns(expectedMovies);
            MovieController target = new MovieController(mockDB.Object);
            ViewResult actual;
            actual = target.Index();
            Assert.IsNotNull(actual);
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest()
        {
            MovieController target = new MovieController(null);
            ActionResult actual;
            actual = target.Create();
            Assert.IsInstanceOfType(actual, typeof(PartialViewResult));
        }

        /// <summary>
        ///A test for Create
        ///</summary>
        [TestMethod()]
        public void CreateTest1()
        {
            Mock<MoviesDbEntities> mockDB = new Mock<MoviesDbEntities>();
            mockDB.Object.Movies = new FakeDbSet<Movie>();
            MovieController target = new MovieController(mockDB.Object);
            ActionResult actual = target.Create(testMovie);
            dynamic data = (actual as JsonResult).Data;
            Assert.IsTrue(data.Success);
        }

        /// <summary>
        ///A test for Delete
        ///</summary>
        [TestMethod()]
        public void DeleteTest()
        {
            Mock<MoviesDbEntities> mockDB = new Mock<MoviesDbEntities>();
            mockDB.Object.Movies = new FakeDbSet<Movie>() { testMovie };            
            MovieController target = new MovieController(mockDB.Object);
            int id = 10;
            ActionResult actual = target.Delete(id);
            dynamic data = ((JsonResult)actual).Data;
            Assert.IsTrue(data.Success);
        }

        /// <summary>
        ///A test for Details
        ///</summary>
        [TestMethod()]
        public void DetailsTest()
        {
            var mockDB = Mock.Of<MoviesDbEntities>();
            mockDB.Movies = new FakeDbSet<Movie>() { testMovie };
            MovieController target = new MovieController(mockDB);
            int id = 10;
            PartialViewResult actual = target.Details(id);
            object data = actual.Model;
            Assert.IsInstanceOfType(data, typeof(Movie));
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        public void EditTest()
        {
            var mockDB = Mock.Of<MoviesDbEntities>();
            mockDB.Movies = new FakeDbSet<Movie>() { testMovie };
            MovieController target = new MovieController(mockDB);
            int id = 10;
            PartialViewResult actual = target.Edit(id);
            Movie alibaba = (Movie)actual.Model;
            Assert.AreEqual(10, alibaba.ID);
        }

        /// <summary>
        ///A test for Edit
        ///</summary>
        [TestMethod()]
        [Ignore]
        public void EditTest1()
        {
            MovieController target = new MovieController(new MoviesDbEntities());
            Movie movie = new Movie { ID = 15, Director = "Bhajesh Roshan", ReleaseDate = new DateTime(1985, 01, 01), Title = "Koi Bhag Gaya" };
            ActionResult actual = target.Edit(movie);
            dynamic data = ((JsonResult)actual).Data;
            Assert.IsTrue(data.Success);
        }

        [TestMethod()]
        [Ignore]
        public void EditTest2()
        {
            Movie movie = new Movie { ID = 15, Director = "Bhajesh Roshan", ReleaseDate = new DateTime(1985, 01, 01), Title = "Koi Bhag Gaya" };
            Mock<MoviesDbEntities> mockDB = new Mock<MoviesDbEntities>();
            mockDB.Object.Movies = new FakeDbSet<Movie>();
            mockDB.SetupSet(db => db.Entry<Movie>(movie).State = System.Data.EntityState.Modified);
            mockDB.Setup(db => db.SaveChanges()).Returns(1);
            MovieController target = new MovieController(mockDB.Object);
            ActionResult actual = target.Edit(movie);
            dynamic data = ((JsonResult)actual).Data;
            Assert.IsTrue(data.Success);
        }

        /// <summary>
        ///A test for List
        ///</summary>
        [TestMethod()]
        public void ListTest()
        {
            var mockDB = Mock.Of<MoviesDbEntities>();
            mockDB.Movies = new FakeDbSet<Movie>() { testMovie };
            MovieController target = new MovieController(mockDB);             
            PartialViewResult actual = target.List();
            var movies = actual.ViewData.Model;
            Assert.IsInstanceOfType(movies, typeof(List<Movie>));
        }
    }
}
