using CodePlus.API.Models.Domain;
using CodePlus.API.Models.DTO;
using CodePlus.API.Repositories.Implementation;
using CodePlus.API.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodePlus.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        public BlogPostsController(IBlogPostRepository blogPostRepository)
        {
            _blogPostRepository = blogPostRepository;
        }

        // POST: {apibaseurl}/api/blogposts
        [HttpPost]
        //[Authorize(Roles = "Writer")]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)
        {
            // Convert DTO to DOmain
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                UrlHandle = request.UrlHandle
                //Categories = new List<Category>()
            };


            //foreach (var categoryGuid in request.Categories)
            //{
            //    var existingCategory = await categoryRepository.GetById(categoryGuid);
            //    if (existingCategory is not null)
            //    {
            //        blogPost.Categories.Add(existingCategory);
            //    }
            //}

            blogPost = await _blogPostRepository.CreateAsync(blogPost);

            // Convert Domain Model back to DTO
            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl = blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                UrlHandle = blogPost.UrlHandle,
                //Categories = blogPost.Categories.Select(x => new CategoryDto
                //{
                //    Id = x.Id,
                //    Name = x.Name,
                //    UrlHandle = x.UrlHandle
                //}).ToList()
            };

            return Ok(response);
        }

    }
}
