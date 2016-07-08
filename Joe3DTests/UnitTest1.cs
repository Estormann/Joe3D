using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Windows.Threading;

namespace Joe3DTests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            string ModelPath = System.IO.Path.Combine(Environment.CurrentDirectory, "Models", "Handgun_obj.obj");

            //Assign
            var thing = Joe3D.Utilities.ModelLoader.Load(ModelPath, false, Dispatcher.CurrentDispatcher);
            //Assert
            Assert.IsNotNull(thing);

        }
    }
}
