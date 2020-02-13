using Microsoft.AspNetCore.Http;
using Software.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Software.Application.Services
{
    public interface IDocumentService
    {
        List<DocumentDto> GetDocuments();

        Document GetById(int Id);

        Task<int> Create(IFormFile request, CancellationToken cancellationToken);

        Task Delete(Document document, CancellationToken cancellationToken);

        bool FileWithinLimit(long fileSize);

        bool CheckExtension(string extensionType);
    }
}
