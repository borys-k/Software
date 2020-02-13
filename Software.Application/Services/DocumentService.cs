using Microsoft.AspNetCore.Http;
using Software.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Software.Application.Services
{
    public class DocumentService : IDocumentService
    {
        private readonly IMainDbContext _dbContext;

        public DocumentService(IMainDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<DocumentDto> GetDocuments()
        {
            var documents = _dbContext.GetAllQuery<Document>().ToList();

            return documents
                .Select(d =>
                    new DocumentDto()
                    {
                        Name = d.Name,
                        Location = "/api/documents/" + d.DocumentId.ToString(), // not enough information about Location
                        FileSize = d.FileSize
                    })
                .ToList();
        }

        public bool FileWithinLimit(long fileSize)
        {

            return fileSize <= 5 * 1024 * 1024;
        }

        public bool CheckExtension(string extensionType)
        {
            return extensionType.ToLower().Equals("pdf");
        }

        public async Task<int> Create(IFormFile file, CancellationToken cancellationToken)
        {
            using (var stream = new MemoryStream())
            {
                await file.CopyToAsync(stream);

                var entity = new Document()
                {
                    Name = WebUtility.HtmlEncode(file.FileName),
                    Content = stream.ToArray(),
                    FileSize = file.Length
                };

                _dbContext.AddEntity<Document>(entity);
                await _dbContext.SaveChangesAsync(cancellationToken);

                return entity.DocumentId;
            }
        }

        public async Task Delete(Document product, CancellationToken cancellationToken)
        {
            _dbContext.RemoveEntity<Document>(product);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }

        public Document GetById(int id)
        {
            return _dbContext.Get<Document>(id);
        }
    }
}
