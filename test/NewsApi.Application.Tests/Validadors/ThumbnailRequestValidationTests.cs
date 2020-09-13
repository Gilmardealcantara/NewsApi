using System;
using System.IO;
using FluentAssertions;
using NewsApi.Application.Dtos;
using NewsApi.Application.Validator;
using Xunit;

namespace NewsApi.Application.Tests.Validadors
{
    public class ThumbnailRequestValidationTests
    {
        [Fact]
        public void Validate_WhenSuccess()
        {
            var fileName = "image.jpg";
            var fullPath = Path.GetFullPath(fileName);
            File.Exists(fullPath).Should().BeTrue();

            var request = new ThumbnailRequest
            {
                NewsId = Guid.NewGuid(),
                FileLength = 3 * 1024 * 1024,
                FileLocalPath = fullPath,
                FileName = fileName,
            };

            var validator = new ThumbnailRequestValidation();
            var validate = validator.Validate(request);

            validate.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("image.jpg", 6 * 1024 * 1024, "011")]
        [InlineData("image.pdf", 3 * 1024 * 1024, "012")]
        [InlineData("no.jpg", 3 * 1024 * 1024, "013")]
        public void Validate_WhenError(string fileName, long fileLength, string errorCode)
        {
            var fullPath = Path.GetFullPath(fileName);

            var request = new ThumbnailRequest
            {
                NewsId = Guid.NewGuid(),
                FileLength = fileLength,
                FileLocalPath = fullPath,
                FileName = fileName,
            };

            var validator = new ThumbnailRequestValidation();
            var validate = validator.Validate(request);

            validate.IsValid.Should().BeFalse();
            validate.Errors.Should().Contain(x => x.ErrorCode == errorCode);
        }
    }
}