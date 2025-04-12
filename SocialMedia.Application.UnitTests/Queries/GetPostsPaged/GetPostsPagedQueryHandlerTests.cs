using System.Linq.Expressions;
using FluentResults;
using Moq;
using Shouldly;
using SocialMedia.Application.Dtos.Post;
using SocialMedia.Application.Interfaces;
using SocialMedia.Application.Queries.GetPostsPaged;
using SocialMedia.Domain.Common;
using SocialMedia.Domain.Entities;

namespace SocialMedia.UnitTests.Queries.GetPostsPaged;

public class GetPostsPagedQueryHandlerTests
{
    private static readonly List<Post> PostsListMock =
    [
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 1",
            AuthorId = "123456789",
            Author = new User
            {
                Id = "123456789",
                GivenName = "John",
                FamilyName = "Doe",
                Image = "image1.jpg",
                Email = string.Empty,
                Posts = new List<Post>(),
                CreatedDate = default,
                UpdatedDate = null
            },
            CreatedDate = DateTimeOffset.Now,
            UpdatedDate = null
        },
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 2",
            AuthorId = "111222333",
            Author = new User 
            { 
                Id = "111222333", 
                GivenName = "Jane", 
                FamilyName = "Smith", 
                Image = "image2.jpg",
                Email = string.Empty,
                Posts = new List<Post>(),
                CreatedDate = default,
                UpdatedDate = null
            },
            CreatedDate = DateTimeOffset.Now - TimeSpan.FromHours(1),
            UpdatedDate = null
        },
        new()
        {
            Id = Guid.NewGuid(),
            Content = "post data 3",
            AuthorId = "987654321",
            Author = new User 
            { 
                Id = "987654321", 
                GivenName = "Mike", 
                FamilyName = "Brown", 
                Image = "image3.jpg",
                Email = string.Empty,
                Posts = new List<Post>(),
                CreatedDate = default,
                UpdatedDate = null
            },
            CreatedDate = DateTimeOffset.Now - TimeSpan.FromHours(2),
            UpdatedDate = null
        }
    ];

    private readonly Mock<IPostRepository> _postRepositoryMock = new();

    [Theory]
    [InlineData(1, 5, "id", "asc")]
    [InlineData(2, 5, null, "desc")]
    [InlineData(3, 3, "id", null)]
    [InlineData(3, 3, "id", "")]
    [InlineData(3, 3, "", "desc")]
    [InlineData(2, 5, "created", "asc")]
    [InlineData(2, 5, "updated", "desc")]
    public async Task Handle_ExistingPostsPaged_ReturnsSuccess(int page, int size, string? sortColumn = null,
        string? sortOrder = null)
    {
        // Arrange
        Expression<Func<Post, object>> orderByPredicate = sortColumn?.ToLower() switch
        {
            "id" => p => p.Id,
            "created" => p => p.CreatedDate,
            "updated" => p => p.UpdatedDate ?? DateTimeOffset.MaxValue,
            _ => p => p.Id
        };

        var postsList = PostsListMock.OrderBy(orderByPredicate.Compile()).ToList();
        var pagedPosts = new Paged<Post>(postsList, postsList.Count, size, page);
        
        var postDtos = postsList.Select(p => new PostDto
        {
            Id = p.Id,
            Content = p.Content,
            AuthorGivenName = p.Author.GivenName,
            AuthorFamilyName = p.Author.FamilyName,
            AuthorImage = p.Author.Image,
            CreatedDate = p.CreatedDate
        }).ToList();
        
        var expectedResult = new Paged<PostDto>(postDtos, postsList.Count, size, page);
        
        var query = new GetPostsPagedQuery(page, size, sortColumn, sortOrder);
        var handler = new GetPostsPagedQueryHandler(_postRepositoryMock.Object);
        
        _postRepositoryMock
            .Setup(x => x.GetPagedAsync(page, size, It.IsAny<Expression<Func<Post, object>>>(), sortOrder,
                It.IsAny<CancellationToken>()))
            .ReturnsAsync(expectedResult);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        _postRepositoryMock.Verify(x =>
            x.GetPagedAsync(page, size, It.IsAny<Expression<Func<Post, object>>>(), sortOrder,
                It.IsAny<CancellationToken>()), Times.Once);
                
        result.IsSuccess.ShouldBeTrue();
        result.Value.ShouldNotBeNull();
        
        result.Value.Data.Count.ShouldBe(expectedResult.Data.Count);
        result.Value.ShouldBeEquivalentTo(expectedResult);
    }
}