using System.Collections.Generic;
using System.Threading.Tasks;
using AdminConsole.Controllers;
using AdminConsole.Logic;
using AdminConsole.Models;
using AdminConsole.ViewModels;
using AutoMapper;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace AdminConsoleTest.Logic
{
    [Collection("Fixture")]
    public class DefaultPreProcessorTest
    {
        private TestFixture _fixture;

        public DefaultPreProcessorTest(TestFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void ProcessShouldSplitWithMinusAndSum()
        {
            var input = new string[]
            {
                "ITEM000001",
                "ITEM000001",
                "ITEM000001",
                "ITEM000001",
                "ITEM000001",
                "ITEM000003-2",
                "ITEM000005",
                "ITEM000005",
                "ITEM000005"
            };

            var processor = new DefaultPreProcessor();
            var output = processor.Process(input);

            Assert.NotNull(output);
            Assert.Equal(3, output.Keys.Count);
            Assert.Equal(5, output["ITEM000001"]);
            Assert.Equal(2, output["ITEM000003"]);
            Assert.Equal(3, output["ITEM000005"]);
        }
    }
}
