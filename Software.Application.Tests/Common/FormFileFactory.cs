using Microsoft.AspNetCore.Http;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Software.Application.Tests.Common
{
    public class FormFileFactory
    {
        public IFormFile CreateFormFile(string name)
        {
            var fileMock = new Mock<IFormFile>();
            var content = "12345678";
            var fileName = name;
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(x => x.OpenReadStream()).Returns(ms);
            fileMock.Setup(x => x.FileName).Returns(fileName);
            fileMock.Setup(x => x.Length).Returns(ms.Length);

            return fileMock.Object;
        }
    }
}
