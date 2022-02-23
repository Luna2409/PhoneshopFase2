using Phoneshop.Domain.Interfaces;
using System;
using System.IO;
using Xunit;
using FluentAssertions;
using System.Collections.Generic;
using Phoneshop.Domain.Entities;
using Phoneshop.Business;

namespace Phoneshop.Tests.ImportServiceTests
{
    public class ConvertXmlToList
    {
        private readonly IImportService _importService;

        public ConvertXmlToList()
        {
            _importService = new ImportService();
        }

        [Fact]
        public void Should_Return_List_From_XML_File()
        {
            string[] xml = new string[]
            {
                $"<?xml version=\"1.0\" encoding=\"utf-8\"?>",
                $"<Phones>",
                $"<Phone>",
                $"<Brand>Huawei</Brand>",
                $"<Type>P30</Type>",
                $"<Price>697</Price>",
                $"<Description>6.47 FHD+ (2340x1080) OLED,</Description>",
                $"<Stock>34</Stock>",
                $"</Phone>",
                $"</Phones>"
            };

            if (!File.Exists("test.xml"))
            {
                File.WriteAllLines("test.xml", xml);
            }

            string path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "\\test.xml";

            _importService.ConvertXmlToList(path).Should().BeOfType<List<Phone>>().And.HaveCount(1);

            File.Delete(path);
        }
    }
}
