using Software.Application.Services;
using Software.Application.Tests.Common;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Software.Application.Tests
{
    public class DocumentServiceTests
    {
        private IMainDbContext _dbContext;
        private IDocumentService _documentService;
        private FormFileFactory _fileMock;

        public DocumentServiceTests()
        {
            _dbContext = MainDbContextFactory.Create();
            _documentService = new DocumentService(_dbContext);
            _fileMock = new FormFileFactory();
        }

        [Fact]
        public async Task CreateFile_ValidFile_ReturnDocumentId()
        {
            var file = _fileMock.CreateFormFile("testname.pdf");

            var result = await _documentService.Create(file, CancellationToken.None);

            Assert.True(result > 0);
        }

        [Fact]
        public async Task GetFiles_ThereIsFiles_ReturnList()
        {
            var file1 = _fileMock.CreateFormFile("1.pdf");
            var file2 = _fileMock.CreateFormFile("2.pdf");

            await _documentService.Create(file1, CancellationToken.None);
            await _documentService.Create(file2, CancellationToken.None);

            var documents = _documentService.GetDocuments();
            Assert.Equal(2, documents.Count);
        }

        [Fact]
        public async Task GetFile_ThereIsFile_ReturnOne()
        {
            var fileName = "1.pdf";
            var file1 = _fileMock.CreateFormFile(fileName);

            var documentId = await _documentService.Create(file1, CancellationToken.None);

            var document = _documentService.GetById(documentId);
            Assert.Equal(fileName, document.Name);
        }

        [Fact]
        public async Task DeleteFile_ThereIsFile_Deleted()
        {
            var fileName = "1.pdf";
            var file1 = _fileMock.CreateFormFile(fileName);

            var documentId = await _documentService.Create(file1, CancellationToken.None);

            var document = _documentService.GetById(documentId);

            await _documentService.Delete(document, CancellationToken.None);

            var check = _documentService.GetById(documentId);

            Assert.Null(check);
        }
    }
}
