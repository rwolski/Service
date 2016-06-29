﻿using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Framework.Tests
{
    [TestClass]
    public abstract class FrameworkUnitTest
    {
        private static IContainer _globalContainer;
        protected ILifetimeScope Container;
        private TestContext _testContext;

        public FrameworkUnitTest()
        {
        }

        public TestContext TestContext
        {
            get { return _testContext; }
            set { _testContext = value; }
        }

        [AssemblyInitialize()]
        public static void AssemblyInitialise(TestContext context)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<TestModules>();

            _globalContainer = builder.Build();
        }

        [ClassInitialize()]
        public static void ClassInitialise(TestContext context)
        {
            var builder = new ContainerBuilder();

            //builder.RegisterModule<TestModules>();

            //_globalContainer = builder.Build();

            //Container = _globalContainer.BeginLifetimeScope();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanup()
        {
            _globalContainer.Dispose();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            var i = 3;
        }

        [TestInitialize]
        public virtual void TestInitialize()
        {
            Container = _globalContainer.BeginLifetimeScope();
        }

        [TestCleanup]
        public virtual void TestCleanup()
        {
            Container.Dispose();
        }
    }
}
