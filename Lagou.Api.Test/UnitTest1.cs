using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Lagou.API.Methods;
using Lagou.API;

namespace Lagou.Api.Test {
    [TestClass]
    public class UnitTest1 {


        [TestMethod]
        public void TestMethod1() {

            var method = new Search() {
                City = "深圳",
                Key = "c#"
            };

            var a = ApiClient.Execute(method).Result;
        }
    }
}
